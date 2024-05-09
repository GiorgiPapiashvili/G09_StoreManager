CREATE TABLE [dbo].[Employees] (
    [EmployeeID]     INT            IDENTITY (1, 1) NOT NULL,
    [EmployeeTypeID] INT            NOT NULL,
    [FirstName]      NVARCHAR (20)  NOT NULL,
    [LastName]       NVARCHAR (30)  NOT NULL,
    [IdentityNumber] VARCHAR (11)   NOT NULL,
    [Email]          NVARCHAR (100) NOT NULL,
    [Phone]          VARCHAR (20)   NOT NULL,
    [AddressLine1]   NVARCHAR (100) NULL,
    [Addressline2]   NVARCHAR (100) NULL,
    [ZipCode]        NVARCHAR (50)  NULL,
    [CityID]         INT            NULL,
    [CreateDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdateDate]     DATETIME       NULL,
    [IsDeleted]      BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([EmployeeID] ASC),
    FOREIGN KEY ([CityID]) REFERENCES [dbo].[Cities] ([CityID]),
    FOREIGN KEY ([EmployeeTypeID]) REFERENCES [dbo].[EmployeeTypes] ([EmployeeTypeID]),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([IdentityNumber] ASC)
);

