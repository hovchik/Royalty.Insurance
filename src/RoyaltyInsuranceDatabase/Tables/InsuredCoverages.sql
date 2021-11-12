CREATE TABLE [dbo].[InsuredCoverages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InsuredId] [int] NOT NULL,
	[CoverageId] [int] NOT NULL,
	[Limit] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[LastModifiedUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_InsuredCoverages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[InsuredCoverages] ADD  CONSTRAINT [DF_InsuredCoverages_Limit]  DEFAULT ((0)) FOR [Limit]
GO