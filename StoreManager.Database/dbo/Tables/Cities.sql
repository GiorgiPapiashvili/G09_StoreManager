CREATE TABLE [dbo].[Cities] (
    [CityID]     INT            IDENTITY (1, 1) NOT NULL,
    [CountryID]  INT            NOT NULL,
    [Name]       NVARCHAR (100) NOT NULL,
    [CreateDate] DATETIME       DEFAULT (getdate()) NOT NULL,
    [UpdateDate] DATETIME       NULL,
    [IsDeleted]  BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([CityID] ASC),
    FOREIGN KEY ([CountryID]) REFERENCES [dbo].[Countries] ([CountryID])
);

