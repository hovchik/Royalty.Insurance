CREATE TABLE [dbo].[UserGarages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FileName] [nvarchar](50) NULL,
	[AssignedInsuredId] [int] NULL,
	[FileFormatId] [tinyint] NOT NULL,
	[Path] [nvarchar](1024) NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[ModifyDatetimeUtc] [datetime] NULL,
 CONSTRAINT [PK_UserGarages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserGarages] ADD  CONSTRAINT [DF_UserGarages_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[UserGarages]   ADD  CONSTRAINT [FK_UserGarages_FileFormats] FOREIGN KEY([FileFormatId])
REFERENCES [dbo].[FileFormats] ([Id])
GO

ALTER TABLE [dbo].[UserGarages] CHECK CONSTRAINT [FK_UserGarages_FileFormats]
GO

ALTER TABLE [dbo].[UserGarages]  ADD  CONSTRAINT [FK_UserGarages_Insureds] FOREIGN KEY([AssignedInsuredId])
REFERENCES [dbo].[Insureds] ([Id])
GO

ALTER TABLE [dbo].[UserGarages] CHECK CONSTRAINT [FK_UserGarages_Insureds]
GO

ALTER TABLE [dbo].[UserGarages]   ADD  CONSTRAINT [FK_UserGarages_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[UserGarages] CHECK CONSTRAINT [FK_UserGarages_Users]
GO
