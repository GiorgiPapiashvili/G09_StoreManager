create procedure sp_UpdateUser
@EmployeeID int,
@UserName nvarchar(50),
@IsActive bit
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

	update Users
	set IsActive = @IsActive,
		UserName = @UserName
	where EmployeeId = @EmployeeID;

	return 0;
end