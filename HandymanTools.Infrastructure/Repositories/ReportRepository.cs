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
    }
}
