using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Models
{
    public class Instructor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Instructor Name is required.")]
        [StringLength(100, ErrorMessage = "Instructor Name cannot exceed 100 characters.")]
        public required string? Name { get; set; }

        [Range(0, 100000, ErrorMessage = "Salary must be between 0 and 100,000.")]
        public decimal? Salary { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        public string? Image { get; set; }
        [Required(ErrorMessage = "Department is required.")]
        public int? DeptId { get; set; }
        public Department? Department { get; set; }

        [Required(ErrorMessage = "Course is required.")]
        public int? CrsId { get; set; }
        public Course? Course { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
