create trigger AutoCreditInsert on Shentukq_StudentCourse18
after insert
as
begin
	declare @sid nchar(10)
	declare @cid nchar(10)
	declare @score int
	select @sid = stkq_StudentId18 from inserted
	select @cid = stkq_CourseId18 from inserted
	select @score = stkq_Score18 from inserted
	if (@score >= 60)
		update Shentukq_Student18 
		set stkq_Credit18 = stkq_Credit18 + (
			select stkq_Credit18 from Shentukq_Course18 where stkq_Id18 = @cid
		)
		where stkq_UserId18 = @sid
	end