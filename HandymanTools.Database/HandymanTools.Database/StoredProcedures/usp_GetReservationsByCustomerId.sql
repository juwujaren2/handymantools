CREATE PROCEDURE [dbo].[usp_GetReservationsByCustomerId]
	@CustomerId varchar(36)
AS
	SELECT Reservation.ReservationNumber,
		STUFF 
		(
			(
				SELECT ', ' + Tool.AbbrDescription 
			), 1, 1, ''
		) AS Tools,
		Reservation.StartDate, 
		Reservation.EndDate, 
		SUM(Tool.RentalPrice) as TotalRentalPrice, 
		SUM(Tool.DepositAmt) as TotalDepositAmount, 
		PickupClerkUser.FirstName, 
		DropOffClerkUser.FirstName 
	FROM Reservation 
		INNER JOIN ReservationTool ON Reservation.ReservationNumber = ReservationTool.ReservationNumber
		INNER JOIN Tool ON Tool.ToolId = ReservationTool.ToolId 
		LEFT JOIN Clerk AS PickupClerk ON Reservation.PickupClerkId = PickupClerk.UserName
		LEFT JOIN [User] AS PickupClerkUser ON PickupClerk.UserName = PickupClerkUser.UserName
		LEFT JOIN Clerk AS DropOffClerk ON Reservation.DropOffClerkId = DropOffClerk.UserName
		LEFT JOIN [User] AS DropOffClerkUser ON DropOffClerk.UserName = DropOffClerkUser.UserName 
	WHERE Reservation.CustomerId = @CustomerId 
	GROUP BY Reservation.ReservationNumber,
		Tool.AbbrDescription, 
		Reservation.StartDate, 
		Reservation.EndDate, 
		PickupClerkUser.FirstName, 
		DropOffClerkUser.FirstName

RETURN 0


