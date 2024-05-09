create procedure sp_GetSale
@ID int
as
begin
	set nocount on

			select * from Sales 
			where SaleID = @ID 

	return 0
end