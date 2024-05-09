create procedure sp_GetRole
@ID int
as
begin
	set nocount on

		select * from Roles
		where RoleID = @ID 

	return 0
end
