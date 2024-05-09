create procedure sp_InsertEmployee
@EmployeeTypeID int,
@FirstName nvarchar(20),
@LastName nvarchar(30),
@IdentityNumber varchar(11),
@Email nvarchar(100),
@Phone varchar(20),
@AddressLine1 nvarchar(100),
@AddressLine2 nvarchar(100),
@ZipCode nvarchar(50),
@CityID int,
@EmployeeID int,
@ID int out
as
begin
	set nocount on;

	declare @RoleCode char(3) = 'ADM';
	declare @HasPermission bit = dbo.HasPermission(@EmployeeID, @RoleCode);
	if @HasPermission = 0
	begin
		raiserror('You do not have permission to perform this operation.', 16, 401)
		return 401;
	end
 
	insert into Employees(EmployeeTypeID, FirstName, LastName, IdentityNumber, Email, Phone, AddressLine1, Addressline2, CityID)
	values (@EmployeeTypeID, @FirstName, @LastName, @IdentityNumber, @Email, @Phone, @AddressLine1, @AddressLine2, @CityID)

	set @ID = @@IDENTITY;

	return 0;
end