// Models/Student.cs
using System.Collections.Generic;
namespace SchoolMvc.Models;
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
    public string? Address { get; set; }
    public int Grade { get; set; }

    public int DeptId { get; set; }
    public Department Department { get; set; } = default!;

    public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
}
