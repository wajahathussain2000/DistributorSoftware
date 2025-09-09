-- Fix Missing Columns Script for Pakistan POS System
-- This script adds missing columns to existing tables

USE [DistributionDB]
GO

-- Add missing columns to Customers table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'ContactName')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [ContactName] [nvarchar](100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'Mobile')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [Mobile] [nvarchar](20) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'OutstandingBalance')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [OutstandingBalance] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'PaymentTerms')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [PaymentTerms] [nvarchar](100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'TaxNumber')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [TaxNumber] [nvarchar](50) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'GSTNumber')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [GSTNumber] [nvarchar](50) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'Remarks')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [Remarks] [nvarchar](500) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [CreatedBy] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'CreatedDate')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [CreatedDate] [datetime] NOT NULL DEFAULT (getdate())
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'ModifiedBy')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [ModifiedBy] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Customers]') AND name = 'ModifiedDate')
BEGIN
    ALTER TABLE [dbo].[Customers] ADD [ModifiedDate] [datetime] NULL
END
GO

-- Add missing columns to Products table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'ProductDescription')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [ProductDescription] [nvarchar](500) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'PurchasePrice')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [PurchasePrice] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'SalePrice')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [SalePrice] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'MRP')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [MRP] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'Quantity')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [Quantity] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'ReservedQuantity')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [ReservedQuantity] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'Remarks')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [Remarks] [nvarchar](500) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'CreatedBy')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [CreatedBy] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'CreatedDate')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [CreatedDate] [datetime] NOT NULL DEFAULT (getdate())
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'ModifiedBy')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [ModifiedBy] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = 'ModifiedDate')
BEGIN
    ALTER TABLE [dbo].[Products] ADD [ModifiedDate] [datetime] NULL
END
GO

-- Add foreign key constraints for CreatedBy and ModifiedBy columns
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customers_Users_Created]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customers]'))
BEGIN
    ALTER TABLE [dbo].[Customers] WITH CHECK ADD CONSTRAINT [FK_Customers_Users_Created] FOREIGN KEY([CreatedBy])
    REFERENCES [dbo].[Users] ([UserId])
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Customers_Users_Modified]') AND parent_object_id = OBJECT_ID(N'[dbo].[Customers]'))
BEGIN
    ALTER TABLE [dbo].[Customers] WITH CHECK ADD CONSTRAINT [FK_Customers_Users_Modified] FOREIGN KEY([ModifiedBy])
    REFERENCES [dbo].[Users] ([UserId])
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Products_Users_Created]') AND parent_object_id = OBJECT_ID(N'[dbo].[Products]'))
BEGIN
    ALTER TABLE [dbo].[Products] WITH CHECK ADD CONSTRAINT [FK_Products_Users_Created] FOREIGN KEY([CreatedBy])
    REFERENCES [dbo].[Users] ([UserId])
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Products_Users_Modified]') AND parent_object_id = OBJECT_ID(N'[dbo].[Products]'))
BEGIN
    ALTER TABLE [dbo].[Products] WITH CHECK ADD CONSTRAINT [FK_Products_Users_Modified] FOREIGN KEY([ModifiedBy])
    REFERENCES [dbo].[Users] ([UserId])
END
GO

PRINT 'Missing columns added successfully to Customers and Products tables!'
PRINT 'Database is now ready for the Pakistan POS System!'
