create procedure sp_UpdateCity
@CityId int,
@CountryID int,
@Name nvarchar(100),
@EmployeeID int
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

	update Cities
	set CountryID = @CountryID,
		Name = @Name,
		UpdateDate = GetDate()
	where CityID = @CityId and IsDeleted = 0;
	
	return 0;
end