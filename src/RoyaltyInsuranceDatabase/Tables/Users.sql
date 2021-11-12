CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [binary](64) NULL,
	[Iteration] [int] NOT NULL,
	[Salting] [binary](64) NULL,
	[IsActive] [bit] NOT NULL,
	[PersonalAvatar] [nvarchar](255) NULL,
	[ActivationExpiryDatetimeUtc] [datetime] NULL,
	[TemporaryPassword] [bit] NOT NULL,
	[ForgetPasswordCode] [varchar](6) NULL,
	[ForgetPasswordDatetimeUtc] [datetime] NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[LastModifiedUtc] [datetime] NOT NULL,
	[HomePhone] [varchar](15) NULL,
	[CellPhone] [varchar](15) NULL,
	[WorkPhone] [varchar](15) NOT NULL,
	[AdditionalPhone] [varchar](15) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserRoleId] [int] NOT NULL ,
	[FailedLoginCount] [int] NOT NULL,
    [IsBlocked] [BIT] NOT NULL
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TRIGGER UpdateLastModifiedUsers
ON [dbo].[Users] AFTER UPDATE AS BEGIN
noop:
END
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_FailedLoginCount]  DEFAULT ((0)) FOR [FailedLoginCount]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsBlocked]  DEFAULT ((0)) FOR [IsBlocked]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_TemporaryPassword]  DEFAULT ((1)) FOR [TemporaryPassword]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_CreateDatetimeUtc]  DEFAULT (getutcdate()) FOR [CreateDatetimeUtc]
GO

ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_LastModifiedUtc]  DEFAULT (getutcdate()) FOR [LastModifiedUtc]
GO


