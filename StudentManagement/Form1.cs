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
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (login_username.Text == "" || login_password.Text == "")
            {
                MessageBox.Show("Completati toate casutele", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else if(checkConnection())
            {
                try {
                    conn.Open();

                    string selectData = "select username, password_user, role_user from users where username = @usern AND password_user = @pass";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("usern", login_username.Text.Trim());
                        cmd.Parameters.AddWithValue("pass", login_password.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            string roleUser = table.Rows[0]["role_user"].ToString();
                            MessageBox.Show("Logare reusita!", "Informare", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (roleUser == "Admin")
                            {
                                DashBoardAdmin dashForm = new DashBoardAdmin();
                                dashForm.Show();
                                ListData.admin_username = login_username.Text;
                            }
                            else if (roleUser == "Student")
                            {
                                StudentInformation dashForm = new StudentInformation();
                                dashForm.Show();
                                ListData.student_username = login_username.Text;
                            }
                            else if (roleUser == "Teacher")
                            {
                                AddStudentToCourse dashForm = new AddStudentToCourse();
                                dashForm.Show();
                                ListData.teacher_username = login_username.Text;
                            }
                            this.Hide();
                        }
                        else
                        {
                                MessageBox.Show("Parola sau utilizator gresit!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);        
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }

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

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register regForm = new Register();
            regForm.Show();

            this.Hide();
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowPassLogin_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = ShowPassLogin.Checked ? '\0' : '*';
            
        }

        private void to_forgot_password_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            info.Show();

            this.Hide();
        }
    }
}
