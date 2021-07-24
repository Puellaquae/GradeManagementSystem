USE [Shentukq_GradeDb18]
GO

ALTER TABLE [dbo].[Shentukq_Teacher18] DROP CONSTRAINT [FK_Shentukq_Teacher18_Shentukq_User18]
GO

DROP TABLE [dbo].[Shentukq_Teacher18]
GO

CREATE TABLE [dbo].[Shentukq_Teacher18](
	[stkq_UserId18] [nchar](10) NOT NULL,
	[stkq_Title18] [nchar](10) NOT NULL,
	[stkq_Telephone18] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Shentukq_Teacher18] PRIMARY KEY CLUSTERED 
(
	[stkq_UserId18] ASC
),
 CONSTRAINT [FK_Shentukq_Teacher18_Shentukq_User18] FOREIGN KEY([stkq_UserId18])
REFERENCES [dbo].[Shentukq_User18] ([stkq_Id18])
ON DELETE CASCADE
) ON [PRIMARY]
GO
