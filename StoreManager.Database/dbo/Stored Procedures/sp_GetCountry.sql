create procedure sp_GetCountry
@ID int
as
begin
	set nocount on

			select * from Countries 
			where CountryID = @ID and IsDeleted = 0

	return 0
end