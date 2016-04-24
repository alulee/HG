/****** Object:  Table [dbo].[HGPedigreeEvent]    Script Date: 2016/3/23 ¤U¤È 02:38:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HGPedigreeEvent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HGPedigreeID] [int] NOT NULL,
	[EventTitle] [nvarchar](200) NOT NULL,
	[EventContent] [nvarchar](max) NOT NULL,
	[EventDateOnUtc] [datetime] NOT NULL,
	[EventPlace] [nvarchar](200) NOT NULL,
	[Longitude] [decimal](18, 5) NULL,
	[Latitude] [decimal](18, 5) NULL,
	[MetaKeywords] [nvarchar](400) NULL,
	[CreatedOnUtc] [datetime] NOT NULL,
	[UpdatedOnUtc] [datetime] NOT NULL,
	[CreatedWho] [nvarchar](20) NOT NULL,
	[UpdatedWho] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_HGPedigreeEvent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



CREATE TABLE [dbo].[HGPedigreeInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HGPedigreeID] [int] NOT NULL,
	[InfoType] [varchar](10) NOT NULL,
	[InfoTitle] [nvarchar](30) NOT NULL,
	[InfoContent] [nvarchar](max) NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[UpdatedOnUtc] [datetime2](7) NOT NULL,
	[CreatedWho] [nvarchar](20) NOT NULL,
	[UpdatedWho] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_HGPedigreeInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



CREATE TABLE [dbo].[HGPedigreeMeta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Editor] [nvarchar](100) NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Image] [image] NULL,
	[PublishDate] [datetime2](7) NULL,
	[Volumes] [int] NOT NULL,
	[Pages] [int] NOT NULL,
	[FamilyName] [nvarchar](10) NOT NULL,
	[OriginalAncestor] [nvarchar](30) NOT NULL,
	[DateMoveToTaiwan] [nvarchar](10) NULL,
	[AncestorToTaiwan] [nvarchar](30) NULL,
	[OriginalAddress] [nvarchar](30) NULL,
	[TotalGenerations] [int] NOT NULL,
	[GenerationToTaiwan] [int] NOT NULL,
	[LivingAreaInTaiwan] [nvarchar](255) NULL,
	[OriginalCollector] [nvarchar](50) NULL,
	[ContentNotes] [nvarchar](max) NULL,
	[TangName] [nvarchar](50) NULL,
	[IsPublic] [bit] NOT NULL,
	[CreatedOnUtc] [datetime2](7) NOT NULL,
	[UpdatedOnUtc] [datetime2](7) NOT NULL,
	[CreatedWho] [nvarchar](20) NOT NULL,
	[UpdatedWho] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_HGPedigreeMeta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
