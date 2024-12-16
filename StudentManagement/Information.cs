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
    public partial class Information : Form
    {
        string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30";

        public Information()
        {
            InitializeComponent();
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void back_to_login_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();

            this.Hide();
        }

        private void confirm_forgot_Click(object sender, EventArgs e)
        {
            if (information_username.Text == "" || comboBox_Question.SelectedIndex == -1 || forgot_answer.Text == "")
            {
                MessageBox.Show("Completati toate casutele", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {

                using (SqlConnection connect = new SqlConnection(conn)) {

                    connect.Open();
                    string selectData = "select username, question, answer from users where username = @user AND question = @question AND answer = @answer";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect)) {

                        cmd.Parameters.AddWithValue("@user", information_username.Text.Trim());
                        cmd.Parameters.AddWithValue("@question", comboBox_Question.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@answer", forgot_answer.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count != 0)
                        {
                            Data.username= information_username.Text.Trim();

                            ChangePassword changePass = new ChangePassword();
                            changePass.Show();
                            this.Hide();
                        }
                        else {
                          MessageBox.Show("Informati incorecte, corectati!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
    }
}
