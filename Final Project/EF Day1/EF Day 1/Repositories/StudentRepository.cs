using EF_Day_1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF_Day_1.Repositories
{
    public class StudentRepository
    {
        private readonly UniversityContext _context;

        public StudentRepository(UniversityContext universityContext)
        {
            _context = universityContext;
        }

        public IQueryable<Student> GetAll()
        {
            return _context.Students.AsQueryable();
        }

        public Student GetById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.StudentId == id);
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var student = GetById(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
