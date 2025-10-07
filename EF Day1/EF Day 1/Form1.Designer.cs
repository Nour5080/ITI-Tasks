namespace EF_Day_1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvStudents = new DataGridView();
            txtName = new TextBox();
            txtMajor = new TextBox();
            add = new Button();
            update = new Button();
            delete = new Button();
            reset = new Button();
            txtId = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            clbCourses = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToOrderColumns = true;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Location = new Point(32, 12);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.Size = new Size(593, 188);
            dgvStudents.TabIndex = 0;
            dgvStudents.CellClick += dgvStudents_CellClick;
            // 
            // txtName
            // 
            txtName.Location = new Point(309, 270);
            txtName.Name = "txtName";
            txtName.Size = new Size(125, 27);
            txtName.TabIndex = 1;
            txtName.Text = "Name";
            // 
            // txtMajor
            // 
            txtMajor.Location = new Point(500, 270);
            txtMajor.Name = "txtMajor";
            txtMajor.Size = new Size(125, 27);
            txtMajor.TabIndex = 2;
            txtMajor.Text = "Major";
            // 
            // add
            // 
            add.Location = new Point(32, 382);
            add.Name = "add";
            add.Size = new Size(94, 29);
            add.TabIndex = 3;
            add.Text = "add";
            add.UseVisualStyleBackColor = true;
            add.Click += add_Click;
            // 
            // update
            // 
            update.Location = new Point(613, 382);
            update.Name = "update";
            update.Size = new Size(94, 29);
            update.TabIndex = 4;
            update.Text = "update";
            update.UseVisualStyleBackColor = true;
            update.Click += update_Click;
            // 
            // delete
            // 
            delete.Location = new Point(222, 382);
            delete.Name = "delete";
            delete.Size = new Size(94, 29);
            delete.TabIndex = 5;
            delete.Text = "delete";
            delete.UseVisualStyleBackColor = true;
            delete.Click += delete_Click;
            // 
            // reset
            // 
            reset.Location = new Point(412, 382);
            reset.Name = "reset";
            reset.Size = new Size(94, 29);
            reset.TabIndex = 6;
            reset.Text = "reset";
            reset.UseVisualStyleBackColor = true;
            reset.Click += reset_Click;
            // 
            // txtId
            // 
            txtId.Location = new Point(92, 270);
            txtId.Name = "txtId";
            txtId.Size = new Size(125, 27);
            txtId.TabIndex = 7;
            txtId.Text = "ID";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(527, 225);
            label1.Name = "label1";
            label1.Size = new Size(48, 20);
            label1.TabIndex = 8;
            label1.Text = "Major";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(340, 225);
            label2.Name = "label2";
            label2.Size = new Size(49, 20);
            label2.TabIndex = 9;
            label2.Text = "Name";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(108, 225);
            label3.Name = "label3";
            label3.Size = new Size(24, 20);
            label3.TabIndex = 10;
            label3.Text = "ID";
            // 
            // clbCourses
            // 
            clbCourses.FormattingEnabled = true;
            clbCourses.Location = new Point(638, 12);
            clbCourses.Name = "clbCourses";
            clbCourses.Size = new Size(150, 180);
            clbCourses.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(clbCourses);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtId);
            Controls.Add(reset);
            Controls.Add(delete);
            Controls.Add(update);
            Controls.Add(add);
            Controls.Add(txtMajor);
            Controls.Add(txtName);
            Controls.Add(dgvStudents);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvStudents;
        private TextBox txtName;
        private TextBox txtMajor;
        private Button add;
        private Button update;
        private Button delete;
        private Button reset;
        private TextBox txtId;
        private Label label1;
        private Label label2;
        private Label label3;
        private CheckedListBox clbCourses;
    }
}
