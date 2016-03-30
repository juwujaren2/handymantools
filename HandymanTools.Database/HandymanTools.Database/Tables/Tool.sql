CREATE TABLE Tool
(
	ToolId int IDENTITY(1,1) NOT NULL,  -- autoincrement surrogate
	AbbrDescription varchar(50) NOT NULL,
	FullDescription varchar (255) NOT NULL,
	RentalPrice decimal (13,4) NOT NULL,
	PurchasePrice decimal (13,4) NOT NULL,
	DepositAmt decimal (13,4) NOT NULL,
	ToolType varchar(25) NOT NULL CHECK (ToolType IN ('Hand', 'Construction', 'Power')),
	SaleDate Date NULL,
	CONSTRAINT pk_Tool_ToolId PRIMARY KEY (ToolId)
)
