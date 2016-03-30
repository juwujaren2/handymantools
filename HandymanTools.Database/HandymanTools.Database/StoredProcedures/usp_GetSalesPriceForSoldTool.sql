CREATE PROCEDURE [dbo].[usp_GetSalesPriceForSoldTool]
	@ToolId int
AS
	SELECT (Tool.PurchasePrice * 0.5) AS SalePrice
	FROM Tool
	WHERE Tool.ToolId = @ToolId
	AND Tool.SaleDate IS NOT NULL;

RETURN 0
