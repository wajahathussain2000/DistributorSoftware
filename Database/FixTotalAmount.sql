-- Fix TotalAmount NULL constraint issue in SalesInvoiceDetails
USE [DistributionDB]
GO

-- Add default constraint for TotalAmount column
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE name = 'DF_SalesInvoiceDetails_TotalAmount')
BEGIN
    ALTER TABLE SalesInvoiceDetails ADD CONSTRAINT DF_SalesInvoiceDetails_TotalAmount DEFAULT(0) FOR TotalAmount
END
GO

-- Also add default constraints for other columns that might have NULL issues
IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE name = 'DF_SalesInvoiceDetails_TaxAmount')
BEGIN
    ALTER TABLE SalesInvoiceDetails ADD CONSTRAINT DF_SalesInvoiceDetails_TaxAmount DEFAULT(0) FOR TaxAmount
END
GO

IF NOT EXISTS (SELECT * FROM sys.default_constraints WHERE name = 'DF_SalesInvoiceDetails_DiscountAmount')
BEGIN
    ALTER TABLE SalesInvoiceDetails ADD CONSTRAINT DF_SalesInvoiceDetails_DiscountAmount DEFAULT(0) FOR DiscountAmount
END
GO

PRINT 'Default constraints added successfully for SalesInvoiceDetails table!'
