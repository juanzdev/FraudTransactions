CREATE DATABASE [TransactionsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'test', FILENAME = N'C:\Users\juank\transactions_db.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'test_log', FILENAME = N'C:\Users\juank\transactions_db_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TransactionsDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TransactionsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TransactionsDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TransactionsDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TransactionsDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TransactionsDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TransactionsDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TransactionsDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TransactionsDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TransactionsDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TransactionsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TransactionsDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TransactionsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TransactionsDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TransactionsDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TransactionsDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TransactionsDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TransactionsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TransactionsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TransactionsDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TransactionsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TransactionsDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TransactionsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TransactionsDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TransactionsDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TransactionsDB] SET  MULTI_USER 
GO
ALTER DATABASE [TransactionsDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TransactionsDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TransactionsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TransactionsDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TransactionsDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TransactionsDB] SET QUERY_STORE = OFF
GO



USE [TransactionsDB]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 11/20/2017 4:44:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 11/20/2017 4:44:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[step] [int] NOT NULL,
	[type] [nvarchar](50) NOT NULL,
	[amount] [int] NOT NULL,
	[nameOrig] [nvarchar](50) NOT NULL,
	[oldBalanceOrig] [int] NOT NULL,
	[newBalanceOrig] [int] NOT NULL,
	[nameDest] [nvarchar](50) NOT NULL,
	[oldBalanceDest] [int] NOT NULL,
	[newBalanceDest] [int] NOT NULL,
	[isFraud] [bit] NOT NULL,
	[isFlaggedFraud] [bit] NOT NULL,
 CONSTRAINT [PK__Table__3214EC07E13C0B97] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/20/2017 4:44:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO

USE [TransactionsDB]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

GO
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (1, N'Assistant')
GO
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (2, N'Manager')
GO
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (3, N'Superintendent')
GO
INSERT [dbo].[Roles] ([Id], [Role]) VALUES (4, N'Administrator')
GO
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO


USE [TransactionsDB]
GO

/****** Object:  StoredProcedure [dbo].[PrepareIndexes]    Script Date: 11/20/2017 10:50:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[PrepareIndexes]

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	

IF EXISTS(SELECT * FROM sys.indexes 
	WHERE name='NameDestNCIndex' AND object_id = OBJECT_ID('dbo.Transactions')) 
	BEGIN 
	DROP INDEX Transactions.NameDestNCIndex;
END
IF EXISTS(SELECT * FROM sys.indexes 
	WHERE name='IsFraudNCIndex' AND object_id = OBJECT_ID('dbo.Transactions')) 
	BEGIN 
	DROP INDEX Transactions.IsFraudNCIndex;
END

    -- Insert statements for procedure here
	CREATE NONCLUSTERED INDEX [NameDestNCIndex]
	ON [dbo].[Transactions] ([nameDest])

	CREATE NONCLUSTERED INDEX [IsFraudNCIndex]
	ON [dbo].[Transactions] ([isFraud])
END

GO


