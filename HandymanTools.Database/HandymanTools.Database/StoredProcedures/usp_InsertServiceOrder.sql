CREATE PROCEDURE [dbo].[usp_InsertServiceOrder]	
	@ToolId int,
	@StartDate Date,
	@EndDate Date,
	@EstimatedCost decimal (13,4),
	@ServiceOrderCreated bit OUTPUT
AS
	DECLARE @ReservationCount int
	DECLARE @ToolCount int

	--Check to make sure tool exists
	SELECT @ToolCount = COUNT(Tool.ToolId) FROM Tool
	WHERE Tool.ToolId = @ToolId;

	--check to make sure tool is not reserved
	SELECT @ReservationCount = COUNT(Reservation.ReservationNumber), @ServiceOrderCreated = 0
	FROM Reservation
	INNER JOIN ReservationTool ON ReservationTool.ReservationNumber = Reservation.ReservationNumber
	INNER JOIN Tool ON ReservationTool.ToolId = Tool.ToolId
	WHERE Tool.ToolId = @ToolId
	AND (@StartDate <= (DATEADD(DD, -1, Reservation.EndDate))) and (Reservation.StartDate <= @EndDate)

	--if reservation does not exist and tool exists, then create new service order
	IF @ReservationCount = 0 AND @ToolCount > 0
	BEGIN
		INSERT INTO ServiceOrder
		VALUES (@ToolID, @StartDate, @EndDate, @EstimatedCost)
		SET @ServiceOrderCreated = 1
	END

RETURN 0
