USE [MyFace]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 11/9/2017 4:27:54 PM ******/
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

