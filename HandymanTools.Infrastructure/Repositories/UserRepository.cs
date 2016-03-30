using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;
        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public string GetPasswordByUserName(string userName)
        {
            string password = string.Empty;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetPasswordByUserName";
                command.Connection = conn;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

                //open, execute stored procedure, and close connection
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //SELECT u.UserName AS 'Email', u.FirstName + ' ' + u.LastName AS Name, c.HomeAreaCode + c.HomePhone AS 'HomePhone', c.WorkAreaCode + c.WorkPhone AS 'WorkPhone', c.[Address] 
                        password = reader.GetString(0);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return password;
        }
    }
}
