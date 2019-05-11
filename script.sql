USE [RivieraSplit]
GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 05/03/2019 15:42:24 ******/
DROP TABLE [dbo].[Gallery]
GO
/****** Object:  Table [dbo].[Options]    Script Date: 05/03/2019 15:42:29 ******/
DROP TABLE [dbo].[Options]
GO
/****** Object:  Table [dbo].[ProductGroups]    Script Date: 05/03/2019 15:42:30 ******/
DROP TABLE [dbo].[ProductGroups]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 05/03/2019 15:42:31 ******/
DROP TABLE [dbo].[Products]
GO
/****** Object:  Table [dbo].[Translations]    Script Date: 05/03/2019 15:42:31 ******/
DROP TABLE [dbo].[Translations]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05/03/2019 15:42:31 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 05/03/2019 15:42:31 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: pravna osoba; 1: fizicka osoba' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'UserType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0: superviosor; 1: standard user' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Users', @level2type=N'COLUMN',@level2name=N'AdminType'
GO
INSERT [dbo].[Users] ([UserId], [UserType], [FirstName], [LastName], [CompanyName], [Address], [PostalCode], [City], [Country], [Pin], [Email], [UserName], [Password], [AdminType], [UserGroupId], [ActivationDate], [ExpirationDate], [IsActive], [IPAddress]) VALUES (N'3f660cd6-c131-4c7f-a41d-29c9d0fe653a', 1, N'Goran', N'Šošić', N'', N'', N'', N'', N'', N'', N'goran.sosic77@gmail.com', N'goran', N'B1pAlUEE/GtUN3ZniF8aVFH043+UC/mw/NgsOXu76b4=', 1, N'3f660cd6-c131-4c7f-a41d-29c9d0fe653a', CAST(0x0000A711014CD521 AS DateTime), CAST(0x0000A711014CD521 AS DateTime), 0, N'')
INSERT [dbo].[Users] ([UserId], [UserType], [FirstName], [LastName], [CompanyName], [Address], [PostalCode], [City], [Country], [Pin], [Email], [UserName], [Password], [AdminType], [UserGroupId], [ActivationDate], [ExpirationDate], [IsActive], [IPAddress]) VALUES (N'01fc8858-efa3-4471-869a-58983e1e2083', 0, N'Igor', N'Gašparović', N'', N'', N'', N'', N'', N'', N'igprog@yahoo.com', N'igor', N'lQr0bcokppTd5n5nik0L+Q==', 0, N'01fc8858-efa3-4471-869a-58983e1e2083', CAST(0x0000A70300F4479D AS DateTime), CAST(0x0000A70300F4479D AS DateTime), 0, N'')
/****** Object:  Table [dbo].[Translations]    Script Date: 05/03/2019 15:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Translations](
	[TranslationId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Language1] [nvarchar](50) NULL,
	[Language2] [nvarchar](50) NULL,
	[Language3] [nvarchar](50) NULL,
	[Language4] [nvarchar](50) NULL,
	[Language5] [nvarchar](50) NULL,
 CONSTRAINT [PK_Translations] PRIMARY KEY CLUSTERED 
(
	[TranslationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Translations] ([TranslationId], [Title], [Language1], [Language2], [Language3], [Language4], [Language5]) VALUES (N'80513e5a-9039-4595-8959-7ed06f3715df', N'Name', N'Ime', N'Name', N'Nome', NULL, NULL)
INSERT [dbo].[Translations] ([TranslationId], [Title], [Language1], [Language2], [Language3], [Language4], [Language5]) VALUES (N'4a67b68f-5044-4caa-92c9-868e4d456dc2', N'Services', N'Usluge', N'Services', N'Services', NULL, NULL)
INSERT [dbo].[Translations] ([TranslationId], [Title], [Language1], [Language2], [Language3], [Language4], [Language5]) VALUES (N'adb4ccc0-0713-430b-94b9-a28a62d8d8e4', N'Under construction', N'Stranica je u izradi', N'Under construction', N'Website under construction', NULL, NULL)
INSERT [dbo].[Translations] ([TranslationId], [Title], [Language1], [Language2], [Language3], [Language4], [Language5]) VALUES (N'62aac601-9221-4269-a39f-bb70990b605a', N'Contact', N'Kontakt', N'Contact', N'Kontakt', NULL, NULL)
INSERT [dbo].[Translations] ([TranslationId], [Title], [Language1], [Language2], [Language3], [Language4], [Language5]) VALUES (N'ae806989-7e50-42b0-ada9-d2f1449346ce', N'Find out more', N'Saznajte više', N'Find out more', N'Finde mehr heraus', NULL, NULL)
/****** Object:  Table [dbo].[Products]    Script Date: 05/03/2019 15:42:31 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'0199267c-465f-48c5-bf5a-7a3d9e5ea763', N'34b7dfb2-42a5-4edc-868d-88c91dcd3e68', N'01fc8858-efa3-4471-869a-58983e1e2083', N'Restaurant xy', N'short description', N'Lorem ipsum dolor sit amet, an sed nominati referrentur. Sed verear denique repudiandae in. His eu alia elaboraret. Mea dolore principes te, esse omittantur voluptatibus cu ius. Has ad epicuri suavitate accommodare, an equidem ceteros qui. Ex mea sonet assueverit, ei nonumes abhorreant cum. Erant sonet at ius.', N'', N'', N'Split', N'+098330966', N'igprog@yahoo.com', N'', N'', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'04.jpg', CAST(0x0000A71D00000000 AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'66342257-162d-40ce-85b1-a2a48dca7850', N'26c0df2f-fdae-4680-9a68-4bdff4ea1d65', N'01fc8858-efa3-4471-869a-58983e1e2083', N'Transport...', N'short description', N'Lorem ipsum dolor sit amet, an sed nominati referrentur. Sed verear denique repudiandae in. His eu alia elaboraret. Mea dolore principes te, esse omittantur voluptatibus cu ius. Has ad epicuri suavitate accommodare, an equidem ceteros qui. Ex mea sonet assueverit, ei nonumes abhorreant cum. Erant sonet at ius.', N'', N'', N'', N'+38598330966', N'igprog@yahoo.com', N'igprog.hr', N'150,55', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'03.jpg', CAST(0x0000A71D00000000 AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [ProductGroup], [ProductOwner], [Title], [ShortDescription], [LongDescription], [Address], [PostalCode], [City], [Phone], [Email], [Web], [Price], [Latitude], [Longitude], [Image], [DateModified], [IsActive], [DisplayType]) VALUES (N'c3aeb74b-f287-4182-8111-c4011f4432cb', N'b8b3ae5d-cd80-416f-99a9-b79916820e97', N'01fc8858-efa3-4471-869a-58983e1e2083', N'Apartments Elvira', N'Apartments 30 m from the sea', N'Long description...', N'', N'', N'Brela', N'', N'', N'www.apartmentselvira.com', N'90', CAST(0.00000000 AS Decimal(18, 8)), CAST(0.00000000 AS Decimal(18, 8)), N'02.jpg', CAST(0x0000A71E00000000 AS DateTime), 1, 0)
/****** Object:  Table [dbo].[ProductGroups]    Script Date: 05/03/2019 15:42:30 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'1dae4b02-f074-4c6b-9478-0c5e837781e4', N'Going out')
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'26c0df2f-fdae-4680-9a68-4bdff4ea1d65', N'Transport')
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'39307ef9-8213-4c71-a426-6503458dc1ad', N'Islands')
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'c83112ab-bf12-4473-98c7-7182ddb459f8', N'Tours')
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'34b7dfb2-42a5-4edc-868d-88c91dcd3e68', N'Eating')
INSERT [dbo].[ProductGroups] ([ProductGroupId], [Title]) VALUES (N'b8b3ae5d-cd80-416f-99a9-b79916820e97', N'Accommodation')
/****** Object:  Table [dbo].[Options]    Script Date: 05/03/2019 15:42:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Options](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Cod] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Options] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Options] ([Id], [Type], [Cod], [Title]) VALUES (N'37589662-fcfb-46b6-9bea-561f7429586e', N'category', N'tours', N'Tours')
INSERT [dbo].[Options] ([Id], [Type], [Cod], [Title]) VALUES (N'db7c3bd1-fc3e-4336-8fe9-64f7a7a059ba', N'category', N'transport', N'Transport')
INSERT [dbo].[Options] ([Id], [Type], [Cod], [Title]) VALUES (N'90b4ee50-0603-4c49-b863-8f58d18670b7', N'category', N'accommodation', N'Accommodation')
INSERT [dbo].[Options] ([Id], [Type], [Cod], [Title]) VALUES (N'7e1a03b7-678b-41ec-a55a-abd8eda0a3b5', N'category', N'goingout', N'Going Out')
INSERT [dbo].[Options] ([Id], [Type], [Cod], [Title]) VALUES (N'b80da8d0-5fde-4f41-b24d-d22c4d4ce3c3', N'category', N'islands', N'Islands')
INSERT [dbo].[Options] ([Id], [Type], [Cod], [Title]) VALUES (N'2b83e161-a2ec-4ef2-9ee1-dd84c5b154dd', N'category', N'eating', N'Eating')
/****** Object:  Table [dbo].[Gallery]    Script Date: 05/03/2019 15:42:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[GalleryId] [uniqueidentifier] NOT NULL,
	[GalleryOwner] [uniqueidentifier] NOT NULL,
	[Image] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[GalleryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Gallery] ([GalleryId], [GalleryOwner], [Image]) VALUES (N'21e1a068-af5f-440f-8643-4982c684cdb5', N'0199267c-465f-48c5-bf5a-7a3d9e5ea763', N'09.jpg')
