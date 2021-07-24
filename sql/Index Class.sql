USE [Shentukq_GradeDb18]
GO

DROP INDEX [IX_Shentukq_Class18] ON [dbo].[Shentukq_Class18]
GO

CREATE NONCLUSTERED INDEX [IX_Shentukq_Class18] ON [dbo].[Shentukq_Class18]
(
	[stkq_MajorId18] ASC
)
GO