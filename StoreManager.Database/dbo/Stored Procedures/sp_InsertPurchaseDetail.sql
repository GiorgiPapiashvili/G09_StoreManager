CREATE procedure [dbo].[sp_InsertPurchaseDetail]
@PurchaseID int,
@ProductID int,
@UnitPrice money,
@EmployeeID int,
@Quantity int
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

	declare @RemainQuantity int = (select Quantity from Remains);

	begin try
		begin transaction;

		insert into PurchaseDetails(PurchaseID, ProductID, UnitPrice, Quantity)
		values (@PurchaseID, @ProductID, @UnitPrice, @Quantity);
		
		if not exists(select ProductId = @ProductId from Remains)
		begin 		
			insert into Remains(ProductId, Quantity) 
			values(@ProductId, @Quantity);
		end
		else
		begin	
			update Remains 
			set UpdateDate = GetDate(), 
				Quantity = @RemainQuantity + @Quantity
			where ProductId = @ProductID;
		end

		commit transaction;
	end try
	begin catch
		rollback transaction;
		throw;
	end catch

	return 0;
end