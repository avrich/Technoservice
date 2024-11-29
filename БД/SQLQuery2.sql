USE [DBTsapkovaDemoExam21]
GO
/****** Object:  StoredProcedure [dbo].[GetRequestMasterid]    Script Date: 08.10.2024 7:46:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetRequestMasterid] @Masterid int = 0
AS
BEGIN
	SELECT        Requestid, RequestMasterid, RequestData
FROM            Request

WHERE (RequestMasterid= @Masterid)

END

exec GetRequestMasterid 105
