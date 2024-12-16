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
    public partial class AddStudentToCourse : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public AddStudentToCourse()
        {
            InitializeComponent();
            DisplayTeacher();
            displayID();
            displayCourses();
            displayAllTeacherStudentData();
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

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudentToCourse addStudent = new AddStudentToCourse();
            addStudent.Show();
            this.Hide();
        }

        private void btnSubject_Click(object sender, EventArgs e)
        {
            AddSubject addSubject = new AddSubject();
            addSubject.Show();
            this.Hide();
        }

        private void btnAddGrades_Click(object sender, EventArgs e)
        {
            AddNote addNote = new AddNote();
            addNote.Show();
            this.Hide();
        }

        private void btnAddCourses_Click(object sender, EventArgs e)
        {
            AddCourse addCourse = new AddCourse();
            addCourse.Show();
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

        public void displayAllTeacherStudentData()
        {
            TeacherStudentData sData = new TeacherStudentData();
            List<TeacherStudentData> listData = sData.AllTeacherStudentData();
            dataGridView1.DataSource = listData;
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
                                cmbCurs.Items.Add(reader["curs"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        public void displayID()
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select * from student";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cmbStudentID.Items.Add(reader["student_id"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        public void displayCnp()
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select * from student where student_id = @student_id";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", cmbStudentID.SelectedItem.ToString());
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cmbCnp.Items.Add(reader["cnp"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        public void displayName()
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select * from student where student_id = @student_id";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", cmbStudentID.SelectedItem.ToString());
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cmbName.Items.Add(reader["nume"].ToString());
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        public void displayPrenume()
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select * from student where student_id = @student_id";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", cmbStudentID.SelectedItem.ToString());
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cmbPrename.Items.Add(reader["prenume"].ToString());
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
            cmbStudentID.SelectedIndex = -1;
            cmbCnp.SelectedIndex = -1;
            cmbName.SelectedIndex = -1;
            cmbPrename.SelectedIndex = -1;
            cmbAn.SelectedIndex = -1;
            cmbCurs.SelectedIndex = -1;
            txtPayment.Text = "";
            cmbStatusPayment.SelectedIndex = -1;       
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string student_id = row.Cells[0].Value.ToString();
                string student_cnp = row.Cells[1].Value.ToString();
                string student_name = row.Cells[2].Value.ToString();
                string student_prename = row.Cells[3].Value.ToString();
                string student_an = row.Cells[4].Value.ToString();
                string student_curs = row.Cells[5].Value.ToString();
                string payment = row.Cells[7].Value.ToString();
                string status_payment = row.Cells[8].Value.ToString();

                cmbStudentID.Text = student_id;
                cmbCnp.Text = student_cnp;
                cmbName.Text = student_name;
                cmbPrename.Text = student_prename;
                cmbAn.Text = student_an;
                cmbCurs.Text = student_curs;
                txtPayment.Text = payment;
                cmbStatusPayment.Text = status_payment;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (cmbStudentID.SelectedIndex == -1 || cmbCnp.SelectedIndex == -1 || cmbName.SelectedIndex == -1 || cmbPrename.SelectedIndex == -1 ||
                cmbAn.SelectedIndex == -1 || cmbCurs.SelectedIndex == -1 || txtPayment.Text == "" || cmbStatusPayment.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select student_id, student_curs from teacher_student where student_id = @student_id and student_curs = @student_curs";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@student_id", cmbStudentID.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@student_curs", cmbCurs.SelectedItem.ToString());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("A fost inscris deja studentul la curs!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            string insertData = "insert into teacher_student(student_id, student_cnp, student_name, student_prename, student_an," +
                                " student_curs, date_insert, payment, status_payment) " +
                                " values (@student_id, @student_cnp, @student_name, @student_prename, @student_an, @student_curs, @date_insert" +
                                ", @payment, @status_payment);";

                            using (SqlCommand insertcmd = new SqlCommand(insertData, conn))
                            {

                                insertcmd.Parameters.AddWithValue("@student_id", cmbStudentID.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@student_cnp", cmbCnp.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@student_name", cmbName.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@student_prename", cmbPrename.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@student_an", cmbAn.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@student_curs", cmbCurs.SelectedItem.ToString());

                                DateTime today = DateTime.Today;
                                insertcmd.Parameters.AddWithValue("@date_insert", today);

                                insertcmd.Parameters.AddWithValue("@payment", txtPayment.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@status_payment", cmbStatusPayment.SelectedItem.ToString());

                                insertcmd.ExecuteNonQuery();
                                clearFields();
                                displayAllTeacherStudentData();
                                MessageBox.Show("S-a adaugat cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (cmbStudentID.SelectedIndex == -1 || cmbCnp.SelectedIndex == -1 || cmbName.SelectedIndex == -1 || cmbPrename.SelectedIndex == -1 ||
                cmbAn.SelectedIndex == -1 || cmbCurs.SelectedIndex == -1 || txtPayment.Text == "" || cmbStatusPayment.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur doriti sa stergeti cursul la" + cmbCurs.SelectedItem.ToString() +
                "studentului cu cnp: " + cmbCnp.SelectedItem.ToString() + "?",
                "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string deleteData = "delete from teacher_student where student_cnp = @student_cnp and student_curs = @student_curs;";

                        using (SqlCommand deletecmd = new SqlCommand(deleteData, conn))
                        {
                            deletecmd.Parameters.AddWithValue("@student_cnp", cmbCnp.SelectedItem.ToString());
                            deletecmd.Parameters.AddWithValue("@student_curs", cmbCurs.SelectedItem.ToString());
                            deletecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllTeacherStudentData();
                            MessageBox.Show("S-a sters cursul la student!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }

                }
            }
        }

        private void cmbStudentID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbName.Items.Clear();
            displayName();
            cmbCnp.Items.Clear();
            displayCnp();
            cmbPrename.Items.Clear();
            displayPrenume();
        }

        public void DisplayTeacher()
        {
            // SQL query to get the teacher's ID from the users table
            string sql = "SELECT teacher_id FROM users WHERE username = @username";
            string temp_teacherName = "";
            string temp_teacherID = "";

            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                try
                {
                    conn.Open();

                    // First query to get the teacher_id
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", ListData.teacher_username); // Safely add parameter to prevent SQL injection

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                temp_teacherID = reader["teacher_id"].ToString(); // Get teacher_id from the user table
                            }
                        }
                    }

                    // If we found a teacher ID, proceed to get the teacher's full name
                    if (!string.IsNullOrEmpty(temp_teacherID))
                    {
                        string selectData = "SELECT full_name FROM teacher WHERE teacher_id = @teacherID";

                        using (SqlCommand cmd = new SqlCommand(selectData, conn))
                        {
                            cmd.Parameters.AddWithValue("@teacherID", temp_teacherID); // Add teacher_id parameter safely

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    temp_teacherName = reader["full_name"].ToString(); // Get the teacher's full name
                                }
                            }
                        }
                    }

                    // Update your label controls with the retrieved data
                    labelTeacherName.Text = temp_teacherName;
                    labelTeacherID.Text = temp_teacherID;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

    }
}
