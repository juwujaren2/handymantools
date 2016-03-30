CREATE TABLE Reservation
(
	ReservationNumber int IDENTITY(1,1) NOT NULL, --autoincrement surrogate
	CustomerId varchar(36) NOT NULL,
	PickupClerkId varchar(36) NULL,
	DropOffClerkId varchar(36) NULL,
	StartDate Date NOT NULL,
	EndDate Date NOT NULL,
	CreditCardNum varchar(16) NULL,
	CreditCardExpDate Date NULL,
	CONSTRAINT pk_Reservation_ReservationNumber PRIMARY KEY (ReservationNumber),
	CONSTRAINT fk_Reservation_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customer(UserName),
	CONSTRAINT fk_Reservation_PickupClerkId FOREIGN KEY (PickupClerkId) REFERENCES Clerk(UserName),
	CONSTRAINT fk_Reservation_DropOffClerkId FOREIGN KEY (DropOffClerkId) REFERENCES Clerk(UserName)
)
