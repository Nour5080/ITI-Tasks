using System.Collections.Generic;
using System.Threading.Tasks;
using MyFirstProject.Models;

namespace MyFirstProject.Repositories
{
    public interface IStudentRepository : IReadableRepository<Student>, IWritableRepository<Student>
    {
        Task<IEnumerable<Student>> GetByDepartmentAsync(int deptId);

        // For including related entities (Department)
        Task<IEnumerable<Student>> GetAllWithDetailsAsync();
       
        Task<Student> GetByUsernameAsync(string username);

        Task<Student?> GetWithDetailsAsync(int id);

    }
}
