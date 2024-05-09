CREATE TABLE [dbo].[Countries] (
    [CountryID]  INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL,
    [CreateDate] DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdateDate] DATETIME       NULL,
    [IsDeleted]  BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CountryID] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

