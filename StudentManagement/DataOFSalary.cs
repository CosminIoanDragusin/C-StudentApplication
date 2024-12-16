using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentManagement
{
    class DataOFSalary
    {
        public string teacher_id { set; get; }
        public string nume { set; get; }
        public string total_zile { set; get; }
        public string salariu_platit { set; get; }
        public string data_plata { set; get; }
        public string status_salariu { set; get; }

        public List<DataOFSalary> AllSalaryData()
        {

            List<DataOFSalary> listData = new List<DataOFSalary>();
            using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                conn.Open();
                string selectData = "select * from salariu";

                using (SqlCommand cmd = new SqlCommand(selectData, conn))
                {

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        DataOFSalary tData = new DataOFSalary();
                        tData.teacher_id = reader["teacher_id"].ToString();
                        tData.nume = reader["nume"].ToString();
                        tData.total_zile = reader["total_zile"].ToString();
                        tData.salariu_platit = reader["salariu_platit"].ToString();
                        tData.data_plata = reader["data_plata"].ToString();
                        tData.status_salariu = reader["status_salariu"].ToString();

                        listData.Add(tData);
                    }
                }
            }
            return listData;
        }
    }
}
