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

namespace StudentManagement
{
    public partial class ViewGrades : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public ViewGrades()
        {
            InitializeComponent();
            DisplayStudent();
            displayAllGradesData();
        }

        public void displayAllGradesData()
        {
            GradesData sData = new GradesData();
            List<GradesData> listData = sData.AllGradesData();
            dataGridView1.DataSource = listData;
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string an = row.Cells[1].Value.ToString();
                string curs = row.Cells[2].Value.ToString();
                string semestru_I = row.Cells[3].Value.ToString();
                string semestru_II = row.Cells[4].Value.ToString();
                string semestru_final = row.Cells[5].Value.ToString();

                labelAn.Text = an;
                labelCurs.Text = curs;
                labelSemI.Text = semestru_I;
                labelSemII.Text = semestru_II;
                labelMedie.Text = semestru_final;

            }
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
                    labelstdID.Text = temp_studentID;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

    }
}
