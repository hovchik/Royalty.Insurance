GO
CREATE TABLE [dbo].[Groups](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL DEFAULT (1),
	[GroupTypeId] [tinyint] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[LastModifiedUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO