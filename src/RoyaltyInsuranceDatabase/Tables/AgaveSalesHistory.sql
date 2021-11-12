CREATE TABLE [dbo].[AgaveSalesHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ErrorMessage] [nvarchar](max) NULL,
	[AuthCode] [int] NULL,
	[ResponseCode] [int] NULL,
	[OrderID] [nvarchar](500) NULL,
	[CvvResponseCode] [nvarchar](500) NULL,
	[CreditCardScheme] [nvarchar](500) NULL,
	[TransactionID] [int] NULL,
	[TransactionTypeId] [int] NOT NULL,
	[ProcessorMessage] [nvarchar](500) NULL,
	[MerchantTransactionTime] [nvarchar](50) NULL,
	[MerchantTransactionDate] [nvarchar](50) NULL,
	[ReferenceNum] [int] NULL,
	[ResponseMessage] [nvarchar](max) NULL,
	[ProcessorCode] [nvarchar](500) NULL,
	[AvsResponseCode] [nvarchar](500) NULL,
	[TransactionTimestamp] [int] NULL,
	[AccountNumber] [int] NULL,
	[ChargeTotal] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[CardHolderName] [nvarchar](250) NULL,
	[CardHolderPhone] [nvarchar](25) NULL,
	[CardHolderAddress] [nvarchar](100) NULL,
	[CardHolderCity] [nvarchar](100) NULL,
	[CardHolderState] [nvarchar](100) NULL,
	[CardHolderEmail] [nvarchar](100) NULL,
	[CardHolderZip] [int] NULL,
	[CreateDateTimeUTC] [datetime] NOT NULL,
 CONSTRAINT [PK_AgaveSalesHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[AgaveSalesHistory] ADD  CONSTRAINT [DF_AgaveSalesHistory_CreateDateTimeUTC]  DEFAULT (getutcdate()) FOR [CreateDateTimeUTC]
GO

ALTER TABLE [dbo].[AgaveSalesHistory] ADD  CONSTRAINT [FK_AgaveSalesHistory_AgaveTransactionType] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[AgaveTransactionType] ([Id])
GO

ALTER TABLE [dbo].[AgaveSalesHistory] CHECK CONSTRAINT [FK_AgaveSalesHistory_AgaveTransactionType]
GO

ALTER TABLE [dbo].[AgaveSalesHistory]  ADD  CONSTRAINT [FK_AgaveSalesHistory_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO

ALTER TABLE [dbo].[AgaveSalesHistory] CHECK CONSTRAINT [FK_AgaveSalesHistory_Users]
GO