using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanTools.Common.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace HandymanTools.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private string _connectionString;

        public ReportRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<InventoryItem> GenerateInventoryReport()
        {
            var result = new List<InventoryItem>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var inventoryCommand = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = @"usp_Report_Inventory",
                    Connection = conn
                };

                var reader = inventoryCommand.ExecuteReader();

                while(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var newItem = new InventoryItem();
                        newItem.Tool = new Tool();
                        newItem.Tool.ToolId = reader.GetInt32(0);
                        newItem.Tool.AbbrDescription = reader.GetString(1);
                        newItem.RentalProfit = reader.GetDecimal(2);
                        newItem.CostOfRepairs = reader.GetDecimal(3);
                        newItem.TotalProfit = reader.GetDecimal(4);
                        result.Add(newItem);
                    }
                    reader.NextResult();
                }

                conn.Close();
            }

                return result;
        }

        public List<ClerkProgressItem> GenerateClerkProgressReport()
        {
            var result = new List<ClerkProgressItem>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlCommand = new SqlCommand {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_Report_ClerkProgress",
                    Connection = conn
                };

                sqlCommand.Parameters.Add("@Year", SqlDbType.Int).Value = DateTime.Now.Year;
                sqlCommand.Parameters.Add("@Month", SqlDbType.Int).Value = DateTime.Now.Month - 1;

                var reader = sqlCommand.ExecuteReader();

                while(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var item = new ClerkProgressItem();
                        item.Clerk = new Clerk();
                        item.Clerk.FirstName = reader.GetString(0);
                        item.Clerk.LastName = reader.GetString(1);
                        item.Pickups = reader.GetInt32(2);
                        item.Dropoffs = reader.GetInt32(3);
                        item.TotalPickupsDropoffs = reader.GetInt32(4);
                        result.Add(item);
                    }
                    reader.NextResult();
                }
                conn.Close();
            }
            return result;
        }

        public List<CustomerRentalSummary> GenerateCustomerRentalSummary()
        {
            var result = new List<CustomerRentalSummary>();
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var sqlCommand = new SqlCommand {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_Report_CustomerRentals",
                    Connection = conn
                };

                sqlCommand.Parameters.Add("@Year", SqlDbType.Int).Value = DateTime.Now.Year;
                sqlCommand.Parameters.Add("@Month", SqlDbType.Int).Value = DateTime.Now.Month - 1;
                
                var reader = sqlCommand.ExecuteReader();
                while(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var customerRental = new CustomerRentalSummary();
                        customerRental.Customer = new Customer();
                        customerRental.Customer.FirstName = reader.GetString(0);
                        customerRental.Customer.LastName = reader.GetString(1);
                        customerRental.Customer.UserName = reader.GetString(2);
                        customerRental.NumberOfRentals = reader.GetInt32(3);
                        result.Add(customerRental);
                    }
                    reader.NextResult();
                }
                conn.Close();
            }
            return result;
        }
    }
}
