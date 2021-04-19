# Sample_CRUD
留言板_CRUD
留言_CRUD

#DB
USE [MVC]
GO
/****** Object:  Table [dbo].[Guestbooks]    Script Date: 2021/4/20 上午 05:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guestbooks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Content] [nvarchar](50) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Reply] [nvarchar](100) NULL,
	[ReplyTime] [datetime] NULL
) ON [PRIMARY]
GO
