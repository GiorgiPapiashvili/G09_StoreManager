create procedure sp_DeleteCustomer
@ID int
as
begin
	set nocount on;

	update Customers
	set IsDeleted = 1 
	where CustomerID = @ID and IsDeleted = 0;

	return 0;
end