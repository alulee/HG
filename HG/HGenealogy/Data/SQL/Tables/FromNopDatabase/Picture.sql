GO

/****** Object:  Table [dbo].[Picture]    Script Date: 2016/04/15 11:46:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Picture](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PictureBinary] [varbinary](max) NULL,
	[MimeType] [nvarchar](40) NOT NULL,
	[SeoFilename] [nvarchar](300) NULL,
	[AltAttribute] [nvarchar](max) NULL,
	[TitleAttribute] [nvarchar](max) NULL,
	[IsNew] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


