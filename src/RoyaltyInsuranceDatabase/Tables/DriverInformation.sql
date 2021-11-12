CREATE TABLE [dbo].[DriverInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DriverName] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[LicenseNumber] [nvarchar](50) NOT NULL,
	[StateId] [int] NOT NULL,
	[DateHired] [datetime] NOT NULL,
	[YearOfExperiance] [int] NOT NULL,
	[Accidents] [ntext] NULL,
	[InsuredId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[LastModifiedUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_DriverInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO