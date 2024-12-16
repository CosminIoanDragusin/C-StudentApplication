using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;

namespace StudentManagement
{
    public partial class ViewTeachers : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        private Chart dashboardChart;
        public ViewTeachers()
        {
            InitializeComponent();
            DisplayStudent();
            PopulateChartData();
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vreti sa inchideti aplicatia?", "Confirmation Message", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }
        }

        private void btnSTDInfo_Click(object sender, EventArgs e)
        {
            StudentInformation stdInfo = new StudentInformation();
            stdInfo.Show();
            this.Hide();
        }

        private void btnSLCourse_Click(object sender, EventArgs e)
        {
            CreareCerereStudentCurs creareCerere = new CreareCerereStudentCurs();
            creareCerere.Show();
            this.Hide();
        }

        private void btnViewGrades_Click(object sender, EventArgs e)
        {
            ViewGrades viewGrades = new ViewGrades();
            viewGrades.Show();
            this.Hide();
        }

        private void btnTCHInfo_Click(object sender, EventArgs e)
        {
            ViewTeachers viewTeachers = new ViewTeachers();
            viewTeachers.Show();
            this.Hide();
        }

        public void DisplayStudent()
        {
            // SQL query to get the teacher's ID from the users table
            string sql = "SELECT student_id FROM users WHERE username = @username";
            string temp_studentName = "";
            string temp_studentID = "";

            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();

                    // First query to get the teacher_id
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", ListData.student_username); // Safely add parameter to prevent SQL injection

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                temp_studentID = reader["student_id"].ToString(); // Get teacher_id from the user table
                            }
                        }
                    }

                    // If we found a teacher ID, proceed to get the teacher's full name
                    if (!string.IsNullOrEmpty(temp_studentID))
                    {
                        string selectData = "SELECT nume FROM student WHERE student_id = @studentID";

                        using (SqlCommand cmd = new SqlCommand(selectData, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentID", temp_studentID); // Add teacher_id parameter safely

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    temp_studentName = reader["nume"].ToString(); // Get the teacher's full name
                                }
                            }
                        }
                    }

                    // Update your label controls with the retrieved data
                    labelStudentName.Text = temp_studentName;
                    labelstudentID.Text = temp_studentID;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

       

        private void PopulateChartData()
        {
            // Clear any previous data
            chartStudent.Series["Number Teachers"].Points.Clear();

            string sql = "SELECT date_insert, COUNT(teacher_id) FROM teacher GROUP BY date_insert";

            try
            {
                // Open database connection
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        // Add data points to the series
                        string date = reader.GetDateTime(0).ToString("yyyy-MM-dd");
                        int count = reader.GetInt32(1);
                        chartStudent.Series["Number Teachers"].Points.AddXY(date, count);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }

    }
}
