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
    public partial class AddSubject : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public AddSubject()
        {

            InitializeComponent();
            displayAllSubjectData();
            displayCourses();
            DisplayTeacher();
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

        public void displayAllSubjectData()
        {
            SubjectData sData = new SubjectData();
            List<SubjectData> listData = sData.AllSubjectData();
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

        public void clearFields()
        {
            txtSubjectID.Text = "";
            txtSubjectCode.Text = "";
            txtSubject.Text = "";
            cmbCurs.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (txtSubjectID.Text == "" || txtSubjectCode.Text == "" || txtSubject.Text == "" || cmbCurs.SelectedIndex == -1
               || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select subject_id from subject where subject_id = @subject_id ";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@subject_id", txtSubjectID.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Subject existent!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string insertData = "insert into subject(subject_id, subject_code, subject, curs," +
                                " date_insert, status_subject)" +
                                "values (@subject_id, @subject_code, @subject, @curs, @date_insert, @status_subject)";

                            using (SqlCommand insertcmd = new SqlCommand(insertData, conn))
                            {

                                insertcmd.Parameters.AddWithValue("@subject_id", txtSubjectID.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@subject_code", txtSubjectCode.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@subject", txtSubject.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@curs", cmbCurs.SelectedItem.ToString());

                                DateTime today = DateTime.Today;
                                insertcmd.Parameters.AddWithValue("@date_insert", today);
                                insertcmd.Parameters.AddWithValue("@status_subject", cmbStatus.SelectedItem.ToString());

                                insertcmd.ExecuteNonQuery();
                                clearFields();
                                displayAllSubjectData();
                                MessageBox.Show("S-a adaugat cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (txtSubjectID.Text == "" || txtSubjectCode.Text == "" || txtSubject.Text == "" || cmbCurs.SelectedIndex == -1
               || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur faceti update la subject cu id: " + getID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string updateData = "update subject set subject_code = @subject_code, subject = @subject, curs = @curs," +
                                " date_update = @date_update, status_subject = @status_subject " +
                                " where subject_id = @subject_id";

                        using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                        {
                            updatecmd.Parameters.AddWithValue("@subject_code", txtSubjectCode.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@subject", txtSubject.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@curs", cmbCurs.SelectedItem.ToString());

                            DateTime today = DateTime.Today;
                            updatecmd.Parameters.AddWithValue("@date_update", today);
                            updatecmd.Parameters.AddWithValue("@status_subject", cmbStatus.SelectedItem.ToString());

                            updatecmd.Parameters.AddWithValue("@subject_id", getID);

                            updatecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllSubjectData();
                            MessageBox.Show("S-a facut update cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (txtSubjectID.Text == "" || txtSubjectCode.Text == "" || txtSubject.Text == "" || cmbCurs.SelectedIndex == -1
               || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur doriti sa stergeti subject cu id: " + getID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string deleteData = "delete from subject where subject_id = @subject_id";

                        using (SqlCommand deletecmd = new SqlCommand(deleteData, conn))
                        {
                            deletecmd.Parameters.AddWithValue("@subject_id", getID);

                            deletecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllSubjectData();
                            MessageBox.Show("S-a sters subject cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }

                }
            }
        }

        private string getID;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                getID = row.Cells[0].Value.ToString();
                string subject_code = row.Cells[1].Value.ToString();
                string subject = row.Cells[2].Value.ToString();
                string curs = row.Cells[3].Value.ToString();
                string status = row.Cells[6].Value.ToString();

                txtSubjectID.Text = getID;
                txtSubjectCode.Text = subject_code;
                txtSubject.Text = subject;
                cmbCurs.Text = curs;
                cmbStatus.Text = status;
            }
        }

        public void DisplayTeacher()
        {
            // SQL query to get the teacher's ID from the users table
            string sql = "SELECT teacher_id FROM users WHERE username = @username";
            string temp_teacherName = "";
            string temp_teacherID = "";

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
