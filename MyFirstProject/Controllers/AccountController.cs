using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using MyFirstProject.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyFirstProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStudentRepository _studentRepository;
        private readonly IInstructorRepository _instructorRepository;
        private readonly IDepartmentRepository _deptRepo;
        private readonly ICourseRepository _courseRepo;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IStudentRepository studentRepository,
            IInstructorRepository instructorRepository,
            IDepartmentRepository deptRepo,
            ICourseRepository courseRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _studentRepository = studentRepository;
            _instructorRepository = instructorRepository;
            _deptRepo = deptRepo;
            _courseRepo = courseRepo;
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            var departments = await _deptRepo.GetAllAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            var courses = await _courseRepo.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "Id", "Name");

            var roles = new List<string> { "Student", "Instructor", "Admin", "HR" };
            ViewBag.Roles = roles.Select(r => new SelectListItem { Value = r, Text = r });

            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name");
                ViewBag.Courses = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name");
                var roles = new List<string> { "Student", "Instructor", "Admin", "HR" };
                ViewBag.Roles = roles.Select(r => new SelectListItem { Value = r, Text = r });
                return View(model);
            }

            if (string.IsNullOrEmpty(model.Role))
            {
                ModelState.AddModelError("", "Role must be selected.");
                ViewBag.Departments = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name");
                ViewBag.Courses = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name");
                var roles = new List<string> { "Student", "Instructor", "Admin", "HR" };
                ViewBag.Roles = roles.Select(r => new SelectListItem { Value = r, Text = r });
                return View(model);
            }

            var user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

                ViewBag.Departments = new SelectList(await _deptRepo.GetAllAsync(), "Id", "Name");
                ViewBag.Courses = new SelectList(await _courseRepo.GetAllAsync(), "Id", "Name");
                var roles = new List<string> { "Student", "Instructor", "Admin", "HR" };
                ViewBag.Roles = roles.Select(r => new SelectListItem { Value = r, Text = r });
                return View(model);
            }

            if (!await _roleManager.RoleExistsAsync(model.Role))
                await _roleManager.CreateAsync(new IdentityRole(model.Role));

            await _userManager.AddToRoleAsync(user, model.Role);

            // ===============================
            // حفظ الصورة
            // ===============================
            string imagePath = null;
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var folderName = model.Role.Equals("Student", StringComparison.OrdinalIgnoreCase)
                    ? "students"
                    : "instructors";

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{folderName}");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = Guid.NewGuid() + Path.GetExtension(model.ImageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }

                imagePath = $"/images/{folderName}/{uniqueFileName}";
            }

            // ===============================
            // إذا كان الطالب
            // ===============================
            if (model.Role.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                var student = new Student
                {
                    Name = model.Username,
                    Username = model.Username,
                    Grade = model.Grade ?? 1,
                    DeptId = model.DeptId ?? 1,
                    Address = model.Address ?? "Not specified",
                    Image = imagePath
                };

                await _studentRepository.AddAsync(student);
                HttpContext.Session.SetInt32("StudentId", student.Id);
            }

            // ===============================
            // إذا كان المعلم (Instructor)
            // ===============================
            else if (model.Role.Equals("Instructor", StringComparison.OrdinalIgnoreCase))
            {
                var instructor = new Instructor
                {
                    Name = model.Username,
                    Username = model.Username,
                    Salary = model.Salary ?? 0,
                    DeptId = model.DeptId ?? 1,
                    CrsId = model.CrsId ?? 1,
                    Address = model.Address ?? "Not specified",
                    Image = imagePath
                };

                await _instructorRepository.AddAsync(instructor);
                HttpContext.Session.SetInt32("InstructorId", instructor.Id);
            }

            // ===============================
            // إذا كان HR
            // ===============================
            else if (model.Role.Equals("HR", StringComparison.OrdinalIgnoreCase))
            {
                // HR ممكن نخزن بياناته في جدول Users فقط أو إضافة حقول إضافية حسب الحاجة
                // حاليا نخليه فقط في Identity + Role
            }

            // تسجيل الدخول تلقائي
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var student = (await _studentRepository.GetAllAsync())
                    .FirstOrDefault(s => s.Username == model.Username);

                if (student != null)
                    HttpContext.Session.SetInt32("StudentId", student.Id);
                else
                {
                    var instructor = (await _instructorRepository.GetAllAsync())
                        .FirstOrDefault(i => i.Username == model.Username);

                    if (instructor != null)
                        HttpContext.Session.SetInt32("InstructorId", instructor.Id);
                }

                return RedirectToLocal(model.ReturnUrl);
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            HttpContext.Session.Remove("StudentId");
            HttpContext.Session.Remove("InstructorId");
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
    }
}
