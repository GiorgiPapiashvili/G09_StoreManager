create procedure sp_InsertUser
@EmployeeID int,
@UserName nvarchar(50),
@Password nvarchar(50),
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

	insert into Users(EmployeeId, UserName, Password) 
	values (@EmployeeID, @UserName, HASHBYTES ('SHA2_256', @Password));
	
	set @ID = @@IDENTITY;

	return 0;
end