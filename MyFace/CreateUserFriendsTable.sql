USE [MyFace]
GO

ALTER TABLE [dbo].[UserFriends] DROP CONSTRAINT [FK_Users_UserFriends]
GO

ALTER TABLE [dbo].[UserFriends] DROP CONSTRAINT [FK_UserFriends_Users]
GO

/****** Object:  Table [dbo].[UserFriends]    Script Date: 11/10/2017 3:10:31 PM ******/
DROP TABLE [dbo].[UserFriends]
GO

/****** Object:  Table [dbo].[UserFriends]    Script Date: 11/10/2017 3:10:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserFriends](
	[pkId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[userId] [int] NOT NULL,
	[friendId] [int] NOT NULL
) ON [PRIMARY]

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


