using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize(Roles = "Admin,Instructor,Student")]
    public class CoursesController : Controller
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IDepartmentRepository _deptRepo;
        private readonly ICourseStudentsRepository _csRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly UserManager<IdentityUser> _userManager;

        public CoursesController(
            ICourseRepository courseRepo,
            IDepartmentRepository deptRepo,
            ICourseStudentsRepository csRepo,
            IStudentRepository studentRepo,
            UserManager<IdentityUser> userManager)
        {
            _courseRepo = courseRepo;
            _deptRepo = deptRepo;
            _csRepo = csRepo;
            _studentRepo = studentRepo;
            _userManager = userManager;
        }

        [Authorize(Roles = "Student")]
        [HttpPost]
        public async Task<IActionResult> JoinCourse(int courseId)
        {
            var student = await _studentRepo.GetByUsernameAsync(User.Identity.Name);
            if (student == null)
            {
                TempData["ErrorMessage"] = "You must login first!";
                return RedirectToAction("Login", "Account");
            }

            var existing = (await _csRepo.GetAllAsync())
                .FirstOrDefault(cs => cs.StdId == student.Id && cs.CrsId == courseId);

            if (existing != null)
            {
                TempData["ErrorMessage"] = "You have already joined this course.";
                return RedirectToAction("Index");
            }

            await _csRepo.AddAsync(new CourseStudents
            {
                StdId = student.Id,
                CrsId = courseId
            });

            TempData["SuccessMessage"] = "Course joined successfully!";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            int pageSize = 5;
            int pageNumber = page ?? 1;

            var courses = await _courseRepo.GetAllWithDetailsAsync();

            List<int> joinedCourseIds = new List<int>();
            List<string> roles = new List<string>();
            List<Course> availableCourses = new List<Course>();
            List<Course> myCourses = new List<Course>();

            if (User.Identity.IsAuthenticated)
            {
                var identityUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (identityUser != null)
                {
                    roles = (await _userManager.GetRolesAsync(identityUser)).ToList();
                }

                if (roles.Contains("Student"))
                {
                    var student = await _studentRepo.GetByUsernameAsync(User.Identity.Name);
                    if (student != null)
                    {
                        joinedCourseIds = (await _csRepo.GetAllAsync())
                            .Where(cs => cs.StdId == student.Id)
                            .Select(cs => cs.CrsId)
                            .ToList();

                        availableCourses = courses
                            .Where(c => !joinedCourseIds.Contains(c.Id))
                            .ToList();

                        myCourses = courses
                            .Where(c => joinedCourseIds.Contains(c.Id))
                            .ToList();
                    }
                }
            }

            // بحث
            if (!string.IsNullOrEmpty(searchString))
            {
                availableCourses = availableCourses
                    .Where(c => (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase)) ||
                                (c.Department != null && !string.IsNullOrEmpty(c.Department.Name) && c.Department.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                myCourses = myCourses
                    .Where(c => (!string.IsNullOrEmpty(c.Name) && c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase)) ||
                                (c.Department != null && !string.IsNullOrEmpty(c.Department.Name) && c.Department.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            ViewBag.Roles = roles;
            ViewBag.JoinedCourseIds = joinedCourseIds;
            ViewBag.AvailableCourses = availableCourses;
            ViewBag.MyCourses = myCourses;
            ViewBag.SearchString = searchString;

            var pagedList = courses.OrderBy(c => c.Id).ToPagedList(pageNumber, pageSize);
            return View(pagedList);
        }

        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _courseRepo.GetByIdWithDetailsAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        // ===============================
        // ADMIN ACTIONS
        // ===============================

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            var departments = await _deptRepo.GetAllAsync();
            ViewData["DeptId"] = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Degree,MinimumDegree,Hours,DeptId")] Course course)
        {
            if (ModelState.IsValid)
            {
                await _courseRepo.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }

            var departments = await _deptRepo.GetAllAsync();
            ViewData["DeptId"] = new SelectList(departments, "Id", "Name", course.DeptId);
            return View(course);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            if (course == null) return NotFound();

            var departments = await _deptRepo.GetAllAsync();
            ViewData["DeptId"] = new SelectList(departments, "Id", "Name", course.DeptId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Degree,MinimumDegree,Hours,DeptId")] Course course)
        {
            if (id != course.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _courseRepo.UpdateAsync(course);
                return RedirectToAction(nameof(Index));
            }

            var departments = await _deptRepo.GetAllAsync();
            ViewData["DeptId"] = new SelectList(departments, "Id", "Name", course.DeptId);
            return View(course);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseRepo.GetByIdWithDetailsAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _courseRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
