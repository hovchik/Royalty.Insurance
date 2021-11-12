GO
CREATE TABLE [dbo].[UnreadMessages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MessageId] [bigint] NOT NULL,
	[ReadUserId] [int] NOT NULL,
	[SendUserId]  [int] NOT NULL,
	[GroupId] [int] NOT NULL,
	[ReadDatetimeUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_UnreadMessages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UnreadMessages] ADD  CONSTRAINT [DF_ReadMessages_ReadDatetimeUtc]  DEFAULT (getutcdate()) FOR [ReadDatetimeUtc]
GO
