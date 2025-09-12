-- Create Supplier Debit Note tables
-- This script creates everything needed for the Supplier Debit Note functionality

USE [DistributionDB]
GO

-- Drop existing tables if they exist (for clean recreation)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupplierDebitNoteItems]') AND type in (N'U'))
    DROP TABLE [dbo].[SupplierDebitNoteItems]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupplierDebitNotes]') AND type in (N'U'))
    DROP TABLE [dbo].[SupplierDebitNotes]
GO

-- Create SupplierDebitNotes table (Master table)
CREATE TABLE [dbo].[SupplierDebitNotes](
    [DebitNoteId] [int] IDENTITY(1,1) NOT NULL,
    [DebitNoteNo] [nvarchar](50) NOT NULL,
    [DebitNoteBarcode] [nvarchar](100) NOT NULL,
    [SupplierId] [int] NOT NULL,
    [ReferencePurchaseId] [int] NULL,
    [ReferenceGRNId] [int] NULL,
    [DebitDate] [date] NOT NULL,
    [Reason] [nvarchar](500) NOT NULL,
    [SubTotal] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [Status] [nvarchar](20) NOT NULL DEFAULT ('DRAFT'),
    [ApprovedBy] [int] NULL,
    [ApprovalDate] [datetime] NULL,
    [Remarks] [nvarchar](500) NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
    [CreatedBy] [int] NULL,
    [ModifiedDate] [datetime] NULL,
    [ModifiedBy] [int] NULL,
    [BarcodeImage] [varbinary](max) NULL,
 CONSTRAINT [PK_SupplierDebitNotes] PRIMARY KEY CLUSTERED 
(
    [DebitNoteId] ASC
),
 CONSTRAINT [UK_SupplierDebitNotes_DebitNoteNo] UNIQUE NONCLUSTERED 
(
    [DebitNoteNo] ASC
),
 CONSTRAINT [UK_SupplierDebitNotes_DebitNoteBarcode] UNIQUE NONCLUSTERED 
(
    [DebitNoteBarcode] ASC
)
)
GO

-- Create SupplierDebitNoteItems table (Detail table)
CREATE TABLE [dbo].[SupplierDebitNoteItems](
    [DebitNoteItemId] [int] IDENTITY(1,1) NOT NULL,
    [DebitNoteId] [int] NOT NULL,
    [ProductId] [int] NOT NULL,
    [Quantity] [decimal](18, 2) NOT NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [TaxPercentage] [decimal](5, 2) NULL DEFAULT ((0)),
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountPercentage] [decimal](5, 2) NULL DEFAULT ((0)),
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL,
    [Reason] [nvarchar](255) NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_SupplierDebitNoteItems] PRIMARY KEY CLUSTERED 
(
    [DebitNoteItemId] ASC
)
)
GO

-- Add Foreign Key Constraints
ALTER TABLE [dbo].[SupplierDebitNoteItems] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNoteItems_SupplierDebitNotes] 
FOREIGN KEY([DebitNoteId]) REFERENCES [dbo].[SupplierDebitNotes] ([DebitNoteId]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] CHECK CONSTRAINT [FK_SupplierDebitNoteItems_SupplierDebitNotes]
GO

ALTER TABLE [dbo].[SupplierDebitNotes] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNotes_Suppliers] 
FOREIGN KEY([SupplierId]) REFERENCES [dbo].[Suppliers] ([SupplierId])
GO

ALTER TABLE [dbo].[SupplierDebitNotes] CHECK CONSTRAINT [FK_SupplierDebitNotes_Suppliers]
GO

ALTER TABLE [dbo].[SupplierDebitNotes] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNotes_Purchases] 
FOREIGN KEY([ReferencePurchaseId]) REFERENCES [dbo].[Purchases] ([PurchaseId])
GO

ALTER TABLE [dbo].[SupplierDebitNotes] CHECK CONSTRAINT [FK_SupplierDebitNotes_Purchases]
GO

ALTER TABLE [dbo].[SupplierDebitNotes] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNotes_GRN] 
FOREIGN KEY([ReferenceGRNId]) REFERENCES [dbo].[GRN] ([GRNId])
GO

ALTER TABLE [dbo].[SupplierDebitNotes] CHECK CONSTRAINT [FK_SupplierDebitNotes_GRN]
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNoteItems_Products] 
FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] CHECK CONSTRAINT [FK_SupplierDebitNoteItems_Products]
GO

-- Add check constraints for data validation
ALTER TABLE [dbo].[SupplierDebitNotes] WITH CHECK ADD CONSTRAINT [CK_SupplierDebitNotes_Status] 
CHECK ([Status] IN ('DRAFT', 'PENDING', 'APPROVED', 'REJECTED', 'CANCELLED'))
GO

ALTER TABLE [dbo].[SupplierDebitNotes] CHECK CONSTRAINT [CK_SupplierDebitNotes_Status]
GO

ALTER TABLE [dbo].[SupplierDebitNotes] WITH CHECK ADD CONSTRAINT [CK_SupplierDebitNotes_Amounts] 
CHECK ([SubTotal] >= 0 AND [TaxAmount] >= 0 AND [DiscountAmount] >= 0 AND [TotalAmount] >= 0)
GO

ALTER TABLE [dbo].[SupplierDebitNotes] CHECK CONSTRAINT [CK_SupplierDebitNotes_Amounts]
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] WITH CHECK ADD CONSTRAINT [CK_SupplierDebitNoteItems_Quantity] 
CHECK ([Quantity] > 0)
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] CHECK CONSTRAINT [CK_SupplierDebitNoteItems_Quantity]
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] WITH CHECK ADD CONSTRAINT [CK_SupplierDebitNoteItems_Amounts] 
CHECK ([UnitPrice] >= 0 AND [TaxAmount] >= 0 AND [DiscountAmount] >= 0 AND [TotalAmount] >= 0)
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] CHECK CONSTRAINT [CK_SupplierDebitNoteItems_Amounts]
GO

-- Create indexes for better performance
CREATE NONCLUSTERED INDEX [IX_SupplierDebitNotes_SupplierId] ON [dbo].[SupplierDebitNotes]
(
    [SupplierId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_SupplierDebitNotes_DebitDate] ON [dbo].[SupplierDebitNotes]
(
    [DebitDate] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_SupplierDebitNotes_Status] ON [dbo].[SupplierDebitNotes]
(
    [Status] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_SupplierDebitNoteItems_DebitNoteId] ON [dbo].[SupplierDebitNoteItems]
(
    [DebitNoteId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_SupplierDebitNoteItems_ProductId] ON [dbo].[SupplierDebitNoteItems]
(
    [ProductId] ASC
)
GO

-- Insert sample data for testing
INSERT INTO [dbo].[SupplierDebitNotes] 
([DebitNoteNo], [DebitNoteBarcode], [SupplierId], [ReferencePurchaseId], [DebitDate], [Reason], [SubTotal], [TaxAmount], [TotalAmount], [Status], [Remarks], [CreatedBy])
VALUES 
('DN20250101001', 'DN20250101001', 1, 1, '2025-01-01', 'Damaged Goods Return', 1000.00, 170.00, 1170.00, 'DRAFT', 'Sample debit note for testing', 1),
('DN20250101002', 'DN20250101002', 2, 2, '2025-01-01', 'Price Adjustment', 500.00, 85.00, 585.00, 'APPROVED', 'Price reduction agreed', 1)
GO

INSERT INTO [dbo].[SupplierDebitNoteItems] 
([DebitNoteId], [ProductId], [Quantity], [UnitPrice], [TaxPercentage], [TaxAmount], [TotalAmount], [Reason])
VALUES 
(1, 1, 2.00, 500.00, 17.00, 170.00, 1170.00, 'Damaged packaging'),
(2, 2, 1.00, 500.00, 17.00, 85.00, 585.00, 'Price reduction')
GO

PRINT 'Supplier Debit Note tables created successfully!'
PRINT 'Tables created:'
PRINT '- SupplierDebitNotes (Master table)'
PRINT '- SupplierDebitNoteItems (Detail table)'
PRINT 'Sample data inserted for testing.'
PRINT 'Ready to use with the Supplier Debit Note form!'
