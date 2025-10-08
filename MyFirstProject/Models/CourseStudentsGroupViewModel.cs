using System.Collections.Generic;

namespace MyFirstProject.Models
{
    public class CourseStudentsGroupViewModel
    {
        public Student Student { get; set; } = new Student();
        public List<CourseStudents> CourseStudentEntries { get; set; } = new List<CourseStudents>();
    }
}
