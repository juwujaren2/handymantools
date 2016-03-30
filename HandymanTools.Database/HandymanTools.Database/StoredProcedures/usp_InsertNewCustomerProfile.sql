CREATE PROCEDURE [dbo].[usp_InsertNewCustomerProfile]
    @UserName varchar(36),    
    @Password varchar(256),
	@FirstName varchar(100),
    @LastName varchar(100),
    @Address varchar(255),
    @HomeAreaCode varchar(5),
    @HomePhone varchar(10),
    @WorkAreaCode varchar(5),
    @WorkPhone varchar(10)
AS
	INSERT INTO [User] (UserName, [Password], FirstName, LastName)
		VALUES (@UserName, @Password, @FirstName, @LastName)

	INSERT INTO Customer (UserName, [Address], HomeAreaCode, HomePhone, WorkAreaCode, WorkPhone)
		 VALUES (@UserName, @Address, @HomeAreaCode, @HomePhone, @WorkAreaCode, @WorkPhone)
RETURN 0