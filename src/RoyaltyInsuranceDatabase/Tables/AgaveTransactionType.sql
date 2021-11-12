CREATE TABLE [dbo].[AgaveTransactionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_AgaveTransactionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AgaveTransactionType] ADD  CONSTRAINT [FK_AgaveTransactionType_AgaveTransactionType] FOREIGN KEY([Id])
REFERENCES [dbo].[AgaveTransactionType] ([Id])
GO

ALTER TABLE [dbo].[AgaveTransactionType] CHECK CONSTRAINT [FK_AgaveTransactionType_AgaveTransactionType]
GO
