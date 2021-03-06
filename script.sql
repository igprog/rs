USE [rs]
GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'AdminType'

GO
EXEC sys.sp_dropextendedproperty @name=N'MS_Description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UserType'

GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.5.2019. 15:08:37 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12.5.2019. 15:08:37 ******/
DROP TABLE [dbo].[Products]
GO
/****** Object:  Table [dbo].[ProductGroups]    Script Date: 12.5.2019. 15:08:37 ******/
DROP TABLE [dbo].[ProductGroups]
GO
/****** Object:  Table [dbo].[ProductGroups]    Script Date: 12.5.2019. 15:08:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductGroups](
	[ProductGroupId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProductGroups] PRIMARY KEY CLUSTERED 
(
	[ProductGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 12.5.2019. 15:08:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductGroup] [uniqueidentifier] NOT NULL,
	[ProductOwner] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[ShortDescription] [nvarchar](50) NULL,
	[LongDescription] [nvarchar](max) NULL,
	[Address] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Web] [nvarchar](50) NULL,
	[Price] [nvarchar](50) NULL,
	[Latitude] [decimal](18, 8) NULL,
	[Longitude] [decimal](18, 8) NULL,
	[Image] [nvarchar](50) NULL,
	[DateModified] [datetime] NULL,
	[IsActive] [int] NULL,
	[DisplayType] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.5.2019. 15:08:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserType] [int] NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Pin] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[AdminType] [int] NULL,
	[UserGroupId] [uniqueidentifier] NULL,
	[ActivationDate] [datetime] NULL,
	[ExpirationDate] [datetime] NULL,
	[IsActive] [int] NULL,
	[IPAddress] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'26c0df2f-fdae-4680-9a68-4bdff4ea1d65', N'Transport')
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'34b7dfb2-42a5-4edc-868d-88c91dcd3e68', N'Eating')
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'b8b3ae5d-cd80-416f-99a9-b79916820e97', N'Accommodation')
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'a68465a1-381b-4d6d-a1ee-4008afd30456', N'26c0df2f-fdae-4680-9a68-4bdff4ea1d65', N'01fc8858-efa3-4471-869a-58983e1e2083', N'taksi 2', N'jhgj', N'hgjhgjhg', N'jhg', N'', N'jhg', N'', N'', N'', N'', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'', CAST(N'2019-05-11 21:03:29.763' AS DateTime), 0, 0)
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'56d9d597-02da-45a4-be80-9f0d8742bc23', N'b8b3ae5d-cd80-416f-99a9-b79916820e97', N'01fc8858-efa3-4471-869a-58983e1e2083', N'Apartmani Split', N'test kratki opis', N'', N'', N'', N'Split', N'098123456789', N'', N'', N'60', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'', CAST(N'2019-05-12 14:03:56.160' AS DateTime), 0, 0)
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'fc55b9aa-6c7c-4dcb-961d-b41b65c3ba98', N'34b7dfb2-42a5-4edc-868d-88c91dcd3e68', N'01fc8858-efa3-4471-869a-58983e1e2083', N'Restoran', N'test', N'dugi opis test', N'hkjh', N'kjh', N'', N'095 95545465464', N'', N'', N'199', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'', CAST(N'2019-05-11 21:56:21.713' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'c3aeb74b-f287-4182-8111-c4011f4432cb', N'b8b3ae5d-cd80-416f-99a9-b79916820e97', N'01fc8858-efa3-4471-869a-58983e1e2083', N'Apartment 1', N'Apartments 30 m from the sea', N'Lorem ipsum dolor sit amet, an sed nominati referrentur. Sed verear denique repudiandae in. His eu alia elaboraret. Mea dolore principes te, esse omittantur voluptatibus cu ius. Has ad epicuri suavitate accommodare, an equidem ceteros qui. Ex mea sonet assueverit, ei nonumes abhorreant cum. Erant sonet at ius.', N'Jardula 26', N'21322', N'Brela', N'098123456', N'igprog@yahoo.com', N'www.apartmentselvira.com', N'90', CAST(43.38381570 AS Decimal(18, 8)), CAST(16.90479430 AS Decimal(18, 8)), N'02.jpg', CAST(N'2019-05-11 19:15:49.440' AS DateTime), 0, 0)
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'ffbc6790-0fd4-46c5-9f9e-d43babe5532a', N'26c0df2f-fdae-4680-9a68-4bdff4ea1d65', N'01fc8858-efa3-4471-869a-58983e1e2083', N'Sailing', N'Test', N'Lorem ipsum dolor sit amet, an sed nominati referrentur. Sed verear denique repudiandae in. His eu alia elaboraret. Mea dolore principes te, esse omittantur voluptatibus cu ius. Has ad epicuri suavitate accommodare,', N'', N'', N'Omiš', N'', N'', N'', N'99', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'', CAST(N'2019-05-12 14:04:22.037' AS DateTime), 0, 0)
INSERT [dbo].[Users] ([UserId], [UserType], [FirstName], [LastName], [CompanyName], [Address], [PostalCode], [City], [Country], [Pin], [Email], [UserName], [Password], [AdminType], [UserGroupId], [ActivationDate], [ExpirationDate], [IsActive], [IPAddress]) VALUES (N'3f660cd6-c131-4c7f-a41d-29c9d0fe653a', 1, N'Goran', N'Šošić', N'', N'', N'', N'', N'', N'', N'goran.sosic77@gmail.com', N'goran', N'B1pAlUEE/GtUN3ZniF8aVFH043+UC/mw/NgsOXu76b4=', 1, N'3f660cd6-c131-4c7f-a41d-29c9d0fe653a', CAST(N'2017-02-05 20:11:48.377' AS DateTime), CAST(N'2017-02-05 20:11:48.377' AS DateTime), 0, N'')
INSERT [dbo].[Users] ([UserId], [UserType], [FirstName], [LastName], [CompanyName], [Address], [PostalCode], [City], [Country], [Pin], [Email], [UserName], [Password], [AdminType], [UserGroupId], [ActivationDate], [ExpirationDate], [IsActive], [IPAddress]) VALUES (N'01fc8858-efa3-4471-869a-58983e1e2083', 0, N'Igor', N'Gašparović', N'', N'', N'', N'', N'', N'', N'igprog@yahoo.com', N'igor', N'lQr0bcokppTd5n5nik0L+Q==', 0, N'01fc8858-efa3-4471-869a-58983e1e2083', CAST(N'2017-01-22 14:49:23.723' AS DateTime), CAST(N'2017-01-22 14:49:23.723' AS DateTime), 0, N'')
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: pravna osoba; 1: fizicka osoba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UserType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: superviosor; 1: standard user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'AdminType'
GO
