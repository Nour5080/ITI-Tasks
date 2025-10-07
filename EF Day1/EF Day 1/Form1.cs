using EF_Day_1.Models;
using EF_Day_1.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EF_Day_1
{
    public partial class Form1 : Form
    {
        private readonly UniversityContext _context;
        private readonly StudentRepository _studentRepo;

        public Form1()
        {
            InitializeComponent();
            _context = new UniversityContext();
            _studentRepo = new StudentRepository(_context);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCourses();    // تحميل الكورسات في CheckedListBox
            LoadStudents();
            SetButtonStates(false);
        }

        // ------------------- Load Courses -------------------
        private void LoadCourses()
        {
            var courses = _context.Courses.ToList();
            clbCourses.Items.Clear();
            foreach (var c in courses)
                clbCourses.Items.Add(c, false);
        }

        // ------------------- Load Students -------------------
        private void LoadStudents()
        {
            var students = _context.Students
                .Include(s => s.Courses)
                .ToList();

            var studentDisplay = students.Select(s => new
            {
                s.StudentId,
                s.Name,
                s.Major,
                Courses = string.Join(", ", s.Courses.Select(c => c.Title))
            }).ToList();

            dgvStudents.DataSource = studentDisplay;
            ClearForm();
        }

        // ------------------- Validation -------------------
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter student name.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMajor.Text))
            {
                MessageBox.Show("Please enter student major.");
                return false;
            }
            return true;
        }

        // ------------------- Button States -------------------
        private void SetButtonStates(bool isStudentSelected)
        {
            add.Enabled = !isStudentSelected;
            update.Enabled = isStudentSelected;
            delete.Enabled = isStudentSelected;
            reset.Enabled = isStudentSelected;
        }

        // ------------------- Clear Form -------------------
        private void ClearForm()
        {
            txtId.Clear();
            txtName.Clear();
            txtMajor.Clear();
            for (int i = 0; i < clbCourses.Items.Count; i++)
                clbCourses.SetItemChecked(i, false);
            SetButtonStates(false);
        }

        // ------------------- DataGrid Cell Click -------------------
        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int studentId = (int)dgvStudents.Rows[e.RowIndex].Cells["StudentId"].Value;
                var student = _studentRepo.GetById(studentId);

                txtId.Text = student.StudentId.ToString();
                txtName.Text = student.Name;
                txtMajor.Text = student.Major;

                // تفعيل CheckedListBox حسب الكورسات
                for (int i = 0; i < clbCourses.Items.Count; i++)
                {
                    var course = clbCourses.Items[i] as Course;
                    clbCourses.SetItemChecked(i, student.Courses.Any(c => c.CourseId == course.CourseId));
                }

                SetButtonStates(true);
            }
        }

        // ------------------- Add Student -------------------
        private void add_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            var student = new Student
            {
                Name = txtName.Text,
                Major = txtMajor.Text
            };

            foreach (var item in clbCourses.CheckedItems)
                student.Courses.Add(item as Course);

            _studentRepo.Add(student);
            LoadStudents();
        }

        // ------------------- Update Student -------------------
        private void update_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            int id = int.Parse(txtId.Text);
            var student = _studentRepo.GetById(id);
            if (student != null)
            {
                student.Name = txtName.Text;
                student.Major = txtMajor.Text;

                student.Courses.Clear();
                foreach (var item in clbCourses.CheckedItems)
                    student.Courses.Add(item as Course);

                _studentRepo.Update(student);
                LoadStudents();
            }
        }

        // ------------------- Delete Student -------------------
        private void delete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            var confirm = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                _studentRepo.Delete(id);
                LoadStudents();
            }
        }

        // ------------------- Reset Form -------------------
        private void reset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
