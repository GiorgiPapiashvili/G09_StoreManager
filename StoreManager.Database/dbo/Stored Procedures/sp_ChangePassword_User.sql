create procedure sp_ChangePassword_User
@UserName nvarchar(50),
@CurrentPassword nvarchar(50),
@NewPassword nvarchar(50)
as
begin
	set nocount on;

	if(@UserName != (select username from users))
		begin
			raiserror('such user not found', 16,1)
			return 1
		end
	else if(HASHBYTES('SHA2_256', @CurrentPassword) != (select Password from users where username = @UserName))
		begin	
			raiserror('Password not match', 16,1)
			return 1
		end

	update Users set Password = HASHBYTES('SHA2_256', @NewPassword) where UserName = @UserName

   return 0;
end