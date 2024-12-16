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
    public partial class AcceptCerere : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public AcceptCerere()
        {
            InitializeComponent();
            displayAllStudentCourseData();
            readOnlyTextFields();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        public void displayAllStudentCourseData()
        {
            StudentCourseData sData = new StudentCourseData();
            List<StudentCourseData> listData = sData.AllStudentCourseData();
            dataGridView1.DataSource = listData;
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

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDashBoard_Click(object sender, EventArgs e)
        {
            DashBoardAdmin dashBoardAdmin = new DashBoardAdmin();
            dashBoardAdmin.Show();
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

        private void btnStudent_Click(object sender, EventArgs e)
        {
            Student std = new Student();
            std.Show();
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
            TeacherSalary salary = new TeacherSalary();
            salary.Show();
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

        public void readOnlyTextFields() {

            txtStudentId.ReadOnly = true;
            txtCourse.ReadOnly = true;
        }

        public void clearFields()
        {
            txtStudentId.Text = "";
            txtCourse.Text = "";
            txtDescription.Text = "";
            txtLicenta.Text = "";
            cmbDepartment.SelectedIndex = -1;
            txtPrice.Text = "";
            cmbStatus.SelectedIndex = -1;

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (txtStudentId.Text == "" || txtCourse.Text == "" || txtDescription.Text == "" || txtLicenta.Text == "" ||
                cmbStatus.SelectedIndex == -1 || cmbDepartment.SelectedIndex == -1 || txtPrice.Text == "")
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur acceptati cererea de inscriere a studentului: " + txtStudentId + "?",
                "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string updateData = "update student_course_handle set descriptie = @descriptie, licenta = @licenta," +
                                " department = @department, pret = @pret, status_curs = @status_curs " +
                                " where student_id = @student_id and curs = @curs;";

                        using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                        {
                            updatecmd.Parameters.AddWithValue("@descriptie", txtDescription.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@licenta", txtLicenta.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@department", cmbDepartment.SelectedItem.ToString());
                            updatecmd.Parameters.AddWithValue("@pret", txtPrice.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@status_curs", cmbStatus.SelectedItem.ToString());

                            updatecmd.Parameters.AddWithValue("@student_id", txtStudentId.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@curs", txtCourse.Text.Trim());

                            updatecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllStudentCourseData();
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
            if (txtStudentId.Text == "" || txtCourse.Text == "")
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur acceptati cererea de inscriere a studentului: " + txtStudentId + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string deleteData = "delete from student_course_handle where student_id = @student_id and curs = @curs";

                        using (SqlCommand deletecmd = new SqlCommand(deleteData, conn))
                        {
                            deletecmd.Parameters.AddWithValue("@student_id", txtStudentId.Text);
                            deletecmd.Parameters.AddWithValue("@curs", txtCourse.Text);

                            deletecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllStudentCourseData();
                            MessageBox.Show("S-a sters cererea cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }

                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string student_id = row.Cells[0].Value.ToString();
                string curs = row.Cells[1].Value.ToString();
                string descriere = row.Cells[2].Value.ToString();
                string licenta = row.Cells[3].Value.ToString();
                string department = row.Cells[4].Value.ToString();
                string pret = row.Cells[5].Value.ToString();
                string status_curs = row.Cells[6].Value.ToString();

                txtStudentId.Text = student_id;
                txtCourse.Text = curs;
                txtDescription.Text = descriere;
                txtLicenta.Text = licenta;
                cmbDepartment.Text = department;
                txtPrice.Text = pret;
                cmbStatus.Text = status_curs;
            }
        }
    }
}
