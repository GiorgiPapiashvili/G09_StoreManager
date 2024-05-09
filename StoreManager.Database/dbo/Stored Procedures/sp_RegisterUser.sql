create procedure sp_RegisterUser
@EmployeeId int,
@UserName nvarchar(50),
@Password nvarchar(50)
as
begin
	set nocount on;

	insert into Users(EmployeeId, UserName, Password)
	values(@EmployeeId, @UserName, HASHBYTES('SHA2_256',@Password))
	
   return 0;
end