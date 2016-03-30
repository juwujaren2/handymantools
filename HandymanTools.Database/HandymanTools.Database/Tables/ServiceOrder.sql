CREATE TABLE ServiceOrder
(
	ToolId int NOT NULL,
	StartDate Date NOT NULL,
	EndDate Date NULL,
	EstimatedCost decimal (13,4) NOT NULL,
	CONSTRAINT pk_ServiceOrder_ToolId_StartDate PRIMARY KEY (ToolId, StartDate),
	CONSTRAINT fk_ServiceOrder_ToolId FOREIGN KEY (ToolId) REFERENCES Tool(ToolId)
)