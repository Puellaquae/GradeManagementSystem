USE [Shentukq_GradeDb18]
GO

ALTER TABLE [dbo].[Shentukq_StudentCourse18] DROP CONSTRAINT [FK_Shentukq_StudentCourse18_Shentukq_Student18]
GO

ALTER TABLE [dbo].[Shentukq_StudentCourse18] DROP CONSTRAINT [FK_Shentukq_StudentCourse18_Shentukq_Course18]
GO

DROP TABLE [dbo].[Shentukq_StudentCourse18]
GO

CREATE TABLE [dbo].[Shentukq_StudentCourse18](
	[stkq_StudentId18] [nchar](10) NOT NULL,
	[stkq_CourseId18] [nchar](10) NOT NULL,
	[stkq_Score18] [int] NULL,
 CONSTRAINT [PK_Shentukq_StudentCourse18] PRIMARY KEY CLUSTERED 
(
	[stkq_StudentId18] ASC,
	[stkq_CourseId18] ASC
),
 CONSTRAINT [FK_Shentukq_StudentCourse18_Shentukq_Course18] FOREIGN KEY([stkq_CourseId18])
REFERENCES [dbo].[Shentukq_Course18] ([stkq_Id18])
ON DELETE CASCADE,
 CONSTRAINT [FK_Shentukq_StudentCourse18_Shentukq_Student18] FOREIGN KEY([stkq_StudentId18])
REFERENCES [dbo].[Shentukq_Student18] ([stkq_UserId18])
ON DELETE CASCADE
) ON [PRIMARY]
GO