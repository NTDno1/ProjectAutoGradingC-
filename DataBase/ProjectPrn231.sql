USE [master]
GO
/****** Object:  Database [ProjectPrn231]    Script Date: 7/10/2023 11:10:21 AM ******/
CREATE DATABASE [ProjectPrn231]
GO
ALTER DATABASE [ProjectPrn231] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProjectPrn231] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProjectPrn231] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProjectPrn231] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProjectPrn231] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProjectPrn231] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProjectPrn231] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProjectPrn231] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProjectPrn231] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProjectPrn231] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProjectPrn231] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProjectPrn231] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProjectPrn231] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProjectPrn231] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProjectPrn231] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProjectPrn231] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProjectPrn231] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProjectPrn231] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProjectPrn231] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProjectPrn231] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProjectPrn231] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProjectPrn231] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProjectPrn231] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProjectPrn231] SET  MULTI_USER 
GO
ALTER DATABASE [ProjectPrn231] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProjectPrn231] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProjectPrn231] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProjectPrn231] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProjectPrn231] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProjectPrn231] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProjectPrn231] SET QUERY_STORE = OFF
GO
USE [ProjectPrn231]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 7/10/2023 11:10:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[id] [int] NOT NULL,
	[Name] [varchar](50) NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_CLASS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 7/10/2023 11:10:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[StudentId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[TotalMark] [float] NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionDetail]    Script Date: 7/10/2023 11:10:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionDetail](
	[QuestionId] [int] IDENTITY(1,1) NOT NULL,
	[Q] [int] NULL,
	[Status] [int] NULL,
	[Category] [int] NULL,
	[Note] [varchar](max) NULL,
	[CreateDate] [date] NULL,
	[UpdateDate] [date] NULL,
 CONSTRAINT [PK_QuestionDetail] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionNo]    Script Date: 7/10/2023 11:10:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionNo](
	[QuestionId] [int] NOT NULL,
	[QuestionNo] [int] NULL,
	[Mark] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[Note] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestCaseDb]    Script Date: 7/10/2023 11:10:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestCaseDb](
	[QuestionNo] [int] IDENTITY(1,1) NOT NULL,
	[Name] [int] NOT NULL,
	[InputTesCase] [nvarchar](max) NULL,
	[OutputTestCase] [nvarchar](max) NULL,
	[Ouput] [nvarchar](max) NULL,
 CONSTRAINT [PK_TestCase] PRIMARY KEY CLUSTERED 
(
	[QuestionNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/10/2023 11:10:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] NOT NULL,
	[UserName] [varchar](max) NOT NULL,
	[PassWord] [varchar](max) NOT NULL,
	[ClassID] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[MSSV] [varchar](max) NOT NULL,
	[Role] [int] NOT NULL,
	[DateCreate] [date] NOT NULL,
	[DateUpdate] [date] NOT NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Class] ([id], [Name], [CreateDate], [UpdateDate]) VALUES (1, N'SE1606', CAST(N'2023-01-01' AS Date), CAST(N'2023-01-05' AS Date))
INSERT [dbo].[Class] ([id], [Name], [CreateDate], [UpdateDate]) VALUES (2, N'SE1437', CAST(N'2023-02-01' AS Date), CAST(N'2023-02-05' AS Date))
GO
INSERT [dbo].[Question] ([StudentId], [QuestionId], [TotalMark], [CreateDate], [UpdateDate]) VALUES (1, 1, 1, CAST(N'2023-01-01' AS Date), CAST(N'2023-01-01' AS Date))
GO
SET IDENTITY_INSERT [dbo].[QuestionDetail] ON 

INSERT [dbo].[QuestionDetail] ([QuestionId], [Q], [Status], [Category], [Note], [CreateDate], [UpdateDate]) VALUES (1, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[QuestionDetail] OFF
GO
INSERT [dbo].[User] ([id], [UserName], [PassWord], [ClassID], [Name], [MSSV], [Role], [DateCreate], [DateUpdate]) VALUES (1, N'user1', N'123', 1, N'A', N'14001', 1, CAST(N'2023-01-01' AS Date), CAST(N'2023-01-02' AS Date))
INSERT [dbo].[User] ([id], [UserName], [PassWord], [ClassID], [Name], [MSSV], [Role], [DateCreate], [DateUpdate]) VALUES (2, N'user2', N'1233', 1, N'B', N'14002', 1, CAST(N'2023-02-01' AS Date), CAST(N'2023-02-02' AS Date))
INSERT [dbo].[User] ([id], [UserName], [PassWord], [ClassID], [Name], [MSSV], [Role], [DateCreate], [DateUpdate]) VALUES (3, N'user3', N'123', 1, N'C', N'15111', 1, CAST(N'2023-01-01' AS Date), CAST(N'2023-01-01' AS Date))
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Mark_User] FOREIGN KEY([StudentId])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Mark_User]
GO
ALTER TABLE [dbo].[Question]  WITH CHECK ADD  CONSTRAINT [FK_Question_QuestionDetail] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[QuestionDetail] ([QuestionId])
GO
ALTER TABLE [dbo].[Question] CHECK CONSTRAINT [FK_Question_QuestionDetail]
GO
ALTER TABLE [dbo].[QuestionNo]  WITH CHECK ADD  CONSTRAINT [FK_QuestionNo_QuestionDetail] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[QuestionDetail] ([QuestionId])
GO
ALTER TABLE [dbo].[QuestionNo] CHECK CONSTRAINT [FK_QuestionNo_QuestionDetail]
GO
ALTER TABLE [dbo].[QuestionNo]  WITH CHECK ADD  CONSTRAINT [FK_QuestionNo_TestCaseDb] FOREIGN KEY([QuestionNo])
REFERENCES [dbo].[TestCaseDb] ([QuestionNo])
GO
ALTER TABLE [dbo].[QuestionNo] CHECK CONSTRAINT [FK_QuestionNo_TestCaseDb]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Class] ([id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Class]
GO
USE [master]
GO
ALTER DATABASE [ProjectPrn231] SET  READ_WRITE 
GO
