CREATE TABLE [dbo].[SavedMarketingRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SavedRequest] [text] NOT NULL,
	[ShortDescription] [nvarchar](200) NULL,
	[CreatedDateUtc] [datetime] NOT NULL,
	[Hash] [int] NULL,
 CONSTRAINT [PK_SavedMarketingRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[SavedMarketingRequests] ADD  CONSTRAINT [DF_SavedMarketingRequests_CreatedDateUtc]  DEFAULT (getutcdate()) FOR [CreatedDateUtc]
GO

ALTER TABLE [dbo].[SavedMarketingRequests] ADD  CONSTRAINT [FK_SavedMarketingRequests_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[SavedMarketingRequests] CHECK CONSTRAINT [FK_SavedMarketingRequests_Users]
GO