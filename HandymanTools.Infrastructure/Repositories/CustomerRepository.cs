using System;
using System.Configuration;
using HandymanTools.Common.Models;
using System.Data.SqlClient;
using System.Data;

namespace HandymanTools.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private string _connectionString;
        public CustomerRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public int AddCustomer(Customer customer)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_InsertNewCustomerProfile";
                command.Connection = conn;
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = customer.UserName;
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = customer.FirstName;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = customer.LastName;
                command.Parameters.Add("@Address", SqlDbType.VarChar).Value = customer.Address;
                command.Parameters.Add("@HomeAreaCode", SqlDbType.VarChar).Value = customer.HomeAreaCode;
                command.Parameters.Add("@HomePhone", SqlDbType.VarChar).Value = customer.HomePhone;
                command.Parameters.Add("@WorkAreaCode", SqlDbType.VarChar).Value = customer.WorkAreaCode;
                command.Parameters.Add("@WorkPhone", SqlDbType.VarChar).Value = customer.WorkPhone;
                command.Parameters.Add("@Password", SqlDbType.VarChar).Value = customer.Password;
                command.Parameters.Add("@PasswordHash", SqlDbType.VarChar).Value = customer.PasswordHash;

                //open, execute stored procedure, and close connection
                conn.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    return -1;
                }
                finally
                {
                    conn.Close();
                }
            }
            return 0;
        }

        public Customer GetCustomer(string userName)
        {
            Customer customerProfile = new Customer { UserName = userName };
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetReservationsByCustomerId";
                command.Connection = conn;
                command.Parameters.Add("@CustomerId", SqlDbType.VarChar).Value = userName;

                //open, execute stored procedure, and close connection
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customerProfile.UserName = reader.GetString(0);
                        customerProfile.FirstName = reader.GetString(1);
                        customerProfile.LastName = reader.GetString(2);
                        customerProfile.HomeAreaCode = reader.GetString(3);
                        customerProfile.HomePhone = reader.GetString(4);
                        customerProfile.WorkAreaCode = reader.GetString(5);
                        customerProfile.WorkPhone = reader.GetString(6);
                        customerProfile.Address = reader.GetString(7);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return customerProfile;
        }
    }
}
