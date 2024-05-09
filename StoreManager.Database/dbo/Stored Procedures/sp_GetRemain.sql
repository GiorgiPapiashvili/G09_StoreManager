create procedure sp_GetRemain
@ID int
as
begin
	set nocount on

			select * from Remains 
			where RemainID = @ID 

	return 0
end