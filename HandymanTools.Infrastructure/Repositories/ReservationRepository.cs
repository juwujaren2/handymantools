﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HandymanTools.Common.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace HandymanTools.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private string _connectionString;

        public ReservationRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

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
            ReservationTool reservation = new ReservationTool();

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
