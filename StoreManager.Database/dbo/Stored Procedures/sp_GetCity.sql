create procedure sp_GetCity
@ID int
as
begin
	set nocount on

			select * from Cities 
			where CityID = @ID and IsDeleted = 0

	return 0
end