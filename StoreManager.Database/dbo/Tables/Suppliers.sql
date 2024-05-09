CREATE TABLE [dbo].[Suppliers] (
    [SupplierID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (100) NOT NULL,
    [TaxCode]      VARCHAR (11)   NOT NULL,
    [Email]        NVARCHAR (100) NOT NULL,
    [Phone]        VARCHAR (20)   NOT NULL,
    [AddressLine1] NVARCHAR (100) NULL,
    [Addressline2] NVARCHAR (100) NULL,
    [ZipCode]      NVARCHAR (50)  NULL,
    [CityID]       INT            NULL,
    [CreateDate]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdateDate]   DATETIME       NULL,
    [IsDeleted]    BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([SupplierID] ASC),
    FOREIGN KEY ([CityID]) REFERENCES [dbo].[Cities] ([CityID]),
    UNIQUE NONCLUSTERED ([Email] ASC)
);

