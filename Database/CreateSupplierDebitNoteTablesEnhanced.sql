-- Enhanced Supplier Debit Note tables with proper business workflow
-- This script creates tables that properly handle the Invoice -> Debit Note -> Balance workflow

USE [DistributionDB]
GO

-- Drop existing tables if they exist (for clean recreation)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupplierDebitNoteItems]') AND type in (N'U'))
    DROP TABLE [dbo].[SupplierDebitNoteItems]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SupplierDebitNotes]') AND type in (N'U'))
    DROP TABLE [dbo].[SupplierDebitNotes]
GO

-- Create SupplierDebitNotes table (Master table) with enhanced business logic
CREATE TABLE [dbo].[SupplierDebitNotes](
    [DebitNoteId] [int] IDENTITY(1,1) NOT NULL,
    [DebitNoteNo] [nvarchar](50) NOT NULL,
    [DebitNoteBarcode] [nvarchar](100) NOT NULL,
    [SupplierId] [int] NOT NULL,
    
    -- Core Business Fields
    [OriginalInvoiceId] [int] NULL, -- Links back to the original supplier invoice
    [OriginalInvoiceNo] [nvarchar](50) NULL, -- Reference to original invoice number
    [OriginalInvoiceAmount] [decimal](18, 2) NULL, -- Original invoice amount
    
    -- Reference Information
    [ReferencePurchaseId] [int] NULL,
    [ReferenceGRNId] [int] NULL,
    
    -- Debit Note Details
    [DebitDate] [date] NOT NULL,
    [Reason] [nvarchar](500) NOT NULL,
    [ReasonCode] [nvarchar](20) NULL, -- DAMAGE, SHORTAGE, OVERCHARGE, QUALITY, etc.
    
    -- Amount Calculations
    [SubTotal] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    
    -- Balance Impact
    [BalanceImpact] [decimal](18, 2) NOT NULL DEFAULT ((0)), -- How much this reduces the payable
    [NewPayableBalance] [decimal](18, 2) NULL, -- Calculated new balance after this debit note
    
    -- Status and Workflow
    [Status] [nvarchar](20) NOT NULL DEFAULT ('DRAFT'),
    [ApprovedBy] [int] NULL,
    [ApprovalDate] [datetime] NULL,
    [SupplierAcknowledged] [bit] NOT NULL DEFAULT ((0)),
    [SupplierAcknowledgedDate] [datetime] NULL,
    
    -- Additional Information
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

-- Create SupplierDebitNoteItems table (Detail table) with enhanced fields
CREATE TABLE [dbo].[SupplierDebitNoteItems](
    [DebitNoteItemId] [int] IDENTITY(1,1) NOT NULL,
    [DebitNoteId] [int] NOT NULL,
    [ProductId] [int] NOT NULL,
    
    -- Item Details
    [Quantity] [decimal](18, 2) NOT NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [OriginalQuantity] [decimal](18, 2) NULL, -- Original quantity from invoice
    [OriginalUnitPrice] [decimal](18, 2) NULL, -- Original unit price from invoice
    
    -- Calculations
    [TaxPercentage] [decimal](5, 2) NULL DEFAULT ((0)),
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountPercentage] [decimal](5, 2) NULL DEFAULT ((0)),
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL,
    
    -- Business Information
    [Reason] [nvarchar](255) NULL,
    [ReasonCode] [nvarchar](20) NULL, -- DAMAGE, SHORTAGE, OVERCHARGE, QUALITY, etc.
    [BatchNumber] [nvarchar](50) NULL,
    [ExpiryDate] [date] NULL,
    
    -- Audit Fields
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
    
 CONSTRAINT [PK_SupplierDebitNoteItems] PRIMARY KEY CLUSTERED 
(
    [DebitNoteItemId] ASC
)
)
GO

-- Create Supplier Balance Tracking Table
CREATE TABLE [dbo].[SupplierBalances](
    [BalanceId] [int] IDENTITY(1,1) NOT NULL,
    [SupplierId] [int] NOT NULL,
    [TransactionType] [nvarchar](20) NOT NULL, -- INVOICE, DEBIT_NOTE, PAYMENT, ADJUSTMENT
    [TransactionId] [int] NOT NULL, -- ID of the related transaction
    [TransactionNo] [nvarchar](50) NOT NULL,
    [TransactionDate] [date] NOT NULL,
    [Amount] [decimal](18, 2) NOT NULL, -- Positive for invoices, negative for debit notes/payments
    [RunningBalance] [decimal](18, 2) NOT NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
    [CreatedBy] [int] NULL,
    
 CONSTRAINT [PK_SupplierBalances] PRIMARY KEY CLUSTERED 
(
    [BalanceId] ASC
)
)
GO

-- Add Foreign Key Constraints (only if referenced tables exist)
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Suppliers]') AND type in (N'U'))
BEGIN
    ALTER TABLE [dbo].[SupplierDebitNotes] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNotes_Suppliers] 
    FOREIGN KEY([SupplierId]) REFERENCES [dbo].[Suppliers] ([SupplierId])
    ALTER TABLE [dbo].[SupplierDebitNotes] CHECK CONSTRAINT [FK_SupplierDebitNotes_Suppliers]
    
    ALTER TABLE [dbo].[SupplierBalances] WITH CHECK ADD CONSTRAINT [FK_SupplierBalances_Suppliers] 
    FOREIGN KEY([SupplierId]) REFERENCES [dbo].[Suppliers] ([SupplierId])
    ALTER TABLE [dbo].[SupplierBalances] CHECK CONSTRAINT [FK_SupplierBalances_Suppliers]
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
    ALTER TABLE [dbo].[SupplierDebitNoteItems] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNoteItems_Products] 
    FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
    ALTER TABLE [dbo].[SupplierDebitNoteItems] CHECK CONSTRAINT [FK_SupplierDebitNoteItems_Products]
END
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] WITH CHECK ADD CONSTRAINT [FK_SupplierDebitNoteItems_SupplierDebitNotes] 
FOREIGN KEY([DebitNoteId]) REFERENCES [dbo].[SupplierDebitNotes] ([DebitNoteId]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SupplierDebitNoteItems] CHECK CONSTRAINT [FK_SupplierDebitNoteItems_SupplierDebitNotes]
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

ALTER TABLE [dbo].[SupplierBalances] WITH CHECK ADD CONSTRAINT [CK_SupplierBalances_TransactionType] 
CHECK ([TransactionType] IN ('INVOICE', 'DEBIT_NOTE', 'PAYMENT', 'ADJUSTMENT'))
GO

ALTER TABLE [dbo].[SupplierBalances] CHECK CONSTRAINT [CK_SupplierBalances_TransactionType]
GO

-- Create indexes for better performance
CREATE NONCLUSTERED INDEX [IX_SupplierDebitNotes_SupplierId] ON [dbo].[SupplierDebitNotes]
(
    [SupplierId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_SupplierDebitNotes_OriginalInvoiceId] ON [dbo].[SupplierDebitNotes]
(
    [OriginalInvoiceId] ASC
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

CREATE NONCLUSTERED INDEX [IX_SupplierBalances_SupplierId] ON [dbo].[SupplierBalances]
(
    [SupplierId] ASC
)
GO

CREATE NONCLUSTERED INDEX [IX_SupplierBalances_TransactionDate] ON [dbo].[SupplierBalances]
(
    [TransactionDate] ASC
)
GO

-- Create stored procedures for business logic

-- Procedure to calculate supplier balance
CREATE PROCEDURE [dbo].[sp_CalculateSupplierBalance]
    @SupplierId INT,
    @AsOfDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @AsOfDate IS NULL
        SET @AsOfDate = GETDATE()
    
    SELECT 
        @SupplierId AS SupplierId,
        ISNULL(SUM(CASE WHEN TransactionType = 'INVOICE' THEN Amount ELSE 0 END), 0) AS TotalInvoices,
        ISNULL(SUM(CASE WHEN TransactionType = 'DEBIT_NOTE' THEN Amount ELSE 0 END), 0) AS TotalDebitNotes,
        ISNULL(SUM(CASE WHEN TransactionType = 'PAYMENT' THEN Amount ELSE 0 END), 0) AS TotalPayments,
        ISNULL(SUM(Amount), 0) AS CurrentBalance
    FROM SupplierBalances 
    WHERE SupplierId = @SupplierId 
    AND TransactionDate <= @AsOfDate
END
GO

-- Procedure to update supplier balance when debit note is created
CREATE PROCEDURE [dbo].[sp_UpdateSupplierBalanceOnDebitNote]
    @SupplierId INT,
    @DebitNoteId INT,
    @DebitNoteNo NVARCHAR(50),
    @DebitDate DATE,
    @Amount DECIMAL(18,2),
    @CreatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @RunningBalance DECIMAL(18,2)
    
    -- Get current running balance
    SELECT @RunningBalance = ISNULL(MAX(RunningBalance), 0)
    FROM SupplierBalances 
    WHERE SupplierId = @SupplierId
    
    -- Calculate new running balance (debit note reduces the balance)
    SET @RunningBalance = @RunningBalance - @Amount
    
    -- Insert balance record
    INSERT INTO SupplierBalances 
    (SupplierId, TransactionType, TransactionId, TransactionNo, TransactionDate, Amount, RunningBalance, CreatedBy)
    VALUES 
    (@SupplierId, 'DEBIT_NOTE', @DebitNoteId, @DebitNoteNo, @DebitDate, -@Amount, @RunningBalance, @CreatedBy)
    
    -- Update the debit note with new balance
    UPDATE SupplierDebitNotes 
    SET NewPayableBalance = @RunningBalance,
        BalanceImpact = @Amount
    WHERE DebitNoteId = @DebitNoteId
    
    SELECT @RunningBalance AS NewBalance
END
GO

-- Insert sample data for testing
INSERT INTO [dbo].[SupplierDebitNotes] 
([DebitNoteNo], [DebitNoteBarcode], [SupplierId], [OriginalInvoiceNo], [OriginalInvoiceAmount], 
 [DebitDate], [Reason], [ReasonCode], [SubTotal], [TaxAmount], [TotalAmount], 
 [BalanceImpact], [Status], [Remarks], [CreatedBy])
VALUES 
('DN20250101001', 'DN20250101001', 1, 'INV-2024-001', 1000.00, '2025-01-01', 
 'Damaged Goods Return', 'DAMAGE', 200.00, 34.00, 234.00, 234.00, 'DRAFT', 
 'Sample debit note for testing', 1)
GO

INSERT INTO [dbo].[SupplierDebitNoteItems] 
([DebitNoteId], [ProductId], [Quantity], [UnitPrice], [OriginalQuantity], [OriginalUnitPrice],
 [TaxPercentage], [TaxAmount], [TotalAmount], [Reason], [ReasonCode])
VALUES 
(1, 1, 2.00, 100.00, 5.00, 100.00, 17.00, 34.00, 234.00, 'Damaged packaging', 'DAMAGE')
GO

-- Insert sample balance record
INSERT INTO [dbo].[SupplierBalances] 
([SupplierId], [TransactionType], [TransactionId], [TransactionNo], [TransactionDate], 
 [Amount], [RunningBalance], [CreatedBy])
VALUES 
(1, 'INVOICE', 1, 'INV-2024-001', '2024-12-01', 1000.00, 1000.00, 1)
GO

PRINT 'Enhanced Supplier Debit Note tables created successfully!'
PRINT 'Tables created:'
PRINT '- SupplierDebitNotes (Enhanced Master table with invoice linking)'
PRINT '- SupplierDebitNoteItems (Enhanced Detail table)'
PRINT '- SupplierBalances (Balance tracking table)'
PRINT 'Stored procedures created:'
PRINT '- sp_CalculateSupplierBalance'
PRINT '- sp_UpdateSupplierBalanceOnDebitNote'
PRINT 'Sample data inserted for testing.'
PRINT 'Ready to use with enhanced business workflow!'
