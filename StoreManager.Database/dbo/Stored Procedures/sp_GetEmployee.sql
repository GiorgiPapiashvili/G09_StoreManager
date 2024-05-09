create procedure sp_GetEmployee
@ID int
as
begin
	set nocount on

			select * from Employees 
			where EmployeeID = @ID and IsDeleted = 0

	return 0
end