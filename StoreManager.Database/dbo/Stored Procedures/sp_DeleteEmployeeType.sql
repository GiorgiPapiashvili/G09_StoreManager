create procedure sp_DeleteEmployeeType
@ID int
as
begin
	
	set nocount on;

		update EmployeeTypes
		set IsDeleted = 1 where EmployeeTypeID = @ID and IsDeleted = 0;

	return 0;
end