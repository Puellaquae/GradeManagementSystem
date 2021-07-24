create trigger AutoCreditDelete on Shentukq_StudentCourse18
after delete
as
begin
	declare @sid nchar(10)
	declare @cid nchar(10)
	declare @oldscore int
	select @sid = stkq_StudentId18 from deleted
	select @cid = stkq_CourseId18 from deleted
	select @oldscore = stkq_Score18 from deleted
	if (@oldscore >= 60)
		update Shentukq_Student18 
		set stkq_Credit18 = stkq_Credit18 - (
			select stkq_Credit18 from Shentukq_Course18 where stkq_Id18 = @cid
		)
		where stkq_UserId18 = @sid
	end