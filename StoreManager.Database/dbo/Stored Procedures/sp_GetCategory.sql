create procedure sp_GetCategory
@ID int
as
begin
	set nocount on

			select * from Categories 
			where CategoryID = @ID and IsDeleted = 0

	return 0
end