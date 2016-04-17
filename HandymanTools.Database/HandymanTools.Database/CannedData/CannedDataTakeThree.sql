USE [handymantools]
GO
/****** Object:  Table [dbo].[Clerk]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clerk](
	[UserName] [varchar](36) NOT NULL,
 CONSTRAINT [pk_Clerk_UserName] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer](
	[UserName] [varchar](36) NOT NULL,
	[Address] [varchar](255) NOT NULL,
	[HomeAreaCode] [varchar](5) NOT NULL,
	[HomePhone] [varchar](10) NOT NULL,
	[WorkAreaCode] [varchar](5) NOT NULL,
	[WorkPhone] [varchar](10) NOT NULL,
 CONSTRAINT [pk_Customer_UserName] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[PowerToolAccessory]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PowerToolAccessory](
	[ToolId] [int] NOT NULL,
	[Accessory] [varchar](50) NOT NULL,
 CONSTRAINT [pk_PowerToolAccessory_ToolId_Accessory] PRIMARY KEY CLUSTERED 
(
	[ToolId] ASC,
	[Accessory] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reservation](
	[ReservationNumber] [int] IDENTITY(10,1) NOT NULL,
	[CustomerId] [varchar](36) NOT NULL,
	[PickupClerkId] [varchar](36) NULL,
	[DropOffClerkId] [varchar](36) NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[CreditCardNum] [varchar](16) NULL,
	[CreditCardExpDate] [date] NULL,
 CONSTRAINT [pk_Reservation_ReservationNumber] PRIMARY KEY CLUSTERED 
(
	[ReservationNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[ReservationTool]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationTool](
	[ReservationNumber] [int] NOT NULL,
	[ToolId] [int] NOT NULL,
 CONSTRAINT [pk_ReservationTool_ReservationNumber_ToolId] PRIMARY KEY CLUSTERED 
(
	[ReservationNumber] ASC,
	[ToolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceOrder]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceOrder](
	[ToolId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NULL,
	[EstimatedCost] [decimal](13, 4) NOT NULL,
 CONSTRAINT [pk_ServiceOrder_ToolId_StartDate] PRIMARY KEY CLUSTERED 
(
	[ToolId] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tool]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tool](
	[ToolId] [int] IDENTITY(121,1) NOT NULL,
	[AbbrDescription] [varchar](50) NOT NULL,
	[FullDescription] [varchar](255) NOT NULL,
	[RentalPrice] [decimal](13, 4) NOT NULL,
	[PurchasePrice] [decimal](13, 4) NOT NULL,
	[DepositAmt] [decimal](13, 4) NOT NULL,
	[ToolType] [varchar](25) NOT NULL,
	[SaleDate] [date] NULL,
 CONSTRAINT [pk_Tool_ToolId] PRIMARY KEY CLUSTERED 
(
	[ToolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[User]    Script Date: 4/16/2016 8:34:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserName] [varchar](36) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[Password] [varchar](256) NOT NULL,
	[PasswordHash] [varchar](256) NOT NULL,
 CONSTRAINT [pk_User_UserName] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[Clerk]  WITH CHECK ADD  CONSTRAINT [fk_Clerk_UserName] FOREIGN KEY([UserName])
REFERENCES [dbo].[User] ([UserName])
GO
ALTER TABLE [dbo].[Clerk] CHECK CONSTRAINT [fk_Clerk_UserName]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [fk_Customer_UserId] FOREIGN KEY([UserName])
REFERENCES [dbo].[User] ([UserName])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [fk_Customer_UserId]
GO
ALTER TABLE [dbo].[PowerToolAccessory]  WITH CHECK ADD  CONSTRAINT [fk_PowerToolAccessory_ToolId] FOREIGN KEY([ToolId])
REFERENCES [dbo].[Tool] ([ToolId])
GO
ALTER TABLE [dbo].[PowerToolAccessory] CHECK CONSTRAINT [fk_PowerToolAccessory_ToolId]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [fk_Reservation_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([UserName])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [fk_Reservation_CustomerId]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [fk_Reservation_DropOffClerkId] FOREIGN KEY([DropOffClerkId])
REFERENCES [dbo].[Clerk] ([UserName])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [fk_Reservation_DropOffClerkId]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [fk_Reservation_PickupClerkId] FOREIGN KEY([PickupClerkId])
REFERENCES [dbo].[Clerk] ([UserName])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [fk_Reservation_PickupClerkId]
GO
ALTER TABLE [dbo].[ReservationTool]  WITH CHECK ADD  CONSTRAINT [fk_ReservationTool_ReservationNumber] FOREIGN KEY([ReservationNumber])
REFERENCES [dbo].[Reservation] ([ReservationNumber])
GO
ALTER TABLE [dbo].[ReservationTool] CHECK CONSTRAINT [fk_ReservationTool_ReservationNumber]
GO
ALTER TABLE [dbo].[ReservationTool]  WITH CHECK ADD  CONSTRAINT [fk_ReservationTool_ToolId] FOREIGN KEY([ToolId])
REFERENCES [dbo].[Tool] ([ToolId])
GO
ALTER TABLE [dbo].[ReservationTool] CHECK CONSTRAINT [fk_ReservationTool_ToolId]
GO
ALTER TABLE [dbo].[ServiceOrder]  WITH CHECK ADD  CONSTRAINT [fk_ServiceOrder_ToolId] FOREIGN KEY([ToolId])
REFERENCES [dbo].[Tool] ([ToolId])
GO
ALTER TABLE [dbo].[ServiceOrder] CHECK CONSTRAINT [fk_ServiceOrder_ToolId]
GO
ALTER TABLE [dbo].[Tool]  WITH CHECK ADD CHECK  (([ToolType]='Power' OR [ToolType]='Construction' OR [ToolType]='Hand'))
GO
