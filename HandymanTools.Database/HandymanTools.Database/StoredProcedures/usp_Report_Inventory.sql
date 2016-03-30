CREATE PROCEDURE [dbo].[usp_Report_Inventory]
	@Year int,
	@Month int
AS
	SELECT Tool.ToolID, Tool.AbbrDescription, RentalProfit, CostOfRepairs, 
	(RentalProfit - CostOfRepairs) AS TotalProfit FROM Tool
	INNER JOIN (
		SELECT Tool.ToolID, SUM(RentalPrice * (DATEDIFF(dd, Reservation.StartDate, Reservation.EndDate) - 1)) AS RentalProfit FROM Tool 
		INNER JOIN ReservationTool ON ReservationTool.ToolID = Tool.ToolID 
		INNER JOIN Reservation ON ReservationTool.ReservationNumber = Reservation.ReservationNumber 
		WHERE
		DATEPART(mm, Reservation.StartDate) = @Month AND DATEPART(yyyy, Reservation.StartDate) = @Year
		AND Tool.SaleDate <> NULL GROUP BY Tool.ToolID
	) AS Profit ON Tool.ToolID = Profit.ToolID JOIN (
		SELECT Tool.ToolID, SUM(ServiceOrder.EstimatedCost) AS CostOfRepairs FROM Tool 
		INNER JOIN ServiceOrder ON ServiceOrder.ToolID = Tool.ToolID 
		WHERE DATEPART(mm, ServiceOrder.StartDate) = @Month AND DATEPART(yyyy, ServiceOrder.StartDate) = @Year
		GROUP BY Tool.ToolID
	) AS Repairs ON Tool.ToolID = Repairs.ToolID
	ORDER BY TotalProfit
RETURN 0
