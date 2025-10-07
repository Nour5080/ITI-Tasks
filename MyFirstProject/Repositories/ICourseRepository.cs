using MyFirstProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstProject.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllAsync();
        Task<List<Course>> GetAllWithDetailsAsync();
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdWithDetailsAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
    }
}
