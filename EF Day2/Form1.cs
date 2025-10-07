using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Models;
using SchoolApp.Repositories;

namespace SchoolApp
{
    public partial class Form1 : Form
    {
        private StudentRepository repo = new StudentRepository();
        private Student selectedStudent = null;

        public Form1()
        {
            InitializeComponent();
            LoadCourses();   
            LoadData();      
            SetButtonState();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCourses();
            LoadData();
        }

        private void LoadCourses()
        {
            using (var context = new SchoolContext())
            {
                clbCourses.Items.Clear();

                foreach (var c in context.Courses.ToList())
                {
                    clbCourses.Items.Add(new CheckedItem { Id = c.Id, Name = c.Title });
                }
            }
        }

        private void LoadData()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = repo.GetAll()
                .Select(s => new
                {
                    s.Id,
                    s.Name,
                    s.Major,
                    Courses = string.Join(", ", s.StudentCourses.Select(sc => sc.Course.Title))
                }).ToList();
        }

        private void SetButtonState()
        {
            btnAdd.Enabled = selectedStudent == null;
            btnUpdate.Enabled = selectedStudent != null;
            btnDelete.Enabled = selectedStudent != null;
        }

        private void ClearForm()
        {
            selectedStudent = null;
            txtName.Text = "";
            txtMajor.Text = "";

            for (int i = 0; i < clbCourses.Items.Count; i++)
                clbCourses.SetItemChecked(i, false);

            SetButtonState();
        }

        private List<int> GetSelectedCourseIds()
        {
            return clbCourses.CheckedItems.Cast<CheckedItem>().Select(ci => ci.Id).ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtMajor.Text))
            {
                MessageBox.Show("Name and Major are required!");
                return;
            }

            var student = new Student
            {
                Name = txtName.Text,
                Major = txtMajor.Text
            };

            repo.Add(student, GetSelectedCourseIds());
            LoadData();
            ClearForm();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStudent == null) return;

            selectedStudent.Name = txtName.Text;
            selectedStudent.Major = txtMajor.Text;

            repo.Update(selectedStudent, GetSelectedCourseIds());
            LoadData();
            ClearForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStudent == null) return;

            repo.Delete(selectedStudent);
            LoadData();
            ClearForm();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                using (var context = new SchoolContext())
                {
                    int id = (int)dataGridView1.Rows[e.RowIndex].Cells["Id"].Value;

                    selectedStudent = context.Students
                        .Include(s => s.StudentCourses)
                        .ThenInclude(sc => sc.Course)
                        .FirstOrDefault(s => s.Id == id);

                    if (selectedStudent == null) return;

                    txtName.Text = selectedStudent.Name;
                    txtMajor.Text = selectedStudent.Major;

                    for (int i = 0; i < clbCourses.Items.Count; i++)
                    {
                        var ci = (CheckedItem)clbCourses.Items[i];
                        clbCourses.SetItemChecked(i,
                            selectedStudent.StudentCourses.Any(sc => sc.CourseId == ci.Id));
                    }

                    SetButtonState();
                }
            }
        }
    }

    public class CheckedItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
