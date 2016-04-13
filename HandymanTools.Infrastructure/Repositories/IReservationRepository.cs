using HandymanTools.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandymanTools.Infrastructure.Repositories
{
    public interface IReservationRepository
    {
        List<ReservationTool> GetReservedToolDetails(int reservationNumber);

        List<ReservationTool> GetReservationsByCustomer(string userName);

        Reservation GetReservationDetails(int reservationNumber);

        int UpdateReservationWithCreditCard(int reservationNumber, string creditCard, DateTime expirationDate, string clerkId);

        int UpdateReservationWithDropoffClerk(int reservationNumber, string clerkId);
    }
}
