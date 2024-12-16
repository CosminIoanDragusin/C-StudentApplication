using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace StudentManagement
    {
        class UserData
        {
            public int id { set; get; }
            public string email { set; get; }
            public string username { set; get; }
            public string password_user { set; get; }
            public string question { set; get; }
            public string answer { set; get; }
            public string role_user { set; get; }
            public int? student_id { set; get; }
            public int? teacher_id { set; get; }
            public string date_register { set; get; }
            public string date_change_user { set; get; }


            public List<UserData> AllUserData()
            {

                List<UserData> listData = new List<UserData>();
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\user\Documents\forgotpassword.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    conn.Open();
                    string selectData = "select * from users";

                    using (SqlCommand cmd = new SqlCommand(selectData, conn))
                    {

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UserData tData = new UserData();
                            tData.id = (int)reader["id"];
                            tData.email = reader["email"].ToString();
                            tData.username = reader["username"].ToString();
                            tData.password_user = reader["password_user"].ToString();
                            tData.question = reader["question"].ToString();
                            tData.answer = reader["answer"].ToString();
                            tData.role_user = reader["role_user"].ToString();
                            tData.student_id = reader["student_id"] != DBNull.Value ? (int?)reader["student_id"] : null;
                            tData.teacher_id = reader["teacher_id"] != DBNull.Value ? (int?)reader["teacher_id"] : null;
                            tData.date_register = reader["date_register"].ToString();
                            tData.date_change_user = reader["date_change_user"].ToString();

                        listData.Add(tData);
                        }
                    }
                }
                return listData;
            }
        }
    }
