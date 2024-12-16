using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace StudentManagement
{
    class CourseData
    {
        public int course_id { set; get; }
        public string curs { set; get; }
        public string descriere { set; get; }
        public string licenta { set; get; }
        public string department { set; get; }
        public string pret { set; get; }
        public string statusul { set; get; }

        public List<CourseData> AllCourseData()
        {

            List<CourseData> listData = new List<CourseData>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from course";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CourseData tData = new CourseData();
                        tData.course_id = (int)reader["id"];
                        tData.curs = reader["curs"].ToString();
                        tData.descriere = reader["descriere"].ToString();
                        tData.licenta = reader["licenta"].ToString();
                        tData.department = reader["department"].ToString();
                        tData.pret = reader["pret"].ToString();
                        tData.statusul = reader["statusul"].ToString();

                        listData.Add(tData);
                    }
                }
            }
            return listData;
        }
    }
}
