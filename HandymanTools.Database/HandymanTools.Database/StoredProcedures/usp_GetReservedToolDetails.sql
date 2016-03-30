CREATE PROCEDURE [dbo].[usp_GetReservedToolDetails]
	@ReservationNumber int
AS
	SELECT Tool.ToolID, Tool.AbbrDescription, Tool.RentalPrice, Tool.DepositAmt
	FROM ReservationTool
		INNER JOIN Tool 
			ON Tool.ToolID = ReservationTool.ToolID
	WHERE ReservationTool.ReservationNumber = @ReservationNumber

RETURN 0
