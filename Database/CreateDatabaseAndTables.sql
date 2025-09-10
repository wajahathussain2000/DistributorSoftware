-- Create DistributionDB database and Sales Return tables
-- This script creates everything needed for the Sales Return functionality

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DistributionDB')
BEGIN
    CREATE DATABASE [DistributionDB]
END
GO

USE [DistributionDB]
GO

-- Drop existing tables if they exist (for clean recreation)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesReturnItems]') AND type in (N'U'))
    DROP TABLE [dbo].[SalesReturnItems]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesReturns]') AND type in (N'U'))
    DROP TABLE [dbo].[SalesReturns]
GO

-- Create SalesReturns table (Master table)
CREATE TABLE [dbo].[SalesReturns](
    [ReturnId] [int] IDENTITY(1,1) NOT NULL,
    [ReturnNumber] [nvarchar](50) NOT NULL,
    [ReturnBarcode] [nvarchar](100) NULL,
    [CustomerId] [int] NOT NULL,
    [ReferenceSalesInvoiceId] [int] NULL,
    [ReturnDate] [date] NOT NULL,
    [Reason] [nvarchar](100) NOT NULL,
    [SubTotal] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [Status] [nvarchar](20) NOT NULL DEFAULT ('PENDING'),
    [Remarks] [nvarchar](500) NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
    [CreatedBy] [int] NULL,
    [ModifiedDate] [datetime] NULL,
    [ModifiedBy] [int] NULL,
    [ProcessedDate] [datetime] NULL,
    [ProcessedBy] [int] NULL,
    [ApprovedDate] [datetime] NULL,
    [ApprovedBy] [int] NULL,
 CONSTRAINT [PK_SalesReturns] PRIMARY KEY CLUSTERED 
(
    [ReturnId] ASC
),
 CONSTRAINT [UK_SalesReturns_ReturnNumber] UNIQUE NONCLUSTERED 
(
    [ReturnNumber] ASC
)
)
GO

-- Create SalesReturnItems table (Detail table)
CREATE TABLE [dbo].[SalesReturnItems](
    [ReturnItemId] [int] IDENTITY(1,1) NOT NULL,
    [ReturnId] [int] NOT NULL,
    [ProductId] [int] NOT NULL,
    [OriginalInvoiceItemId] [int] NULL,
    [Quantity] [decimal](18, 2) NOT NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [TaxPercentage] [decimal](5, 2) NULL DEFAULT ((0)),
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountPercentage] [decimal](5, 2) NULL DEFAULT ((0)),
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL,
    [Remarks] [nvarchar](255) NULL,
    [StockUpdated] [bit] NOT NULL DEFAULT ((0)),
    [StockUpdatedDate] [datetime] NULL,
    [StockUpdatedBy] [int] NULL,
 CONSTRAINT [PK_SalesReturnItems] PRIMARY KEY CLUSTERED 
(
    [ReturnItemId] ASC
)
)
GO

-- Add Foreign Key Constraint for SalesReturnItems to SalesReturns
ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [FK_SalesReturnItems_SalesReturns] 
FOREIGN KEY([ReturnId]) REFERENCES [dbo].[SalesReturns] ([ReturnId]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [FK_SalesReturnItems_SalesReturns]
GO

-- Add check constraints for data validation
ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [CK_SalesReturns_Status] 
CHECK ([Status] IN ('PENDING', 'PROCESSED', 'APPROVED', 'REJECTED', 'CANCELLED'))
GO

ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [CK_SalesReturns_Status]
GO

ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [CK_SalesReturns_Amounts] 
CHECK ([SubTotal] >= 0 AND [TaxAmount] >= 0 AND [DiscountAmount] >= 0 AND [TotalAmount] >= 0)
GO

ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [CK_SalesReturns_Amounts]
GO

ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [CK_SalesReturnItems_Quantity] 
CHECK ([Quantity] > 0)
GO

ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [CK_SalesReturnItems_Quantity]
GO

ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [CK_SalesReturnItems_Amounts] 
CHECK ([UnitPrice] >= 0 AND [TaxAmount] >= 0 AND [DiscountAmount] >= 0 AND [TotalAmount] >= 0)
GO

ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [CK_SalesReturnItems_Amounts]
GO

-- Insert sample data for testing
INSERT INTO [dbo].[SalesReturns] 
([ReturnNumber], [CustomerId], [ReferenceSalesInvoiceId], [ReturnDate], [Reason], [SubTotal], [TaxAmount], [DiscountAmount], [TotalAmount], [Status], [Remarks])
VALUES 
('SR20250101001', 1, 1, '2025-01-01', 'Damaged Product', 100.00, 10.00, 0.00, 110.00, 'PENDING', 'Sample return for testing'),
('SR20250101002', 2, 2, '2025-01-01', 'Wrong Item', 50.00, 5.00, 0.00, 55.00, 'PROCESSED', 'Customer received wrong size')
GO

INSERT INTO [dbo].[SalesReturnItems] 
([ReturnId], [ProductId], [Quantity], [UnitPrice], [TaxPercentage], [TaxAmount], [DiscountPercentage], [DiscountAmount], [TotalAmount], [Remarks])
VALUES 
(1, 1, 2.00, 50.00, 10.00, 10.00, 0.00, 0.00, 110.00, 'Damaged packaging'),
(2, 2, 1.00, 50.00, 10.00, 5.00, 0.00, 0.00, 55.00, 'Wrong color received')
GO

PRINT 'Database DistributionDB and Sales Return tables created successfully!'
PRINT 'Tables created:'
PRINT '- SalesReturns (Master table)'
PRINT '- SalesReturnItems (Detail table)'
PRINT 'Sample data inserted for testing.'
PRINT 'Ready to use with the Sales Return form!'
