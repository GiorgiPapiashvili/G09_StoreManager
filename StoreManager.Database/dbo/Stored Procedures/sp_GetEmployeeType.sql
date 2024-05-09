create procedure sp_GetEmployeeType
@ID int
as
begin
	set nocount on

			select * from EmployeeTypes 
			where EmployeeTypeID = @ID and IsDeleted = 0

	return 0
end