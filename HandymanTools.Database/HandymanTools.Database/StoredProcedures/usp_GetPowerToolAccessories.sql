CREATE PROCEDURE [dbo].[usp_GetPowerToolAccessories]
	@ToolId int
AS
	SELECT Accessory
	FROM dbo.PowerToolAccessory
	WHERE ToolId = @ToolId
RETURN 0
