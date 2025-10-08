﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstProject.Models;

namespace MyFirstProject.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Student> AddAsync(Student entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return;
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students
                                 .Include(s => s.Department)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllWithDetailsAsync()
        {
            return await _context.Students
                                 .Include(s => s.Department)
                                 .ToListAsync();
        }
        public async Task DeleteStudentWithCoursesAsync(int studentId)
        {
            var student = await _context.Students
                                .Include(s => s.CourseStudents)
                                .FirstOrDefaultAsync(s => s.Id == studentId);

            if (student != null)
            {
                // امسح كل سجلات الكورسات أولاً
                if (student.CourseStudents.Any())
                    _context.CourseStudents.RemoveRange(student.CourseStudents);

                // امسح الطالب نفسه
                _context.Students.Remove(student);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<Student> GetByUsernameAsync(string username)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Username == username);
        }
        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
                                 .Include(s => s.Department)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student?> GetWithDetailsAsync(int id)
        {
            return await _context.Students
                                 .Include(s => s.Department)
                                 .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Student>> GetByDepartmentAsync(int deptId)
        {
            return await _context.Students
                                 .Where(s => s.DeptId == deptId)
                                 .Include(s => s.Department)
                                 .ToListAsync();
        }

        public async Task UpdateAsync(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
