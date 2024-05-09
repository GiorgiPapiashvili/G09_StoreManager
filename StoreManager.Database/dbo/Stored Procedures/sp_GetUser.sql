create procedure sp_GetUser
@ID int
as
begin
	set nocount on

		select * from Users 
		where EmployeeId = @ID 

	return 0
end