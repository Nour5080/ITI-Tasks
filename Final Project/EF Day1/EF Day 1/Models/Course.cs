using System;
using System.Collections.Generic;

namespace EF_Day_1.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int Credit { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
