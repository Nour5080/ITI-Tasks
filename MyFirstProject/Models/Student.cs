using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MyFirstProject.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required.")]
        [StringLength(50, ErrorMessage = "Student Name cannot exceed 50 characters.")]
        public string Name { get; set; }

        public string? Image { get; set; } // path

        [NotMapped]
        public IFormFile? ImageFile { get; set; } // uploaded file

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        [Range(1, 12, ErrorMessage = "Grade must be between 1 and 12.")]
        public int Grade { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "Department is required.")]
        public int DeptId { get; set; }

        public Department? Department { get; set; }
        public string Username { get; set; } = string.Empty;

        public ICollection<CourseStudents>? CourseStudents { get; set; }
    }
}
