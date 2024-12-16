
namespace StudentManagement
{
    partial class ViewTeachers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ExitIcon = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnTCHInfo = new System.Windows.Forms.Button();
            this.btnViewGrades = new System.Windows.Forms.Button();
            this.btnSLCourse = new System.Windows.Forms.Button();
            this.btnSTDInfo = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelstudentID = new System.Windows.Forms.Label();
            this.labelStudentName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chartStudent = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStudent)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.ExitIcon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 48);
            this.panel1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 25);
            this.label1.TabIndex = 14;
            this.label1.Text = "Student Management C# Project";
            // 
            // ExitIcon
            // 
            this.ExitIcon.AutoSize = true;
            this.ExitIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ExitIcon.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExitIcon.ForeColor = System.Drawing.Color.Red;
            this.ExitIcon.Location = new System.Drawing.Point(1031, 9);
            this.ExitIcon.Name = "ExitIcon";
            this.ExitIcon.Size = new System.Drawing.Size(21, 22);
            this.ExitIcon.TabIndex = 13;
            this.ExitIcon.Text = "X";
            this.ExitIcon.Click += new System.EventHandler(this.ExitIcon_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(-1, 194);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 19);
            this.label9.TabIndex = 45;
            this.label9.Text = "Student ID:";
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(145)))), ((int)(((byte)(105)))));
            this.btnLogOut.FlatAppearance.BorderSize = 0;
            this.btnLogOut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnLogOut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(50, 472);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(73, 33);
            this.btnLogOut.TabIndex = 44;
            this.btnLogOut.Text = "Log Out";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnTCHInfo
            // 
            this.btnTCHInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(145)))), ((int)(((byte)(105)))));
            this.btnTCHInfo.FlatAppearance.BorderSize = 0;
            this.btnTCHInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnTCHInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnTCHInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTCHInfo.ForeColor = System.Drawing.Color.White;
            this.btnTCHInfo.Location = new System.Drawing.Point(3, 344);
            this.btnTCHInfo.Name = "btnTCHInfo";
            this.btnTCHInfo.Size = new System.Drawing.Size(194, 33);
            this.btnTCHInfo.TabIndex = 42;
            this.btnTCHInfo.Text = "Teacher Information";
            this.btnTCHInfo.UseVisualStyleBackColor = false;
            this.btnTCHInfo.Click += new System.EventHandler(this.btnTCHInfo_Click);
            // 
            // btnViewGrades
            // 
            this.btnViewGrades.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(145)))), ((int)(((byte)(105)))));
            this.btnViewGrades.FlatAppearance.BorderSize = 0;
            this.btnViewGrades.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnViewGrades.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnViewGrades.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewGrades.ForeColor = System.Drawing.Color.White;
            this.btnViewGrades.Location = new System.Drawing.Point(3, 306);
            this.btnViewGrades.Name = "btnViewGrades";
            this.btnViewGrades.Size = new System.Drawing.Size(194, 33);
            this.btnViewGrades.TabIndex = 41;
            this.btnViewGrades.Text = "View Grades";
            this.btnViewGrades.UseVisualStyleBackColor = false;
            this.btnViewGrades.Click += new System.EventHandler(this.btnViewGrades_Click);
            // 
            // btnSLCourse
            // 
            this.btnSLCourse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(145)))), ((int)(((byte)(105)))));
            this.btnSLCourse.FlatAppearance.BorderSize = 0;
            this.btnSLCourse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnSLCourse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnSLCourse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSLCourse.ForeColor = System.Drawing.Color.White;
            this.btnSLCourse.Location = new System.Drawing.Point(3, 268);
            this.btnSLCourse.Name = "btnSLCourse";
            this.btnSLCourse.Size = new System.Drawing.Size(194, 33);
            this.btnSLCourse.TabIndex = 40;
            this.btnSLCourse.Text = "Select Course";
            this.btnSLCourse.UseVisualStyleBackColor = false;
            this.btnSLCourse.Click += new System.EventHandler(this.btnSLCourse_Click);
            // 
            // btnSTDInfo
            // 
            this.btnSTDInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(145)))), ((int)(((byte)(105)))));
            this.btnSTDInfo.FlatAppearance.BorderSize = 0;
            this.btnSTDInfo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnSTDInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.btnSTDInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSTDInfo.ForeColor = System.Drawing.Color.White;
            this.btnSTDInfo.Location = new System.Drawing.Point(3, 230);
            this.btnSTDInfo.Name = "btnSTDInfo";
            this.btnSTDInfo.Size = new System.Drawing.Size(194, 33);
            this.btnSTDInfo.TabIndex = 39;
            this.btnSTDInfo.Text = "Student Information";
            this.btnSTDInfo.UseVisualStyleBackColor = false;
            this.btnSTDInfo.Click += new System.EventHandler(this.btnSTDInfo_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::StudentManagement.Properties.Resources.icons8_admin_100;
            this.pictureBox1.Location = new System.Drawing.Point(50, 53);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(113)))), ((int)(((byte)(73)))));
            this.panel2.Controls.Add(this.labelstudentID);
            this.panel2.Controls.Add(this.labelStudentName);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.btnLogOut);
            this.panel2.Controls.Add(this.btnTCHInfo);
            this.panel2.Controls.Add(this.btnViewGrades);
            this.panel2.Controls.Add(this.btnSLCourse);
            this.panel2.Controls.Add(this.btnSTDInfo);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 600);
            this.panel2.TabIndex = 2;
            // 
            // labelstudentID
            // 
            this.labelstudentID.AutoSize = true;
            this.labelstudentID.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelstudentID.ForeColor = System.Drawing.Color.White;
            this.labelstudentID.Location = new System.Drawing.Point(85, 194);
            this.labelstudentID.Name = "labelstudentID";
            this.labelstudentID.Size = new System.Drawing.Size(64, 19);
            this.labelstudentID.TabIndex = 49;
            this.labelstudentID.Text = "-----------";
            // 
            // labelStudentName
            // 
            this.labelStudentName.AutoSize = true;
            this.labelStudentName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStudentName.ForeColor = System.Drawing.Color.White;
            this.labelStudentName.Location = new System.Drawing.Point(85, 158);
            this.labelStudentName.Name = "labelStudentName";
            this.labelStudentName.Size = new System.Drawing.Size(49, 19);
            this.labelStudentName.TabIndex = 48;
            this.labelStudentName.Text = "--------";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(2, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 19);
            this.label2.TabIndex = 37;
            this.label2.Text = "Welcome, ";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1080, 600);
            this.panel3.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.chartStudent);
            this.panel4.Location = new System.Drawing.Point(204, 51);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(873, 546);
            this.panel4.TabIndex = 2;
            // 
            // chartStudent
            // 
            chartArea1.Name = "ChartArea1";
            this.chartStudent.ChartAreas.Add(chartArea1);
            this.chartStudent.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartStudent.Legends.Add(legend1);
            this.chartStudent.Location = new System.Drawing.Point(0, 0);
            this.chartStudent.Name = "chartStudent";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Number Teachers";
            this.chartStudent.Series.Add(series1);
            this.chartStudent.Size = new System.Drawing.Size(873, 546);
            this.chartStudent.TabIndex = 0;
            this.chartStudent.Text = "Chart Teacher";
            // 
            // ViewTeachers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 600);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ViewTeachers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ViewTeachers";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStudent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label ExitIcon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnTCHInfo;
        private System.Windows.Forms.Button btnViewGrades;
        private System.Windows.Forms.Button btnSLCourse;
        private System.Windows.Forms.Button btnSTDInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label labelstudentID;
        private System.Windows.Forms.Label labelStudentName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStudent;
    }
}