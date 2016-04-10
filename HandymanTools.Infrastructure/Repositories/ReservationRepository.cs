using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanTools.Common.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using HandymanTools.Common.Enums;

namespace HandymanTools.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private string _connectionString;

        public ReservationRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Tool> GetReservedToolDetails(int reservationNumber)
        {
            List<Tool> tools = new List<Tool>();     

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetReservedToolDetails";
                command.Connection = conn;
                command.Parameters.Add("@ReservationNumber", SqlDbType.Int).Value = reservationNumber;

                //open, execute stored procedure, and close connection
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Tool tool = new Tool();
                        tool.ToolId = reader.GetInt32(0);
                        tool.AbbrDescription = reader.GetString(1);
                        tool.RentalPrice = reader.GetDecimal(2);
                        tool.DepositAmount = reader.GetDecimal(3);
                        tool.ToolType = (ToolType)Enum.Parse(typeof(ToolType), reader.GetString(4));
                        tools.Add(tool);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return tools;

        }

        public List<ReservationTool> GetReservationsByCustomer(string userName)
        {
            List<ReservationTool> reservations = new List<ReservationTool>();

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

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ReservationTool reservation = new ReservationTool();
                        reservation.ReservationNumber = reader.GetInt32(0);
                        reservation.Tool.AbbrDescription = reader.GetString(1);
                        reservation.Reservation.StartDate = reader.GetDateTime(2);
                        reservation.Reservation.EndDate = reader.GetDateTime(3);
                        reservation.Tool.RentalPrice = reader.GetDecimal(4);
                        reservation.Tool.DepositAmount = reader.GetDecimal(5);
                        reservation.Reservation.PickupClerk.FirstName = reader.GetString(6);
                        reservation.Reservation.DropOffClerk.FirstName = reader.GetString(7);
                        reservations.Add(reservation);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return reservations;
        }
    }
}
