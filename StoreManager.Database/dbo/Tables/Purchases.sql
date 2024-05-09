CREATE TABLE [dbo].[Purchases] (
    [PurchaseID]   INT      IDENTITY (1, 1) NOT NULL,
    [SupplierID]   INT      NOT NULL,
    [EmployeeID]   INT      NOT NULL,
    [Status]       TINYINT  DEFAULT ((0)) NOT NULL,
    [PurchaseDate] DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([PurchaseID] ASC),
    CONSTRAINT [check_Purchase_Status] CHECK ([Status]=(2) OR [Status]=(1) OR [Status]=(0)),
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID]),
    FOREIGN KEY ([SupplierID]) REFERENCES [dbo].[Suppliers] ([SupplierID])
);

