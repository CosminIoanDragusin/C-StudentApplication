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
    public partial class AddNote : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public AddNote()
        {
            InitializeComponent();
            displayCnp();
            displayCourses();
            displayAllGradesData();
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

        public void displayAllGradesData()
        {
            GradesData sData = new GradesData();
            List<GradesData> listData = sData.AllGradesData();
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

        public void displayCnp()
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
                                cmbCnp.Items.Add(reader["cnp"].ToString());
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
            cmbCurs.SelectedIndex = -1;
            cmbCnp.SelectedIndex = -1;
            cmbAn.SelectedIndex = -1;
            txtSemI.Text = "";
            txtSemII.Text = "";

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
               string cnp = row.Cells[0].Value.ToString();
                string an = row.Cells[1].Value.ToString();
                string curs = row.Cells[2].Value.ToString();
                string semestru_I = row.Cells[3].Value.ToString();
                string semestru_II = row.Cells[4].Value.ToString();

                cmbCnp.Text = cnp;
                cmbAn.Text = an;
                cmbCurs.Text = curs;
                txtSemI.Text = semestru_I;
                txtSemII.Text = semestru_II;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (cmbCurs.SelectedIndex == -1 || cmbCnp.SelectedIndex == -1 || cmbAn.SelectedIndex == -1 || txtSemI.Text == "" || txtSemII.Text == "")
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkConnection())
            {
                float finalCheck1 = 0;
                float finalCheck2 = 0;
                float finalResult = 0;

                try
                {
                    conn.Open();

                    string selectData = "select curs from nota where curs = @curs ";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@curs", cmbCurs.SelectedItem.ToString());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Nota a fost acordata la student!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            finalCheck1 = float.Parse(txtSemI.Text.Trim());
                            finalCheck2 = float.Parse(txtSemII.Text.Trim());
                            finalResult = (finalCheck1 + finalCheck2) / 2.0f;

                            string insertData = "insert into nota(cnp, an, curs, semestru_I, semestru_II, semestru_final) " +
                                " values (@cnp, @an, @curs, @semestru_I, @semestru_II, @semestru_final);";

                            using (SqlCommand insertcmd = new SqlCommand(insertData, conn))
                            {

                                insertcmd.Parameters.AddWithValue("@cnp", cmbCnp.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@an", cmbAn.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@curs", cmbCurs.SelectedItem.ToString());

                                insertcmd.Parameters.AddWithValue("@semestru_I", txtSemI.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@semestru_II", txtSemII.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@semestru_final", finalResult);

                                insertcmd.ExecuteNonQuery();
                                clearFields();
                                displayAllGradesData();
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
            if (cmbCurs.SelectedIndex == -1 || cmbCnp.SelectedIndex == -1 || cmbAn.SelectedIndex == -1 || txtSemI.Text == "" || txtSemII.Text == "")
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur faceti update la nota studentului: " + cmbCnp.SelectedItem.ToString() + "?",
                "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                float finalCheck1 = 0;
                float finalCheck2 = 0;
                float finalResult = 0;
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        finalCheck1 = float.Parse(txtSemI.Text.Trim());
                        finalCheck2 = float.Parse(txtSemII.Text.Trim());
                        finalResult = (finalCheck1 + finalCheck2) / 2.0f;

                        string updateData = "update nota set an = @an, curs = @curs," +
                                " semestru_I = @semestru_I, semestru_II = @semestru_II, semestru_final = @semestru_final " +
                                " where cnp = @cnp and curs = @curs;";

                        using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                        {
                            updatecmd.Parameters.AddWithValue("@an", cmbAn.SelectedItem.ToString());
                            updatecmd.Parameters.AddWithValue("@curs", cmbCurs.SelectedItem.ToString());
                            updatecmd.Parameters.AddWithValue("@semestru_I", txtSemI.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@semestru_II", txtSemII.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@semestru_final", finalResult);

                            updatecmd.Parameters.AddWithValue("@cnp", cmbCnp.SelectedItem.ToString());

                            updatecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllGradesData();
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
            if (cmbCurs.SelectedIndex == -1 || cmbCnp.SelectedIndex == -1 || cmbAn.SelectedIndex == -1 || txtSemI.Text == "" || txtSemII.Text == "")
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur doriti sa stergeti nota studentului cu cnp: " + cmbCnp.SelectedItem.ToString() + "?",
                "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string deleteData = "delete from nota where cnp = @cnp and curs = @curs;";

                        using (SqlCommand deletecmd = new SqlCommand(deleteData, conn))
                        {
                            deletecmd.Parameters.AddWithValue("@cnp", cmbCnp.SelectedItem.ToString());
                            deletecmd.Parameters.AddWithValue("@curs", cmbCurs.SelectedItem.ToString());
                            deletecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllGradesData();
                            MessageBox.Show("S-a sters nota de la student cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }

                }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();
                    using (DataTable dt = new DataTable("nota"))
                    {
                        string searchData = "select * from nota where cnp like @cnp or curs like @curs or an like @an;";

                        using (SqlCommand searchcmd = new SqlCommand(searchData, conn))
                        {
                            string searchText = "%" + txtSearch.Text + "%";
                            searchcmd.Parameters.AddWithValue("@cnp",  txtSearch.Text);
                            searchcmd.Parameters.AddWithValue("@curs", txtSearch.Text);
                            searchcmd.Parameters.AddWithValue("@an", txtSearch.Text);
                            SqlDataAdapter adapter = new SqlDataAdapter(searchcmd);
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                            MessageBox.Show("S-a cautat cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }

        }
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) {
                btnSearch.PerformClick();
            } // the enter 
        }
    }
}
