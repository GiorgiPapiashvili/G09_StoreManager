CREATE TABLE [dbo].[Products] (
    [ProductID]   INT             IDENTITY (1, 1) NOT NULL,
    [CategoryID]  INT             NOT NULL,
    [ProductCode] VARCHAR (10)    NOT NULL,
    [Name]        NVARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (1000) NULL,
    [UnitPrice]   MONEY           NOT NULL,
    [CreateDate]  DATETIME        DEFAULT (getdate()) NOT NULL,
    [UpdateDate]  DATETIME        NULL,
    [IsDeleted]   BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    FOREIGN KEY ([CategoryID]) REFERENCES [dbo].[Categories] ([CategoryID]),
    UNIQUE NONCLUSTERED ([ProductCode] ASC)
);

