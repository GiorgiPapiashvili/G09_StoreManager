create procedure sp_UpdateRole
@ID int,
@RoleName nvarchar(50),
@RoleCode varchar(3),
@EmployeeID int
as
begin
	set nocount on;

	declare @RoleCodeForUpdate char(3) = 'ADM';
	declare @HasPermission bit = dbo.HasPermission(@EmployeeID, @RoleCode);
	if @HasPermission = 0
	begin
		raiserror('You do not have permission to perform this operation.', 16, 401)
		return 401;
	end

	update Roles
	set RoleName = @RoleName,
	RoleCode = @RoleCode,
	UpdateDate = GETDATE()
	where RoleID = @ID and IsDeleted = 0;

	return 0;
end
