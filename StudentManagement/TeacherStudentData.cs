using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentManagement
{
    class TeacherStudentData
    {
        public string student_id { set; get; }
        public string student_cnp { set; get; }
        public string student_name{ set; get; }
        public string student_prename { set; get; }
        public string student_an { set; get; }
        public string student_curs { set; get; }
        public string date_insert { set; get; }
        public string payment { set; get; }
        public string status_payment { set; get; }

        public List<TeacherStudentData> AllTeacherStudentData()
        {

            List<TeacherStudentData> listData = new List<TeacherStudentData>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from teacher_student";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        TeacherStudentData tData = new TeacherStudentData();
                        tData.student_id = reader["student_id"].ToString();
                        tData.student_cnp = reader["student_cnp"].ToString();
                        tData.student_name = reader["student_name"].ToString();
                        tData.student_prename = reader["student_prename"].ToString();
                        tData.student_an = reader["student_an"].ToString();
                        tData.student_curs = reader["student_curs"].ToString();
                        tData.date_insert = reader["date_insert"].ToString();
                        tData.payment = reader["payment"].ToString();
                        tData.status_payment = reader["status_payment"].ToString();

                        listData.Add(tData);
                    }
                }
            }
            return listData;
        }
    }
}
