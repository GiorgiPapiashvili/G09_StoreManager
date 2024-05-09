CREATE TABLE [dbo].[Roles] (
    [RoleID]     INT           IDENTITY (1, 1) NOT NULL,
    [RoleName]   NVARCHAR (50) NOT NULL,
    [RoleCode]   CHAR (3)      NOT NULL,
    [CreateDate] DATETIME      DEFAULT (getdate()) NOT NULL,
    [UpdateDate] DATETIME      NULL,
    [IsDeleted]  BIT           DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([RoleID] ASC)
);

