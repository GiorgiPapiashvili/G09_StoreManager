CREATE TABLE [dbo].[PurchaseDetails] (
    [PurchaseID] INT   NOT NULL,
    [ProductID]  INT   NOT NULL,
    [UnitPrice]  MONEY NOT NULL,
    [Quantity]   INT   NOT NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC, [PurchaseID] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[Products] ([ProductID]),
    FOREIGN KEY ([PurchaseID]) REFERENCES [dbo].[Purchases] ([PurchaseID])
);

