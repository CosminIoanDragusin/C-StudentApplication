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
    public partial class TeacherSalary : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        
        public TeacherSalary()
        {
            InitializeComponent();
            displayAllSalaryData();
            displayTeacherID();
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

        public void clearFields()
        {
            cmbTeacherID.SelectedIndex = -1;
            cmbName.SelectedIndex = -1;
            txtSalary.Text = "";
            txtNumarZile.Text = "";
            cmbStatus.SelectedIndex = -1;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFields();
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
            TeacherSalary teacherSalary = new TeacherSalary();
            teacherSalary.Show();
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

        public void displayAllSalaryData()
        {

            DataOFSalary tData = new DataOFSalary();
            List<DataOFSalary> listData = tData.AllSalaryData();
            dataGridView1.DataSource = listData;
        }

        public void displayTeacherID()
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select * from teacher";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cmbTeacherID.Items.Add(reader["teacher_id"].ToString());
                                
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }
            }
        }

        public void displayTeacherNume()
        {
            if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select * from teacher where teacher_id = @teacher_id";
                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@teacher_id", cmbTeacherID.SelectedItem.ToString());
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                               
                                cmbName.Items.Add(reader["full_name"].ToString());
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
            if (cmbTeacherID.SelectedIndex == -1 || cmbName.SelectedIndex == -1 || txtSalary.Text == "" || txtNumarZile.Text == ""
               || cmbStatus.SelectedIndex == -1 )
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur faceti update la teacher cu id: " + cmbTeacherID.SelectedItem.ToString() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection()) { 
               try
                {
                    conn.Open();

                            string updateData = "update salariu set total_zile = @total_zile, salariu_platit = @salariu_platit, " +
                                  " data_plata = @data_plata, status_salariu = @status_salariu " +
                                  "where teacher_id = @teacher_id and nume = @nume ";

                            using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                            {
                                updatecmd.Parameters.AddWithValue("@total_zile", txtNumarZile.Text.Trim());
                                updatecmd.Parameters.AddWithValue("@salariu_platit", txtSalary.Text.Trim());

                                DateTime today = DateTime.Today;
                                updatecmd.Parameters.AddWithValue("@data_plata", today);
                                updatecmd.Parameters.AddWithValue("@status_salariu", cmbStatus.SelectedItem.ToString());

                                updatecmd.Parameters.AddWithValue("@teacher_id", cmbTeacherID.SelectedItem.ToString());
                                updatecmd.Parameters.AddWithValue("@nume", cmbName.SelectedItem.ToString());

                                updatecmd.ExecuteNonQuery();
                                clearFields();
                                displayAllSalaryData();
                                MessageBox.Show("S-a facut update cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string teacher_id = row.Cells[0].Value?.ToString();  // Handle null safely
                string nume = row.Cells[1].Value?.ToString() ?? "";
                string total_zile = row.Cells[2].Value?.ToString() ?? "";
                string salariu_platit = row.Cells[3].Value?.ToString() ?? "";
                string status_salariu = row.Cells[5].Value?.ToString() ?? "";

                cmbTeacherID.Text = teacher_id;
                cmbName.Text = nume;             
                txtSalary.Text = salariu_platit;
                txtNumarZile.Text = total_zile;
                cmbStatus.Text = status_salariu;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbTeacherID.SelectedIndex == -1 || cmbName.SelectedIndex == -1 || txtSalary.Text == "" || txtNumarZile.Text == ""
               || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select teacher_id, nume from salariu where teacher_id = @teacher_id and nume = @nume ";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@teacher_id", cmbTeacherID.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@nume", cmbName.SelectedItem.ToString());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Salariu acordat deja!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {

                            string insertData = "insert into salariu(teacher_id, nume, total_zile, salariu_platit," +
                                " data_plata, status_salariu)" +
                                "values (@teacher_id, @nume, @total_zile, @salariu_platit, " +
                                " @data_plata, @status_salariu)";

                            using (SqlCommand insertcmd = new SqlCommand(insertData, conn))
                            {
                                insertcmd.Parameters.AddWithValue("@teacher_id", cmbTeacherID.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@nume", cmbName.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@total_zile", txtNumarZile.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@salariu_platit", txtSalary.Text.Trim()); 

                                DateTime today = DateTime.Today;
                                insertcmd.Parameters.AddWithValue("@data_plata", today);
                                insertcmd.Parameters.AddWithValue("@status_salariu", cmbStatus.SelectedItem.ToString());

                                insertcmd.ExecuteNonQuery();
                                clearFields();
                                displayAllSalaryData();
                                MessageBox.Show("S-a adaugat cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                finally { conn.Close(); }

            }
        }

        private void cmbTeacherID_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbName.Items.Clear();
            displayTeacherNume();
        }
    }
}
