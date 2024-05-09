CREATE TABLE [dbo].[Sales] (
    [SaleID]     INT      IDENTITY (1, 1) NOT NULL,
    [EmployeeID] INT      NOT NULL,
    [CustomerID] INT      NOT NULL,
    [Status]     TINYINT  DEFAULT ((0)) NOT NULL,
    [SaleDate]   DATETIME DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([SaleID] ASC),
    CONSTRAINT [check_Sales_Status] CHECK ([Status]=(2) OR [Status]=(1) OR [Status]=(0)),
    FOREIGN KEY ([CustomerID]) REFERENCES [dbo].[Customers] ([CustomerID]),
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

