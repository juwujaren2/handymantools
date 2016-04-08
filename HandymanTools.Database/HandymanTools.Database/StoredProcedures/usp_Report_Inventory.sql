CREATE PROCEDURE [dbo].[usp_Report_Inventory]
	@Year int,
	@Month int
AS
	SELECT Tool.ToolId, Tool.AbbrDescription, RentalProfit, CostOfRepairs, 
	(RentalProfit - CostOfRepairs) AS TotalProfit FROM Tool
	INNER JOIN (
		SELECT Tool.ToolID, SUM(RentalPrice * (DATEDIFF(dd, Reservation.StartDate, Reservation.EndDate) - 1)) AS RentalProfit FROM Tool 
		INNER JOIN ReservationTool ON ReservationTool.ToolID = Tool.ToolID 
		INNER JOIN Reservation ON ReservationTool.ReservationNumber = Reservation.ReservationNumber
		WHERE Tool.SaleDate <> NULL GROUP BY Tool.ToolID
	) AS Profit ON Tool.ToolId = Profit.ToolID JOIN (
		SELECT Tool.ToolId, SUM(ServiceOrder.EstimatedCost) AS CostOfRepairs FROM Tool 
		INNER JOIN ServiceOrder ON ServiceOrder.ToolID = Tool.ToolID 
		GROUP BY Tool.ToolID
	) AS Repairs ON Tool.ToolId = Repairs.ToolId
	ORDER BY TotalProfit
RETURN 0
