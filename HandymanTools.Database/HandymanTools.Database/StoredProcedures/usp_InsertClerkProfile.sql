CREATE PROCEDURE [dbo].[usp_InsertClerkProfile]
	@UserName varchar(36),
	@FirstName varchar(100),
    @LastName varchar(100),
    @Password varchar(256),
	@PasswordHash varchar(256)
AS
	INSERT INTO [User] (UserName, [Password], [PasswordHash], FirstName, LastName)
	VALUES (@UserName, @Password, @PasswordHash, @FirstName, @LastName)

	INSERT INTO Clerk (UserName)
	VALUES (@UserName)

RETURN 0
