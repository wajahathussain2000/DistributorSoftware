USE [master]
GO
/****** Object:  Database [DistributionDB]    Script Date: 9/5/2025 9:16:41 AM ******/
CREATE DATABASE [DistributionDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DistributionDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DistributionDB.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DistributionDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DistributionDB_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DistributionDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DistributionDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DistributionDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DistributionDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DistributionDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DistributionDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DistributionDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DistributionDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [DistributionDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DistributionDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DistributionDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DistributionDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DistributionDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DistributionDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DistributionDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DistributionDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DistributionDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DistributionDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DistributionDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DistributionDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DistributionDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DistributionDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DistributionDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DistributionDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DistributionDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DistributionDB] SET  MULTI_USER 
GO
ALTER DATABASE [DistributionDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DistributionDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DistributionDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DistributionDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DistributionDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DistributionDB]
GO
/****** Object:  Table [dbo].[Brands]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brands](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_Brands] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ChartOfAccounts]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[AccountCode] [nvarchar](20) NOT NULL,
	[AccountName] [nvarchar](100) NOT NULL,
	[AccountType] [nvarchar](50) NOT NULL,
	[ParentAccountId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_ChartOfAccounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CustomerCategories]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerCategories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[DiscountPercentage] [decimal](5, 2) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_CustomerCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customers]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerCode] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](100) NULL,
	[ContactName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](255) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[Country] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JournalVoucherDetails]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalVoucherDetails](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherId] [int] NOT NULL,
	[AccountId] [int] NOT NULL,
	[DebitAmount] [decimal](18, 2) NOT NULL,
	[CreditAmount] [decimal](18, 2) NOT NULL,
	[Narration] [nvarchar](255) NULL,
 CONSTRAINT [PK_JournalVoucherDetails] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[JournalVouchers]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JournalVouchers](
	[VoucherId] [int] IDENTITY(1,1) NOT NULL,
	[VoucherNumber] [nvarchar](50) NOT NULL,
	[VoucherDate] [date] NOT NULL,
	[Reference] [nvarchar](100) NULL,
	[Narration] [nvarchar](500) NULL,
	[TotalDebit] [decimal](18, 2) NOT NULL,
	[TotalCredit] [decimal](18, 2) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_JournalVouchers] PRIMARY KEY CLUSTERED 
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LoginHistory]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginHistory](
	[LoginId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[LoginDate] [datetime] NOT NULL DEFAULT (getdate()),
	[LogoutDate] [datetime] NULL,
	[IPAddress] [nvarchar](45) NULL,
	[UserAgent] [nvarchar](500) NULL,
	[LoginStatus] [nvarchar](20) NOT NULL DEFAULT ('Success'),
	[FailureReason] [nvarchar](255) NULL,
	[SessionDuration] [int] NULL,
 CONSTRAINT [PK_LoginHistory] PRIMARY KEY CLUSTERED 
(
	[LoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[LineTotal] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Orders]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](50) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[RequiredDate] [datetime] NULL,
	[ShippedDate] [datetime] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Notes] [nvarchar](500) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionId] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](100) NOT NULL,
	[PermissionCode] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Module] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Category] [nvarchar](50) NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[StockQuantity] [int] NOT NULL DEFAULT ((0)),
	[ReorderLevel] [int] NOT NULL DEFAULT ((10)),
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedDate] [datetime] NULL,
	[BrandId] [int] NULL,
	[CategoryId] [int] NULL,
	[UnitId] [int] NULL,
	[Barcode] [nvarchar](100) NULL,
	[WarehouseId] [int] NULL,
	[BatchNumber] [nvarchar](50) NULL,
	[ExpiryDate] [date] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchaseInvoiceDetails]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseInvoiceDetails](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[PurchaseInvoiceId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[TaxPercentage] [decimal](5, 2) NULL,
	[TaxAmount] [decimal](18, 2) NOT NULL,
	[DiscountPercentage] [decimal](5, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[BatchNumber] [nvarchar](50) NULL,
	[ExpiryDate] [date] NULL,
	[Remarks] [nvarchar](255) NULL,
 CONSTRAINT [PK_PurchaseInvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PurchaseInvoices]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseInvoices](
	[PurchaseInvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [nvarchar](50) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[InvoiceDate] [date] NOT NULL,
	[DueDate] [date] NULL,
	[SubTotal] [decimal](18, 2) NOT NULL,
	[TaxAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[FreightAmount] [decimal](18, 2) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[PaidAmount] [decimal](18, 2) NOT NULL,
	[BalanceAmount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_PurchaseInvoices] PRIMARY KEY CLUSTERED 
(
	[PurchaseInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolePermissions]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePermissions](
	[RolePermissionId] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[IsGranted] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED 
(
	[RolePermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesInvoiceDetails]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesInvoiceDetails](
	[DetailId] [int] IDENTITY(1,1) NOT NULL,
	[SalesInvoiceId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[TaxPercentage] [decimal](5, 2) NULL,
	[TaxAmount] [decimal](18, 2) NOT NULL,
	[DiscountPercentage] [decimal](5, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Remarks] [nvarchar](255) NULL,
 CONSTRAINT [PK_SalesInvoiceDetails] PRIMARY KEY CLUSTERED 
(
	[DetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SalesInvoices]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesInvoices](
	[SalesInvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [nvarchar](50) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[SalesmanId] [int] NULL,
	[InvoiceDate] [date] NOT NULL,
	[DueDate] [date] NULL,
	[SubTotal] [decimal](18, 2) NOT NULL,
	[TaxAmount] [decimal](18, 2) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[PaidAmount] [decimal](18, 2) NOT NULL,
	[BalanceAmount] [decimal](18, 2) NOT NULL,
	[PaymentMode] [nvarchar](20) NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_SalesInvoices] PRIMARY KEY CLUSTERED 
(
	[SalesInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stock]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stock](
	[StockId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[WarehouseId] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[BatchNumber] [nvarchar](50) NULL,
	[ExpiryDate] [date] NULL,
	[LastUpdated] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_Stock] PRIMARY KEY CLUSTERED 
(
	[StockId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StockAdjustments]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockAdjustments](
	[AdjustmentId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[SystemQuantity] [int] NOT NULL,
	[PhysicalQuantity] [int] NOT NULL,
	[Difference] [int] NOT NULL,
	[Reason] [nvarchar](255) NOT NULL,
	[AdjustmentType] [nvarchar](20) NULL,
	[CreatedBy] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_StockAdjustments] PRIMARY KEY CLUSTERED 
(
	[AdjustmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StockMovement]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockMovement](
	[MovementId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[WarehouseId] [int] NOT NULL,
	[MovementType] [nvarchar](20) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[ReferenceType] [nvarchar](50) NULL,
	[ReferenceId] [int] NULL,
	[BatchNumber] [nvarchar](50) NULL,
	[ExpiryDate] [date] NULL,
	[MovementDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[Remarks] [nvarchar](500) NULL,
 CONSTRAINT [PK_StockMovement] PRIMARY KEY CLUSTERED 
(
	[MovementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierCode] [nvarchar](50) NOT NULL,
	[SupplierName] [nvarchar](200) NOT NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](500) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[GSTNumber] [nvarchar](20) NULL,
	[NTNNumber] [nvarchar](20) NULL,
	[PaymentTerms] [int] NULL,
	[CreditLimit] [decimal](18, 2) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Units]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[UnitId] [int] IDENTITY(1,1) NOT NULL,
	[UnitName] [nvarchar](50) NOT NULL,
	[UnitCode] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[UnitId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserActivityLog]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserActivityLog](
	[LogId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[ActivityType] [nvarchar](50) NOT NULL,
	[ActivityDescription] [nvarchar](500) NULL,
	[Module] [nvarchar](50) NULL,
	[IPAddress] [nvarchar](45) NULL,
	[UserAgent] [nvarchar](500) NULL,
	[ActivityDate] [datetime] NOT NULL DEFAULT (getdate()),
	[AdditionalData] [nvarchar](max) NULL,
 CONSTRAINT [PK_UserActivityLog] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PasswordHash] [nvarchar](255) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[IsAdmin] [bit] NOT NULL DEFAULT ((0)),
	[LastLoginDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Warehouses]    Script Date: 9/5/2025 9:16:41 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Warehouses](
	[WarehouseId] [int] IDENTITY(1,1) NOT NULL,
	[WarehouseName] [nvarchar](100) NOT NULL,
	[Location] [nvarchar](255) NULL,
	[ContactPerson] [nvarchar](100) NULL,
	[ContactPhone] [nvarchar](20) NULL,
	[IsActive] [bit] NOT NULL DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_Warehouses] PRIMARY KEY CLUSTERED 
(
	[WarehouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Customers_CustomerCode]    Script Date: 9/5/2025 9:16:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Customers_CustomerCode] ON [dbo].[Customers]
(
	[CustomerCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Orders_OrderNumber]    Script Date: 9/5/2025 9:16:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Orders_OrderNumber] ON [dbo].[Orders]
(
	[OrderNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Products_ProductCode]    Script Date: 9/5/2025 9:16:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Products_ProductCode] ON [dbo].[Products]
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_Email]    Script Date: 9/5/2025 9:16:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_Username]    Script Date: 9/5/2025 9:16:41 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username] ON [dbo].[Users]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChartOfAccounts] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[ChartOfAccounts] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CustomerCategories] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CustomerCategories] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[JournalVoucherDetails] ADD  DEFAULT ((0)) FOR [DebitAmount]
GO
ALTER TABLE [dbo].[JournalVoucherDetails] ADD  DEFAULT ((0)) FOR [CreditAmount]
GO
ALTER TABLE [dbo].[JournalVouchers] ADD  DEFAULT ((0)) FOR [TotalDebit]
GO
ALTER TABLE [dbo].[JournalVouchers] ADD  DEFAULT ((0)) FOR [TotalCredit]
GO
ALTER TABLE [dbo].[JournalVouchers] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[OrderDetails] ADD  DEFAULT ((0)) FOR [Discount]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [OrderDate]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT ((0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[PurchaseInvoiceDetails] ADD  DEFAULT ((0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoiceDetails] ADD  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ((0)) FOR [SubTotal]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ((0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ((0)) FOR [FreightAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ((0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ((0)) FOR [PaidAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ((0)) FOR [BalanceAmount]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT ('PENDING') FOR [Status]
GO
ALTER TABLE [dbo].[PurchaseInvoices] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[SalesInvoiceDetails] ADD  DEFAULT ((0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[SalesInvoiceDetails] ADD  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ((0)) FOR [SubTotal]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ((0)) FOR [TaxAmount]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ((0)) FOR [DiscountAmount]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ((0)) FOR [TotalAmount]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ((0)) FOR [PaidAmount]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ((0)) FOR [BalanceAmount]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT ('PENDING') FOR [Status]
GO
ALTER TABLE [dbo].[SalesInvoices] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Stock] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Stock] ADD  DEFAULT (getdate()) FOR [LastUpdated]
GO
ALTER TABLE [dbo].[StockMovement] ADD  DEFAULT (getdate()) FOR [MovementDate]
GO
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Suppliers] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Brands]  WITH CHECK ADD  CONSTRAINT [FK_Brands_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Brands] CHECK CONSTRAINT [FK_Brands_Users]
GO
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Users]
GO
ALTER TABLE [dbo].[ChartOfAccounts]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccounts_Parent] FOREIGN KEY([ParentAccountId])
REFERENCES [dbo].[ChartOfAccounts] ([AccountId])
GO
ALTER TABLE [dbo].[ChartOfAccounts] CHECK CONSTRAINT [FK_ChartOfAccounts_Parent]
GO
ALTER TABLE [dbo].[ChartOfAccounts]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccounts_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[ChartOfAccounts] CHECK CONSTRAINT [FK_ChartOfAccounts_Users]
GO
ALTER TABLE [dbo].[JournalVoucherDetails]  WITH CHECK ADD  CONSTRAINT [FK_JournalVoucherDetails_ChartOfAccounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccounts] ([AccountId])
GO
ALTER TABLE [dbo].[JournalVoucherDetails] CHECK CONSTRAINT [FK_JournalVoucherDetails_ChartOfAccounts]
GO
ALTER TABLE [dbo].[JournalVoucherDetails]  WITH CHECK ADD  CONSTRAINT [FK_JournalVoucherDetails_JournalVouchers] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[JournalVouchers] ([VoucherId])
GO
ALTER TABLE [dbo].[JournalVoucherDetails] CHECK CONSTRAINT [FK_JournalVoucherDetails_JournalVouchers]
GO
ALTER TABLE [dbo].[JournalVouchers]  WITH CHECK ADD  CONSTRAINT [FK_JournalVouchers_Users_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[JournalVouchers] CHECK CONSTRAINT [FK_JournalVouchers_Users_Created]
GO
ALTER TABLE [dbo].[JournalVouchers]  WITH CHECK ADD  CONSTRAINT [FK_JournalVouchers_Users_Modified] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[JournalVouchers] CHECK CONSTRAINT [FK_JournalVouchers_Users_Modified]
GO
ALTER TABLE [dbo].[LoginHistory]  WITH CHECK ADD  CONSTRAINT [FK_LoginHistory_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[LoginHistory] CHECK CONSTRAINT [FK_LoginHistory_Users]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Customers]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[PurchaseInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseInvoiceDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[PurchaseInvoiceDetails] CHECK CONSTRAINT [FK_PurchaseInvoiceDetails_Products]
GO
ALTER TABLE [dbo].[PurchaseInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseInvoiceDetails_PurchaseInvoices] FOREIGN KEY([PurchaseInvoiceId])
REFERENCES [dbo].[PurchaseInvoices] ([PurchaseInvoiceId])
GO
ALTER TABLE [dbo].[PurchaseInvoiceDetails] CHECK CONSTRAINT [FK_PurchaseInvoiceDetails_PurchaseInvoices]
GO
ALTER TABLE [dbo].[PurchaseInvoices]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseInvoices_Suppliers] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([SupplierId])
GO
ALTER TABLE [dbo].[PurchaseInvoices] CHECK CONSTRAINT [FK_PurchaseInvoices_Suppliers]
GO
ALTER TABLE [dbo].[PurchaseInvoices]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseInvoices_Users_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[PurchaseInvoices] CHECK CONSTRAINT [FK_PurchaseInvoices_Users_Created]
GO
ALTER TABLE [dbo].[PurchaseInvoices]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseInvoices_Users_Modified] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[PurchaseInvoices] CHECK CONSTRAINT [FK_PurchaseInvoices_Users_Modified]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([PermissionId])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Permissions]
GO
ALTER TABLE [dbo].[RolePermissions]  WITH CHECK ADD  CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[RolePermissions] CHECK CONSTRAINT [FK_RolePermissions_Roles]
GO
ALTER TABLE [dbo].[SalesInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceDetails_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[SalesInvoiceDetails] CHECK CONSTRAINT [FK_SalesInvoiceDetails_Products]
GO
ALTER TABLE [dbo].[SalesInvoiceDetails]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoiceDetails_SalesInvoices] FOREIGN KEY([SalesInvoiceId])
REFERENCES [dbo].[SalesInvoices] ([SalesInvoiceId])
GO
ALTER TABLE [dbo].[SalesInvoiceDetails] CHECK CONSTRAINT [FK_SalesInvoiceDetails_SalesInvoices]
GO
ALTER TABLE [dbo].[SalesInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoices_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO
ALTER TABLE [dbo].[SalesInvoices] CHECK CONSTRAINT [FK_SalesInvoices_Customers]
GO
ALTER TABLE [dbo].[SalesInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoices_Users_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SalesInvoices] CHECK CONSTRAINT [FK_SalesInvoices_Users_Created]
GO
ALTER TABLE [dbo].[SalesInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoices_Users_Modified] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SalesInvoices] CHECK CONSTRAINT [FK_SalesInvoices_Users_Modified]
GO
ALTER TABLE [dbo].[SalesInvoices]  WITH CHECK ADD  CONSTRAINT [FK_SalesInvoices_Users_Salesman] FOREIGN KEY([SalesmanId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[SalesInvoices] CHECK CONSTRAINT [FK_SalesInvoices_Users_Salesman]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Products]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Users] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Users]
GO
ALTER TABLE [dbo].[Stock]  WITH CHECK ADD  CONSTRAINT [FK_Stock_Warehouses] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouses] ([WarehouseId])
GO
ALTER TABLE [dbo].[Stock] CHECK CONSTRAINT [FK_Stock_Warehouses]
GO
ALTER TABLE [dbo].[StockAdjustments]  WITH CHECK ADD  CONSTRAINT [FK_StockAdjustments_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StockAdjustments] CHECK CONSTRAINT [FK_StockAdjustments_Products]
GO
ALTER TABLE [dbo].[StockMovement]  WITH CHECK ADD  CONSTRAINT [FK_StockMovement_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[StockMovement] CHECK CONSTRAINT [FK_StockMovement_Products]
GO
ALTER TABLE [dbo].[StockMovement]  WITH CHECK ADD  CONSTRAINT [FK_StockMovement_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[StockMovement] CHECK CONSTRAINT [FK_StockMovement_Users]
GO
ALTER TABLE [dbo].[StockMovement]  WITH CHECK ADD  CONSTRAINT [FK_StockMovement_Warehouses] FOREIGN KEY([WarehouseId])
REFERENCES [dbo].[Warehouses] ([WarehouseId])
GO
ALTER TABLE [dbo].[StockMovement] CHECK CONSTRAINT [FK_StockMovement_Warehouses]
GO
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_Users_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_Suppliers_Users_Created]
GO
ALTER TABLE [dbo].[Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_Users_Modified] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Suppliers] CHECK CONSTRAINT [FK_Suppliers_Users_Modified]
GO
ALTER TABLE [dbo].[UserActivityLog]  WITH CHECK ADD  CONSTRAINT [FK_UserActivityLog_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserActivityLog] CHECK CONSTRAINT [FK_UserActivityLog_Users]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[Warehouses]  WITH CHECK ADD  CONSTRAINT [FK_Warehouses_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Warehouses] CHECK CONSTRAINT [FK_Warehouses_Users]
GO
ALTER TABLE [dbo].[StockAdjustments]  WITH CHECK ADD CHECK  (([AdjustmentType]='Decrease' OR [AdjustmentType]='Increase'))
GO
/****** Object:  StoredProcedure [dbo].[sp_AuthenticateUser]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_AuthenticateUser]
        @Username NVARCHAR(50),
        @Password NVARCHAR(255)
    AS
    BEGIN
        SET NOCOUNT ON;
        
        SELECT 
            u.[UserId],
            u.[Username],
            u.[Email],
            u.[FirstName],
            u.[LastName],
            u.[IsActive],
            u.[IsAdmin],
            u.[LastLoginDate],
            r.[RoleName]
        FROM [dbo].[Users] u
        LEFT JOIN [dbo].[UserRoles] ur ON u.[UserId] = ur.[UserId]
        LEFT JOIN [dbo].[Roles] r ON ur.[RoleId] = r.[RoleId]
        WHERE u.[Username] = @Username 
        AND u.[PasswordHash] = @Password 
        AND u.[IsActive] = 1;
        
        -- Update last login date
        UPDATE [dbo].[Users] 
        SET [LastLoginDate] = GETDATE()
        WHERE [Username] = @Username;
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_CheckEmailExists]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_CheckEmailExists]
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT COUNT(*) 
    FROM Users 
    WHERE Email = @Email;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_CreateSalesInvoice]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_CreateSalesInvoice]
    @InvoiceNumber NVARCHAR(50),
    @CustomerId INT,
    @SalesmanId INT = NULL,
    @InvoiceDate DATE,
    @DueDate DATE = NULL,
    @SubTotal DECIMAL(18,2),
    @TaxAmount DECIMAL(18,2),
    @DiscountAmount DECIMAL(18,2),
    @TotalAmount DECIMAL(18,2),
    @PaymentMode NVARCHAR(20) = NULL,
    @Remarks NVARCHAR(500) = NULL,
    @CreatedBy INT,
    @InvoiceId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        INSERT INTO SalesInvoices (
            InvoiceNumber, CustomerId, SalesmanId, InvoiceDate, DueDate,
            SubTotal, TaxAmount, DiscountAmount, TotalAmount,
            PaidAmount, BalanceAmount, PaymentMode, Status, Remarks, CreatedBy
        )
        VALUES (
            @InvoiceNumber, @CustomerId, @SalesmanId, @InvoiceDate, @DueDate,
            @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount,
            0, @TotalAmount, @PaymentMode, 'PENDING', @Remarks, @CreatedBy
        );
        
        SET @InvoiceId = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        SELECT 1 AS Success, @InvoiceId AS InvoiceId;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 AS Success, ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[sp_CreateUser]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_CreateUser]
        @Username NVARCHAR(50),
        @Email NVARCHAR(100),
        @PasswordHash NVARCHAR(255),
        @FirstName NVARCHAR(50),
        @LastName NVARCHAR(50),
        @RoleId INT,
        @CreatedBy INT
    AS
    BEGIN
        SET NOCOUNT ON;
        DECLARE @UserId INT;
        
        BEGIN TRANSACTION;
        BEGIN TRY
            -- Insert user
            INSERT INTO [dbo].[Users] ([Username], [Email], [PasswordHash], [FirstName], [LastName], [CreatedDate])
            VALUES (@Username, @Email, @PasswordHash, @FirstName, @LastName, GETDATE());
            
            SET @UserId = SCOPE_IDENTITY();
            
            -- Assign role
            INSERT INTO [dbo].[UserRoles] ([UserId], [RoleId], [CreatedDate])
            VALUES (@UserId, @RoleId, GETDATE());
            
            -- Log activity
            INSERT INTO [dbo].[UserActivityLog] ([UserId], [ActivityType], [ActivityDescription], [Module], [AdditionalData])
            VALUES (@CreatedBy, 'User Created', 'Created new user: ' + @Username, 'User Management', 'UserID: ' + CAST(@UserId AS NVARCHAR(10)));
            
            COMMIT TRANSACTION;
            SELECT @UserId AS UserId, 'User created successfully' AS Message;
        END TRY
        BEGIN CATCH
            ROLLBACK TRANSACTION;
            SELECT -1 AS UserId, ERROR_MESSAGE() AS Message;
        END CATCH
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_CustomerLedgerReport]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_CustomerLedgerReport]
    @CustomerId INT,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        'SALES' AS TransactionType,
        si.InvoiceNumber AS Reference,
        si.InvoiceDate AS TransactionDate,
        si.TotalAmount AS Debit,
        0 AS Credit,
        si.Remarks AS Narration
    FROM SalesInvoices si
    WHERE si.CustomerId = @CustomerId
    AND (@StartDate IS NULL OR si.InvoiceDate >= @StartDate)
    AND (@EndDate IS NULL OR si.InvoiceDate <= @EndDate)
    
    UNION ALL
    
    SELECT 
        'PAYMENT' AS TransactionType,
        'PAYMENT' AS Reference,
        GETDATE() AS TransactionDate, -- You'll need to add payment date to your payment table
        0 AS Debit,
        si.PaidAmount AS Credit,
        'Payment received' AS Narration
    FROM SalesInvoices si
    WHERE si.CustomerId = @CustomerId
    AND si.PaidAmount > 0
    AND (@StartDate IS NULL OR si.InvoiceDate >= @StartDate)
    AND (@EndDate IS NULL OR si.InvoiceDate <= @EndDate)
    
    ORDER BY TransactionDate;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetLoginHistoryReport]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_GetLoginHistoryReport]
        @StartDate DATETIME = NULL,
        @EndDate DATETIME = NULL,
        @UserId INT = NULL,
        @LoginStatus NVARCHAR(20) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        
        IF @StartDate IS NULL SET @StartDate = DATEADD(DAY, -30, GETDATE());
        IF @EndDate IS NULL SET @EndDate = GETDATE();
        
        SELECT 
            lh.[LoginId],
            u.[Username],
            u.[Email],
            u.[FirstName] + ' ' + u.[LastName] AS [FullName],
            lh.[LoginDate],
            lh.[LogoutDate],
            lh.[IPAddress],
            lh.[LoginStatus],
            lh.[FailureReason],
            lh.[SessionDuration],
            DATEDIFF(MINUTE, lh.[LoginDate], ISNULL(lh.[LogoutDate], GETDATE())) AS [CalculatedDuration]
        FROM [dbo].[LoginHistory] lh
        INNER JOIN [dbo].[Users] u ON lh.[UserId] = u.[UserId]
        WHERE lh.[LoginDate] BETWEEN @StartDate AND @EndDate
        AND (@UserId IS NULL OR lh.[UserId] = @UserId)
        AND (@LoginStatus IS NULL OR lh.[LoginStatus] = @LoginStatus)
        ORDER BY lh.[LoginDate] DESC;
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetProductStock]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_GetProductStock]
    @ProductId INT = NULL,
    @WarehouseId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        s.StockId,
        s.ProductId,
        p.ProductName,
        p.ProductCode,
        s.WarehouseId,
        w.WarehouseName,
        s.Quantity,
        s.BatchNumber,
        s.ExpiryDate,
        s.LastUpdated,
        s.UpdatedBy,
        u.FirstName + ' ' + u.LastName AS UpdatedByUser
    FROM Stock s
    INNER JOIN Products p ON s.ProductId = p.ProductId
    INNER JOIN Warehouses w ON s.WarehouseId = w.WarehouseId
    LEFT JOIN Users u ON s.UpdatedBy = u.UserId
    WHERE (@ProductId IS NULL OR s.ProductId = @ProductId)
    AND (@WarehouseId IS NULL OR s.WarehouseId = @WarehouseId)
    ORDER BY p.ProductName, w.WarehouseName;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetPurchaseInvoice]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_GetPurchaseInvoice]
    @InvoiceId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get invoice header
    SELECT 
        pi.PurchaseInvoiceId,
        pi.InvoiceNumber,
        pi.SupplierId,
        s.SupplierName,
        s.SupplierCode,
        pi.InvoiceDate,
        pi.DueDate,
        pi.SubTotal,
        pi.TaxAmount,
        pi.DiscountAmount,
        pi.FreightAmount,
        pi.TotalAmount,
        pi.PaidAmount,
        pi.BalanceAmount,
        pi.Status,
        pi.Remarks,
        pi.CreatedDate,
        pi.ModifiedDate
    FROM PurchaseInvoices pi
    INNER JOIN Suppliers s ON pi.SupplierId = s.SupplierId
    WHERE pi.PurchaseInvoiceId = @InvoiceId;
    
    -- Get invoice details
    SELECT 
        pid.DetailId,
        pid.PurchaseInvoiceId,
        pid.ProductId,
        p.ProductName,
        p.ProductCode,
        pid.Quantity,
        pid.UnitPrice,
        pid.TaxPercentage,
        pid.TaxAmount,
        pid.DiscountPercentage,
        pid.DiscountAmount,
        pid.TotalAmount,
        pid.BatchNumber,
        pid.ExpiryDate,
        pid.Remarks
    FROM PurchaseInvoiceDetails pid
    INNER JOIN Products p ON pid.ProductId = p.ProductId
    WHERE pid.PurchaseInvoiceId = @InvoiceId;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserActivityReport]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_GetUserActivityReport]
        @StartDate DATETIME = NULL,
        @EndDate DATETIME = NULL,
        @UserId INT = NULL,
        @RoleId INT = NULL,
        @ActivityType NVARCHAR(50) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        
        IF @StartDate IS NULL SET @StartDate = DATEADD(DAY, -30, GETDATE());
        IF @EndDate IS NULL SET @EndDate = GETDATE();
        
        SELECT 
            ual.[LogId],
            u.[Username],
            u.[Email],
            u.[FirstName] + ' ' + u.[LastName] AS [FullName],
            r.[RoleName],
            ual.[ActivityType],
            ual.[ActivityDescription],
            ual.[Module],
            ual.[IPAddress],
            ual.[ActivityDate],
            ual.[AdditionalData]
        FROM [dbo].[UserActivityLog] ual
        INNER JOIN [dbo].[Users] u ON ual.[UserId] = u.[UserId]
        INNER JOIN [dbo].[UserRoles] ur ON u.[UserId] = ur.[UserId]
        INNER JOIN [dbo].[Roles] r ON ur.[RoleId] = r.[RoleId]
        WHERE ual.[ActivityDate] BETWEEN @StartDate AND @EndDate
        AND (@UserId IS NULL OR ual.[UserId] = @UserId)
        AND (@RoleId IS NULL OR ur.[RoleId] = @RoleId)
        AND (@ActivityType IS NULL OR ual.[ActivityType] = @ActivityType)
        ORDER BY ual.[ActivityDate] DESC;
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_LogLoginAttempt]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_LogLoginAttempt]
        @UserId INT,
        @LoginStatus NVARCHAR(20),
        @IPAddress NVARCHAR(45) = NULL,
        @UserAgent NVARCHAR(500) = NULL,
        @FailureReason NVARCHAR(255) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        
        INSERT INTO [dbo].[LoginHistory] ([UserId], [LoginDate], [IPAddress], [UserAgent], [LoginStatus], [FailureReason])
        VALUES (@UserId, GETDATE(), @IPAddress, @UserAgent, @LoginStatus, @FailureReason);
        
        SELECT SCOPE_IDENTITY() AS LoginId;
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_TestConnection]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[sp_TestConnection]
    AS
    BEGIN
        SET NOCOUNT ON;
        SELECT 'Database connection successful' AS Status, GETDATE() AS CurrentTime;
    END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateLastLogin]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_UpdateLastLogin]
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Users 
    SET LastLoginDate = GETDATE()
    WHERE UserId = @UserId;
    
    SELECT @@ROWCOUNT;
END

GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateStock]    Script Date: 9/5/2025 9:16:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[sp_UpdateStock]
    @ProductId INT,
    @WarehouseId INT,
    @Quantity DECIMAL(18,2),
    @BatchNumber NVARCHAR(50) = NULL,
    @ExpiryDate DATE = NULL,
    @UpdatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Update or insert stock
        IF EXISTS (SELECT 1 FROM Stock WHERE ProductId = @ProductId AND WarehouseId = @WarehouseId)
        BEGIN
            UPDATE Stock 
            SET Quantity = @Quantity,
                BatchNumber = @BatchNumber,
                ExpiryDate = @ExpiryDate,
                LastUpdated = GETDATE(),
                UpdatedBy = @UpdatedBy
            WHERE ProductId = @ProductId AND WarehouseId = @WarehouseId;
        END
        ELSE
        BEGIN
            INSERT INTO Stock (ProductId, WarehouseId, Quantity, BatchNumber, ExpiryDate, LastUpdated, UpdatedBy)
            VALUES (@ProductId, @WarehouseId, @Quantity, @BatchNumber, @ExpiryDate, GETDATE(), @UpdatedBy);
        END
        
        COMMIT TRANSACTION;
        SELECT 1 AS Success;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 AS Success, ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END

GO
USE [master]
GO
ALTER DATABASE [DistributionDB] SET  READ_WRITE 
GO
