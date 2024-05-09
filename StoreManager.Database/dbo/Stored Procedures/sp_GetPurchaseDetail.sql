create procedure sp_GetPurchaseDetail
@ID int
as
begin
	set nocount on

			select * from PurchaseDetails 
			where PurchaseID = @ID 

	return 0
end