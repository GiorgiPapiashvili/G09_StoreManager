create procedure sp_DeleteUser
@ID int
as
begin
	set nocount on;

	update Users
	set IsActive = 1
	where EmployeeId = @ID and IsActive = 0;
	
	return 0;
end