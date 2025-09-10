-- Simple Sales Return Tables (without foreign key dependencies)
-- This script creates the Sales Return tables without requiring other tables to exist first

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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UK_SalesReturns_ReturnNumber] UNIQUE NONCLUSTERED 
(
    [ReturnNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Add Foreign Key Constraint for SalesReturnItems to SalesReturns
ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [FK_SalesReturnItems_SalesReturns] 
FOREIGN KEY([ReturnId]) REFERENCES [dbo].[SalesReturns] ([ReturnId]) ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [FK_SalesReturnItems_SalesReturns]
GO

-- Add indexes for better performance
CREATE NONCLUSTERED INDEX [IX_SalesReturns_CustomerId] ON [dbo].[SalesReturns]
(
    [CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_SalesReturns_ReturnDate] ON [dbo].[SalesReturns]
(
    [ReturnDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_SalesReturns_Status] ON [dbo].[SalesReturns]
(
    [Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_SalesReturnItems_ProductId] ON [dbo].[SalesReturnItems]
(
    [ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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

-- Create stored procedures for common operations
CREATE PROCEDURE [dbo].[sp_GetSalesReturnsByDateRange]
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        sr.ReturnId,
        sr.ReturnNumber,
        sr.ReturnDate,
        sr.CustomerId,
        sr.ReferenceSalesInvoiceId,
        sr.Reason,
        sr.TotalAmount,
        sr.Status,
        sr.CreatedDate
    FROM SalesReturns sr
    WHERE sr.ReturnDate BETWEEN @StartDate AND @EndDate
    ORDER BY sr.ReturnDate DESC, sr.ReturnId DESC;
END
GO

CREATE PROCEDURE [dbo].[sp_GetSalesReturnDetails]
    @ReturnId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        sri.ReturnItemId,
        sri.ProductId,
        sri.Quantity,
        sri.UnitPrice,
        sri.TaxAmount,
        sri.DiscountAmount,
        sri.TotalAmount,
        sri.Remarks,
        sri.StockUpdated
    FROM SalesReturnItems sri
    WHERE sri.ReturnId = @ReturnId
    ORDER BY sri.ReturnItemId;
END
GO

CREATE PROCEDURE [dbo].[sp_UpdateStockForSalesReturn]
    @ReturnId INT,
    @UpdatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Mark items as stock updated (actual stock update would be done when Products table exists)
        UPDATE SalesReturnItems
        SET StockUpdated = 1,
            StockUpdatedDate = GETDATE(),
            StockUpdatedBy = @UpdatedBy
        WHERE ReturnId = @ReturnId AND StockUpdated = 0;
        
        -- Update return status
        UPDATE SalesReturns
        SET Status = 'PROCESSED',
            ProcessedDate = GETDATE(),
            ProcessedBy = @UpdatedBy
        WHERE ReturnId = @ReturnId;
        
        COMMIT TRANSACTION;
        
        SELECT 'SUCCESS' as Result, 'Stock update marked successfully' as Message;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 'ERROR' as Result, ERROR_MESSAGE() as Message;
    END CATCH
END
GO

PRINT 'Sales Return tables created successfully!'
PRINT 'Features included:'
PRINT '- SalesReturns master table'
PRINT '- SalesReturnItems detail table'
PRINT '- Data validation constraints'
PRINT '- Performance indexes'
PRINT '- Sample test data'
PRINT '- Stored procedures for common operations'
PRINT '- Stock update tracking (ready for when Products table exists)'
PRINT '- Audit trail fields'
PRINT ''
PRINT 'Note: Foreign keys to Customers, Products, and SalesInvoices will be added'
PRINT 'when those tables are created in your database.'
