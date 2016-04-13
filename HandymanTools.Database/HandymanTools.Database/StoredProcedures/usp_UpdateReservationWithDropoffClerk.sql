CREATE PROCEDURE [dbo].[usp_UpdateReservationWithDropoffClerk]
	@ClerkId varchar(36),
	@ReservationNumber int
AS
	UPDATE Reservation
	SET DropoffClerkId = @ClerkId
	WHERE ReservationNumber = @ReservationNumber
	
RETURN 0
