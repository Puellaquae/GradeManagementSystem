USE [Shentukq_GradeDb18]
GO

ALTER TABLE [dbo].[Shentukq_TeacherCourse18] DROP CONSTRAINT [FK_Shentukq_TeacherCourse18_Shentukq_Teacher18]
GO

ALTER TABLE [dbo].[Shentukq_TeacherCourse18] DROP CONSTRAINT [FK_Shentukq_TeacherCourse18_Shentukq_Course18]
GO

DROP TABLE [dbo].[Shentukq_TeacherCourse18]
GO

CREATE TABLE [dbo].[Shentukq_TeacherCourse18](
	[stkq_TeacherId18] [nchar](10) NOT NULL,
	[stkq_CourseId18] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Shentukq_TeacherCourse18] PRIMARY KEY CLUSTERED 
(
	[stkq_TeacherId18] ASC,
	[stkq_CourseId18] ASC
),
 CONSTRAINT [FK_Shentukq_TeacherCourse18_Shentukq_Course18] FOREIGN KEY([stkq_CourseId18])
REFERENCES [dbo].[Shentukq_Course18] ([stkq_Id18])
ON DELETE CASCADE,
 CONSTRAINT [FK_Shentukq_TeacherCourse18_Shentukq_Teacher18] FOREIGN KEY([stkq_TeacherId18])
REFERENCES [dbo].[Shentukq_Teacher18] ([stkq_UserId18])
ON DELETE CASCADE
) ON [PRIMARY]
GO