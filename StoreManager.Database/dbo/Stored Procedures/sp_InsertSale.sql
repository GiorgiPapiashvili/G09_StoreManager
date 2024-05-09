create procedure sp_InsertSale
@EmployeeID int,
@CustomerID int,
@Status tinyint,
@ID int out
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

	insert into Sales(EmployeeID, CustomerID, Status) 
	values (@EmployeeID, @CustomerID, @Status);

	set @ID = @@IDENTITY;
		
	return 0;
end