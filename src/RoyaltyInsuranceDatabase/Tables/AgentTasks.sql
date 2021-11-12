CREATE TABLE [dbo].[AgentTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[AssigneeId] [int] NULL,
	[AgentTaskStatusId] [int] NOT NULL,
	[AgentTaskTypeId] [tinyint] NOT NULL,
	[CanceledReason] [nvarchar](255) NULL,
	[DueDatetimeUtc] [datetime] NULL,	
	[CompletedDatetimeUtc] [datetime] NULL,
	[InsuredId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
	[CreateDatetimeUtc] [datetime] NOT NULL,	
	[LastModifiedUtc] [datetime] NOT NULL,
 CONSTRAINT [PK_AgentTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO