create procedure sp_UpdateRemain
@ID int,
@Quantity int,
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
	
	update Remains
	set Quantity = @Quantity,
	UpdateDate = GETDATE()
	where RemainID = @ID and IsDeleted = 0;

	return 0;
end