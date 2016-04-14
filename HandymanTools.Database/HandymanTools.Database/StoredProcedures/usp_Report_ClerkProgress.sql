CREATE PROCEDURE [dbo].[usp_Report_ClerkProgress]
	@Year int,
	@Month int
AS
	SELECT [User].FirstName, [User].LastName, ISNULL(TotalPickups, 0), ISNULL(TotalDropoffs, 0), (ISNULL(TotalPickups, 0) + ISNULL(TotalDropoffs, 0)) AS TotalSum 
	FROM Clerk
		INNER JOIN [User] ON [User].UserName = Clerk.UserName
		INNER JOIN (
			SELECT Reservation.PickupClerkId, count(Reservation.PickupClerkId) AS TotalPickups FROM Reservation

			WHERE DATEPART(mm, Reservation.StartDate) = @Month AND DATEPART(yyyy, Reservation.StartDate) = @Year
			GROUP BY Reservation.PickupClerkId
		) AS Pickups on Pickups.PickupClerkId = Clerk.UserName 
		FULL OUTER JOIN (
			SELECT Reservation.DropOffClerkId, count(Reservation.DropOffClerkId) AS TotalDropoffs FROM Reservation
			WHERE DATEPART(mm, Reservation.StartDate) = @Month AND DATEPART(yyyy, Reservation.StartDate) = @Year
			GROUP BY Reservation.DropOffClerkId
		) AS Dropoff on Dropoff.DropOffClerkId = Clerk.UserName
	WHERE [User].FirstName IS NOT NULL AND [User].LastName IS NOT NULL
	ORDER BY TotalPickups, TotalDropoffs
RETURN 0
