CREATE TABLE [dbo].[EmployeeTypes] (
    [EmployeeTypeID] INT             IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (50)   NOT NULL,
    [Description]    NVARCHAR (1000) NULL,
    [CreateDate]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [UpdateDate]     DATETIME        NULL,
    [IsDeleted]      BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([EmployeeTypeID] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

