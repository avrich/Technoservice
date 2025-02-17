USE [DBTsapkovaDemoExam21]
GO
/****** Object:  StoredProcedure [dbo].[GetRequestMasterNameLike]    Script Date: 08.10.2024 7:46:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetRequestMasterNameLike] (@MasterNameLike nvarchar(150))
AS 
BEGIN
	SELECT Request.Requestid, Request.RequestMasterid, Request.RequestData
    FROM Request INNER JOIN
	[User] On Request.RequestMasterid = [User].Userid
    WHERE [User].UserFullName LIKE  ('%' + @MasterNameLike + '%')

END

exec GetRequestMasterName N'Парфений'