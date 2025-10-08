using MyFirstProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFirstProject.Repositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllWithDetailsAsync();
        Task<Department> GetByIdWithDetailsAsync(int id);
    }
}
