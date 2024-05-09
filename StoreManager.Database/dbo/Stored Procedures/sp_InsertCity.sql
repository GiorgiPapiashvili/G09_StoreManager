create procedure sp_InsertCity
@CountryID int,
@Name nvarchar(100),
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

	insert into Cities(CountryID, Name)
	values(@CountryID, @Name);

	set @ID = @@IDENTITY;
	
	return 0;
end