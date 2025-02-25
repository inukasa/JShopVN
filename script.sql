USE [master]
GO
/****** Object:  Database [JShopVN]    Script Date: 2/22/2020 12:15:32 AM ******/
CREATE DATABASE [JShopVN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'JShopVN', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\MSSQL\DATA\JShopVN.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'JShopVN_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER01\MSSQL\DATA\JShopVN_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [JShopVN] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [JShopVN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [JShopVN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [JShopVN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [JShopVN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [JShopVN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [JShopVN] SET ARITHABORT OFF 
GO
ALTER DATABASE [JShopVN] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [JShopVN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [JShopVN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [JShopVN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [JShopVN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [JShopVN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [JShopVN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [JShopVN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [JShopVN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [JShopVN] SET  DISABLE_BROKER 
GO
ALTER DATABASE [JShopVN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [JShopVN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [JShopVN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [JShopVN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [JShopVN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [JShopVN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [JShopVN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [JShopVN] SET RECOVERY FULL 
GO
ALTER DATABASE [JShopVN] SET  MULTI_USER 
GO
ALTER DATABASE [JShopVN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [JShopVN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [JShopVN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [JShopVN] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [JShopVN] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'JShopVN', N'ON'
GO
ALTER DATABASE [JShopVN] SET QUERY_STORE = OFF
GO
USE [JShopVN]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [JShopVN]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[id] [int] NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[roleid] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[id] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[packid] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[totalprice] [money] NOT NULL,
 CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[id] [int] NOT NULL,
	[country] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Game]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Game](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[country] [int] NULL,
	[publisher] [int] NULL,
	[image] [nvarchar](max) NULL,
 CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Note]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Note](
	[id] [int] NOT NULL,
	[note] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Note] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[id] [int] NOT NULL,
	[userid] [int] NOT NULL,
	[totalprice] [money] NOT NULL,
	[datecreated] [date] NOT NULL,
	[status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[id] [int] NOT NULL,
	[orderid] [int] NOT NULL,
	[packid] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [money] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Package]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[price] [money] NOT NULL,
	[gameid] [int] NOT NULL,
 CONSTRAINT [PK_Price] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publisher]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publisher](
	[id] [int] NOT NULL,
	[publisher] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Publisher] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id] [int] NOT NULL,
	[role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2/22/2020 12:15:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] NOT NULL,
	[accid] [int] NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[phone] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[facebook] [nvarchar](max) NULL,
	[balance] [money] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Account] ([id], [username], [password], [roleid]) VALUES (1, N'nan', N'swcmiq', 2)
INSERT [dbo].[Account] ([id], [username], [password], [roleid]) VALUES (2, N'123', N'123', 2)
INSERT [dbo].[Cart] ([id], [userid], [packid], [quantity], [totalprice]) VALUES (2, 1, 3, 1, 150000.0000)
INSERT [dbo].[Country] ([id], [country]) VALUES (1, N'Việt Nam')
INSERT [dbo].[Country] ([id], [country]) VALUES (2, N'Nước Ngoài')
INSERT [dbo].[Game] ([id], [name], [country], [publisher], [image]) VALUES (1, N'Onmyoji', 2, NULL, N'img1.jfif')
INSERT [dbo].[Game] ([id], [name], [country], [publisher], [image]) VALUES (2, N'Cửu Âm Chân Kinh', 1, NULL, N'img2.jpg')
INSERT [dbo].[Order] ([id], [userid], [totalprice], [datecreated], [status]) VALUES (1, 1, 150000.0000, CAST(N'2020-02-21' AS Date), N'Đã hủy')
INSERT [dbo].[Order] ([id], [userid], [totalprice], [datecreated], [status]) VALUES (2, 1, 150000.0000, CAST(N'2020-02-21' AS Date), N'Đã hủy')
INSERT [dbo].[Order] ([id], [userid], [totalprice], [datecreated], [status]) VALUES (3, 1, 150000.0000, CAST(N'2020-02-21' AS Date), N'Đã hủy')
INSERT [dbo].[Order] ([id], [userid], [totalprice], [datecreated], [status]) VALUES (4, 1, 150000.0000, CAST(N'2020-02-21' AS Date), N'Đã hủy')
INSERT [dbo].[OrderDetail] ([id], [orderid], [packid], [quantity], [price]) VALUES (1, 1, 3, 1, 150000.0000)
INSERT [dbo].[OrderDetail] ([id], [orderid], [packid], [quantity], [price]) VALUES (2, 2, 3, 1, 150000.0000)
INSERT [dbo].[OrderDetail] ([id], [orderid], [packid], [quantity], [price]) VALUES (3, 3, 3, 1, 150000.0000)
INSERT [dbo].[OrderDetail] ([id], [orderid], [packid], [quantity], [price]) VALUES (4, 4, 3, 1, 150000.0000)
INSERT [dbo].[Package] ([id], [name], [price], [gameid]) VALUES (1, N'Gói 5000 gosu', 450000.0000, 2)
INSERT [dbo].[Package] ([id], [name], [price], [gameid]) VALUES (2, N'Gói 1000 gosu', 90000.0000, 2)
INSERT [dbo].[Package] ([id], [name], [price], [gameid]) VALUES (3, N'Gói 2000 ...', 150000.0000, 1)
INSERT [dbo].[Role] ([id], [role]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([id], [role]) VALUES (2, N'Customer')
INSERT [dbo].[User] ([id], [accid], [name], [phone], [email], [facebook], [balance]) VALUES (1, 1, N'Xoài Lắc', N'0913780585', N'namhase04850@fpt.edu.vn', NULL, 300000.0000)
INSERT [dbo].[User] ([id], [accid], [name], [phone], [email], [facebook], [balance]) VALUES (2, 2, N'Phạm Minh Tuấn', N'0352598844', N'tuan23798@gmail.com', N'tuaans', 0.0000)
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_balance]  DEFAULT ((0)) FOR [balance]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Role] FOREIGN KEY([roleid])
REFERENCES [dbo].[Role] ([id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Role]
GO
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_Country] FOREIGN KEY([country])
REFERENCES [dbo].[Country] ([id])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_Country]
GO
ALTER TABLE [dbo].[Game]  WITH CHECK ADD  CONSTRAINT [FK_Game_Publisher] FOREIGN KEY([publisher])
REFERENCES [dbo].[Publisher] ([id])
GO
ALTER TABLE [dbo].[Game] CHECK CONSTRAINT [FK_Game_Publisher]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([userid])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([orderid])
REFERENCES [dbo].[Order] ([id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Package] FOREIGN KEY([packid])
REFERENCES [dbo].[Package] ([id])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Package]
GO
ALTER TABLE [dbo].[Package]  WITH CHECK ADD  CONSTRAINT [FK_Package_Game] FOREIGN KEY([gameid])
REFERENCES [dbo].[Game] ([id])
GO
ALTER TABLE [dbo].[Package] CHECK CONSTRAINT [FK_Package_Game]
GO
USE [master]
GO
ALTER DATABASE [JShopVN] SET  READ_WRITE 
GO
