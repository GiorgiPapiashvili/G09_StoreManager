CREATE FUNCTION [dbo].[HasPermission]
(
	@EmployeeId int,
	@RoleCode char(3)
)
RETURNS bit
AS
BEGIN
		Declare @HasPermission bit =  isnull(( select Distinct 1 
							from UserRoles as ur
								inner join Roles as r on ur.RoleID = r.RoleID
							where EmployeeID = @EmployeeId and RoleCode = @RoleCode), 0);

		return @HasPermission;
END
