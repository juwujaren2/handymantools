CREATE PROCEDURE [dbo].[usp_Report_CustomerRentals]
	@Year int,
	@Month int
AS
	 SELECT FirstName, LastName, Customer.UserName, COUNT(ReservationTool.ToolId) AS NumberOfRentals FROM Customer
		 INNER JOIN Reservation ON Customer.UserName = Reservation.CustomerId
		 INNER JOIN [User] ON [User].UserName = Customer.UserName
		 INNER JOIN ReservationTool ON ReservationTool.ReservationNumber = Reservation.ReservationNumber 
	 WHERE DATEPART(mm, Reservation.StartDate) = @Month AND DATEPART(yyyy, Reservation.StartDate) = @Year
	 GROUP BY ReservationTool.ToolId, FirstName, LastName, Customer.UserName
	 ORDER BY NumberOfRentals, LastName
RETURN 0
