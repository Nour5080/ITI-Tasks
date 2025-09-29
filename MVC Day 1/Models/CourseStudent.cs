namespace SchoolMvc.Models
{
    public class CourseStudent
    {
        public int Id { get; set; }

        public int Degree { get; set; }

        // Foreign Keys
        public int StudentId { get; set; }
        public Student Student { get; set; } = default!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = default!;
    }
}
