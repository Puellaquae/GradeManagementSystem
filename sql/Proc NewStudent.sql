go
create procedure NewStudent
@id nchar(10),
@name nchar(10),
@age int,
@gender nchar(10),
@password nchar(256),
@sourceplace nchar(10),
@classid nchar(10)
as begin
insert into Shentukq_User18(stkq_Id18, stkq_Name18, stkq_Age18,stkq_Gender18, stkq_Password18, stkq_Role18) values(@id, @name, @age, @gender, @password, 'STUDENT')
insert into Shentukq_Student18(stkq_UserId18, stkq_SourcePlace18, stkq_ClassId18) values(@id, @sourceplace, @classid)
end