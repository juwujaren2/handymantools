CREATE PROCEDURE [dbo].[usp_UpdateReservationWithCreditCard]
	@ClerkId int,
	@CreditCardNum varchar(16),
	@CreditCardExpDate Date,
	@ReservationNumber int
AS
	UPDATE Reservation
	SET PickupClerkId = @ClerkId, CreditCardNum = @CreditCardNum, CreditCardExpDate = @CreditCardExpDate
	WHERE ReservationNumber = @ReservationNumber

RETURN 0
