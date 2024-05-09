create procedure sp_DeteleRole
@ID int
as
begin
	set nocount on;

		update Roles
		set IsDeleted = 1
		where RoleID = @ID and IsDeleted = 0;

	return 0;
end
