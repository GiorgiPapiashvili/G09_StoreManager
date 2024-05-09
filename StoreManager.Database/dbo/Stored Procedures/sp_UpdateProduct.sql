create procedure sp_UpdateProduct
@ProductId int,
@CategoryID int,
@ProductCode varchar(10),
@Description nvarchar(1000),
@Name nvarchar(50),
@UnitPrice money,
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

	Update Products
	set CategoryID = @CategoryID,
		ProductCode = @ProductCode,
		Name = @Name,
		Description = @Description,
		UnitPrice = @UnitPrice,
		UpdateDate = GetDate()
	where ProductID = @ProductId and IsDeleted = 0;
		
	return 0;
end