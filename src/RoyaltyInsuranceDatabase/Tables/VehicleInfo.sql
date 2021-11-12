CREATE TABLE [dbo].[VehicleInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Year] [int] NOT NULL,
	[Make] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[GVW] [int] NOT NULL,
	[ActualValue] [int] NOT NULL,
	[Radius] [nvarchar](10) NOT NULL,
	[VIN] [nvarchar](50) NOT NULL,
	[Comments] [ntext] NULL,
	[IsTruck] [bit] NOT NULL,
 CONSTRAINT [PK_VehicleInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
