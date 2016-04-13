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
using System.Globalization;

namespace HandymanTools.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private string _connectionString;

        public ReservationRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        /// <summary>
        /// Calls stored procedure that retrieves reserved tools for a reservation
        /// </summary>
        /// <param name="reservationNumber"></param>
        /// <returns></returns>
        public List<ReservationTool> GetReservedToolDetails(int reservationNumber)
        {
            List<ReservationTool> tools = new List<ReservationTool>();     

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
                        ReservationTool tool = new ReservationTool();
                        tool.ToolId = reader.GetInt32(0);
                        tool.Tool.AbbrDescription = reader.GetString(1);
                        tool.Tool.RentalPrice = reader.GetDecimal(2);
                        tool.Tool.DepositAmount = reader.GetDecimal(3);
                        tool.Tool.ToolType = (ToolType)Enum.Parse(typeof(ToolType), reader.GetString(4));
                        tools.Add(tool);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return tools;

        }

        /// <summary>
        /// Calls stored procedure that stores a new reservation
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="toolIds"></param>
        /// <returns></returns>
        public int MakeReservation(string customerId, DateTime startDate, DateTime endDate, List<int> toolIds )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "usp_InsertNewCustomerReservation",
                    Connection = conn
                };
                command.Parameters.Add("@CustomerId", SqlDbType.VarChar).Value = customerId;
                command.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate;
                command.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate;
                command.Parameters.Add("@ToolList", SqlDbType.VarChar).Value = string.Join(",", toolIds);
                command.Parameters.Add(new SqlParameter("@ReservationNumber", SqlDbType.Int) { Direction = ParameterDirection.Output});
                //open, execute stored procedure, and close connection
                conn.Open();
                command.ExecuteNonQuery();
                var reservationId = int.Parse(command.Parameters["@ReservationNumber"].Value.ToString());
                return reservationId;
            }
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
                        reservation.Reservation.PickupClerk.FirstName = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        reservation.Reservation.DropOffClerk.FirstName = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        reservations.Add(reservation);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return reservations;
        }
        
        /// <summary>
        /// Calls stored procedure that returns details for a single reservation
        /// </summary>
        /// <param name="reservationNumber"></param>
        /// <returns>Reservation</returns>
        public Reservation GetReservationDetails(int reservationNumber)
        {
            Reservation reservation = new Reservation();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_GetReservationDetails";
                command.Connection = conn;
                command.Parameters.Add("@ReservationNumber", SqlDbType.Int).Value = reservationNumber;

                //open, execute stored procedure, and close connection
                conn.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservation.ReservationNumber = reservationNumber;
                        reservation.StartDate = reader.GetDateTime(0);
                        reservation.EndDate = reader.GetDateTime(1);
                        reservation.CreditCardNumber = reader.GetString(2);
                        reservation.CreditCardExpirationDate = reader.GetDateTime(3);
                        reservation.Customer.FirstName = reader.GetString(4);
                        reservation.Customer.LastName = reader.GetString(5);
                        reservation.PickupClerk.FirstName = reader.GetString(6);
                        reservation.DropOffClerk.FirstName = reader.IsDBNull(7) ? string.Empty : reader.GetString(6);
                    }
                    reader.NextResult();
                }
                reader.Close();
                conn.Close();
            }
            return reservation;
        }

        /// <summary>
        /// Calls stored procedure that updates reservation with credit card and pickup clerk information
        /// </summary>
        /// <param name="reservationNumber"></param>
        /// <param name="creditCard"></param>
        /// <param name="expirationDate"></param>
        /// <param name="clerkId"></param>
        /// <returns></returns>
        public int UpdateReservationWithCreditCard(int reservationNumber, string creditCard, DateTime expirationDate, string clerkId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UpdateReservationWithCreditCard";
                command.Connection = conn;
                command.Parameters.Add("@ReservationNumber", SqlDbType.Int).Value = reservationNumber;
                command.Parameters.Add("@ClerkId", SqlDbType.VarChar).Value = clerkId;
                command.Parameters.Add("@CreditCardNum", SqlDbType.VarChar).Value = creditCard;
                command.Parameters.Add("@CreditCardExpDate", SqlDbType.Date).Value = expirationDate;

                //open, execute stored procedure, and close connection
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            return 0;
        }
        
        public int UpdateReservationWithDropoffClerk(int reservationNumber, string clerkId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UpdateReservationWithDropoffClerk";
                command.Connection = conn;
                command.Parameters.Add("@ReservationNumber", SqlDbType.Int).Value = reservationNumber;
                command.Parameters.Add("@ClerkId", SqlDbType.VarChar).Value = clerkId;

                //open, execute stored procedure, and close connection
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
            return 0;
        }
    }
}
