CREATE TABLE [dbo].[CurrencyLogs] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [IsActive]   BIT           NOT NULL,
    [IsDeleted]  BIT           NOT NULL,
    [CreatedAt]  DATETIME2 (7) NOT NULL,
    [ModifiedAt] DATETIME2 (7) NULL,
    CONSTRAINT [PK_CurrencyLogs] PRIMARY KEY CLUSTERED ([Id] ASC)
);

