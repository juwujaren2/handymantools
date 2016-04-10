CREATE PROCEDURE [dbo].[usp_UpdateToolToSoldTool]
	@ToolId int
AS
UPDATE Tool
	SET Tool.SaleDate = GETDATE()
	WHERE Tool.ToolId = @ToolId
	AND Tool.SaleDate IS NULL;

RETURN 0
