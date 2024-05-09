CREATE TABLE [dbo].[SaleDetails] (
    [SaleID]    INT   NOT NULL,
    [ProductID] INT   NOT NULL,
    [UnitPrice] MONEY NOT NULL,
    [Quantity]  INT   NOT NULL,
    PRIMARY KEY CLUSTERED ([SaleID] ASC, [ProductID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]),
    FOREIGN KEY ([SaleID]) REFERENCES [dbo].[Sales] ([SaleID])
);

