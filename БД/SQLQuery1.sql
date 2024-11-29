SELECT        dbo.Request.Requestid, dbo.Request.RequestData, dbo.Device.DeviceName, dbo.Defect.DefectName, dbo.[User].UserFullName, dbo.Status.StatusName, User_1.UserFullName AS Master, dbo.Request.RequestTime, 
                         dbo.Priority.PriorityName, dbo.Phase.PhaseName, dbo.Request.RequestComment
FROM            dbo.Defect INNER JOIN
                         dbo.Request ON dbo.Defect.Defectid = dbo.Request.RequestDefectid INNER JOIN
                         dbo.Device ON dbo.Request.RequestDeviceid = dbo.Device.Deviceid INNER JOIN
                         dbo.Phase ON dbo.Request.RequestPhaseid = dbo.Phase.Phaseid INNER JOIN
                         dbo.Priority ON dbo.Request.RequestPrioryid = dbo.Priority.Priorityid INNER JOIN
                         dbo.Status ON dbo.Request.RequestStatusid = dbo.Status.Statusid INNER JOIN
                         dbo.[User] ON dbo.Request.RequestClientid = dbo.[User].Userid INNER JOIN
                         dbo.Role ON dbo.[User].UserRoleid = dbo.Role.Roleid
                         dbo.[User] AS User_1 On dbo.Request.RequestMasterid = User_1.Userid 