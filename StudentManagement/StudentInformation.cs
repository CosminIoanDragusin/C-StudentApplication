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
    public partial class StudentInformation : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public StudentInformation()
        {
            InitializeComponent();
            DisplayStudent();
            displayStudentAllData();
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

        public bool checkConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                return true;
            }
            else { return false; }
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
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }

        }

        public void displayStudentAllData()
        {
            string sqlDisplay = "SELECT * FROM student WHERE student_id = @student_id";
           
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(sqlDisplay, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", labelstudentID.Text.Trim());

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if any data is returned
                            if (reader.HasRows)
                            {
                                // Move to the first row (there should only be one row for a student_id)
                                if (reader.Read())
                                {
                                    // Retrieve data from the query result
                                    string labelstudentID = reader["student_id"].ToString();
                                    string studentCnp = reader["cnp"].ToString();
                                    string studentName = reader["nume"].ToString();
                                    string studentPrenume = reader["prenume"].ToString();
                                    string studentAn = reader["an"].ToString();
                                    string DataNasteri = reader["data_nasteri"].ToString();
                                    string studentStatus = reader["status_student"].ToString();
                                    string studentPayment = reader["payment"].ToString();
                                    string studentPayStatus = reader["payment_status"].ToString();
                                    string imagePath = reader["image"].ToString();
                                    string dateInsertion = reader["date_insert"].ToString();


                                    // Display the data in your label controls
                                    labelID.Text = labelstudentID;
                                    labelCnp.Text = studentCnp;
                                    labelName.Text = studentName;
                                    labelPrenume.Text = studentPrenume;
                                    labelAn.Text = studentAn;
                                    labelDataNasteri.Text = DataNasteri;
                                    labelStatus.Text = studentStatus;
                                    labelPayment.Text = studentPayment;
                                    labelPayStatus.Text = studentPayStatus;
                                    labelDateInsert.Text = dateInsertion;
                                    StudentImage.Image = Image.FromFile(imagePath); 
                                }
                            }
                           
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }

            }
        }
    }
}
