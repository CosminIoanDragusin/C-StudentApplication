using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace StudentManagement
{
    public partial class Register : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");

        public Register()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

       

        private void button_register_Click(object sender, EventArgs e)
        {
            if (signup_email.Text == " " || signup_username.Text == " " || signup_password.Text == " " || signup_password_2.Text == " "
                || comboBox_Question.SelectedIndex == -1 || signup_answer.Text == " " || cmb_role.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (signup_password.Text != signup_password_2.Text) {
                MessageBox.Show("Parolele trebuie sa fie identice", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (signup_password.Text.Length < 3)
            {
                MessageBox.Show("Parola trebuie sa aiba minim 3 caractere", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string selectData = "select username from users where username = @usern";

                        using (SqlCommand checkUsers = new SqlCommand(selectData, conn))
                        {
                            checkUsers.Parameters.AddWithValue("@usern", signup_username.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(checkUsers);
                            DataTable table = new DataTable();

                            adapter.Fill(table);

                            if (table.Rows.Count > 0)
                            {
                                MessageBox.Show($"{signup_username.Text} este deja existent, folositi alt nume", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                // Generate ID based on the role (student or teacher)
                               // string generatedID = "";
                                if (cmb_role.SelectedItem.ToString() == "Student")
                                {
                                   StudentIDGenerator(); // Call the StudentIDGenerator method

                                    string RegisterStudent = "insert into users(email, username, password_user, question, answer, role_user, student_id, date_register) " +
                                    " values(@email, @usern, @pass, @question, @answer, @role, @student_id, @date_reg) ";
                                    using (SqlCommand cmd = new SqlCommand(RegisterStudent, conn))
                                    {

                                        cmd.Parameters.AddWithValue("@email", signup_email.Text.Trim());
                                        cmd.Parameters.AddWithValue("@usern", signup_username.Text.Trim());
                                        cmd.Parameters.AddWithValue("@pass", signup_password.Text.Trim());
                                        cmd.Parameters.AddWithValue("@question", comboBox_Question.SelectedItem.ToString());
                                        cmd.Parameters.AddWithValue("@answer", signup_answer.Text.Trim());
                                        cmd.Parameters.AddWithValue("@role", cmb_role.SelectedItem.ToString());
                                        cmd.Parameters.AddWithValue("@student_id", idGenStudent);

                                        DateTime today = DateTime.Today;
                                        cmd.Parameters.AddWithValue("@date_reg", today);

                                        cmd.ExecuteNonQuery();

                                        string insertStudent = "insert into student(student_id, date_insert) " +
                               "values (@student_id, @date_insert)";

                                        using (SqlCommand studentCmd = new SqlCommand(insertStudent, conn))
                                        {
                                            studentCmd.Parameters.AddWithValue("@student_id", idGenStudent);
                                            //DateTime today2 = DateTime.Today;
                                            studentCmd.Parameters.AddWithValue("@date_insert", today);

                                            studentCmd.ExecuteNonQuery();
                                        }

                                        MessageBox.Show("S-a inregistrat contul", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        Form1 loginForm1 = new Form1();
                                        loginForm1.Show();

                                        this.Hide();
                                    }
                                }
                                else if (cmb_role.SelectedItem.ToString() == "Teacher")
                                {
                                    TeacherIDGenerator(); // Call the TeacherIDGenerator method

                                    string RegisterTeacher = "insert into users(email, username, password_user, question, answer, role_user, teacher_id, date_register) " +
                                    " values(@email, @usern, @pass, @question, @answer, @role, @teacher_id, @date_reg) ";
                                    using (SqlCommand cmd = new SqlCommand(RegisterTeacher, conn))
                                    {

                                        cmd.Parameters.AddWithValue("@email", signup_email.Text.Trim());
                                        cmd.Parameters.AddWithValue("@usern", signup_username.Text.Trim());
                                        cmd.Parameters.AddWithValue("@pass", signup_password.Text.Trim());
                                        cmd.Parameters.AddWithValue("@question", comboBox_Question.SelectedItem.ToString());
                                        cmd.Parameters.AddWithValue("@answer", signup_answer.Text.Trim());
                                        cmd.Parameters.AddWithValue("@role", cmb_role.SelectedItem.ToString());
                                        cmd.Parameters.AddWithValue("@teacher_id", idGenTeacher);

                                        DateTime today = DateTime.Today;
                                        cmd.Parameters.AddWithValue("@date_reg", today);

                                        cmd.ExecuteNonQuery();

                                        string insertTeacher = "insert into teacher(teacher_id, date_insert) " +
                               "values (@teacher_id, @date_insert)";

                                        using (SqlCommand teacherCmd = new SqlCommand(insertTeacher, conn))
                                        {
                                            teacherCmd.Parameters.AddWithValue("@teacher_id", idGenTeacher);
                                            teacherCmd.Parameters.AddWithValue("@date_insert", today);

                                            teacherCmd.ExecuteNonQuery();
                                        }

                                        MessageBox.Show("S-a inregistrat contul", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        Form1 loginForm1 = new Form1();
                                        loginForm1.Show();

                                        this.Hide();
                                    }
                                }
                                else {
                                    string RegisterAccount = "insert into users(email, username, password_user, question, answer, role_user, date_register) " +
                                         " values(@email, @usern, @pass, @question, @answer, @role, @date_reg) ";
                                    using (SqlCommand cmd = new SqlCommand(RegisterAccount, conn))
                                    {

                                        cmd.Parameters.AddWithValue("@email", signup_email.Text.Trim());
                                        cmd.Parameters.AddWithValue("@usern", signup_username.Text.Trim());
                                        cmd.Parameters.AddWithValue("@pass", signup_password.Text.Trim());
                                        cmd.Parameters.AddWithValue("@question", comboBox_Question.SelectedItem.ToString());
                                        cmd.Parameters.AddWithValue("@answer", signup_answer.Text.Trim());
                                        cmd.Parameters.AddWithValue("@role", cmb_role.SelectedItem.ToString());

                                        DateTime today = DateTime.Today;
                                        cmd.Parameters.AddWithValue("@date_reg", today);

                                        cmd.ExecuteNonQuery();

                                        MessageBox.Show("S-a inregistrat contul", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        Form1 loginForm1 = new Form1();
                                        loginForm1.Show();

                                        this.Hide();
                                    }
                                }
                                
                            }
                        }
                    }

                    catch (Exception ex) {
                        MessageBox.Show("Conection failed" + ex , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally 
                    {
                        conn.Close();
                    }
                }           
            }
        }

        private int idGenStudent = 0;
        // Student ID generator method
        public void StudentIDGenerator()
        {
            using (SqlConnection conn
                 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30")) 
            {
                conn.Open();
                string selectData = "SELECT MAX(student_id) FROM student";

                using (SqlCommand cmd = new SqlCommand(selectData, conn)) {

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        int temp = Convert.ToInt32(result);

                        idGenStudent = temp + 1; // Increment the ID
                    }
                    else
                    {
                        idGenStudent = 1; // Start from 1 if no records exist
                    }
                }
                conn.Close();
            }

        }

        private int idGenTeacher = 0;
        // TeacherID generator method
        public void TeacherIDGenerator()
        {
            using (SqlConnection conn
                 = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "SELECT MAX(teacher_id) FROM teacher";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    object resultTeacher = cmd.ExecuteScalar();

                    if (resultTeacher != null && resultTeacher != DBNull.Value)
                    {
                        int tempTeacher = Convert.ToInt32(resultTeacher);

                        idGenTeacher = tempTeacher + 1; // Increment the ID
                    }
                    else
                    {
                        idGenTeacher = 1; // Start from 1 if no records exist
                    }
                }
                conn.Close();
            }

        }

        public bool checkConnection() {
            if (conn.State == ConnectionState.Closed)
            {
                return true;
            }
            else { return false; }
        }

        private void button_to_login_Click(object sender, EventArgs e)
        {
            Form1 loginForm1 = new Form1();
            loginForm1.Show();

            this.Hide();
        }

        private void ExitIcon_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signup_password_checkbox_CheckedChanged(object sender, EventArgs e)
        {
            signup_password.PasswordChar = signup_password_checkbox.Checked ? '\0' : '*';
            signup_password_2.PasswordChar = signup_password_checkbox.Checked ? '\0' : '*';
        }
    }
}
