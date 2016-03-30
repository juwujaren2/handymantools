CREATE TABLE ReservationTool
(
	ReservationNumber int NOT NULL,
	ToolId int NOT NULL,
	CONSTRAINT pk_ReservationTool_ReservationNumber_ToolId PRIMARY KEY (ReservationNumber, ToolId),
	CONSTRAINT fk_ReservationTool_ReservationNumber FOREIGN KEY (ReservationNumber) REFERENCES Reservation(ReservationNumber),
	CONSTRAINT fk_ReservationTool_ToolId FOREIGN KEY (ToolId) REFERENCES Tool(ToolId)
)