using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StudentManagement
{
    class StudentData
    {
        public string student_id { set; get; }
        public string cnp { set; get; }
        public string nume { set; get; }
        public string prenume { set; get; }
        public string an { set; get; }
        public string data_nasteri { set; get; }
        public string curs { set; get; }
        public string status_student { set; get; }
        public string payment { set; get; }
        public string payment_status { set; get; }
        public string image { set; get; }
        public string date_insert { set; get; }
        public string date_update { set; get; }

        public List<StudentData> AllStudentData()
        {

            List<StudentData> listData = new List<StudentData>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from student";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        StudentData sData = new StudentData();
                        sData.student_id = reader["student_id"].ToString();
                        sData.cnp = reader["cnp"].ToString();
                        sData.nume = reader["nume"].ToString();
                        sData.prenume = reader["prenume"].ToString();
                        sData.an = reader["an"].ToString();
                        sData.data_nasteri = reader["data_nasteri"].ToString();
                        sData.curs = reader["curs"].ToString();
                        sData.status_student = reader["status_student"].ToString();
                        sData.payment = reader["payment"].ToString();
                        sData.payment_status = reader["payment_status"].ToString();
                        sData.image = reader["image"].ToString();
                        sData.date_insert = reader["date_insert"].ToString();
                        sData.date_update = reader["date_update"].ToString();

                        listData.Add(sData);
                    }
                }
            }
            return listData;
        }
    }
}
