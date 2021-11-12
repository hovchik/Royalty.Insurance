CREATE TABLE [dbo].[UserPhoneCallHistory](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserPhoneId] [int] NOT NULL,
	[InitialCallTypeId] [int] NOT NULL,
	[CurrentCallTypeId] [int] NOT NULL,
	[CallerNumber] [nvarchar](15) NOT NULL,
	[CallId] [nvarchar](15) NOT NULL,
	[Extension] [int] NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,
	[EndDatetimeUtc] [datetime] NULL,
	[CallerName] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserPhoneCallHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO