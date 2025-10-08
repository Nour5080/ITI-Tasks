using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required.")]
        [StringLength(100, ErrorMessage = "Department Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Manager Name is required.")]
        [StringLength(100, ErrorMessage = "Manager Name cannot exceed 100 characters.")]
        public string ManagerName { get; set; }

        public ICollection<Course>? Courses { get; set; }
        public ICollection<Student>? Students { get; set; }
        public ICollection<Instructor>? Instructors { get; set; }
    }
}
