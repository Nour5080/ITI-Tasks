using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Course Name is required.")]
        [StringLength(100, ErrorMessage = "Course Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100.")]
        public int Degree { get; set; }

        [Range(0, 100, ErrorMessage = "Minimum Degree must be between 0 and 100.")]
        public int MinimumDegree { get; set; }

        [Range(1, 50, ErrorMessage = "Hours must be between 1 and 50.")]
        public int Hours { get; set; }

        // Foreign Key
        [Required(ErrorMessage = "Department is required.")]
        public int DeptId { get; set; }

        // Navigation Properties
        public Department? Department { get; set; }
        public ICollection<CourseStudents>? CourseStudents { get; set; }
        public ICollection<Instructor>? Instructors { get; set; }
    }
}
