create procedure sp_DeleteCity
@ID int
as
begin
	set nocount on;

	update Cities
	set IsDeleted = 1
	where CityID = @ID and IsDeleted = 0;
	
	return 0;
end