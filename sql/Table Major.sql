USE [Shentukq_GradeDb18]
GO

DROP TABLE [dbo].[Shentukq_Major18]
GO

CREATE TABLE [dbo].[Shentukq_Major18](
	[stkq_Id18] [nchar](10) NOT NULL,
	[stkq_Name18] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Shentukq_Major18] PRIMARY KEY CLUSTERED 
(
	[stkq_Id18] ASC
)
) ON [PRIMARY]
GO