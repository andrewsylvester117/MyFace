USE [MyFace]
GO

/****** Object:  Table [dbo].[Posts]    Script Date: 11/9/2017 4:27:28 PM ******/
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

