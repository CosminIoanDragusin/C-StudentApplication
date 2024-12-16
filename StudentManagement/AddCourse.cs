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
using System.Data;
using System.IO;

namespace StudentManagement
{
    public partial class AddCourse : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public AddCourse()
        {
            InitializeComponent();
            displayAllCourseData();
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

        public bool checkConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                return true;
            }
            else { return false; }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (txtCourseID.Text == "" || txtCourse.Text == "" || txtDescription.Text == "" || txtDegree.Text == "" || cmbDepartment.SelectedIndex == -1 
                || txtPrice.Text == "" || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select id from course where id = @course_id ";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@course_id", txtCourseID.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Course existent!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string insertData = "insert into course(id, curs, descriere, licenta," +
                                " department, pret, statusul)" +
                                "values (@course_id, @curs, @descriere, @licenta, @department, @pret, @statusul)";

                            using (SqlCommand insertcmd = new SqlCommand(insertData, conn))
                            {

                                insertcmd.Parameters.AddWithValue("@course_id", txtCourseID.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@curs", txtCourse.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@descriere", txtDescription.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@licenta", txtDegree.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@department", cmbDepartment.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@pret", txtPrice.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@statusul", cmbStatus.SelectedItem.ToString());

                                insertcmd.ExecuteNonQuery();
                                clearFields();
                                displayAllCourseData();
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
            if (txtCourseID.Text == "" || txtCourse.Text == "" || txtDescription.Text == "" || txtDegree.Text == "" || cmbDepartment.SelectedIndex == -1
                || txtPrice.Text == "" || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur faceti update la cursul cu id: " + getID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string updateData = "update course set curs = @curs, descriere = @descriere, licenta = @licenta," +
                                " department = @department, pret = @pret, statusul = @statusul " +
                                " where id = @course_id";

                        using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                        {
                            updatecmd.Parameters.AddWithValue("@curs", txtCourse.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@descriere", txtDescription.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@licenta", txtDegree.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@department", cmbDepartment.SelectedItem.ToString());
                            updatecmd.Parameters.AddWithValue("@pret", txtPrice.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@statusul", cmbStatus.SelectedItem.ToString());

                            updatecmd.Parameters.AddWithValue("@course_id", getID);

                            updatecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllCourseData();
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
            if (txtCourseID.Text == "" || txtCourse.Text == "" || txtDescription.Text == "" || txtDegree.Text == "" || cmbDepartment.SelectedIndex == -1
                || txtPrice.Text == "" || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur doriti sa stergeti cursul cu id: " + getID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string deleteData = "delete from course where id = @course_id";

                        using (SqlCommand deletecmd = new SqlCommand(deleteData, conn))
                        {
                            deletecmd.Parameters.AddWithValue("@course_id", getID);

                            deletecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllCourseData();
                            MessageBox.Show("S-a sters subject cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }

                }
            }
        }

        public void clearFields()
        {
            txtCourseID.Text = "";
            txtCourse.Text = "";
            txtDescription.Text = "";
            txtDegree.Text = "";
            cmbDepartment.SelectedIndex = -1;
            txtPrice.Text = "";
            cmbStatus.SelectedIndex = -1;
        }

        public void displayAllCourseData()
        {

            CourseData tData = new CourseData();
            List<CourseData> listData = tData.AllCourseData();
            dataGridView1.DataSource = listData;
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

        private string getID;

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                getID = row.Cells[0].Value.ToString();
                string course = row.Cells[1].Value.ToString();
                string descriere = row.Cells[2].Value.ToString();
                string licenta = row.Cells[3].Value.ToString();
                string department = row.Cells[4].Value.ToString();
                string price = row.Cells[5].Value.ToString();
                string statusul = row.Cells[6].Value.ToString();

                txtCourseID.Text = getID;
                txtCourse.Text = course;
                txtDescription.Text = descriere;
                txtDegree.Text = licenta;
                cmbDepartment.Text = department;
                txtPrice.Text = price;
                cmbStatus.Text = statusul;
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
