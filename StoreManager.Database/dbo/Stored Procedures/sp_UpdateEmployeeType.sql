create procedure sp_UpdateEmployeeType
@EmployeeTypeId int,
@Name nvarchar(50),
@Description nvarchar(1000),
@EmployeeID int
as
begin 
	set nocount on;

	declare @RoleCode char(3) = 'ADM';
	declare @HasPermission bit = dbo.HasPermission(@EmployeeID, @RoleCode);
	if @HasPermission = 0
	begin
		raiserror('You do not have permission to perform this operation.', 16, 401)
		return 401;
	end

	update EmployeeTypes
	set Name = @Name,
		Description = @Description,
		UpdateDate = GetDate()
	where EmployeeTypeID = @EmployeeTypeId and IsDeleted = 0;

	return 0;
end