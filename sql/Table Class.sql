USE [Shentukq_GradeDb18]
GO

ALTER TABLE [dbo].[Shentukq_Class18] DROP CONSTRAINT [FK_Shentukq_Class18_Shentukq_Major18]
GO

DROP TABLE [dbo].[Shentukq_Class18]
GO

CREATE TABLE [dbo].[Shentukq_Class18](
	[stkq_Id18] [nchar](10) NOT NULL,
	[stkq_Name18] [nchar](10) NOT NULL,
	[stkq_MajorId18] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Shentukq_Class18] PRIMARY KEY CLUSTERED 
(
	[stkq_Id18] ASC
),

CONSTRAINT [FK_Shentukq_Class18_Shentukq_Major18] FOREIGN KEY([stkq_MajorId18])
REFERENCES [dbo].[Shentukq_Major18] ([stkq_Id18])
ON DELETE CASCADE

) ON [PRIMARY]
GO
