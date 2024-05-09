create procedure sp_GetSupplier
@ID int
as
begin
	set nocount on

			select * from Suppliers 
			where SupplierID = @ID and IsDeleted = 0 

	return 0
end