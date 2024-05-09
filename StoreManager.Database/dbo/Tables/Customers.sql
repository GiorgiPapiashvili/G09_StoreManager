CREATE TABLE [dbo].[Customers] (
    [CustomerID]   INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]    NVARCHAR (50)  NOT NULL,
    [LastName]     NVARCHAR (50)  NOT NULL,
    [Email]        NVARCHAR (100) NULL,
    [Phone]        VARCHAR (20)   NULL,
    [AddressLine1] NVARCHAR (100) NULL,
    [Addressline2] NVARCHAR (100) NULL,
    [ZipCode]      NVARCHAR (50)  NULL,
    [CityID]       INT            NULL,
    [CreateDate]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdateDate]   DATETIME       NULL,
    [IsDeleted]    BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CustomerID] ASC),
    FOREIGN KEY ([CityID]) REFERENCES [dbo].[Cities] ([CityID]),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

