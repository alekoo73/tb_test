USE [Testapp2]
GO
/****** Object:  UserDefinedFunction [dbo].[getAge]    Script Date: 21.04.2020 10:58:11 ******/
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
/****** Object:  Table [dbo].[Persons]    Script Date: 21.04.2020 10:58:11 ******/
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
/****** Object:  Table [dbo].[Phones]    Script Date: 21.04.2020 10:58:11 ******/
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
/****** Object:  Table [dbo].[Relations]    Script Date: 21.04.2020 10:58:11 ******/
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
/****** Object:  Table [dbo].[Settlements]    Script Date: 21.04.2020 10:58:11 ******/
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
