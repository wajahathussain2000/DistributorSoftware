-- Add Foreign Key relationships to Sales Return tables
-- This script adds the proper foreign key constraints to link with existing tables

USE [DistributionDB]
GO

-- Add Foreign Key Constraint for SalesReturns to Customers
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesReturns_Customers')
BEGIN
    ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [FK_SalesReturns_Customers] 
    FOREIGN KEY([CustomerId]) REFERENCES [dbo].[Customers] ([CustomerId])
    
    ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_Customers]
    PRINT 'Added FK_SalesReturns_Customers constraint'
END
ELSE
    PRINT 'FK_SalesReturns_Customers constraint already exists'
GO

-- Add Foreign Key Constraint for SalesReturns to SalesInvoices
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesReturns_SalesInvoices')
BEGIN
    ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [FK_SalesReturns_SalesInvoices] 
    FOREIGN KEY([ReferenceSalesInvoiceId]) REFERENCES [dbo].[SalesInvoices] ([SalesInvoiceId])
    
    ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_SalesInvoices]
    PRINT 'Added FK_SalesReturns_SalesInvoices constraint'
END
ELSE
    PRINT 'FK_SalesReturns_SalesInvoices constraint already exists'
GO

-- Add Foreign Key Constraint for SalesReturnItems to Products
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesReturnItems_Products')
BEGIN
    ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [FK_SalesReturnItems_Products] 
    FOREIGN KEY([ProductId]) REFERENCES [dbo].[Products] ([ProductId])
    
    ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [FK_SalesReturnItems_Products]
    PRINT 'Added FK_SalesReturnItems_Products constraint'
END
ELSE
    PRINT 'FK_SalesReturnItems_Products constraint already exists'
GO

-- Add Foreign Key Constraint for SalesReturnItems to SalesInvoiceDetails (if needed)
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesReturnItems_SalesInvoiceDetails')
BEGIN
    ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [FK_SalesReturnItems_SalesInvoiceDetails] 
    FOREIGN KEY([OriginalInvoiceItemId]) REFERENCES [dbo].[SalesInvoiceDetails] ([SalesInvoiceDetailId])
    
    ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [FK_SalesReturnItems_SalesInvoiceDetails]
    PRINT 'Added FK_SalesReturnItems_SalesInvoiceDetails constraint'
END
ELSE
    PRINT 'FK_SalesReturnItems_SalesInvoiceDetails constraint already exists'
GO

-- Update the stored procedures to use correct column names
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetSalesReturnsByDateRange')
    DROP PROCEDURE [dbo].[sp_GetSalesReturnsByDateRange]
GO

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
        c.ContactName as CustomerName,
        si.InvoiceNumber as ReferenceInvoice,
        sr.Reason,
        sr.TotalAmount,
        sr.Status,
        sr.CreatedDate
    FROM SalesReturns sr
    INNER JOIN Customers c ON sr.CustomerId = c.CustomerId
    LEFT JOIN SalesInvoices si ON sr.ReferenceSalesInvoiceId = si.SalesInvoiceId
    WHERE sr.ReturnDate BETWEEN @StartDate AND @EndDate
    ORDER BY sr.ReturnDate DESC, sr.ReturnId DESC;
END
GO

-- Update the stock update procedure to work with Products table
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_UpdateStockForSalesReturn')
    DROP PROCEDURE [dbo].[sp_UpdateStockForSalesReturn]
GO

CREATE PROCEDURE [dbo].[sp_UpdateStockForSalesReturn]
    @ReturnId INT,
    @UpdatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Update stock for each returned item
        UPDATE p
        SET p.Quantity = p.Quantity + sri.Quantity,
            p.ModifiedDate = GETDATE(),
            p.ModifiedBy = @UpdatedBy
        FROM Products p
        INNER JOIN SalesReturnItems sri ON p.ProductId = sri.ProductId
        WHERE sri.ReturnId = @ReturnId AND sri.StockUpdated = 0;
        
        -- Mark items as stock updated
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
        
        SELECT 'SUCCESS' as Result, 'Stock updated successfully' as Message;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 'ERROR' as Result, ERROR_MESSAGE() as Message;
    END CATCH
END
GO

PRINT 'Foreign key relationships added successfully!'
PRINT 'Sales Return tables are now properly linked to:'
PRINT '- Customers table'
PRINT '- SalesInvoices table' 
PRINT '- Products table'
PRINT '- SalesInvoiceDetails table'
PRINT 'Stored procedures updated with correct column names.'
