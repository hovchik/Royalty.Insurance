GO
CREATE TABLE [dbo].[Agencies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Address] [nvarchar](255) NOT NULL,
	[City] [nvarchar](255) NOT NULL,
	[State] [nvarchar](255) NOT NULL,
	[Zip] [nvarchar](7) NOT NULL,
	[PhoneNumber] [varchar](15) NOT NULL,
	[FaxNumber] [varchar](15) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[LastModifiedUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Agencies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Agencies] ADD  CONSTRAINT [DF_Agencies_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[Agencies] ADD  CONSTRAINT [DF_Agencies_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
