using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Extensions;

namespace MyFirstProject.Controllers
{
    [Authorize(Roles = "Admin,HR")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _deptRepo;

        public DepartmentsController(IDepartmentRepository deptRepo)
        {
            _deptRepo = deptRepo;
        }

        // ===========================
        // INDEX: Search + Pagination
        // ===========================
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page, int pageSize = 5)
        {
            var departments = await _deptRepo.GetAllWithDetailsAsync();

            // Null-safe Search
            if (!string.IsNullOrEmpty(searchString))
            {
                departments = departments.Where(d =>
                    (!string.IsNullOrEmpty(d.Name) && d.Name.Contains(searchString, System.StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(d.ManagerName) && d.ManagerName.Contains(searchString, System.StringComparison.OrdinalIgnoreCase))
                );
            }

            int pageNumber = page ?? 1;
            var pagedList = departments.OrderBy(d => d.Name).ToPagedList(pageNumber, pageSize);

            ViewBag.SearchString = searchString;

            // جلب كل أدوار المستخدم من Claims
            var userRoles = User.Claims
                                .Where(c => c.Type == System.Security.Claims.ClaimTypes.Role)
                                .Select(c => c.Value)
                                .ToList();
            ViewBag.UserRoles = userRoles;

            return View(pagedList);
        }

        // ===========================
        // DETAILS
        // ===========================
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var department = await _deptRepo.GetByIdWithDetailsAsync(id.Value);
            if (department == null) return NotFound();

            return View(department);
        }

        // ===========================
        // CREATE
        // ===========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                await _deptRepo.AddAsync(department);
                TempData["SuccessMessage"] = "Department added successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // ===========================
        // EDIT
        // ===========================
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _deptRepo.GetByIdAsync(id.Value);
            if (department == null) return NotFound();

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {
            if (id != department.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _deptRepo.UpdateAsync(department);
                TempData["SuccessMessage"] = "Department updated successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // ===========================
        // DELETE
        // ===========================
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _deptRepo.GetByIdAsync(id.Value);
            if (department == null) return NotFound();

            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _deptRepo.DeleteAsync(id);
            TempData["SuccessMessage"] = "Department deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}
