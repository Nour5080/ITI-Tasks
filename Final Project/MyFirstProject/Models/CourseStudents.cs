using System.ComponentModel.DataAnnotations;

namespace MyFirstProject.Models
{
    public class CourseStudents
    {
        public int Id { get; set; }

        [Range(0, 100, ErrorMessage = "Degree must be between 0 and 100.")]
        public int Degree { get; set; }

        // Foreign Keys
        [Required(ErrorMessage = "Course is required.")]
        public int CrsId { get; set; }
        public Course? Course { get; set; }

        [Required(ErrorMessage = "Student is required.")]
        public int StdId { get; set; }
        public Student? Student { get; set; }
    }
}
    