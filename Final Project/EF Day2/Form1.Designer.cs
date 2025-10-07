namespace SchoolApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMajor;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblMajor;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckedListBox clbCourses;
        private System.Windows.Forms.Label lblCourses;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dataGridView1 = new DataGridView();
            txtName = new TextBox();
            txtMajor = new TextBox();
            lblName = new Label();
            lblMajor = new Label();
            lblCourses = new Label();
            clbCourses = new CheckedListBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeight = 29;
            dataGridView1.Location = new Point(20, 200);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(500, 250);
            dataGridView1.TabIndex = 10;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // txtName
            // 
            txtName.Location = new Point(120, 20);
            txtName.Name = "txtName";
            txtName.Size = new Size(150, 27);
            txtName.TabIndex = 1;
            // 
            // txtMajor
            // 
            txtMajor.Location = new Point(120, 50);
            txtMajor.Name = "txtMajor";
            txtMajor.Size = new Size(150, 27);
            txtMajor.TabIndex = 3;
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(80, 25);
            lblName.TabIndex = 0;
            lblName.Text = "Name:";
            // 
            // lblMajor
            // 
            lblMajor.Location = new Point(20, 50);
            lblMajor.Name = "lblMajor";
            lblMajor.Size = new Size(80, 25);
            lblMajor.TabIndex = 2;
            lblMajor.Text = "Major:";
            // 
            // lblCourses
            // 
            lblCourses.Location = new Point(20, 80);
            lblCourses.Name = "lblCourses";
            lblCourses.Size = new Size(80, 25);
            lblCourses.TabIndex = 4;
            lblCourses.Text = "Courses:";
            // 
            // clbCourses
            // 
            clbCourses.CheckOnClick = true;
            clbCourses.Location = new Point(120, 80);
            clbCourses.Name = "clbCourses";
            clbCourses.Size = new Size(250, 92);
            clbCourses.TabIndex = 5;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(400, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(400, 60);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 30);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(400, 100);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(400, 140);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(80, 30);
            btnClear.TabIndex = 9;
            btnClear.Text = "Clear";
            btnClear.Click += btnClear_Click;
            // 
            // Form1
            // 
            ClientSize = new Size(550, 480);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblMajor);
            Controls.Add(txtMajor);
            Controls.Add(lblCourses);
            Controls.Add(clbCourses);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(dataGridView1);
            Name = "Form1";
            Text = "Student Management";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
