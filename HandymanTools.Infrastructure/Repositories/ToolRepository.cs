using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanTools.Common.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace HandymanTools.Infrastructure.Repositories
{
    public class ToolRepository : IToolRepository
    {
        private string _connectionString;

        public ToolRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public int AddTool(Tool tool)
        {
            int toolId = 0;
            string accessories = string.Join(",", tool.Accessories.Where(a => !string.IsNullOrWhiteSpace(a)));
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
                command.Parameters.Add("@AccessoryList", SqlDbType.VarChar).Value = accessories;
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
    }
}
