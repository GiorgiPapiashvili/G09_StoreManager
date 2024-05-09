create procedure sp_UpdatePurchase
@PurchaseId int,
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

	update Purchases
	set Status = @Status
	where PurchaseID = @PurchaseId;

	return 0;
end