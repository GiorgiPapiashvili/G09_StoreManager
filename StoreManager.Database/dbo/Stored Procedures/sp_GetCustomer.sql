create procedure sp_GetCustomer
@ID int
as
begin
	set nocount on

			select * from Customers 
			where CustomerID = @ID and IsDeleted = 0

	return 0
end