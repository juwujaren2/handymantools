using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanTools.Common.Enums;
using HandymanTools.Common.Models;

namespace HandymanTools.Infrastructure.Repositories
{
    public class ToolRepository
    {
        private readonly string _connectionString;
        public ToolRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public List<Tool> CheckToolAvailability(ToolType toolType, DateTime startDate, DateTime endDate)
        {
            var returnList = new List<Tool>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_GetToolAvailability",
                    Connection = conn
                };
                command.Parameters.Add("@ToolType", SqlDbType.VarChar).Value = toolType.ToString();
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate;
                command.Parameters.Add("@ToolType", SqlDbType.Date).Value = endDate;

                //open, execute stored procedure, and close connection
                conn.Open();
                var reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        returnList.Add(new Tool() {ToolId = reader.GetInt32(0), AbbrDescription = reader.GetString(1), DepositAmount = reader.GetDecimal(2), RentalPrice = reader.GetDecimal(3)});
                    }
                    break;
                }
                reader.Close();
                conn.Close();
            }
            return returnList;
        }

        public Tool GetToolInfo(int toolId)
        {
            Tool returnTool = null;
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_ViewToolDetails",
                    Connection = conn
                };
                command.Parameters.Add("@ToolType", SqlDbType.Int).Value = toolId;

                //open, execute stored procedure, and close connection
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                       returnTool = new Tool()
                       {
                           ToolId = reader.GetInt32(0),
                           AbbrDescription = reader.GetString(1),
                           FullDescription = reader.GetString(2),
                           ToolType = (ToolType)Enum.Parse(typeof(ToolType), reader.GetString(3), false),
                           DepositAmount = reader.GetDecimal(4),
                           PurchasePrice = reader.GetDecimal(5),
                           RentalPrice = reader.GetDecimal(6)
                       };
                        
                    }
                    break;
                } 
               
                reader.Close();
                conn.Close();
                return returnTool;
            }
        }

        public bool SellTool(int toolId)
        {
            return true;
        }

        public int AddTool(Tool tool)
        {
            return 0;
        }
    }
}
