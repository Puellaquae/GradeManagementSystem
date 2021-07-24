USE [Shentukq_GradeDb18]
GO

ALTER TABLE [dbo].[Shentukq_ClassCourse18] DROP CONSTRAINT [FK_Shentukq_ClassCourse18_Shentukq_Course18]
GO

ALTER TABLE [dbo].[Shentukq_ClassCourse18] DROP CONSTRAINT [FK_Shentukq_ClassCourse18_Shentukq_Class18]
GO

DROP TABLE [dbo].[Shentukq_ClassCourse18]
GO

CREATE TABLE [dbo].[Shentukq_ClassCourse18](
	[stkq_ClassId18] [nchar](10) NOT NULL,
	[stkq_CourseId18] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Shentukq_ClassCourse18] PRIMARY KEY CLUSTERED 
(
	[stkq_ClassId18] ASC,
	[stkq_CourseId18] ASC
),
 CONSTRAINT [FK_Shentukq_ClassCourse18_Shentukq_Class18] FOREIGN KEY([stkq_ClassId18])
REFERENCES [dbo].[Shentukq_Class18] ([stkq_Id18])
ON DELETE CASCADE,
 CONSTRAINT [FK_Shentukq_ClassCourse18_Shentukq_Course18] FOREIGN KEY([stkq_CourseId18])
REFERENCES [dbo].[Shentukq_Course18] ([stkq_Id18])
ON DELETE CASCADE
) ON [PRIMARY]
GO

