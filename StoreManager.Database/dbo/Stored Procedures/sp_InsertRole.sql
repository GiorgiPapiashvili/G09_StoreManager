create procedure sp_InsertRole
@RoleName nvarchar(50),
@RoleCode char(3),
@EmployeeID int,
@ID int out
as
begin
	set nocount on;

	declare @EmployeeRoleCode char(3) = 'ADM';
	declare @HasPermission bit = dbo.HasPermission(@EmployeeID, @EmployeeRoleCode);
	if @HasPermission = 0
	begin
		raiserror('You do not have permission to perform this operation.', 16, 401)
		return 401;
	end

	insert into Roles(RoleName, RoleCode)
	values(@RoleName, @RoleCode)

	set @ID = @@IDENTITY;

	return 0;
end
