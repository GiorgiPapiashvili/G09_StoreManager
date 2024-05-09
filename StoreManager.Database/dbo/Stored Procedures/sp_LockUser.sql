create procedure sp_LockUser
@UserName nvarchar(50),
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

	update Users set IsActive = 0
	where @UserName = UserName and IsActive = 1;
	
	return 0;
end