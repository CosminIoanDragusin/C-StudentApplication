using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace StudentManagement
{
    class TeacherData
    {
        public string teacher_id { set; get; }
        public string full_name { set; get; }
        public string gender { set; get; }
        public string year_experience { set; get; }
        public string experience { set; get; }
        public string department { set; get; }
        public int? salary { set; get; }
        public string salary_status { set; get; }
        public string image { set; get; }
        public string date_insert { set; get; }
        public string date_update { set; get; }
        public string status { set; get; }

        public List<TeacherData> AllTeacherData() {

            List<TeacherData> listData = new List<TeacherData>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from teacher";

                using (SqlCommand cmd = new SqlCommand(selectData, conn)) {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read()) {
                        TeacherData tData = new TeacherData();
                        tData.teacher_id = reader["teacher_id"].ToString();
                        tData.full_name = reader["full_name"].ToString();
                        tData.gender = reader["gender"].ToString();
                        tData.year_experience = reader["year_experience"].ToString();
                        tData.experience = reader["experience"].ToString();
                        tData.department = reader["department"].ToString();
                        tData.salary = reader["salary"] != DBNull.Value ? (int?)reader["salary"] : null;
                        tData.salary_status = reader["salary_status"].ToString();
                        tData.image = reader["image"].ToString();
                        tData.date_insert = reader["date_insert"].ToString();
                        tData.date_update = reader["date_update"].ToString();
                        tData.status = reader["status"].ToString();

                        listData.Add(tData);
                    }
                } 
            }
            return listData;
        }
    }
}
