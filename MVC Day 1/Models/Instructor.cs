// Models/Instructor.cs
namespace SchoolMvc.Models;
public class Instructor
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Salary { get; set; }
    public string? Address { get; set; }
    public string? Image { get; set; }

    public int DeptId { get; set; }
    public Department Department { get; set; } = default!;

    public int CrsId { get; set; }
    public Course Course { get; set; } = default!;
}
