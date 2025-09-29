// Models/Course.cs
using System.Collections.Generic;
namespace SchoolMvc.Models;
public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public int Degree { get; set; }
    public int MinimumDegree { get; set; }
    public int Hours { get; set; }

    public int DeptId { get; set; }
    public Department Department { get; set; } = default!;

    public ICollection<CourseStudent> CourseStudents { get; set; } = new List<CourseStudent>();
    public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
}
