
/****** Object:  Table [dbo].[Posts]    Script Date: 11/9/2017 16:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[postId] [int] NOT NULL,
	[publisherId] [int] NOT NULL,
	[postHeader] [nvarchar](50) NULL,
	[postText] [nvarchar](max) NULL,
	[postImage] [image] NULL,
	[likeCount] [int] NOT NULL,
	[dislikeCount] [int] NOT NULL,
	[originalPostId] [int] NULL,
 CONSTRAINT [PK_Posts] PRIMARY KEY CLUSTERED 
(
	[postId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFriends]    Script Date: 11/9/2017 16:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFriends](
	[userId] [int] NOT NULL,
	[friendId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/9/2017 16:41:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[userId] [int] NOT NULL,
	[realName] [nvarchar](50) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[status] [nvarchar](255) NULL,
	[dateOfBirth] [date] NULL,
	[image] [image] NOT NULL,
	[address1] [nvarchar](255) NULL,
	[adress2] [nvarchar](255) NULL,
	[phoneNumber] [nvarchar](50) NULL,
	[email] [nvarchar](255) NOT NULL,
	[zodiacSign] [nvarchar](50) NULL,
	[prefferedSSN] [nvarchar](50) NOT NULL,
	[description] [nvarchar](500) NULL,
	[isMaleGender] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Posts] FOREIGN KEY([originalPostId])
REFERENCES [dbo].[Posts] ([postId])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Posts]
GO
ALTER TABLE [dbo].[Posts]  WITH CHECK ADD  CONSTRAINT [FK_Posts_Users] FOREIGN KEY([publisherId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[Posts] CHECK CONSTRAINT [FK_Posts_Users]
GO
ALTER TABLE [dbo].[UserFriends]  WITH CHECK ADD  CONSTRAINT [FK_UserFriends_Users] FOREIGN KEY([friendId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[UserFriends] CHECK CONSTRAINT [FK_UserFriends_Users]
GO
ALTER TABLE [dbo].[UserFriends]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserFriends] FOREIGN KEY([userId])
REFERENCES [dbo].[Users] ([userId])
GO
ALTER TABLE [dbo].[UserFriends] CHECK CONSTRAINT [FK_Users_UserFriends]
GO
