create trigger AutoCreditUpdare on Shentukq_StudentCourse18
after update
as
begin
	declare @sid nchar(10)
	declare @cid nchar(10)
	declare @newscore int
	declare @oldscore int
	select @sid = stkq_StudentId18 from inserted
	select @cid = stkq_CourseId18 from inserted
	select @newscore = stkq_Score18 from inserted
	select @oldscore = stkq_Score18 from deleted
	if (@oldscore < 60 and @newscore >= 60)
		update Shentukq_Student18 
		set stkq_Credit18 = stkq_Credit18 + (
			select stkq_Credit18 from Shentukq_Course18 where stkq_Id18 = @cid
		)
		where stkq_UserId18 = @sid
	else if (@oldscore >= 60 and @newscore < 60)
		update Shentukq_Student18 
		set stkq_Credit18 = stkq_Credit18 - (
			select stkq_Credit18 from Shentukq_Course18 where stkq_Id18 = @cid
		)
		where stkq_UserId18 = @sid
	end