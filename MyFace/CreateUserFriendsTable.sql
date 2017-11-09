USE [MyFace]
GO

/****** Object:  Table [dbo].[UserFriends]    Script Date: 11/9/2017 4:27:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserFriends](
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

