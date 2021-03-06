USE [master]
GO
/****** Object:  Database [BlogAppDB]    Script Date: 7.06.2020 20:15:55 ******/
CREATE DATABASE [BlogAppDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BlogAppDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BlogAppDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BlogAppDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\BlogAppDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [BlogAppDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BlogAppDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BlogAppDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BlogAppDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BlogAppDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BlogAppDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BlogAppDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [BlogAppDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BlogAppDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BlogAppDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BlogAppDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BlogAppDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BlogAppDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BlogAppDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BlogAppDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BlogAppDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BlogAppDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BlogAppDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BlogAppDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BlogAppDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BlogAppDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BlogAppDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BlogAppDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BlogAppDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BlogAppDB] SET RECOVERY FULL 
GO
ALTER DATABASE [BlogAppDB] SET  MULTI_USER 
GO
ALTER DATABASE [BlogAppDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BlogAppDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BlogAppDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BlogAppDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BlogAppDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'BlogAppDB', N'ON'
GO
ALTER DATABASE [BlogAppDB] SET QUERY_STORE = OFF
GO
USE [BlogAppDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7.06.2020 20:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogCategory]    Script Date: 7.06.2020 20:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogCategory](
	[BlogId] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_BlogCategory] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC,
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 7.06.2020 20:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[BlogId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime2](7) NOT NULL,
	[IsApproved] [bit] NOT NULL,
 CONSTRAINT [PK_Blogs] PRIMARY KEY CLUSTERED 
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 7.06.2020 20:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[CreatedTime] [datetime2](7) NOT NULL,
	[UpdatedTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 7.06.2020 20:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Surname] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[WebSite] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[BlogId] [int] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200606102108_InitialCreate', N'3.1.4')
INSERT [dbo].[BlogCategory] ([BlogId], [CategoryId]) VALUES (1, 1)
INSERT [dbo].[BlogCategory] ([BlogId], [CategoryId]) VALUES (2, 1)
INSERT [dbo].[BlogCategory] ([BlogId], [CategoryId]) VALUES (3, 2)
INSERT [dbo].[BlogCategory] ([BlogId], [CategoryId]) VALUES (4, 3)
SET IDENTITY_INSERT [dbo].[Blogs] ON 

INSERT [dbo].[Blogs] ([BlogId], [Title], [Description], [Content], [CreatedTime], [UpdatedTime], [IsApproved]) VALUES (1, N'Hello, ASP .NET Core!', N'here’s the first of a new series of posts. The topic: ASP .NET Core!', N'With all the things I’ve been working with lately, I’ve still kept up to date on what’s new with ASP .NET Core for building Web Apps, Web APIs and even full-stack C# web applications with Blazor. With the release of ASP .NET Core 2.1, and the upcoming releases of 2.2 (late 2018) and 3.0 (2019), now is a great time to be an ASP .NET Core developer. But where should you begin? You have many options.', CAST(N'2020-06-06T13:33:02.9443276' AS DateTime2), CAST(N'2020-06-06T13:33:02.9455202' AS DateTime2), 1)
INSERT [dbo].[Blogs] ([BlogId], [Title], [Description], [Content], [CreatedTime], [UpdatedTime], [IsApproved]) VALUES (2, N'Azure Blob Storage from ASP .NET Core File Upload', N'This is the second of a new series of posts on ASP .NET Core', N'In the past week, I had the opportunity to participate in a hackfest with several colleagues from across the globe, to work on real-life customer projects. I took a break from my primary project to help a colleague with a simple problem: upload a file from a web browser and save it into Azure Blob Storage within an ASP .NET Core web application!', CAST(N'2020-06-06T13:33:02.9455871' AS DateTime2), CAST(N'2020-06-06T13:33:02.9455878' AS DateTime2), 1)
INSERT [dbo].[Blogs] ([BlogId], [Title], [Description], [Content], [CreatedTime], [UpdatedTime], [IsApproved]) VALUES (3, N'Introduction to DESIGN PATTERNS', N'Design patterns are typical solutions to common problems in software design. Each pattern is like a blueprint that you can customize to solve a particular design problem in your code.', N'Patterns are a toolkit of solutions to common problems in software design. They define a common language that helps your team communicate more efficiently.', CAST(N'2020-06-06T13:33:02.9455880' AS DateTime2), CAST(N'2020-06-06T13:33:02.9455881' AS DateTime2), 1)
INSERT [dbo].[Blogs] ([BlogId], [Title], [Description], [Content], [CreatedTime], [UpdatedTime], [IsApproved]) VALUES (4, N'SQL Server Authentication', N'Authentication, The process of identity verification of the user.', N'Authentication, The process of identity verification of the user. In Laymen terms, Attention is the process to check “Who are you?”. It can be based on user-id & password or token-based or certificate-based. Authentication is the key to allow only authorized users can connect to access the data and system.', CAST(N'2020-06-06T13:33:02.9455883' AS DateTime2), CAST(N'2020-06-06T13:33:02.9455884' AS DateTime2), 1)
SET IDENTITY_INSERT [dbo].[Blogs] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [Description], [CreatedTime], [UpdatedTime]) VALUES (1, N'.Net Core', N'Most valuable articles about .net Core', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [Description], [CreatedTime], [UpdatedTime]) VALUES (2, N'Design Patters', N'Most valuable articles about Desşgn Patters', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [Description], [CreatedTime], [UpdatedTime]) VALUES (3, N'Sql Server', N'Most valuable articles about Sql Server', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Categories] ([CategoryId], [CategoryName], [Description], [CreatedTime], [UpdatedTime]) VALUES (4, N'c#', N'Most valuable articles about c^#', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([CommentId], [Name], [Surname], [Email], [WebSite], [Content], [BlogId]) VALUES (1, N'Shahed', N'Rose', N'shahed.rose@gmail.com', NULL, N'Yep, exciting times ahead with ASP .NET Core!', 1)
INSERT [dbo].[Comments] ([CommentId], [Name], [Surname], [Email], [WebSite], [Content], [BlogId]) VALUES (2, N'Hamilton', N'Stone', N'hamilton.stone@yahoo.com', NULL, N'You’re welcome, thanks for reading!', 1)
INSERT [dbo].[Comments] ([CommentId], [Name], [Surname], [Email], [WebSite], [Content], [BlogId]) VALUES (3, N'Zhang', N'Lingkang', N'Lingkang.Zhang@yahoo.com', NULL, N'The book is awesome, easy-understanding and well-written. Just have a little suggestion to organize the content not in alphabetical order but by categories would be better. And also put some code in it [rather than having it in separate archive] so that it would be easier to read on an iPad when travel.', 3)
SET IDENTITY_INSERT [dbo].[Comments] OFF
/****** Object:  Index [IX_BlogCategory_CategoryId]    Script Date: 7.06.2020 20:16:40 ******/
CREATE NONCLUSTERED INDEX [IX_BlogCategory_CategoryId] ON [dbo].[BlogCategory]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_BlogId]    Script Date: 7.06.2020 20:16:40 ******/
CREATE NONCLUSTERED INDEX [IX_Comments_BlogId] ON [dbo].[Comments]
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BlogCategory]  WITH CHECK ADD  CONSTRAINT [FK_BlogCategory_Blogs_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogCategory] CHECK CONSTRAINT [FK_BlogCategory_Blogs_BlogId]
GO
ALTER TABLE [dbo].[BlogCategory]  WITH CHECK ADD  CONSTRAINT [FK_BlogCategory_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogCategory] CHECK CONSTRAINT [FK_BlogCategory_Categories_CategoryId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Blogs_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blogs] ([BlogId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Blogs_BlogId]
GO
USE [master]
GO
ALTER DATABASE [BlogAppDB] SET  READ_WRITE 
GO
