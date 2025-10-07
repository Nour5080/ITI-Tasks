using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList;

namespace MyFirstProject.Repositories
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly AppDbContext _context;

        public InstructorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Instructor>> GetAllAsync()
        {
            return await _context.Instructors.ToListAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAllWithDetailsAsync()
        {
            return await _context.Instructors
                        .Include(i => i.Department)
                        .Include(i => i.Course)
                        .ToListAsync();
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors
                        .Include(i => i.Department)
                        .Include(i => i.Course)
                        .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Instructor instructor)
        {
            await _context.Instructors.AddAsync(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Instructor instructor)
        {
            _context.Instructors.Update(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var instructor = await _context.Instructors.FindAsync(id);
            if (instructor != null)
            {
                _context.Instructors.Remove(instructor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
