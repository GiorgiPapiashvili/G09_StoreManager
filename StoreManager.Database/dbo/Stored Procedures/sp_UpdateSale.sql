create procedure sp_UpdateSale
@SaleID int,
@Status tinyint,
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

	update Sales
	set Status = @Status
	where SaleID = @SaleID;

	return 0;
end