create procedure sp_DeleteProduct
@ID int
as
begin

	set nocount on;

		Update Products
		set IsDeleted = 1
		where ProductID = @ID and IsDeleted = 0;
		
	return 0;
end