/****** Object:  Database [GeicoTaskAppTest]    Script Date: 5/5/2022 4:41:32 AM ******/
CREATE DATABASE [GeicoTaskAppTest]  (EDITION = 'Standard', SERVICE_OBJECTIVE = 'S0', MAXSIZE = 250 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS;
GO
ALTER DATABASE [GeicoTaskAppTest] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [GeicoTaskAppTest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET ARITHABORT OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GeicoTaskAppTest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GeicoTaskAppTest] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [GeicoTaskAppTest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GeicoTaskAppTest] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GeicoTaskAppTest] SET  MULTI_USER 
GO
ALTER DATABASE [GeicoTaskAppTest] SET ENCRYPTION ON
GO
ALTER DATABASE [GeicoTaskAppTest] SET QUERY_STORE = ON
GO
ALTER DATABASE [GeicoTaskAppTest] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[TaskPriority]    Script Date: 5/5/2022 4:41:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskPriority](
	[PId] [int] IDENTITY(1,1) NOT NULL,
	[PType] [varchar](10) NOT NULL,
	[PDescription] [varchar](max) NULL,
 CONSTRAINT [PK_Priority] PRIMARY KEY CLUSTERED 
(
	[PId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskStatus]    Script Date: 5/5/2022 4:41:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskStatus](
	[SId] [int] IDENTITY(1,1) NOT NULL,
	[SType] [varchar](10) NOT NULL,
	[SDescription] [varchar](max) NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[SId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskTable]    Script Date: 5/5/2022 4:41:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskTable](
	[TId] [int] IDENTITY(1,1) NOT NULL,
	[TName] [varchar](100) NOT NULL,
	[Description] [varchar](max) NULL,
	[TDueDate] [datetime] NULL,
	[TPriorityId] [int] NULL,
	[TStatusId] [int] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[TId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TaskPriority] ON 
GO
INSERT [dbo].[TaskPriority] ([PId], [PType], [PDescription]) VALUES (1, N'High', N'high')
GO
INSERT [dbo].[TaskPriority] ([PId], [PType], [PDescription]) VALUES (2, N'Middle', N'middle')
GO
INSERT [dbo].[TaskPriority] ([PId], [PType], [PDescription]) VALUES (3, N'Low', N'low')
GO
SET IDENTITY_INSERT [dbo].[TaskPriority] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskStatus] ON 
GO
INSERT [dbo].[TaskStatus] ([SId], [SType], [SDescription]) VALUES (1, N'New', N'New Task')
GO
INSERT [dbo].[TaskStatus] ([SId], [SType], [SDescription]) VALUES (2, N'InProgress', N'Due')
GO
INSERT [dbo].[TaskStatus] ([SId], [SType], [SDescription]) VALUES (3, N'Finished', N'Finished')
GO
SET IDENTITY_INSERT [dbo].[TaskStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[TaskTable] ON 
GO
INSERT [dbo].[TaskTable] ([TId], [TName], [Description], [TDueDate], [TPriorityId], [TStatusId]) VALUES (1, N'Task1', N'Task', CAST(N'2022-05-10T14:13:45.207' AS DateTime), NULL, 1)
GO
INSERT [dbo].[TaskTable] ([TId], [TName], [Description], [TDueDate], [TPriorityId], [TStatusId]) VALUES (2, N'Task2', N'Task2', CAST(N'2022-06-21T15:05:09.427' AS DateTime), 1, 3)
GO
INSERT [dbo].[TaskTable] ([TId], [TName], [Description], [TDueDate], [TPriorityId], [TStatusId]) VALUES (3, N'Task3', N'Task3', CAST(N'2022-05-13T15:14:19.983' AS DateTime), 1, 1)
GO
INSERT [dbo].[TaskTable] ([TId], [TName], [Description], [TDueDate], [TPriorityId], [TStatusId]) VALUES (4, N'Task4', N'Task4', CAST(N'2022-05-29T15:53:28.440' AS DateTime), 1, 2)
GO
INSERT [dbo].[TaskTable] ([TId], [TName], [Description], [TDueDate], [TPriorityId], [TStatusId]) VALUES (5, N'Task5', N'Task5', CAST(N'2022-05-15T15:29:33.940' AS DateTime), 1, 1)
GO
INSERT [dbo].[TaskTable] ([TId], [TName], [Description], [TDueDate], [TPriorityId], [TStatusId]) VALUES (6, N'Task6', N'Task6', CAST(N'2022-05-29T16:08:12.587' AS DateTime), 1, 1)
GO
INSERT [dbo].[TaskTable] ([TId], [TName], [Description], [TDueDate], [TPriorityId], [TStatusId]) VALUES (7, N'Task7', N'Task7', CAST(N'2022-05-29T16:12:17.563' AS DateTime), 1, 2)
GO
SET IDENTITY_INSERT [dbo].[TaskTable] OFF
GO
ALTER DATABASE [GeicoTaskAppTest] SET  READ_WRITE 
GO
