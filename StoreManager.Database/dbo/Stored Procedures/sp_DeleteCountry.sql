create procedure sp_DeleteCountry
@ID int
as
begin
	set nocount on;

	update Countries
	set IsDeleted = 1 where CountryID = @ID and IsDeleted = 0;

	return 0;
end