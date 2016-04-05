CREATE PROCEDURE [dbo].[usp_InsertNewPowerToolAccessory]
	@ToolId int,
	@Accessory int
AS
	INSERT INTO PowerToolAccessory (ToolId, Accessory)
	VALUES (@ToolID, @Accessory);
RETURN 0

