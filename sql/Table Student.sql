USE [Shentukq_GradeDb18]
GO

ALTER TABLE [dbo].[Shentukq_Student18] DROP CONSTRAINT [FK_Shentukq_Student18_Shentukq_User18]
GO

ALTER TABLE [dbo].[Shentukq_Student18] DROP CONSTRAINT [FK_Shentukq_Student18_Shentukq_Class18]
GO

ALTER TABLE [dbo].[Shentukq_Student18] DROP CONSTRAINT [DF_Shentukq_Student18_stkq_Credit18]
GO

DROP TABLE [dbo].[Shentukq_Student18]
GO

CREATE TABLE [dbo].[Shentukq_Student18](
	[stkq_UserId18] [nchar](10) NOT NULL,
	[stkq_SourcePlace18] [nchar](10) NOT NULL,
	[stkq_ClassId18] [nchar](10) NOT NULL,
	[stkq_Credit18] [int] NULL,
 CONSTRAINT [PK_Shentukq_Student18_1] PRIMARY KEY CLUSTERED 
(
	[stkq_UserId18] ASC
), 
 CONSTRAINT [DF_Shentukq_Student18_stkq_Credit18]  DEFAULT ((0)) FOR [stkq_Credit18],
 CONSTRAINT [FK_Shentukq_Student18_Shentukq_Class18] FOREIGN KEY([stkq_ClassId18])
REFERENCES [dbo].[Shentukq_Class18] ([stkq_Id18])
ON DELETE CASCADE,
 CONSTRAINT [FK_Shentukq_Student18_Shentukq_User18] FOREIGN KEY([stkq_UserId18])
REFERENCES [dbo].[Shentukq_User18] ([stkq_Id18])
ON DELETE CASCADE
) ON [PRIMARY]
GO