CREATE PROCEDURE [dbo].[usp_InsertServiceOrder]	
	@ToolId int,
	@StartDate Date,
	@EndDate Date,
	@EstimatedCost decimal (13,4),
	@ServiceOrderCreated bit OUTPUT
AS
	DECLARE @ReservationCount int
	SELECT @ReservationCount = COUNT(Reservation.ReservationNumber), @ServiceOrderCreated = 0
	FROM Reservation
	INNER JOIN ReservationTool ON ReservationTool.ReservationNumber = Reservation.ReservationNumber
	INNER JOIN Tool ON ReservationTool.ToolID = Tool.ToolID
	WHERE Tool.ToolID = @ToolId
	AND (@StartDate <= Reservation.EndDate) and (Reservation.StartDate <= @EndDate)

	IF @ReservationCount = 0
	BEGIN
		INSERT INTO ServiceOrder
		VALUES (@ToolID, @StartDate, @EndDate, @EstimatedCost)
		SET @ServiceOrderCreated = 1
	END

RETURN 0
