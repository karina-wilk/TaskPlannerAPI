USE [Planner_NH]
GO
/****** Object:  Table [dbo].[Duties]    Script Date: 03.06.2018 14:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Duties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Enabled] [bit] NOT NULL,
	[TaskNeverEnds] [bit] NOT NULL,
	[TaskType] [int] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[FamilyMemberId] [int] NOT NULL,
	[StartDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Duties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Families]    Script Date: 03.06.2018 14:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Families](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Families] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FamilyMembers]    Script Date: 03.06.2018 14:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FamilyMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[FamilyId] [int] NOT NULL,
	[FirstName] [nvarchar](32) NULL,
	[LastName] [nvarchar](128) NULL,
 CONSTRAINT [PK_FamilyMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Duties]  WITH CHECK ADD  CONSTRAINT [FK_Duties_FamilyMember] FOREIGN KEY([FamilyMemberId])
REFERENCES [dbo].[FamilyMembers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Duties] CHECK CONSTRAINT [FK_Duties_FamilyMember]
GO
ALTER TABLE [dbo].[FamilyMembers]  WITH CHECK ADD  CONSTRAINT [FK_FamilyMembers_FamilyId] FOREIGN KEY([FamilyId])
REFERENCES [dbo].[Families] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FamilyMembers] CHECK CONSTRAINT [FK_FamilyMembers_FamilyId]
GO
/****** Object:  StoredProcedure [dbo].[GetNotActiveMembers]    Script Date: 03.06.2018 14:08:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetNotActiveMembers]
(
	@familyId int = NULL
)
AS   

    SET NOCOUNT ON;  
   /****** Script for SelectTopNRows command from SSMS  ******/
SELECT [Id]
      ,[IsActive]
      ,[FamilyId]
      ,[FirstName]
      ,[LastName]
  FROM [dbo].[FamilyMembers]
   WHERE FamilyId=@familyId AND IsActive=0
   
    --SELECT FirstName, LastName, Department  
    --FROM HumanResources.vEmployeeDepartmentHistory  
    --WHERE FirstName = @FirstName AND LastName = @LastName  
    --AND EndDate IS NULL;  


GO
