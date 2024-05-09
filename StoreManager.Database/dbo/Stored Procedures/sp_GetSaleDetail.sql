create procedure sp_GetSaleDetail
@ID int
as
begin
	set nocount on

			select * from SaleDetails 
			where SaleID = @ID 

	return 0
end