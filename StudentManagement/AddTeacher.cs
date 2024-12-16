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
    public partial class AddTeacher : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public AddTeacher()
        {
            InitializeComponent();

            displayAllTeacherData();
        }

        public bool checkConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                return true;
            }
            else { return false; }
        }

        public void displayAllTeacherData() {

            TeacherData tData = new TeacherData();
            List<TeacherData> listData = tData.AllTeacherData();
            dataGridView1.DataSource = listData;
        }

        public void clearFields() {
            txtTeacherId.Text = "";
            txtName.Text = "";
            cmbGender.SelectedIndex = -1;
            cmbYearExp.SelectedIndex = -1;
            txtExperience.Text = "";
            cmbDepartment.SelectedIndex = -1;
            txtSalary.Text = "";
            cmbSalaryStatus.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            pictureTeacher.Image = null;
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (txtTeacherId.Text == "" || txtName.Text == "" || cmbGender.SelectedIndex == -1 || cmbYearExp.SelectedIndex == -1 
                || txtExperience.Text == "" || cmbDepartment.SelectedIndex == -1 || txtSalary.Text == "" || cmbSalaryStatus.SelectedIndex == -1
                || cmbStatus.SelectedIndex == -1 || pictureTeacher == null)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (checkConnection())
            {
                try
                {
                    conn.Open();

                    string selectData = "select teacher_id from teacher where teacher_id = @teacher_id ";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {
                        cmd.Parameters.AddWithValue("@teacher_id", txtTeacherId.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();

                        adapter.Fill(table);

                        if (table.Rows.Count > 0)
                        {
                            MessageBox.Show("Teacher existent!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            string relativePath = Path.Combine("Images", pictureTeacher.Text.Trim() + ".jpg");
                            string path = Path.Combine(baseDirectory, relativePath);
                            string directoryPath = Path.GetDirectoryName(path);

                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            File.Copy(pictureTeacher.ImageLocation, path, true);

                            string insertData = "insert into teacher(teacher_id, full_name, gender, year_experience," +
                                " experience, department, salary, salary_status, image, date_insert, status)" +
                                "values (@teacher_id, @name, @gender, @year_experience, @experience, @department, @salary, @salary_status," +
                                "@image, @date_insert, @status)";

                            using (SqlCommand insertcmd = new SqlCommand(insertData, conn)) {

                                insertcmd.Parameters.AddWithValue("@teacher_id", txtTeacherId.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@year_experience", cmbYearExp.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@experience", txtExperience.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@department", cmbDepartment.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@salary", txtSalary.Text.Trim());
                                insertcmd.Parameters.AddWithValue("@salary_status", cmbSalaryStatus.SelectedItem.ToString());
                                insertcmd.Parameters.AddWithValue("@image", path);

                                DateTime today = DateTime.Today;
                                insertcmd.Parameters.AddWithValue("@date_insert", today);
                                insertcmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());

                                insertcmd.ExecuteNonQuery();
                                clearFields();
                                displayAllTeacherData();
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
            if (txtTeacherId.Text == "" || txtName.Text == "" || cmbGender.SelectedIndex == -1 || cmbYearExp.SelectedIndex == -1
                || txtExperience.Text == "" || cmbDepartment.SelectedIndex == -1 || txtSalary.Text == "" || cmbSalaryStatus.SelectedIndex == -1
                || cmbStatus.SelectedIndex == -1 || pictureTeacher == null)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur faceti update la teacher cu id: " + getID +"?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string updateData = "update teacher set full_name = @name, gender = @gender, year_experience = @year_experience," +
                                  " experience = @experience, department = @department, salary = @salary, salary_status = @salary_status, " +
                                  " image = @image, date_update = @date_update, status = @status " +
                                  "where teacher_id = @teacher_id";

                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string relativePath = Path.Combine("Images", txtTeacherId.Text.Trim() + ".jpg");
                        string path = Path.Combine(baseDirectory, relativePath);
                        string directoryPath = Path.GetDirectoryName(path);

                        // Check if the directory exists; if not, create it
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Check if a new image is provided, and update the image
                        if (!string.IsNullOrEmpty(pictureTeacher.ImageLocation))
                        {
                            // Copy the new image to the directory, overwrite if it exists
                            File.Copy(pictureTeacher.ImageLocation, path, true);
                        }

                        using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                                {
                                    updatecmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                                    updatecmd.Parameters.AddWithValue("@gender", cmbGender.SelectedItem.ToString());
                                    updatecmd.Parameters.AddWithValue("@year_experience", cmbYearExp.SelectedItem.ToString());
                                    updatecmd.Parameters.AddWithValue("@experience", txtExperience.Text.Trim());
                                    updatecmd.Parameters.AddWithValue("@department", cmbDepartment.SelectedItem.ToString());
                                    updatecmd.Parameters.AddWithValue("@salary", txtSalary.Text.Trim());
                                    updatecmd.Parameters.AddWithValue("@salary_status", cmbSalaryStatus.SelectedItem.ToString());
                                    updatecmd.Parameters.AddWithValue("@image", path); // Set image path

                                    DateTime today = DateTime.Today;
                                    updatecmd.Parameters.AddWithValue("@date_update", today);
                                    updatecmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                                    updatecmd.Parameters.AddWithValue("@teacher_id", txtTeacherId.Text);

                                    updatecmd.ExecuteNonQuery();
                                    clearFields();
                                    displayAllTeacherData();
                                    MessageBox.Show("S-a facut update cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                getID = row.Cells[0].Value?.ToString();  // Handle null safely
                string full_name = row.Cells[1].Value?.ToString() ?? "";
                string gender = row.Cells[2].Value?.ToString() ?? "";
                string year_experience = row.Cells[3].Value?.ToString() ?? "";
                string experience = row.Cells[4].Value?.ToString() ?? "";
                string department = row.Cells[5].Value?.ToString() ?? "";
                string salary = row.Cells[6].Value?.ToString() ?? "0";
                string salary_status = row.Cells[7].Value?.ToString() ?? "";

                string imagePath = row.Cells[8].Value?.ToString();
                string status = row.Cells[11].Value?.ToString() ?? "";

                // Log imagePath value to diagnose what is being passed
                MessageBox.Show($"Image Path: {imagePath}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Safely handle image loading with additional checks for valid path
                try
                {
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        // Check if the path is well-formed and the file exists
                        if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute) && File.Exists(imagePath))
                        {
                            pictureTeacher.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            MessageBox.Show("Invalid or missing file path for image: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            pictureTeacher.Image = null;  // Clear the image or set a default image
                        }
                    }
                    else
                    {
                        // Handle cases where the imagePath is null or empty
                        MessageBox.Show("No image path provided.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        pictureTeacher.Image = null;  // Clear the image or set a default image
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Assign values to the text boxes
                txtTeacherId.Text = getID;
                txtName.Text = full_name;
                cmbGender.Text = gender;
                cmbYearExp.Text = year_experience;
                txtExperience.Text = experience;
                cmbDepartment.Text = department;
                txtSalary.Text = salary;
                cmbSalaryStatus.Text = salary_status;
                cmbStatus.Text = status;

                try
                {
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))  // Ensure the image path is valid
                    {
                        pictureTeacher.Image = Image.FromFile(imagePath);  // Load the image
                    }
                    else
                    {
                        MessageBox.Show("Image not found or invalid path.", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        pictureTeacher.Image = null;  // Clear the image if not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    pictureTeacher.Image = null;  // Clear the image in case of an error
                }
            }
        }
        

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (txtTeacherId.Text == "" || txtName.Text == "" || cmbGender.SelectedIndex == -1 || cmbYearExp.SelectedIndex == -1
               || txtExperience.Text == "" || cmbDepartment.SelectedIndex == -1 || txtSalary.Text == "" || cmbSalaryStatus.SelectedIndex == -1
               || cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur doriti sa stergeti teacher cu id: " + getID + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                                string deleteData = "delete from teacher where teacher_id = @teacher_id";

                                using (SqlCommand deletecmd = new SqlCommand(deleteData, conn))
                                {
                                    deletecmd.Parameters.AddWithValue("@teacher_id", getID);

                                    deletecmd.ExecuteNonQuery();
                                    clearFields();
                                    displayAllTeacherData();
                                    MessageBox.Show("S-a sters teacher cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ExitIcon_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    pictureTeacher.ImageLocation = imagePath;
                }
            }
            catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
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
    }
}
