create procedure sp_userLogin
@UserName nvarchar(50),
@Password nvarchar(50),
@SuccessfullLogin bit output
as
begin
	set nocount on;	

	declare @UserPassword varbinary(50)
	declare @Status bit

	select @UserPassword = Password, @Status = IsActive from Users
	where @UserName = UserName

	if @Status = 0
	begin
		raiserror('User is not active', 16, 3)
		return 3;
	end
	else if @UserPassword = HASHBYTES('SHA2_256', @Password)
	begin
		set @SuccessfullLogin = 1;
		return 0;
	end
	else
	begin
		raiserror('Username or password not match', 16, 2)
		set @SuccessfullLogin = 0;
		return 2;
	end
		
	return 0;
end