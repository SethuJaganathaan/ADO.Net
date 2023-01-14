using ADOdummy.DTO;
using System.Data;
using System.Data.SqlClient;

namespace ADOdummy.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentIncharge { get; set; }

        public static SqlConnection connection;
        public static SqlCommand cmd;

        public Department(int Did,string Dname,string Dincharge)
        {
            DepartmentId = Did;
            DepartmentName = Dname;
            DepartmentIncharge = Dincharge;
        }

        public static void Connection()
        {
            connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog = ADO; Integrated Security= true");
            connection.Open();
        }

        public static List<Department> GetAllDepartment()
        {
            Connection();
            cmd = new SqlCommand("select * from Department",connection);
            List<Department> department = new List<Department>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                department.Add(new Department((int)reader[0], reader[1].ToString(), reader[2].ToString()));
            }
            reader.Close();
            connection.Close();
            return department;
        }
        public static string InsertUpdate(int id,string dname,string diname)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("InsertUpdateDepartment", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DepartmentId", id);
            cmd.Parameters.AddWithValue("@DepartmentName",dname);
            cmd.Parameters.AddWithValue("@DepartmentIncharge",diname);

            string result = cmd.ExecuteNonQuery().ToString();
            return result;

        }
        public static List<Department> GETandDelete(int id,string method)
        {
            Connection();
            SqlCommand cmd = new SqlCommand("GETandDELETEdepartment",connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@DepartmentId", id);
            cmd.Parameters.AddWithValue("@Method", method);
            string Result;
            List<Department> department = new List<Department>();
            if(method == "get")
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    department.Add(new Department(
                        (int)reader[0],
                        (reader[1].ToString()),
                        (reader[2].ToString())
                        ));
                }
                reader.Close();
                
            }
            else
            {
                Result = cmd.ExecuteNonQuery().ToString();

            }
            return department;
        }
        public static List<UserDepartmentDTO> GetDepartmentIncharge()
        {
            Connection();
            cmd = new SqlCommand("select Users.UserName,Department.DepartmentName,Department.DepartmentIncharge from Users inner join Department on Users.DepartmentId = Department.DepartmentId",connection);
            List<UserDepartmentDTO> department = new List<UserDepartmentDTO>();
            SqlDataReader Dreader = cmd.ExecuteReader();
            while (Dreader.Read())
            {
                department.Add(new UserDepartmentDTO
                {
                    UserName  = (string)Dreader[0],
                    DepartmentName = (string)Dreader[1],
                    DepartmentIncharge = (string)Dreader[2]
                });
            }
            Dreader.Close();
            return department;
        }
    }
}
