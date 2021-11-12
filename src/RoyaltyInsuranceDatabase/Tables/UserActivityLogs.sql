CREATE TABLE [dbo].[UserActivityLogs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SessionId] [uniqueidentifier] NOT NULL,
	[DeviceIp] [nvarchar](50) NOT NULL,
	[RefreshToken] [nvarchar](64) NOT NULL,
	[RefreshTokenExpireAt] [datetime] NOT NULL,
	[LogInDatetimeUtc] [datetime] NOT NULL,
	[LogOutDatetimeUtc] [datetime] NULL,
 CONSTRAINT [PK_UserActivityLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO