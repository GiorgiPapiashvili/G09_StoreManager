create procedure sp_GetUserId
@UserName nvarchar(50),
@ID int out
as 
begin
    set nocount on;

	set @ID = (select  EmployeeId from Users where UserName = @UserName)

	return 0;
end