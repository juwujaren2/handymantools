CREATE PROCEDURE [dbo].[usp_GetReservedToolDetails]
	@ReservationNumber int
AS
	SELECT Tool.ToolId, Tool.AbbrDescription, Tool.FullDescription, Tool.PurchasePrice, Tool.RentalPrice, Tool.DepositAmt, Tool.SaleDate, Tool.ToolType
	FROM ReservationTool
		INNER JOIN Tool 
			ON Tool.ToolId = ReservationTool.ToolId
	WHERE ReservationTool.ReservationNumber = @ReservationNumber

RETURN 0
