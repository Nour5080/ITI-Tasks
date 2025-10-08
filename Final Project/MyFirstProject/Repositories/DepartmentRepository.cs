using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task AddAsync(Department entity)
        {
            await _context.Departments.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department entity)
        {
            _context.Departments.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Departments.FindAsync(id);
            if (entity != null)
            {
                _context.Departments.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // ✅ التفاصيل كاملة
        public async Task<IEnumerable<Department>> GetAllWithDetailsAsync()
        {
            return await _context.Departments
                .Include(d => d.Courses)
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .ToListAsync();
        }

        public async Task<Department> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.Courses)
                .Include(d => d.Students)
                .Include(d => d.Instructors)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
