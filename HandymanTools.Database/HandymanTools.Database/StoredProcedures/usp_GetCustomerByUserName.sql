CREATE PROCEDURE [dbo].[usp_GetCustomerByUserName]
	@UserName varchar(36)
AS
	SELECT c.UserName, u.FirstName, u.LastName, c.HomeAreaCode, c.HomePhone, c.WorkAreaCode, c.WorkPhone, c.[Address] 
	FROM Customer c 
	INNER JOIN [User] u ON c.UserName = u.UserName 
	WHERE u.UserName = @UserName 
	
RETURN 0
