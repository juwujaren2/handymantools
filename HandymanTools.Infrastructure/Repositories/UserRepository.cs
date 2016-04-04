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
        public string GetPasswordByUserName(string userName, out string passwdHash)
        {
            string password = string.Empty;
            passwdHash = string.Empty;
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
                        password = reader.GetString(0);
                        passwdHash = reader.GetString(1);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return password;
        }
        public User GetUserByUserName(string userName)
        {
            User user = null;
            string firstName = null;
            string lastName = null;
            string customerAddress = null;
            string homeAreaCode = null;
            string homePhone = null;
            string workAreaCode = null;
            string workPhone = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetUserByUserName";
                command.Connection = conn;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = userName;

                //open, execute stored procedure, and close connection
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        firstName = reader.GetString(1);
                        lastName = reader.GetString(2);
                        customerAddress = reader.GetString(3);
                        homeAreaCode = reader.GetString(4);
                        homePhone = reader.GetString(5);
                        workAreaCode = reader.GetString(6);
                        workPhone = reader.GetString(7);
                    }
                    break;
                }
                reader.Close();
                conn.Close();
            }
            if (String.IsNullOrEmpty(customerAddress))
            {
                user = new Clerk
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName
                };               
            }
            else
            {
                user = new Customer
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Address = customerAddress,
                    HomeAreaCode = homeAreaCode,
                    HomePhone = homePhone,
                    WorkAreaCode = workAreaCode,
                    WorkPhone = workPhone
                };
            }
            return user;
        }
    }
}
