CREATE PROCEDURE [dbo].[usp_InsertNewTool]
@ToolId int OUTPUT,
@AbbrDescription varchar(50),
@FullDescription varchar(255),
@RentalPrice decimal(13,4),
@PurchasePrice decimal (13,4),
@DepositAmount decimal (13,4),
@ToolType varchar(25),
@Accessory varchar(50)
AS
	INSERT INTO Tool (AbbrDescription, FullDescription, RentalPrice, PurchasePrice, 
			DepositAmt, ToolType)
	VALUES (@AbbrDescription, @FullDescription, @RentalPrice, @PurchasePrice, 
			@DepositAmount, @ToolType);

	SET @ToolId = SCOPE_IDENTITY()

	IF @ToolType = 'Power'
	BEGIN
		INSERT INTO PowerToolAccessory (ToolId, Accessory)
		VALUES (@ToolID, @Accessory);
	END

RETURN 0
