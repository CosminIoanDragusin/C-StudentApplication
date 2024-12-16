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
using System.IO;

namespace StudentManagement
{
    public partial class PaymentStudent : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30");
        public PaymentStudent()
        {
            InitializeComponent();
            displayAllStudentData();
        }

        public bool checkConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                return true;
            }
            else { return false; }
        }

        public void displayAllStudentData()
        {
            StudentData sData = new StudentData();
            List<StudentData> listData = sData.AllStudentData();
            dataGridView1.DataSource = listData;
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
            txtStudentId.Text = "";
            txtCnp.Text = "";
            txtName.Text = "";
            txtPrenume.Text = "";
            cmbAn.SelectedIndex = -1;
            txtPayment.Text = "";
            cmbPayment_Status.SelectedIndex = -1;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (txtStudentId.Text == "" || txtCnp.Text == "" || txtName.Text == "" || txtPrenume.Text == "" || cmbAn.SelectedIndex == -1
                ||  txtPayment.Text == "" || cmbPayment_Status.SelectedIndex == -1)
            {
                MessageBox.Show("Completati toate casutele!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (MessageBox.Show("Sigur faceti update la student cu id: " + txtStudentId + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (checkConnection())
                {
                    try
                    {
                        conn.Open();

                        string updateData = "update student set cnp = @cnp, nume = @name, prenume = @prenume," +
                                " an = @an, payment = @payment, payment_status = @payment_status, image = @image , date_update = @date_update" +
                                " where student_id = @student_id";

                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        string relativePath = Path.Combine("Images", txtStudentId.Text.Trim() + ".jpg");
                        string path = Path.Combine(baseDirectory, relativePath);
                        string directoryPath = Path.GetDirectoryName(path);

                        // Check if the directory exists; if not, create it
                        if (!Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Check if a new image is provided, and update the image
                        if (!string.IsNullOrEmpty(studentPicture.ImageLocation))
                        {
                            // Copy the new image to the directory, overwrite if it exists
                            File.Copy(studentPicture.ImageLocation, path, true);
                        }

                        using (SqlCommand updatecmd = new SqlCommand(updateData, conn))
                        {
                            updatecmd.Parameters.AddWithValue("@cnp", txtCnp.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@prenume", txtPrenume.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@an", cmbAn.SelectedItem.ToString());
                            updatecmd.Parameters.AddWithValue("@payment", txtPayment.Text.Trim());
                            updatecmd.Parameters.AddWithValue("@payment_status", cmbPayment_Status.SelectedItem.ToString());
                            updatecmd.Parameters.AddWithValue("@image", path);

                            DateTime today = DateTime.Today;
                            updatecmd.Parameters.AddWithValue("@date_update", today);
                            updatecmd.Parameters.AddWithValue("@student_id", txtStudentId.Text.Trim());

                            updatecmd.ExecuteNonQuery();
                            clearFields();
                            displayAllStudentData();
                            MessageBox.Show("S-a facut update cu succes!!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    finally { conn.Close(); }
                }
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string id = row.Cells[0].Value.ToString();
                string cnp = row.Cells[1].Value.ToString();
                string name = row.Cells[2].Value.ToString();
                string prenume = row.Cells[3].Value.ToString();
                string an = row.Cells[4].Value.ToString();
                string payment = row.Cells[8].Value.ToString();
                string payment_status = row.Cells[9].Value.ToString();
                string imagePath = row.Cells[10].Value.ToString();

                // Safely handle image loading with additional checks for valid path
                try
                {
                    if (!string.IsNullOrEmpty(imagePath))
                    {
                        // Check if the path is well-formed and the file exists
                        if (Uri.IsWellFormedUriString(imagePath, UriKind.Absolute) && File.Exists(imagePath))
                        {
                            studentPicture.Image = Image.FromFile(imagePath);
                        }
                        else
                        {
                            MessageBox.Show("Invalid or missing file path for image: " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            studentPicture.Image = null;  // Clear the image or set a default image
                        }
                    }
                    else
                    {
                        // Handle cases where the imagePath is null or empty
                        MessageBox.Show("No image path provided.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        studentPicture.Image = null;  // Clear the image or set a default image
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                txtStudentId.Text = id;
                txtCnp.Text = cnp;
                txtName.Text = name;
                txtPrenume.Text = prenume;
                cmbAn.Text = an;
                txtPayment.Text = payment;
                cmbPayment_Status.Text = payment_status;

                try
                {
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))  // Ensure the image path is valid
                    {
                        studentPicture.Image = Image.FromFile(imagePath);  // Load the image
                    }
                    else
                    {
                        MessageBox.Show("Image not found or invalid path.", "Image Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        studentPicture.Image = null;  // Clear the image if not found
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    studentPicture.Image = null;  // Clear the image in case of an error
             }
        }
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
                    studentPicture.ImageLocation = imagePath;
                }
            }
            catch (Exception ex) { MessageBox.Show("Conection failed" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
