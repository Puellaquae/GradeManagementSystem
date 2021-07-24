USE [Shentukq_GradeDb18]
GO

DROP INDEX [IX_Shentukq_Student18] ON [dbo].[Shentukq_Student18]
GO

CREATE NONCLUSTERED INDEX [IX_Shentukq_Student18] ON [dbo].[Shentukq_Student18]
(
	[stkq_ClassId18] ASC
)
GO