using MyFirstProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstProject.Repositories
{
    public interface IInstructorRepository
    {
        Task<IEnumerable<Instructor>> GetAllAsync();
        Task<IEnumerable<Instructor>> GetAllWithDetailsAsync(); // جلب مع الـ Department + Course
        Task<Instructor> GetByIdAsync(int id);
        Task AddAsync(Instructor instructor);
        Task UpdateAsync(Instructor instructor);
        Task DeleteAsync(int id);
        Task<List<Instructor>> GetAllWithDepartmentAsync();
    }
}
