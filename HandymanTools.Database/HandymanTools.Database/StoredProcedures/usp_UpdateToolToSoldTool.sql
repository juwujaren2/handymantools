CREATE PROCEDURE [dbo].[usp_UpdateToolToSoldTool]
	@ToolId int
AS
UPDATE Tool
	SET Tool.SaleDate = GETDATE()
	WHERE Tool.ToolID = @ToolID
	AND Tool.SaleDate IS NULL;

RETURN 0
