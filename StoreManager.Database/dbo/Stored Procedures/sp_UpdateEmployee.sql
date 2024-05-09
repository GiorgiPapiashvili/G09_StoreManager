create procedure sp_UpdateEmployee
@EmployeeId int,
@EmployeeTypeID int,
@FirstName nvarchar(50),
@LastName nvarchar(50),
@IdentityNumber varchar(11),
@Email nvarchar(100),
@Phone varchar(20),
@AddressLine1 nvarchar(100),
@AddressLine2 nvarchar(100),
@ZipCode nvarchar(50),
@CityID int
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

	update Employees
	set EmployeeTypeID = @EmployeeTypeID,
		FirstName = @FirstName,
		LastName = @LastName,
		IdentityNumber = @IdentityNumber,
		Email = @Email,
		Phone = @Phone,
		AddressLine1 = @AddressLine1,
		Addressline2 = @AddressLine2,
		ZipCode = @ZipCode,
		CityID = @CityID,
		UpdateDate = GetDate()
	where EmployeeID = @EmployeeId and IsDeleted = 0;

	return 0;
end