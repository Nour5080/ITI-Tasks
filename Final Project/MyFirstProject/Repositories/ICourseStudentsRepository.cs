using MyFirstProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstProject.Repositories
{
    public interface ICourseStudentsRepository : IRepository<CourseStudents>
    {
        Task<IEnumerable<CourseStudents>> GetAllWithDetailsAsync();
        Task<CourseStudents> GetByIdWithDetailsAsync(int id); // ✅ الدالة اللي عملناها
    }
}
