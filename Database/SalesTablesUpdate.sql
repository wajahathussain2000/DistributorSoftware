-- Sales Tables Update Script for Pakistan POS System
-- This script adds missing columns and creates missing tables

USE [DistributionDB]
GO

-- Add missing columns to SalesInvoices table for POS system
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'CustomerName')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [CustomerName] [nvarchar](100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'CustomerPhone')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [CustomerPhone] [nvarchar](20) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'CustomerAddress')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [CustomerAddress] [nvarchar](500) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'TaxableAmount')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [TaxableAmount] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'TaxPercentage')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [TaxPercentage] [decimal](5, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'DiscountPercentage')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [DiscountPercentage] [decimal](5, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'ChangeAmount')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [ChangeAmount] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

-- Barcode and Printing columns for POS system
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'Barcode')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [Barcode] [nvarchar](50) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'BarcodeImage')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [BarcodeImage] [varbinary](max) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'PrintStatus')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [PrintStatus] [bit] NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'PrintDate')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [PrintDate] [datetime] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'CashierId')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [CashierId] [int] NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = 'TransactionTime')
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] ADD [TransactionTime] [datetime] NOT NULL DEFAULT (getdate())
END
GO

-- Add missing columns to SalesInvoiceDetails table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoiceDetails]') AND name = 'ProductCode')
BEGIN
    ALTER TABLE [dbo].[SalesInvoiceDetails] ADD [ProductCode] [nvarchar](50) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoiceDetails]') AND name = 'ProductName')
BEGIN
    ALTER TABLE [dbo].[SalesInvoiceDetails] ADD [ProductName] [nvarchar](100) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoiceDetails]') AND name = 'ProductDescription')
BEGIN
    ALTER TABLE [dbo].[SalesInvoiceDetails] ADD [ProductDescription] [nvarchar](500) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoiceDetails]') AND name = 'TaxableAmount')
BEGIN
    ALTER TABLE [dbo].[SalesInvoiceDetails] ADD [TaxableAmount] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoiceDetails]') AND name = 'LineTotal')
BEGIN
    ALTER TABLE [dbo].[SalesInvoiceDetails] ADD [LineTotal] [decimal](18, 2) NOT NULL DEFAULT ((0))
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoiceDetails]') AND name = 'BatchNumber')
BEGIN
    ALTER TABLE [dbo].[SalesInvoiceDetails] ADD [BatchNumber] [nvarchar](50) NULL
END
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoiceDetails]') AND name = 'ExpiryDate')
BEGIN
    ALTER TABLE [dbo].[SalesInvoiceDetails] ADD [ExpiryDate] [date] NULL
END
GO

-- Create SalesPayments table for POS system
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesPayments]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[SalesPayments](
        [PaymentId] [int] IDENTITY(1,1) NOT NULL,
        [SalesInvoiceId] [int] NOT NULL,
        [PaymentMode] [nvarchar](20) NOT NULL,
        [Amount] [decimal](18, 2) NOT NULL,
        [CardNumber] [nvarchar](20) NULL,
        [CardType] [nvarchar](20) NULL,
        [TransactionId] [nvarchar](50) NULL,
        [BankName] [nvarchar](100) NULL,
        [ChequeNumber] [nvarchar](20) NULL,
        [ChequeDate] [date] NULL,
        [MobileNumber] [nvarchar](20) NULL,
        [PaymentReference] [nvarchar](100) NULL,
        [PaymentDate] [datetime] NOT NULL DEFAULT (getdate()),
        [Status] [nvarchar](20) NOT NULL DEFAULT ('COMPLETED'),
        [Remarks] [nvarchar](500) NULL,
        [CreatedBy] [int] NOT NULL,
        [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_SalesPayments] PRIMARY KEY CLUSTERED 
        (
            [PaymentId] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
    ) ON [PRIMARY]
END
GO

-- Add foreign key constraints for SalesPayments
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SalesPayments_SalesInvoices]') AND parent_object_id = OBJECT_ID(N'[dbo].[SalesPayments]'))
BEGIN
    ALTER TABLE [dbo].[SalesPayments] WITH CHECK ADD CONSTRAINT [FK_SalesPayments_SalesInvoices] FOREIGN KEY([SalesInvoiceId])
    REFERENCES [dbo].[SalesInvoices] ([SalesInvoiceId])
    ON DELETE CASCADE
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SalesPayments_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[SalesPayments]'))
BEGIN
    ALTER TABLE [dbo].[SalesPayments] WITH CHECK ADD CONSTRAINT [FK_SalesPayments_Users] FOREIGN KEY([CreatedBy])
    REFERENCES [dbo].[Users] ([UserId])
END
GO

-- Add foreign key constraint for CashierId in SalesInvoices
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_SalesInvoices_Users_Cashier]') AND parent_object_id = OBJECT_ID(N'[dbo].[SalesInvoices]'))
BEGIN
    ALTER TABLE [dbo].[SalesInvoices] WITH CHECK ADD CONSTRAINT [FK_SalesInvoices_Users_Cashier] FOREIGN KEY([CashierId])
    REFERENCES [dbo].[Users] ([UserId])
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = N'IX_SalesInvoices_InvoiceNumber')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SalesInvoices_InvoiceNumber] ON [dbo].[SalesInvoices]
    (
        [InvoiceNumber] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = N'IX_SalesInvoices_CustomerId')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SalesInvoices_CustomerId] ON [dbo].[SalesInvoices]
    (
        [CustomerId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[SalesInvoices]') AND name = N'IX_SalesInvoices_InvoiceDate')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_SalesInvoices_InvoiceDate] ON [dbo].[SalesInvoices]
    (
        [InvoiceDate] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
END
GO

-- Insert sample data for testing (optional)
-- Uncomment the following section if you want sample data

/*
-- Sample Sales Invoice Data
INSERT INTO SalesInvoices (
    InvoiceNumber, CustomerId, CustomerName, CustomerPhone, CustomerAddress,
    InvoiceDate, SubTotal, DiscountAmount, DiscountPercentage, TaxableAmount,
    TaxAmount, TaxPercentage, TotalAmount, PaidAmount, ChangeAmount,
    PaymentMode, Status, Remarks, CreatedBy, CreatedDate, TransactionTime,
    Barcode, PrintStatus, CashierId
) VALUES (
    '2025090001', 1, 'ABC Company Ltd.', '+92-21-1234567', '123 Main Street, Karachi',
    GETDATE(), 10000.00, 500.00, 5.00, 9500.00,
    1615.00, 17.00, 11115.00, 11115.00, 0.00,
    'CASH', 'PAID', 'Sample invoice for testing', 1, GETDATE(), GETDATE(),
    '2025090001', 1, 1
)

-- Sample Sales Invoice Details
INSERT INTO SalesInvoiceDetails (
    SalesInvoiceId, ProductId, ProductCode, ProductName, Quantity, UnitPrice,
    DiscountAmount, DiscountPercentage, TaxableAmount, TaxAmount, TaxPercentage,
    LineTotal, Remarks
) VALUES (
    1, 1, 'P001', 'Sample Product A', 10, 1000.00,
    0.00, 0.00, 10000.00, 1700.00, 17.00,
    11700.00, 'Sample product detail'
)

-- Sample Sales Payment
INSERT INTO SalesPayments (
    SalesInvoiceId, PaymentMode, Amount, PaymentDate, Status, Remarks, CreatedBy, CreatedDate
) VALUES (
    1, 'CASH', 11115.00, GETDATE(), 'COMPLETED', 'Cash payment', 1, GETDATE()
)
*/

PRINT 'Sales tables updated successfully for Pakistan POS System!'
PRINT 'Added missing columns and created SalesPayments table.'
PRINT 'Ready for production use!'
