go
create procedure NewTeacher
@id nchar(10),
@name nchar(10),
@age int,
@gender nchar(10),
@password nchar(256),
@title nchar(10),
@tele nchar(10)
as begin
insert into Shentukq_User18(stkq_Id18, stkq_Name18, stkq_Age18,stkq_Gender18, stkq_Password18, stkq_Role18) values(@id, @name, @age, @gender, @password, 'STUDENT')
insert into Shentukq_Teacher18(stkq_UserId18, stkq_Title18, stkq_Telephone18) values(@id, @title, @tele)
end