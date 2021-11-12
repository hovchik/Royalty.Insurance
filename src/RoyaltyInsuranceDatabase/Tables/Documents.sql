CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsuredId] [int] NULL,
	[DocumentName] [nvarchar](100) NOT NULL,
	[Path] [nvarchar](1024) NOT NULL,
	[GroupId] [nvarchar](100) NULL,
	[DriveItemId] [nvarchar](100) NULL,
	[DocumentTypeId] TINYINT NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[DeletedBy] [int] NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[LastModifiedUtc] [datetime] NOT NULL,
	[DeleteDatetimeUtc] [datetime] NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO

ALTER TABLE [dbo].[Documents]  ADD  CONSTRAINT [FK_Documents_Insureds] FOREIGN KEY([InsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO

ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Insureds]
GO

ALTER TABLE [dbo].[Documents]   ADD  CONSTRAINT [FK_Documents_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Documents]   ADD  CONSTRAINT [FK_Documents_DocumentType] FOREIGN KEY([DocumentTypeId])
REFERENCES [dbo].[DocumentType] ([Id])
GO
ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Users]
GO

ALTER TABLE [dbo].[Documents]   ADD  CONSTRAINT [FK_Documents_Users1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[Documents] CHECK CONSTRAINT [FK_Documents_Users1]
GO
