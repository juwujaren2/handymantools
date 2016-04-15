CREATE PROCEDURE [dbo].[usp_GetToolAvailability]
	@StartDate Date,
	@EndDate Date,
	@ToolType varchar(25)
AS
SELECT t.ToolId, t.AbbrDescription, t.DepositAmt, t.RentalPrice
FROM Tool t
WHERE ToolType = @ToolType
AND ToolID NOT IN (
SELECT DISTINCT t.ToolId
FROM ReservationTool rt
INNER JOIN Tool t on t.ToolId = rt.ToolId
INNER JOIN Reservation r on r.ReservationNumber = rt.ReservationNumber
WHERE (DATEADD(DD, -1, EndDate)) >= @StartDate AND StartDate <= @EndDate
UNION
SELECT DISTINCT so.ToolId
FROM ServiceOrder so
WHERE (DATEADD(DD, -1, EndDate)) >= @StartDate AND StartDate <= @EndDate)
AND SaleDate IS NULL

RETURN 0
