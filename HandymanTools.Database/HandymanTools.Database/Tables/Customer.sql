CREATE TABLE Customer
(
	UserName varchar(36) NOT NULL,
	[Address] varchar(255) NOT NULL,
	HomeAreaCode varchar(5) NOT NULL,
	HomePhone varchar(10) NOT NULL,
	WorkAreaCode varchar(5) NOT NULL,
	WorkPhone varchar(10) NOT NULL,
	CONSTRAINT pk_Customer_UserName PRIMARY KEY (UserName),
	CONSTRAINT fk_Customer_UserId FOREIGN KEY (UserName) REFERENCES [User](UserName)
);