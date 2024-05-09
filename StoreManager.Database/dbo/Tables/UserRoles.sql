CREATE TABLE [dbo].[UserRoles] (
    [RoleID]     INT NOT NULL,
    [EmployeeID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleID] ASC, [EmployeeID] ASC),
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Users] ([EmployeeId]),
    FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([RoleID])
);

