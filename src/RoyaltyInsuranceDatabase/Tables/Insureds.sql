CREATE TABLE [dbo].[Insureds](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SocialSecurityNumber] [varchar](50) NOT NULL,
	[StateNumber] [nvarchar](50) NOT NULL,
	[MotorCarrierNumber] [nvarchar](120) NOT NULL,
	[IsFilings] [bit] NOT NULL,
	[InsuredStatusId] [int] NULL,
	[YearsInsured] [int] NOT NULL,
	[FartherState] [tinyint] NOT NULL,
	[DBA] [nvarchar](100) NULL,
	[MailingStateId] [int] NOT NULL,
	[MailingCityId] [int] NOT NULL,
	[MailingZipCodeId] [int] NOT NULL,
	[MailingStreetAddress] [nvarchar](256) NOT NULL,
	[MailingPhone] [varchar](15) NULL,
	[MailingEmail] [nvarchar](256) NULL,
	[MailingName] [nvarchar](256) NOT NULL,
	[GaragingStateId] [int] NOT NULL,
	[GaragingCityId] [int] NOT NULL,
	[GaragingZipCodeId] [int] NOT NULL,
	[GaragingStreetAddress] [nvarchar](256) NOT NULL,
	[GaragingPhone] [varchar](15) NULL,
	[GaragingEmail] [nvarchar](256) NULL,
	[GaragingName] [nvarchar](256) NOT NULL,
	[LegalStatusId] [int] NULL,
	[DotNumber] [int] NULL,
	[CreateBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[LastModifiedUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_Insureds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TRIGGER UpdateLastModifiedInsureds
ON [dbo].[Insureds] AFTER UPDATE AS BEGIN
noop:
END
GO
