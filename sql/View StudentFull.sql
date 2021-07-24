USE [Shentukq_GradeDb18]
GO

DROP VIEW [dbo].[StudentFull]
GO

CREATE VIEW [dbo].[StudentFull]
AS
SELECT  dbo.Shentukq_Student18.stkq_UserId18 AS StudentId, dbo.Shentukq_User18.stkq_Name18 AS Name, 
                   dbo.Shentukq_User18.stkq_Age18 AS Age, dbo.Shentukq_User18.stkq_Gender18 AS Gender, 
                   dbo.Shentukq_Class18.stkq_Name18 AS Class, dbo.Shentukq_Student18.stkq_ClassId18 AS ClassId, 
                   dbo.Shentukq_Major18.stkq_Name18 AS Major, dbo.Shentukq_Class18.stkq_MajorId18 AS MajorId, 
                   dbo.Shentukq_Student18.stkq_Credit18 AS Credit, dbo.Shentukq_Student18.stkq_SourcePlace18 AS SourcePlace
FROM      dbo.Shentukq_Student18 INNER JOIN
                   dbo.Shentukq_User18 ON dbo.Shentukq_Student18.stkq_UserId18 = dbo.Shentukq_User18.stkq_Id18 INNER JOIN
                   dbo.Shentukq_Class18 ON dbo.Shentukq_Student18.stkq_ClassId18 = dbo.Shentukq_Class18.stkq_Id18 INNER JOIN
                   dbo.Shentukq_Major18 ON dbo.Shentukq_Class18.stkq_MajorId18 = dbo.Shentukq_Major18.stkq_Id18
GO