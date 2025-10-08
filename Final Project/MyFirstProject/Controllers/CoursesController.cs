using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        private readonly IInstructorRepository _instructorRepo;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;

        public CoursesController(
            ICourseRepository courseRepo,
            IDepartmentRepository deptRepo,
            ICourseStudentsRepository csRepo,
            IStudentRepository studentRepo,
            IInstructorRepository instructorRepo,
            UserManager<IdentityUser> userManager,
            AppDbContext context)
        {
            _courseRepo = courseRepo;
            _deptRepo = deptRepo;
            _csRepo = csRepo;
            _studentRepo = studentRepo;
            _instructorRepo = instructorRepo;
            _userManager = userManager;
            _context = context;
        }

        // ===========================================================
        // Index - عرض الكورسات حسب الدور
        // ===========================================================
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            int pageSize = 5;
            int pageNumber = page ?? 1;

            var allCourses = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructors)
                .ToListAsync();

            // ===================== Instructor =====================
            if (roles.Contains("Instructor"))
            {
                var instructor = (await _instructorRepo.GetAllAsync())
                    .FirstOrDefault(i => i.Username == user.UserName);

                var instructorCourses = new List<Course>();
                if (instructor != null)
                {
                    instructorCourses = allCourses
                        .Where(c => c.Instructors.Any(i => i.Id == instructor.Id))
                        .ToList();
                }

                if (!string.IsNullOrEmpty(searchString))
                {
                    instructorCourses = instructorCourses
                        .Where(c => c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                ViewData["Title"] = "My Courses";
                ViewBag.Roles = roles;
                return View(instructorCourses.ToPagedList(pageNumber, pageSize));
            }

            // ===================== Student =====================
            if (roles.Contains("Student"))
            {
                var student = await _studentRepo.GetByUsernameAsync(user.UserName);
                if (student == null)
                {
                    TempData["ErrorMessage"] = "You must login first!";
                    return RedirectToAction("Login", "Account");
                }

                var joinedCourseIds = (await _csRepo.GetAllAsync())
                    .Where(cs => cs.StdId == student.Id)
                    .Select(cs => cs.CrsId)
                    .ToList();

                var availableCourses = allCourses
                    .Where(c => !joinedCourseIds.Contains(c.Id))
                    .ToList();

                var myCourses = allCourses
                    .Where(c => joinedCourseIds.Contains(c.Id))
                    .ToList();

                if (!string.IsNullOrEmpty(searchString))
                {
                    availableCourses = availableCourses
                        .Where(c => c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    myCourses = myCourses
                        .Where(c => c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                ViewBag.AvailableCourses = availableCourses.ToPagedList(pageNumber, pageSize);
                ViewBag.MyCourses = myCourses.ToPagedList(pageNumber, pageSize);
                ViewBag.Roles = roles;
                ViewData["Title"] = "Courses";

                return View();
            }

            // ===================== Admin =====================
            if (roles.Contains("Admin"))
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    allCourses = allCourses
                        .Where(c => c.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                ViewData["Title"] = "All Courses";
                ViewBag.Roles = roles;
                return View(allCourses.OrderBy(c => c.Id).ToPagedList(pageNumber, pageSize));
            }

            return Forbid();
        }

        // ===========================================================
        // Student Join Course
        // ===========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Student")]
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
            return RedirectToAction(nameof(Index));
        }

        // ===========================================================
        // Admin Actions
        // ===========================================================
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
            var course = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructors)
                .FirstOrDefaultAsync(c => c.Id == id);

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
        // ===========================================================
        // Course Details - للجميع حسب الرول
        // ===========================================================
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> Details(int id)
        {
            var course = await _context.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructors)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (course == null)
            {
                TempData["ErrorMessage"] = "Course not found!";
                return RedirectToAction(nameof(Index));
            }

            return View(course);
        }

    }
}
