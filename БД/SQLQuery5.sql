CREATE PROCEDURE GetRequestMasterNameLike (@MasterNameLike nvarchar(150))
AS 
BEGIN
	SELECT Request.Requestid, Request.RequestMasterid, Request.RequestData
    FROM Request INNER JOIN
	[User] On Request.RequestMasterid = [User].Userid
    WHERE [User].UserFullName LIKE  ('%' + @MasterNameLike + '%')

END
GO

Exec GetRequestMasterNameLike N'Парфений'