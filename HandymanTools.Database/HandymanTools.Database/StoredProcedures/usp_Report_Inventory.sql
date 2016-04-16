CREATE PROCEDURE [dbo].[usp_Report_Inventory]
AS
	SELECT Tool.ToolId, Tool.AbbrDescription, ISNULL(RentalProfit, 0) AS RentalProfit, ISNULL(CostOfRepairs, 0) AS CostOfRepairs, 
	ISNULL((RentalProfit - CostOfRepairs), 0) AS TotalProfit FROM Tool
	LEFT OUTER JOIN (
		SELECT Tool.ToolID, SUM(RentalPrice * DATEDIFF(dd, Reservation.StartDate, Reservation.EndDate)) AS RentalProfit FROM Tool 
		INNER JOIN ReservationTool ON ReservationTool.ToolID = Tool.ToolID 
		INNER JOIN Reservation ON ReservationTool.ReservationNumber = Reservation.ReservationNumber
		WHERE Tool.SaleDate IS NULL GROUP BY Tool.ToolID
	) AS Profit ON Tool.ToolId = Profit.ToolID 
	LEFT OUTER JOIN (
		SELECT Tool.ToolId, SUM(ServiceOrder.EstimatedCost) AS CostOfRepairs FROM Tool 
		INNER JOIN ServiceOrder ON ServiceOrder.ToolID = Tool.ToolID 
		GROUP BY Tool.ToolID
	) AS Repairs ON Tool.ToolId = Repairs.ToolId
	ORDER BY TotalProfit
RETURN 0
