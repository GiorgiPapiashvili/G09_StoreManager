create procedure sp_GetProduct
@ID int
as
begin
	set nocount on

			select * from Products 
			where ProductID = @ID and IsDeleted = 0

	return 0
end