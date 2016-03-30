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
            int customerId = 0;
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
                command.Parameters.Add("@UserId", SqlDbType.Int).Direction = ParameterDirection.Output;

                //open, execute stored procedure, and close connection
                conn.Open();
                command.ExecuteNonQuery();
                customerId = Convert.ToInt32(command.Parameters["@UserId"].Value);
                conn.Close();
            }
            return customerId;
        }

        public Customer GetCustomer(int customerId)
        {
            Customer customerProfile = new Customer { UserId = customerId };
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetReservationsByCustomerId";
                command.Connection = conn;
                command.Parameters.Add("@CustomerId", SqlDbType.VarChar).Value = customerId;

                //open, execute stored procedure, and close connection
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while(reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //SELECT u.UserName AS 'Email', u.FirstName + ' ' + u.LastName AS Name, c.HomeAreaCode + c.HomePhone AS 'HomePhone', c.WorkAreaCode + c.WorkPhone AS 'WorkPhone', c.[Address] 
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
