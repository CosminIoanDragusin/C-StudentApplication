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
    public partial class DashBoardAdmin : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public DashBoardAdmin()
        {
            InitializeComponent();
            displayAllUserData();
            DashboardDisplayNumberOfStudents();
            DashboardDisplayNumberOfCourse();
            DashboardDisplayNumberOfTeachers();
            DashboardDisplayNumberOfUser();
            DashboardDisplayBudgetUniversity();
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Vreti sa inchideti aplicatia?", "Confirmation Message", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
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

        private void btnStudent_Click(object sender, EventArgs e)
        {
           Student student = new Student();
            student.Show();
            this.Hide();
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            DashBoardAdmin dashAdmin = new DashBoardAdmin();
            dashAdmin.Show();
            this.Hide();
        }

        private void btnCourse_Click(object sender, EventArgs e)
        {
            AcceptCerere acceptCerere = new AcceptCerere();
            acceptCerere.Show();
            this.Hide();
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
            AddTeacher addTeacher = new AddTeacher();
            addTeacher.Show();
            this.Hide();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentStudent payStd = new PaymentStudent();
            payStd.Show();
            this.Hide();
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            TeacherSalary teacherSalary = new TeacherSalary();
            teacherSalary.Show();
            this.Hide();
        }

        public void DashboardDisplayNumberOfStudents()
        {
            string sql = "SELECT COUNT(student_id) FROM student ";
            int tempNS = 0;

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tempNS = reader.GetInt32(0);
                    }
                }
                labelStudentNumar.Text = tempNS.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DashboardDisplayNumberOfCourse()
        {
            string sql = "SELECT COUNT(id) FROM course ";
            int tempNC = 0;

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tempNC = reader.GetInt32(0);
                    }
                }
                labelNumberCourse.Text = tempNC.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DashboardDisplayNumberOfTeachers()
        {
            string sql = "SELECT COUNT(teacher_id) FROM teacher ";
            int tempNT = 0;

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tempNT = reader.GetInt32(0);
                    }
                }
                labelNumberTeacher.Text = tempNT.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DashboardDisplayNumberOfUser()
        {
            string sql = "SELECT COUNT(id) FROM users ";
            int tempNU = 0;

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tempNU = reader.GetInt32(0);
                    }
                }
                labelNumberUser.Text = tempNU.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        public void DashboardDisplayBudgetUniversity()
        {
            string sql = "SELECT SUM(CAST(payment AS INT)) FROM student WHERE payment_status = 'Approved' ";
            int tempTB = 0;

            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        tempTB = reader.GetInt32(0);
                    }
                }
                labelTotalBudget.Text = tempTB.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
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

        public void displayAllUserData()
        {

            UserData tData = new UserData();
            List<UserData> listData = tData.AllUserData();
            dataGridView1.DataSource = listData;
        }

        public void clearFields()
        {
            txtEmail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            cmbQuestion.SelectedIndex = -1;
            txtAnswer.Text = "";
            cmbRole_User.SelectedIndex = -1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                string email = row.Cells[1].Value.ToString();
                string username = row.Cells[2].Value.ToString();
                string password = row.Cells[3].Value.ToString();
                string question = row.Cells[4].Value.ToString();
                string answer = row.Cells[5].Value.ToString();
                string role = row.Cells[6].Value.ToString();

                txtEmail.Text = email;
                txtUsername.Text = username;
                txtPassword.Text = password;
                cmbQuestion.Text = question;
                txtAnswer.Text = answer;
                cmbRole_User.Text = role;
                
            }

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtUsername.Text == "" || txtPassword.Text == "" || cmbQuestion.SelectedIndex == -1
                || txtAnswer.Text == "" || cmbRole_User.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur faceti update la user cu username: " + txtUsername.Text + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string updateData = "update users set email = @email, password_user = @password_user, question = @question, " +
                                " answer = @answer, role_user = @role_user, date_change_user = @date_change_user " +
                                "where username = @username";

                        using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                        {
                            updatecmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@password_user", txtPassword.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@question", cmbQuestion.SelectedItem.ToString());
                            updatecmd.Parameters.AddWithValue("@answer", txtAnswer.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@role_user", cmbRole_User.SelectedItem.ToString());

                            DateTime today = DateTime.Today;
                            updatecmd.Parameters.AddWithValue("@date_change_user", today);
                            updatecmd.Parameters.AddWithValue("@username", txtUsername.Text);

                            updatecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllUserData();
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
            if (txtEmail.Text == "" || txtUsername.Text == "" || txtPassword.Text == "" || cmbQuestion.SelectedIndex == -1
                || txtAnswer.Text == "" || cmbRole_User.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur doriti sa stergeti utilizatorul cu username: " + txtUsername.Text + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string deleteData = "delete from users where username = @username";

                        using (SqlCommand deletecmd = new SqlCommand(deleteData, conn))
                        {
                            deletecmd.Parameters.AddWithValue("@username", txtUsername.Text);

                            deletecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllUserData();
                            MessageBox.Show("S-a sters user cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }

                }
            }
        }
    }
}
