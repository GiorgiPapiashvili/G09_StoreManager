create procedure sp_GetPurchase
@ID int
as
begin
	set nocount on

			select * from Purchases 
			where PurchaseID = @ID 

	return 0
end