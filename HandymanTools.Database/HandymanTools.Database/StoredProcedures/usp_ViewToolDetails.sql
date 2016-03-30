CREATE PROCEDURE [dbo].[usp_ViewToolDetails]
	@ToolId int
AS
	SELECT ToolId, AbbrDescription, FullDescription, ToolType, DepositAmt, PurchasePrice, RentalPrice
	FROM Tool
	WHERE ToolId = @ToolId

RETURN 0
