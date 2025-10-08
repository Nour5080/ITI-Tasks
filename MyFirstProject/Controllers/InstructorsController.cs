using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using System.Threading.Tasks;

namespace MyFirstProject.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class InstructorsController : Controller
    {
        private readonly IInstructorRepository _instructorRepo;
        private readonly IDepartmentRepository _deptRepo;
        private readonly ICourseRepository _courseRepo;

        public InstructorsController(
            IInstructorRepository instructorRepo,
            IDepartmentRepository deptRepo,
            ICourseRepository courseRepo)
        {
            _instructorRepo = instructorRepo;
            _deptRepo = deptRepo;
            _courseRepo = courseRepo;
        }

        // ======================================================
        // عرض جميع الـ Instructors
        // ======================================================
        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorRepo.GetAllAsync();
            return View(instructors);
        }

        // ======================================================
        // عرض تفاصيل Instructor محدد
        // ======================================================
        public async Task<IActionResult> Details(int id)
        {
            var instructor = await _instructorRepo.GetByIdAsync(id);
            if (instructor == null)
                return NotFound();

            return View(instructor);
        }

        // ======================================================
        // إنشاء Instructor جديد (Admin فقط)
        // ======================================================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name");
            ViewData["CrsId"] = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Salary,Address,Image,DeptId,CrsId,Username")] Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                await _instructorRepo.AddAsync(instructor);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", instructor.DeptId);
            ViewData["CrsId"] = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name", instructor.CrsId);
            return View(instructor);
        }

        // ======================================================
        // تعديل Instructor (Admin + HR)
        // ======================================================
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = await _instructorRepo.GetByIdAsync(id);
            if (instructor == null)
                return NotFound();

            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", instructor.DeptId);
            ViewData["CrsId"] = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name", instructor.CrsId);
            return View(instructor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Salary,Address,Image,DeptId,CrsId,Username")] Instructor instructor)
        {
            if (id != instructor.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _instructorRepo.UpdateAsync(instructor);
                return RedirectToAction(nameof(Index));
            }

            ViewData["DeptId"] = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name", instructor.DeptId);
            ViewData["CrsId"] = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name", instructor.CrsId);
            return View(instructor);
        }

        // ======================================================
        // حذف Instructor (Admin + HR)
        // ======================================================
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> Delete(int id)
        {
            var instructor = await _instructorRepo.GetByIdAsync(id);
            if (instructor == null)
                return NotFound();

            return View(instructor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _instructorRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
