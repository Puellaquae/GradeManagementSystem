USE [Shentukq_GradeDb18]
GO

DROP TABLE [dbo].[Shentukq_User18]
GO

CREATE TABLE [dbo].[Shentukq_User18](
	[stkq_Id18] [nchar](10) NOT NULL,
	[stkq_Name18] [nchar](10) NOT NULL,
	[stkq_Age18] [int] NOT NULL,
	[stkq_Gender18] [nchar](10) NOT NULL,
	[stkq_Password18] [nchar](256) NOT NULL,
	[stkq_Role18] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Shentukq_User18] PRIMARY KEY CLUSTERED 
(
	[stkq_Id18] ASC
)
)
GO