// Models/Department.cs
using System.Collections.Generic;
namespace SchoolMvc.Models;
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ManagerName { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
    public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
