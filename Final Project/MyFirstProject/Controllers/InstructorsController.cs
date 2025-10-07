using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace MyFirstProject.Controllers
{
    [Authorize(Roles = "Admin,HR,Instructor")]
    public class InstructorsController : Controller
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly IDepartmentRepository _deptRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InstructorsController(
            IInstructorRepository instructorRepo,
            IDepartmentRepository deptRepo,
            ICourseRepository courseRepo,
            IWebHostEnvironment webHostEnvironment)
        {
            _instructorRepo = instructorRepo;
            _deptRepo = deptRepo;
            _courseRepo = courseRepo;
            _webHostEnvironment = webHostEnvironment;
        }

        private List<string> GetUserRoles()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return User.Claims
                           .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList();
            }
            return new List<string>();
        }

        // INDEX
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var instructors = await _instructorRepo.GetAllWithDetailsAsync();
            var query = instructors.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(i =>
                    (!string.IsNullOrEmpty(i.Name) && i.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase)) ||
                    (i.Department != null && !string.IsNullOrEmpty(i.Department.Name) && i.Department.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase)) ||
                    (i.Course != null && !string.IsNullOrEmpty(i.Course.Name) && i.Course.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                );
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;

            // استخدام ToPagedList (غير async)
            var pagedList = query.OrderBy(i => i.Id).ToPagedList(pageNumber, pageSize);

            // Default values
            foreach (var instructor in pagedList)
            {
                instructor.Address ??= "No Address";
                instructor.Name ??= "No Name";
                instructor.Department ??= new Department { Name = "No Department" };
                instructor.Course ??= new Course { Name = "No Course" };
                instructor.Salary ??= 0m;
            }

            ViewBag.UserRoles = GetUserRoles();
            ViewBag.SearchString = searchString;

            return View(pagedList);
        }

        // CREATE GET
        public async Task<IActionResult> Create()
        {
            ViewBag.DeptId = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name");
            ViewBag.CrsId = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name");
            ViewBag.UserRoles = GetUserRoles();
            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instructor instructor, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    Directory.CreateDirectory(uploadsFolder);

                    string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await ImageFile.CopyToAsync(stream);

                    instructor.Image = "/images/" + fileName;
                }

                instructor.DeptId = instructor.DeptId == 0 ? null : instructor.DeptId;
                instructor.CrsId = instructor.CrsId == 0 ? null : instructor.CrsId;

                await _instructorRepo.AddAsync(instructor);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DeptId = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", instructor.DeptId);
            ViewBag.CrsId = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name", instructor.CrsId);
            ViewBag.UserRoles = GetUserRoles();
            return View(instructor);
        }

        // EDIT GET
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorRepo.GetByIdAsync(id);
            if (instructor == null) return NotFound();

            ViewBag.DeptId = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", instructor.DeptId);
            ViewBag.CrsId = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name", instructor.CrsId);
            ViewBag.UserRoles = GetUserRoles();
            return View(instructor);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instructor instructor, IFormFile? ImageFile)
        {
            if (id != instructor.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    Directory.CreateDirectory(uploadsFolder);

                    string fileName = Guid.NewGuid() + Path.GetExtension(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await ImageFile.CopyToAsync(stream);

                    instructor.Image = "/images/" + fileName;
                }

                instructor.DeptId = instructor.DeptId == 0 ? null : instructor.DeptId;
                instructor.CrsId = instructor.CrsId == 0 ? null : instructor.CrsId;

                await _instructorRepo.UpdateAsync(instructor);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DeptId = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", instructor.DeptId);
            ViewBag.CrsId = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name", instructor.CrsId);
            ViewBag.UserRoles = GetUserRoles();
            return View(instructor);
        }

        // DELETE GET
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _instructorRepo.GetByIdAsync(id);
            if (instructor == null) return NotFound();

            ViewBag.UserRoles = GetUserRoles();
            return View(instructor);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _instructorRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var instructor = await _instructorRepo.GetByIdAsync(id);
            if (instructor == null) return NotFound();

            instructor.Department ??= new Department { Name = "No Department" };
            instructor.Course ??= new Course { Name = "No Course" };
            instructor.Address ??= "No Address";

            ViewBag.UserRoles = GetUserRoles();
            return View(instructor);
        }
    }
}
