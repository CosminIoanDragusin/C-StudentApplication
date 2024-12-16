using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentManagement
{
    class StudentCourseData
    {
        public string student_id { set; get; }
        public string curs { set; get; }
        public string descriptie { set; get; }
        public string licenta { set; get; }
        public string department { set; get; }
        public string pret { set; get; }
        public string status_curs { set; get; }

        public List<StudentCourseData> AllStudentCourseData()
        {

            List<StudentCourseData> listData = new List<StudentCourseData>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from student_course_handle";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        StudentCourseData tData = new StudentCourseData();
                        tData.student_id = reader["student_id"].ToString();
                        tData.curs = reader["curs"].ToString();
                        tData.descriptie = reader["descriptie"].ToString();
                        tData.licenta = reader["licenta"].ToString();
                        tData.department = reader["department"].ToString();
                        tData.pret = reader["pret"].ToString();
                        tData.status_curs = reader["status_curs"].ToString();

                        listData.Add(tData);
                    }
                }
            }
            return listData;
        }

    }
   
}
