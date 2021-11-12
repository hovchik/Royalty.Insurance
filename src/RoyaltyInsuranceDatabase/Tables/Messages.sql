GO
CREATE TABLE [dbo].[Messages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[RecipientGroupId] [int] NOT NULL,
	[Body] [nvarchar](1024) NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Messages] ADD  CONSTRAINT [DF_Messages_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO