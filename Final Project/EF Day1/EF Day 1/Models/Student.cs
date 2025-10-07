using System;
using System.Collections.Generic;

namespace EF_Day_1.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Major { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
