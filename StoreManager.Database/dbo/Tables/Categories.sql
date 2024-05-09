CREATE TABLE [dbo].[Categories] (
    [CategoryID]  INT             IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (1000) NULL,
    [CreateDate]  DATETIME        DEFAULT (getdate()) NOT NULL,
    [UpdateDate]  DATETIME        NULL,
    [IsDeleted]   BIT             DEFAULT ((0)) NULL,
    PRIMARY KEY CLUSTERED ([CategoryID] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

