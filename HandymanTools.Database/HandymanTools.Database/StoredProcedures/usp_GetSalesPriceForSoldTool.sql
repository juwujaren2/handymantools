CREATE PROCEDURE [dbo].[usp_GetSalesPriceForSoldTool]
	@ToolId int
AS
	SELECT ToolId, AbbrDescription, (Tool.PurchasePrice * 0.5) AS SalePrice, SaleDate
	FROM Tool
	WHERE Tool.ToolId = @ToolId
	AND Tool.SaleDate IS NOT NULL;

RETURN 0
