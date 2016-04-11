CREATE PROCEDURE [dbo].[usp_GetToolAvailability]
	@StartDate Date,
	@EndDate Date,
	@ToolType varchar(25)
AS
SELECT t.ToolId, AbbrDescription, t.DepositAmt, RentalPrice 
FROM Tool t
	LEFT JOIN ReservationTool rt ON t.ToolId = rt.ToolId
	LEFT JOIN Reservation r ON r.ReservationNumber = rt.ReservationNumber AND (DATEADD(DD, -1, r.EndDate)) >= @StartDate AND r.StartDate <= @EndDate
	LEFT JOIN ServiceOrder s ON s.ToolId = t.ToolId AND s.EndDate >= @StartDate AND s.StartDate <= @EndDate
WHERE s.ToolId IS NULL AND r.ReservationNumber IS NULL and t.ToolType = @ToolType AND t.SaleDate IS NULL

RETURN 0
