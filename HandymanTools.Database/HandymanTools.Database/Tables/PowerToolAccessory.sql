CREATE TABLE PowerToolAccessory
(
	ToolId int NOT NULL,
	Accessory varchar(50) NOT NULL,
	CONSTRAINT pk_PowerToolAccessory_ToolId_Accessory PRIMARY KEY (ToolId, Accessory),
	CONSTRAINT fk_PowerToolAccessory_ToolId FOREIGN KEY (ToolId) REFERENCES Tool(ToolId)
)
