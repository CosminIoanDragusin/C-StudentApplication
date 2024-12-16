using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentManagement
{
    class GradesData
    {
        public string cnp { set; get; }
        public string an { set; get; }
        public string curs { set; get; }
        public float semestru_I { set; get; }
        public float semestru_II { set; get; }
        public float semestru_final { set; get; }

        public List<GradesData> AllGradesData()
        {

            List<GradesData> listData = new List<GradesData>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from nota";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        GradesData tData = new GradesData();
                        tData.cnp = reader["cnp"].ToString();
                        tData.an = reader["an"].ToString();
                        tData.curs = reader["curs"].ToString();
                        tData.semestru_I = Convert.ToSingle(reader["semestru_I"]);
                        tData.semestru_II = Convert.ToSingle(reader["semestru_II"]);
                        tData.semestru_final = Convert.ToSingle(reader["semestru_final"]);

                        listData.Add(tData);
                    }
                }
            }
            return listData;
        }
    }
}
