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
    public class ToolRepository : IToolRepository
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
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate;

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
                command.Parameters.Add("@ToolId", SqlDbType.Int).Value = toolId;

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

                if (returnTool != null && returnTool.ToolType == ToolType.Power)
                {
                    returnTool.Accessories = GetPowerToolAccessories(returnTool);
                }
               
                reader.Close();
                conn.Close();
                return returnTool;
            }
        }

        public List<PowerToolAccessory> GetPowerToolAccessories(Tool tool)
        {
            var returnList = new List<PowerToolAccessory>();
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_GetPowerToolAccessories",
                    Connection = conn
                };
                command.Parameters.Add("@ToolId", SqlDbType.Int).Value = tool.ToolId;

                //open, execute stored procedure, and close connection
                conn.Open();
                var reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        returnList.Add(new PowerToolAccessory()
                        {
                            Tool = tool,
                            ToolId = tool.ToolId,
                            Accessory = reader.GetString(0)
                        });
                    }
                    break;
                }
            }
            return returnList;
        }

        public int AddTool(Tool tool)
        {
            int toolId = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_InsertNewTool";
                command.Connection = conn;
                command.Parameters.Add("@AbbrDescription", SqlDbType.VarChar).Value = tool.AbbrDescription;
                command.Parameters.Add("@FullDescription", SqlDbType.VarChar).Value = tool.FullDescription;
                command.Parameters.Add("@RentalPrice", SqlDbType.Decimal).Value = tool.RentalPrice;
                command.Parameters.Add("@PurchasePrice", SqlDbType.Decimal).Value = tool.PurchasePrice;
                command.Parameters.Add("@DepositAmount", SqlDbType.Decimal).Value = tool.DepositAmount;
                command.Parameters.Add("@ToolType", SqlDbType.VarChar).Value = tool.ToolType;
                command.Parameters.Add("@ToolId", SqlDbType.Int).Direction = ParameterDirection.Output;

                //open, execute stored procedure, and close connection
                conn.Open();
                command.ExecuteNonQuery();
                toolId = Convert.ToInt32(command.Parameters["@ToolId"].Value);
                conn.Close();
            }
            return toolId;
        }

        public int SellTool(int ToolId)
        {
            throw new NotImplementedException();
        }

        public int AddPowerToolAccessory(int toolId, string accessory)
        {
            PowerToolAccessory PowerToolAccessory = new PowerToolAccessory();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_InsertNewPowerToolAccessory";
                command.Connection = conn;
                command.Parameters.Add("@ToolId", SqlDbType.Int).Value = toolId;
                command.Parameters.Add("@Accessory", SqlDbType.VarChar).Value = accessory;

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
    }
}
