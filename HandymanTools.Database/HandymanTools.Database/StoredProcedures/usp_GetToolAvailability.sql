CREATE PROCEDURE [dbo].[usp_GetToolAvailability]
	@StartDate Date,
	@EndDate Date
AS
SELECT t.ToolID, AbbrDescription, t.DepositAmt, RentalPrice 
FROM Tool t
	LEFT JOIN ReservationTool rt ON t.ToolId = rt.ToolId
	LEFT JOIN Reservation r ON r.ReservationNumber = rt.ReservationNumber AND r.EndDate >= @StartDate AND r.StartDate <= @EndDate
	LEFT JOIN ServiceOrder s ON s.ToolId = t.ToolId AND s.EndDate >= @StartDate AND s.StartDate <= @EndDate
WHERE s.ToolId IS NULL AND r.ReservationNumber IS NULL 

RETURN 0
