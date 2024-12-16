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
    public partial class CreareCerereStudentCurs : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public CreareCerereStudentCurs()
        {
            InitializeComponent();
            DisplayStudent();
            displayCourses();
            displayAllStudentCourseData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Vreti sa inchideti aplicatia?", "Confirmation Message", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Hide();
            }

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

        public bool checkConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                return true;
            }
            else { return false; }
        }

        public void displayCourses()
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select * from course";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cmbCourseCerere.Items.Add(reader["curs"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        public void displayAllStudentCourseData()
        {
            StudentCourseData sData = new StudentCourseData();
            List<StudentCourseData> listData = sData.AllStudentCourseData();
            dataGridView1.DataSource = listData;
        }

        private void buttonJoinCourse_Click(object sender, EventArgs e)
        {
            if (cmbCourseCerere.SelectedIndex == -1)
               
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select student_id, curs from student_course_handle where student_id = @student_id and curs = @curs ";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", labelstudentID.Text.Trim());
                        cmd.Parameters.AddWithValue("@curs", cmbCourseCerere.SelectedItem.ToString());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("V-ati inscris deja la un curs!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string insertData = "insert into student_course_handle(student_id, curs) " +
                                "values (@student_id, @curs)";

                            using (SqlCommand insertcmd = new SqlCommand(insertData, conn))
                            {

                                insertcmd.Parameters.AddWithValue("@student_id", labelstudentID.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@curs", cmbCourseCerere.SelectedItem.ToString());

                                insertcmd.ExecuteNonQuery();
                                clearFields();
                                displayAllStudentCourseData();
                                MessageBox.Show("S-a adaugat cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        public void clearFields()
        {
            cmbCourseCerere.SelectedIndex = -1;

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string curs = row.Cells[1].Value.ToString();
                string licenta = row.Cells[3].Value.ToString();
                string department = row.Cells[4].Value.ToString();
                string pret = row.Cells[5].Value.ToString();
                string status_curs = row.Cells[6].Value.ToString();

                labelCourse.Text = curs;
                labelLicenta.Text = licenta;
                labelDepartment.Text = department;
                labelPrice.Text = pret;
                labelStatus.Text = status_curs;
            }
        }
    }
}
