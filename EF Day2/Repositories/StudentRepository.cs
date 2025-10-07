using Microsoft.EntityFrameworkCore;
using SchoolApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolApp.Repositories
{
    public class StudentRepository
    {
        private SchoolContext _context = new SchoolContext();

        public List<Student> GetAll()
        {
            return _context.Students
                .Include(s => s.StudentCourses)
                .ThenInclude(sc => sc.Course)
                .ToList();
        }

        public void Add(Student student, List<int> courseIds)
        {
            foreach (var id in courseIds)
            {
                student.StudentCourses.Add(new StudentCourse { StudentId = student.Id, CourseId = id });
            }
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student, List<int> courseIds)
        {
            var existing = _context.Students
                .Include(s => s.StudentCourses)
                .First(s => s.Id == student.Id);

            existing.Name = student.Name;
            existing.Major = student.Major;

            existing.StudentCourses.Clear();
            foreach (var id in courseIds)
                existing.StudentCourses.Add(new StudentCourse { StudentId = student.Id, CourseId = id });

            _context.SaveChanges();
        }

        public void Delete(Student student)
        {
            var existing = _context.Students
                .Include(s => s.StudentCourses)
                .First(s => s.Id == student.Id);

            _context.Students.Remove(existing);
            _context.SaveChanges();
        }
    }
}
