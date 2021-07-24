USE [Shentukq_GradeDb18]
GO

DROP TABLE [dbo].[Shentukq_Course18]
GO

CREATE TABLE [dbo].[Shentukq_Course18](
	[stkq_Id18] [nchar](10) NOT NULL,
	[stkq_Name18] [nchar](10) NOT NULL,
	[stkq_Semester18] [nchar](10) NOT NULL,
	[stkq_Hours18] [int] NOT NULL,
	[stkq_Credit18] [int] NOT NULL,
	[stkq_ExamMethod18] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Shentukq_Course18] PRIMARY KEY CLUSTERED 
(
	[stkq_Id18] ASC
)
) ON [PRIMARY]
GO