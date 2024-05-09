CREATE TABLE [dbo].[Users] (
    [EmployeeId] INT            NOT NULL,
    [UserName]   NVARCHAR (50)  NOT NULL,
    [Password]   VARBINARY (50) NOT NULL,
    [IsActive]   BIT            DEFAULT ((1)) NOT NULL,
    [CreateDate] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employees] ([EmployeeID]),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

