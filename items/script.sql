USE [master]
GO

/****** Object:  Database [Testapp2]    Script Date: 21.04.2020 12:14:43 ******/
CREATE DATABASE [Testapp2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Testapp2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Testapp2.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Testapp2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\Testapp2_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Testapp2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [Testapp2] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [Testapp2] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [Testapp2] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [Testapp2] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [Testapp2] SET ARITHABORT OFF 
GO

ALTER DATABASE [Testapp2] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [Testapp2] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [Testapp2] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [Testapp2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [Testapp2] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [Testapp2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [Testapp2] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [Testapp2] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [Testapp2] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [Testapp2] SET  DISABLE_BROKER 
GO

ALTER DATABASE [Testapp2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [Testapp2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [Testapp2] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [Testapp2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [Testapp2] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [Testapp2] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [Testapp2] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [Testapp2] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [Testapp2] SET  MULTI_USER 
GO

ALTER DATABASE [Testapp2] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [Testapp2] SET DB_CHAINING OFF 
GO

ALTER DATABASE [Testapp2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [Testapp2] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [Testapp2] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [Testapp2] SET  READ_WRITE 
GO


USE [Testapp2]
GO
/****** Object:  UserDefinedFunction [dbo].[getAge]    Script Date: 21.04.2020 12:11:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[getAge](@dt datetime) 
RETURNS int
AS
BEGIN
    RETURN 
        DATEDIFF(yy, @dt, getdate())
        - CASE 
            WHEN 
                MONTH(@dt) > MONTH(GETDATE()) OR 
                (MONTH(@dt) = MONTH(GETDATE()) AND DAY(@dt) > DAY(GETDATE())) 
            THEN 1 
            ELSE 0 
        END
END
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 21.04.2020 12:11:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Sex] [tinyint] NOT NULL,
	[IdNumber] [varchar](11) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[SettlementId] [int] NOT NULL,
	[PictureAddress] [nvarchar](200) NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 21.04.2020 12:11:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[Number] [varchar](50) NOT NULL,
	[PersonId] [int] NOT NULL,
	[PhoneType] [tinyint] NOT NULL,
 CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Relations]    Script Date: 21.04.2020 12:11:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Relations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RelationType] [tinyint] NOT NULL,
	[RelatedPerson1Id] [int] NOT NULL,
	[RelatedPerson2Id] [int] NOT NULL,
 CONSTRAINT [PK_Relations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settlements]    Script Date: 21.04.2020 12:11:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settlements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Settlements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 

INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [Sex], [IdNumber], [BirthDate], [SettlementId], [PictureAddress]) VALUES (1, N'ალექსი', N'ონეზაშვილი', 1, N'01010018471', CAST(N'1973-09-11' AS Date), 1, N'Pictures/2.png')
INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [Sex], [IdNumber], [BirthDate], [SettlementId], [PictureAddress]) VALUES (4, N'გიორგი', N'გიორგაძე', 1, N'91010012119', CAST(N'1980-01-01' AS Date), 2, N'pictures/2.png')
INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [Sex], [IdNumber], [BirthDate], [SettlementId], [PictureAddress]) VALUES (5, N'ია', N'იაშვილი', 0, N'90012255883', CAST(N'1990-12-12' AS Date), 3, N'pictures/3.png')
INSERT [dbo].[Persons] ([Id], [FirstName], [LastName], [Sex], [IdNumber], [BirthDate], [SettlementId], [PictureAddress]) VALUES (11, N'ზაური', N'გიორგაძე', 1, N'01010018475', CAST(N'1960-04-18' AS Date), 2, N'Pictures/2.png')
SET IDENTITY_INSERT [dbo].[Persons] OFF
INSERT [dbo].[Phones] ([Number], [PersonId], [PhoneType]) VALUES (N'25802300', 4, 1)
INSERT [dbo].[Phones] ([Number], [PersonId], [PhoneType]) VALUES (N'2825566', 11, 1)
INSERT [dbo].[Phones] ([Number], [PersonId], [PhoneType]) VALUES (N'577010109', 4, 0)
INSERT [dbo].[Phones] ([Number], [PersonId], [PhoneType]) VALUES (N'577012101', 11, 0)
INSERT [dbo].[Phones] ([Number], [PersonId], [PhoneType]) VALUES (N'599094646', 1, 0)
SET IDENTITY_INSERT [dbo].[Relations] ON 

INSERT [dbo].[Relations] ([Id], [RelationType], [RelatedPerson1Id], [RelatedPerson2Id]) VALUES (2, 0, 11, 1)
INSERT [dbo].[Relations] ([Id], [RelationType], [RelatedPerson1Id], [RelatedPerson2Id]) VALUES (3, 1, 11, 4)
INSERT [dbo].[Relations] ([Id], [RelationType], [RelatedPerson1Id], [RelatedPerson2Id]) VALUES (4, 2, 4, 5)
INSERT [dbo].[Relations] ([Id], [RelationType], [RelatedPerson1Id], [RelatedPerson2Id]) VALUES (5, 3, 1, 5)
INSERT [dbo].[Relations] ([Id], [RelationType], [RelatedPerson1Id], [RelatedPerson2Id]) VALUES (6, 3, 5, 11)
INSERT [dbo].[Relations] ([Id], [RelationType], [RelatedPerson1Id], [RelatedPerson2Id]) VALUES (7, 2, 4, 11)
SET IDENTITY_INSERT [dbo].[Relations] OFF
SET IDENTITY_INSERT [dbo].[Settlements] ON 

INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (1, N'თბილისი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (2, N'ქუთაისი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (3, N'ბათუმი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (4, N'გორი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (5, N'კასპი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (6, N'ბაკურიანი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (7, N'ფოთი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (8, N'გუდაური')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (9, N'წყნეთი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (10, N'კოჯორი')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (11, N'ტაბახმელა')
INSERT [dbo].[Settlements] ([Id], [Name]) VALUES (12, N'მუხათწყარო')
SET IDENTITY_INSERT [dbo].[Settlements] OFF
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_Persons] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_Persons]
GO
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD  CONSTRAINT [FK_Relations_Persons] FOREIGN KEY([RelatedPerson1Id])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Relations] CHECK CONSTRAINT [FK_Relations_Persons]
GO
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD  CONSTRAINT [FK_Relations_Persons1] FOREIGN KEY([RelatedPerson2Id])
REFERENCES [dbo].[Persons] ([Id])
GO
ALTER TABLE [dbo].[Relations] CHECK CONSTRAINT [FK_Relations_Persons1]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [CK_PersonBirthDate] CHECK  (([dbo].[getAge]([BirthDate])>=(18)))
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [CK_PersonBirthDate]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [CK_PersonFirstName] CHECK  ((len([FirstName])>=(2) AND len([FirstName])<=(50)))
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [CK_PersonFirstName]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [CK_PersonIdNumber] CHECK  ((len([IdNumber])=(11)))
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [CK_PersonIdNumber]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [CK_PersonLasttName] CHECK  ((len([LastName])>=(2) AND len([LastName])<=(50)))
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [CK_PersonLasttName]
GO
ALTER TABLE [dbo].[Persons]  WITH CHECK ADD  CONSTRAINT [CK_PersonSex] CHECK  (([Sex]>=(0) AND [Sex]<=(1)))
GO
ALTER TABLE [dbo].[Persons] CHECK CONSTRAINT [CK_PersonSex]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [CK_Phones] CHECK  ((len([Number])>=(4) AND len([Number])<=(50)))
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [CK_Phones]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [CK_PhonesType] CHECK  (([PhoneType]>=(0) AND [PhoneType]<=(2)))
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [CK_PhonesType]
GO
ALTER TABLE [dbo].[Relations]  WITH CHECK ADD  CONSTRAINT [CK_Relations] CHECK  (([RelationType]>=(0) AND [RelationType]<=(3)))
GO
ALTER TABLE [dbo].[Relations] CHECK CONSTRAINT [CK_Relations]
GO
