create procedure sp_DeleteEmployee
@ID int
as
begin
	
	set nocount on;

		update Employees
		set IsDeleted = 1
		where EmployeeID = @ID and IsDeleted = 0;

	return 0;
end