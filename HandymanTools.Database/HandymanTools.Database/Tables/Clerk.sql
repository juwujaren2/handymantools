CREATE TABLE Clerk
(
	UserName varchar(36) NOT NULL,
	CONSTRAINT pk_Clerk_UserName PRIMARY KEY (UserName),
	CONSTRAINT fk_Clerk_UserName FOREIGN KEY (UserName) REFERENCES [User](UserName)	
);
