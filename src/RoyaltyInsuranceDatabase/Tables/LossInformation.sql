CREATE TABLE [dbo].[LossInformation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EffectiveDate] [datetime] NOT NULL,
	[ExpireDate] [datetime] NOT NULL,
	[InsuranceName] [nvarchar](50) NOT NULL,
	[LesseeName] [nvarchar](50) NULL,
	[PoliceNumber] [nvarchar](50) NOT NULL,
	[LesseeMCNumber] [nvarchar](50) NULL,
	[NumberOfClaims] [nvarchar](50) NOT NULL,
	[Comments] [ntext] NULL,
	[InsuredId] [int] NOT NULL,
 CONSTRAINT [PK_LossInformation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO