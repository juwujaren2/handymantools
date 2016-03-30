CREATE PROCEDURE [dbo].[usp_GetPasswordByUserName]
	@UserName varchar(36) 
AS
	SELECT [Password] 
	FROM [User] u
	WHERE u.UserName = @UserName
RETURN 0
