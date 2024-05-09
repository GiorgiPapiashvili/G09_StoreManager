create procedure sp_DeleteCategory
@ID int
as
begin
	set nocount on;

	update Categories
	set IsDeleted = 1
	where CategoryID = @ID and IsDeleted = 0;
	
	return 0;
end