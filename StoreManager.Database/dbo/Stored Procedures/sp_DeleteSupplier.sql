create procedure sp_DeleteSupplier
@ID int
as
begin
	
	set nocount on;

		update Suppliers
		set IsDeleted = 1
		where SupplierID = @ID and IsDeleted = 0;

	return 0;
end