using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace MyFirstProject.Controllers
{
    [Authorize(Roles = "Admin,Instructor")]
    public class CourseStudentsController : Controller
    {
        private readonly ICourseStudentsRepository _courseStudentsRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IStudentRepository _studentRepo;

        public CourseStudentsController(
            ICourseStudentsRepository courseStudentsRepo,
            ICourseRepository courseRepo,
            IStudentRepository studentRepo)
        {
            _courseStudentsRepo = courseStudentsRepo;
            _courseRepo = courseRepo;
            _studentRepo = studentRepo;
        }

        // ===========================
        // INDEX: Search + Pagination
        // ===========================
        public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 5)
        {
            var courseStudents = await _courseStudentsRepo.GetAllWithDetailsAsync();

            if (!string.IsNullOrEmpty(search))
            {
                courseStudents = courseStudents
                    .Where(cs =>
                        (!string.IsNullOrEmpty(cs.Student?.Name) && cs.Student.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase)) ||
                        (!string.IsNullOrEmpty(cs.Course?.Name) && cs.Course.Name.Contains(search, System.StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();
            }

            var pagedList = courseStudents.OrderBy(cs => cs.Id).ToPagedList(page, pageSize);

            ViewBag.Search = search;
            ViewBag.UserRoles = User.Claims
                                    .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                    .Select(c => c.Value)
                                    .ToList();

            return View(pagedList);
        }

        // ===========================
        // DETAILS
        // ===========================
        public async Task<IActionResult> Details(int id)
        {
            var courseStudent = await _courseStudentsRepo.GetByIdWithDetailsAsync(id);
            if (courseStudent == null) return NotFound();
            return View(courseStudent);
        }

        // ===========================
        // CREATE
        // ===========================
        public async Task<IActionResult> Create()
        {
            await PopulateCoursesAndStudents();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CrsId,StdId,Degree")] CourseStudents courseStudent)
        {
            if (ModelState.IsValid)
            {
                await _courseStudentsRepo.AddAsync(courseStudent);
                TempData["SuccessMessage"] = "Course registration added successfully!";
                return RedirectToAction(nameof(Index));
            }

            await PopulateCoursesAndStudents(courseStudent.CrsId, courseStudent.StdId);
            return View(courseStudent);
        }

        // ===========================
        // EDIT
        // ===========================
        public async Task<IActionResult> Edit(int id)
        {
            var courseStudent = await _courseStudentsRepo.GetByIdWithDetailsAsync(id);
            if (courseStudent == null) return NotFound();

            await PopulateCoursesAndStudents(courseStudent.CrsId, courseStudent.StdId);
            return View(courseStudent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CrsId,StdId,Degree")] CourseStudents courseStudent)
        {
            if (id != courseStudent.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _courseStudentsRepo.UpdateAsync(courseStudent);
                TempData["SuccessMessage"] = "Course registration updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            await PopulateCoursesAndStudents(courseStudent.CrsId, courseStudent.StdId);
            return View(courseStudent);
        }

        // ===========================
        // DELETE
        // ===========================
        public async Task<IActionResult> Delete(int id)
        {
            var courseStudent = await _courseStudentsRepo.GetByIdWithDetailsAsync(id);
            if (courseStudent == null) return NotFound();
            return View(courseStudent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseStudentsRepo.DeleteAsync(id);
            TempData["SuccessMessage"] = "Course registration deleted successfully!";
            return RedirectToAction(nameof(Index));
        }

        // ===========================
        // Helper method: Populate dropdowns
        // ===========================
        private async Task PopulateCoursesAndStudents(int? selectedCourseId = null, int? selectedStudentId = null)
        {
            var courses = await _courseRepo.GetAllAsync();
            var students = await _studentRepo.GetAllAsync();

            ViewBag.Courses = new SelectList(courses, "Id", "Name", selectedCourseId);
            ViewBag.Students = new SelectList(students, "Id", "Name", selectedStudentId);
        }
    }
}
