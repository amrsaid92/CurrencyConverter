CREATE TABLE [dbo].[Users] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (MAX) NULL,
    [Email]      NVARCHAR (MAX) NULL,
    [Password]   NVARCHAR (MAX) NULL,
    [Roles]      NVARCHAR (MAX) NULL,
    [IsActive]   BIT            NOT NULL,
    [IsDeleted]  BIT            NOT NULL,
    [CreatedAt]  DATETIME2 (7)  NOT NULL,
    [ModifiedAt] DATETIME2 (7)  NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

