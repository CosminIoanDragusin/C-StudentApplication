using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentManagement
{
    class SubjectData
    {
        public int subject_id { set; get; }
        public string subject_code { set; get; }
        public string subject { set; get; }
        public string curs { set; get; }
        public string date_insert { set; get; }
        public string date_update { set; get; }
        public string status_subject { set; get; }

        public List<SubjectData> AllSubjectData()
        {

            List<SubjectData> listData = new List<SubjectData>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from subject";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SubjectData tData = new SubjectData();
                        tData.subject_id = (int)reader["subject_id"];
                        tData.subject_code = reader["subject_code"].ToString();
                        tData.subject = reader["subject"].ToString();
                        tData.curs = reader["curs"].ToString();
                        tData.date_insert = reader["date_insert"].ToString();
                        tData.date_update = reader["date_update"].ToString();
                        tData.status_subject = reader["status_subject"].ToString();

                        listData.Add(tData);
                    }
                }
            }
            return listData;
        }
    }
}
