using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstProject.Helpers;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MyFirstProject.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IDepartmentRepository _deptRepo;

        public StudentsController(IStudentRepository studentRepo, IDepartmentRepository deptRepo)
        {
            _studentRepo = studentRepo;
            _deptRepo = deptRepo;
        }

        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Index(string searchString, int pageNumber = 1, int pageSize = 5)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewBag.SearchString = searchString;

            var studentsQuery = (await _studentRepo.GetAllWithDetailsAsync()).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                studentsQuery = studentsQuery.Where(s =>
                    (!string.IsNullOrEmpty(s.Name) && s.Name.Contains(searchString)) ||
                    (s.Department != null && s.Department.Name.Contains(searchString))
                );
            }

            var pagedList = await PaginatedList<Student>.CreateAsync(
                studentsQuery.OrderBy(s => s.Id), pageNumber, pageSize);

            ViewBag.UserRoles = User.Claims
                                     .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                     .Select(c => c.Value)
                                     .ToList();

            return View(pagedList);
        }

        [Authorize(Roles = "Admin,HR,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var student = await _studentRepo.GetWithDetailsAsync(id.Value);
            if (student == null) return NotFound();

            if (User.IsInRole("Student") && student.Username != User.Identity?.Name)
                return Forbid();

            ViewBag.UserRoles = User.Claims
                                     .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                     .Select(c => c.Value)
                                     .ToList();

            return View(student);
        }

        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var student = await _studentRepo.GetByIdAsync(id.Value);
            if (student == null) return NotFound();

            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", student.DeptId);
            ViewBag.UserRoles = User.Claims
                                     .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                     .Select(c => c.Value)
                                     .ToList();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Edit(int id, Student student, IFormFile ImageFile)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                // Handle image upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = $"{Path.GetRandomFileName().Replace(".", "")}_{ImageFile.FileName}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/students", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    // هنا نحفظ مسار الصورة في قاعدة البيانات
                    student.Image = "/images/students/" + fileName;
                }

                await _studentRepo.UpdateAsync(student);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", student.DeptId);
            return View(student);
        }

        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var student = await _studentRepo.GetWithDetailsAsync(id.Value);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _studentRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Student student, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                // Handle image upload
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = $"{Path.GetRandomFileName().Replace(".", "")}_{ImageFile.FileName}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/students", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }

                    student.Image = "/images/students/" + fileName;
                }

                await _studentRepo.AddAsync(student);
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", student.DeptId);
            return View(student);
        }
    }
}
