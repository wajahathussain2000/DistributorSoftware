-- Integrate Tax Configuration with Sales Forms
-- Add TaxCategoryId to SalesInvoices and SalesInvoiceDetails

USE DistributionDB;
GO

-- Add TaxCategoryId to SalesInvoices table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesInvoices') AND name = 'TaxCategoryId')
BEGIN
    ALTER TABLE SalesInvoices ADD TaxCategoryId INT NULL;
    PRINT 'Added TaxCategoryId column to SalesInvoices table';
END
ELSE
BEGIN
    PRINT 'TaxCategoryId column already exists in SalesInvoices table';
END

-- Add TaxCategoryId to SalesInvoiceDetails table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesInvoiceDetails') AND name = 'TaxCategoryId')
BEGIN
    ALTER TABLE SalesInvoiceDetails ADD TaxCategoryId INT NULL;
    PRINT 'Added TaxCategoryId column to SalesInvoiceDetails table';
END
ELSE
BEGIN
    PRINT 'TaxCategoryId column already exists in SalesInvoiceDetails table';
END

-- Add TaxCategoryId to SalesReturns table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesReturns') AND name = 'TaxCategoryId')
BEGIN
    ALTER TABLE SalesReturns ADD TaxCategoryId INT NULL;
    PRINT 'Added TaxCategoryId column to SalesReturns table';
END
ELSE
BEGIN
    PRINT 'TaxCategoryId column already exists in SalesReturns table';
END

-- Add TaxCategoryId to SalesReturnDetails table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesReturnDetails') AND name = 'TaxCategoryId')
BEGIN
    ALTER TABLE SalesReturnDetails ADD TaxCategoryId INT NULL;
    PRINT 'Added TaxCategoryId column to SalesReturnDetails table';
END
ELSE
BEGIN
    PRINT 'TaxCategoryId column already exists in SalesReturnDetails table';
END

-- Add foreign key constraints
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesInvoices_TaxCategories')
BEGIN
    ALTER TABLE SalesInvoices 
    ADD CONSTRAINT FK_SalesInvoices_TaxCategories 
    FOREIGN KEY (TaxCategoryId) REFERENCES TaxCategories(TaxCategoryId);
    PRINT 'Added foreign key constraint FK_SalesInvoices_TaxCategories';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_SalesInvoices_TaxCategories already exists';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesInvoiceDetails_TaxCategories')
BEGIN
    ALTER TABLE SalesInvoiceDetails 
    ADD CONSTRAINT FK_SalesInvoiceDetails_TaxCategories 
    FOREIGN KEY (TaxCategoryId) REFERENCES TaxCategories(TaxCategoryId);
    PRINT 'Added foreign key constraint FK_SalesInvoiceDetails_TaxCategories';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_SalesInvoiceDetails_TaxCategories already exists';
END

-- Update existing records to use default tax category (GST)
DECLARE @DefaultTaxCategoryId INT;
SELECT @DefaultTaxCategoryId = TaxCategoryId FROM TaxCategories WHERE TaxCategoryCode = 'GST' AND IsActive = 1;

IF @DefaultTaxCategoryId IS NOT NULL
BEGIN
    UPDATE SalesInvoices SET TaxCategoryId = @DefaultTaxCategoryId WHERE TaxCategoryId IS NULL;
    UPDATE SalesInvoiceDetails SET TaxCategoryId = @DefaultTaxCategoryId WHERE TaxCategoryId IS NULL;
    UPDATE SalesReturns SET TaxCategoryId = @DefaultTaxCategoryId WHERE TaxCategoryId IS NULL;
    UPDATE SalesReturnDetails SET TaxCategoryId = @DefaultTaxCategoryId WHERE TaxCategoryId IS NULL;
    PRINT 'Updated existing records with default tax category';
END
ELSE
BEGIN
    PRINT 'Default tax category not found';
END

PRINT 'Tax integration with Sales forms completed successfully!';
