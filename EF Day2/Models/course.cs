using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Credit { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
