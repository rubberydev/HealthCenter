USE [healthCenter]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 24/04/2018 13:32:18 ******/
SET ANSI_NULLS ON
GO

SET IDENTITY_INSERT [dbo].[UserTypes] ON 

INSERT [dbo].[UserTypes] ([UserTypeId], [Name]) VALUES (2, N'Facebook')
INSERT [dbo].[UserTypes] ([UserTypeId], [Name]) VALUES (4, N'Instagram')
INSERT [dbo].[UserTypes] ([UserTypeId], [Name]) VALUES (6, N'LinkedIn')
INSERT [dbo].[UserTypes] ([UserTypeId], [Name]) VALUES (1, N'Local')
INSERT [dbo].[UserTypes] ([UserTypeId], [Name]) VALUES (3, N'Twitter')

SET IDENTITY_INSERT [dbo].[UserTypes] OFF
