
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE GetRequestMasterName (@MasterName nvarchar(150))
AS
BEGIN
	SELECT Request.RequestId, Request.RequestMasterId, Request.RequestData
    FROM Request INNER JOIN
	[User] On Request.RequestMasterid = [User].UserId
    WHERE ([User].UserFullName = @MasterName)

END
GO 

Exec GetRequestMasterName N'Ситников Парфений Всеволодович'