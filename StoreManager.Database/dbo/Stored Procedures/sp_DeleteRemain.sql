create procedure sp_DeleteRemain
@ID int
as
begin
	set nocount on;

		update Remains
		set IsDeleted = 1
		where RemainID = @ID and IsDeleted = 0;

	return 0;
end