CREATE TABLE [User] 
(	
	UserName varchar(36) NOT NULL,
	FirstName varchar(100) NOT NULL,
	LastName varchar(100) NOT NULL,
	[Password] varchar(256) NOT NULL,
	CONSTRAINT pk_User_UserName PRIMARY KEY (UserName)
)
