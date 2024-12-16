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
    public partial class ChangePassword : Form
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30";
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Back_to_information_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            info.Show();
            this.Hide();
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            string getUsername = Data.username;


            if (change_password.Text == " " || confirm_change_password.Text == " ") {
                MessageBox.Show("Completati toate casutele", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (change_password.Text != confirm_change_password.Text)
            {
                MessageBox.Show("Parolele trebuie sa fie identice", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (change_password.Text.Length < 3)
            {
                MessageBox.Show("Parola trebuie sa aiba minim 3 caractere", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {

                    connect.Open();

                    string selectData = "update users set password_user = @pass, date_change_user = @date where username = @usern";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect)) {

                        DateTime today = DateTime.Today;
                        cmd.Parameters.AddWithValue("@pass", change_password.Text.Trim());
                        cmd.Parameters.AddWithValue("@usern", getUsername);
                        cmd.Parameters.AddWithValue("@date", today);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("S-a schimbat parola cu succes!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Form1 loginForm = new Form1();
                        loginForm.Show();
                        this.Hide();
                    }
                }             
            }
        }
    }
}
