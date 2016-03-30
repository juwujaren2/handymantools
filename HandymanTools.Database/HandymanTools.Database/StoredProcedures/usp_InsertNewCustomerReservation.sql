CREATE PROCEDURE [dbo].[usp_InsertNewCustomerReservation]
	@ReservationNumber int OUTPUT,
	@CustomerId varchar(36),
	@StartDate Date,
	@EndDate Date,
	@ToolList varchar(MAX)
AS
	INSERT INTO Reservation (CustomerId, StartDate, EndDate)
		 VALUES (@CustomerId, @StartDate, @EndDate)

	SET @ReservationNumber = SCOPE_IDENTITY()

	INSERT INTO ReservationTool (ReservationNumber, ToolId)
	SELECT @ReservationNumber, Id 
	FROM fn_SplitStrings_Integer(@ToolList, ',')

RETURN 0
