using System.Collections.Generic;

namespace SchoolApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Major { get; set; }

        public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
