USE [vm]
GO
ALTER TABLE [dbo].[VendingMachines] DROP CONSTRAINT [FK_VendingMachines_Wallets]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Wallets]
GO
ALTER TABLE [dbo].[Goods] DROP CONSTRAINT [FK_Goods_VendingMachines]
GO
/****** Object:  Table [dbo].[Wallets]    Script Date: 23.02.2015 17:56:31 ******/
DROP TABLE [dbo].[Wallets]
GO
/****** Object:  Table [dbo].[VendingMachines]    Script Date: 23.02.2015 17:56:31 ******/
DROP TABLE [dbo].[VendingMachines]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.02.2015 17:56:31 ******/
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[Goods]    Script Date: 23.02.2015 17:56:31 ******/
DROP TABLE [dbo].[Goods]
GO
/****** Object:  Table [dbo].[Goods]    Script Date: 23.02.2015 17:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Goods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [int] NOT NULL,
	[VendingMachineId] [int] NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_Goods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 23.02.2015 17:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WalletId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[VendingMachines]    Script Date: 23.02.2015 17:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VendingMachines](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[WalletId] [int] NOT NULL,
 CONSTRAINT [PK_VendingMachine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Wallets]    Script Date: 23.02.2015 17:56:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[r1] [int] NOT NULL,
	[r2] [int] NOT NULL,
	[r5] [int] NOT NULL,
	[r10] [int] NOT NULL,
 CONSTRAINT [PK_Wallet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Goods]  WITH CHECK ADD  CONSTRAINT [FK_Goods_VendingMachines] FOREIGN KEY([VendingMachineId])
REFERENCES [dbo].[VendingMachines] ([Id])
GO
ALTER TABLE [dbo].[Goods] CHECK CONSTRAINT [FK_Goods_VendingMachines]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Wallets] FOREIGN KEY([WalletId])
REFERENCES [dbo].[Wallets] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Wallets]
GO
ALTER TABLE [dbo].[VendingMachines]  WITH CHECK ADD  CONSTRAINT [FK_VendingMachines_Wallets] FOREIGN KEY([WalletId])
REFERENCES [dbo].[Wallets] ([Id])
GO
ALTER TABLE [dbo].[VendingMachines] CHECK CONSTRAINT [FK_VendingMachines_Wallets]
GO
