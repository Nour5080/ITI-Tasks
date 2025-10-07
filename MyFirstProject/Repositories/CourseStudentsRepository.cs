using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstProject.Repositories
{
    public class CourseStudentsRepository : ICourseStudentsRepository
    {
        private readonly AppDbContext _context;

        public CourseStudentsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CourseStudents>> GetAllAsync()
        {
            return await _context.CourseStudents.ToListAsync();
        }

        public async Task<CourseStudents> GetByIdAsync(int id)
        {
            return await _context.CourseStudents.FindAsync(id);
        }

        public async Task AddAsync(CourseStudents entity)
        {
            await _context.CourseStudents.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CourseStudents entity)
        {
            _context.CourseStudents.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.CourseStudents.FindAsync(id);
            if (entity != null)
            {
                _context.CourseStudents.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // ✅ جلب كل البيانات مع روابط Student و Course
        public async Task<IEnumerable<CourseStudents>> GetAllWithDetailsAsync()
        {
            return await _context.CourseStudents
                .Include(cs => cs.Student)
                .Include(cs => cs.Course)
                .ToListAsync();
        }

        // ✅ الدالة اللي كانت missing
        public async Task<CourseStudents> GetByIdWithDetailsAsync(int id)
        {
            return await _context.CourseStudents
                .Include(cs => cs.Student)
                .Include(cs => cs.Course)
                .FirstOrDefaultAsync(cs => cs.Id == id);
        }
    }
}
