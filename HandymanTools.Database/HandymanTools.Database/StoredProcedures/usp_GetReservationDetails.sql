CREATE PROCEDURE [dbo].[usp_GetReservationDetails]
	@ReservationNumber int
AS
	SELECT Reservation.StartDate, Reservation.EndDate, Reservation.CreditCardNum, CustomerUser.FirstName, CustomerUser.LastName, PickupClerkUser.FirstName, DropOffClerkUser.FirstName 
	FROM Reservation
		INNER JOIN Customer ON Reservation.CustomerId = Customer.UserName
		INNER JOIN [User] AS CustomerUser ON CustomerUser.UserName = Customer.UserName
		INNER JOIN Clerk AS PickupClerk ON PickupClerk.UserName = Reservation.PickupClerkId
		INNER JOIN [User] AS PickupClerkUser ON PickupClerkUser.UserName = PickupClerk.UserName
		INNER JOIN Clerk AS DropOffClerk ON DropOffClerk.UserName = Reservation.DropOffClerkId
		INNER JOIN [User] AS DropOffClerkUser ON DropOffClerkUser.UserName = DropOffClerk.UserName
	WHERE Reservation.ReservationNumber = @ReservationNumber

RETURN 0
