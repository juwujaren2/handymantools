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
    public class ServiceOrderRepository : IServiceOrderRepository
    {
        private readonly string _connectionString;
        public ServiceOrderRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        public bool InsertServiceOrder(int toolId, DateTime startDate, DateTime endDate, decimal estimatedCost)
        {
            bool serviceOrderCreated = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_InsertServiceOrder";
                command.Connection = conn;
                command.Parameters.Add("@ToolId", SqlDbType.Int).Value = toolId;
                command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
                command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
                command.Parameters.Add("@EstimatedCost", SqlDbType.Decimal).Value = estimatedCost;
                command.Parameters.Add("@ServiceOrderCreated", SqlDbType.Bit).Direction = ParameterDirection.Output;

                //open, execute stored procedure, and close connection
                conn.Open();
                command.ExecuteNonQuery();
                serviceOrderCreated = (bool)command.Parameters["@ServiceOrderCreated"].Value;
                conn.Close();
            }
            return serviceOrderCreated;
        }
    }
}
