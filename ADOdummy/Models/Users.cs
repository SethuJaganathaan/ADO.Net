using ADOdummy.DTO;
using System.Data;
using System.Data.SqlClient;

namespace ADOdummy.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime DOB { get; set; }
        public int DepartmentId { get; set; }

        // public virtual Department UserDepartment { get; set; }

        public static SqlConnection connection;
        public static SqlCommand cmd;
        public Users(int Uid, string Uname, DateTime dob, int UDid)
        {
            UserId = Uid;
            UserName = Uname;
            DOB = dob;
            DepartmentId = UDid;
        }

        public static List<UserDTO> GetAllUser()
        {
            Department.Connection();
            cmd = new SqlCommand("select * from Users", Department.connection);
            List<UserDTO> users = new List<UserDTO>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new UserDTO
                {
                    UserId = (int)reader[0],
                    UserName = reader[1].ToString(),
                    DOB = Convert.ToDateTime(reader[2]),
                    DepartmentId = (int)reader[3],
                });
            }
            reader.Close();
            return users;
        }
        public static List<UserDTO> GETandDELETE(int Id, string method)
        {
            Department.Connection();
            SqlCommand cmd = new SqlCommand("GETandDELETEuser", Department.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", Id);
            cmd.Parameters.AddWithValue("@Method", method);

            List<UserDTO> userDTOs = new List<UserDTO>();
            string result;
            if (method == "get")
            {
                SqlDataReader dataReader = cmd.ExecuteReader();
                while (dataReader.Read())
                {
                    userDTOs.Add(new UserDTO
                    {
                        UserId = (int)dataReader[0],
                        UserName = dataReader[1].ToString(),
                        DOB = Convert.ToDateTime(dataReader[2]),
                        DepartmentId = (int)dataReader[3],
                    });
                }
                dataReader.Close();
            }
            else
            {
                result = cmd.ExecuteNonQuery().ToString();
            } 
            return userDTOs;
        }
        public static string InsertUpdate(int id,string Uname,DateTime dob,int Did)
        {
            Department.Connection();
            SqlCommand cmd = new SqlCommand("InsertUpdateUsers",Department.connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserId", id);
            cmd.Parameters.AddWithValue("@UserName", Uname);
            cmd.Parameters.AddWithValue("@DOB", dob);
            cmd.Parameters.AddWithValue("@DepartmentId", Did);

            string result = cmd.ExecuteNonQuery().ToString();
            return result;
        }
    }
}
