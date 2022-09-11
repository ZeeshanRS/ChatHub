USE [master]
GO

CREATE DATABASE [chat-app]
GO

USE [chat-app]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserChats](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ToUserId] [int] NOT NULL,
	[FromUserId] [int] NOT NULL,
	[ToUserName] [varchar](50) NULL,
	[FromUserName] [varchar](50) NULL,
	[Message] [varchar](max) NOT NULL,
	[SentDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserChats] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/10/2022 6:30:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[PasswordKey] [varchar](50) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Note] [varchar](max) NOT NULL,
	[ConnectionId] [varchar](50) NOT NULL,
	[LastLogin] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserChats] ADD  CONSTRAINT [DF_USerChats_SentDateTime]  DEFAULT (getdate()) FOR [SentDateTime]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Admin]  DEFAULT ((0)) FOR [Admin]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_LastLogin]  DEFAULT (getdate()) FOR [LastLogin]
GO
USE [master]
GO
ALTER DATABASE [chat-app] SET  READ_WRITE 
GO
