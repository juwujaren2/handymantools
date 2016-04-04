CREATE PROCEDURE [dbo].[usp_GetUserByUserName]
	@UserName varchar(36)
AS
	SELECT u.UserName, u.FirstName, u.LastName, c.[Address], c.HomeAreaCode, c.HomePhone, c.WorkAreaCode, c.WorkPhone 
	FROM [User] u
	LEFT JOIN Customer c ON u.UserName = c.UserName 
	WHERE u.UserName = @UserName
RETURN 0
