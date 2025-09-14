-- Integrate Pricing & Discount Setup with Sales Forms
-- Add PricingRuleId and DiscountRuleId to SalesInvoices and SalesInvoiceDetails

USE DistributionDB;
GO

-- Add PricingRuleId to SalesInvoices table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesInvoices') AND name = 'PricingRuleId')
BEGIN
    ALTER TABLE SalesInvoices ADD PricingRuleId INT NULL;
    PRINT 'Added PricingRuleId column to SalesInvoices table';
END
ELSE
BEGIN
    PRINT 'PricingRuleId column already exists in SalesInvoices table';
END

-- Add DiscountRuleId to SalesInvoices table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesInvoices') AND name = 'DiscountRuleId')
BEGIN
    ALTER TABLE SalesInvoices ADD DiscountRuleId INT NULL;
    PRINT 'Added DiscountRuleId column to SalesInvoices table';
END
ELSE
BEGIN
    PRINT 'DiscountRuleId column already exists in SalesInvoices table';
END

-- Add PricingRuleId to SalesInvoiceDetails table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesInvoiceDetails') AND name = 'PricingRuleId')
BEGIN
    ALTER TABLE SalesInvoiceDetails ADD PricingRuleId INT NULL;
    PRINT 'Added PricingRuleId column to SalesInvoiceDetails table';
END
ELSE
BEGIN
    PRINT 'PricingRuleId column already exists in SalesInvoiceDetails table';
END

-- Add DiscountRuleId to SalesInvoiceDetails table
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('SalesInvoiceDetails') AND name = 'DiscountRuleId')
BEGIN
    ALTER TABLE SalesInvoiceDetails ADD DiscountRuleId INT NULL;
    PRINT 'Added DiscountRuleId column to SalesInvoiceDetails table';
END
ELSE
BEGIN
    PRINT 'DiscountRuleId column already exists in SalesInvoiceDetails table';
END

-- Add foreign key constraints
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesInvoices_PricingRules')
BEGIN
    ALTER TABLE SalesInvoices 
    ADD CONSTRAINT FK_SalesInvoices_PricingRules 
    FOREIGN KEY (PricingRuleId) REFERENCES PricingRules(PricingRuleId);
    PRINT 'Added foreign key constraint FK_SalesInvoices_PricingRules';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_SalesInvoices_PricingRules already exists';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesInvoices_DiscountRules')
BEGIN
    ALTER TABLE SalesInvoices 
    ADD CONSTRAINT FK_SalesInvoices_DiscountRules 
    FOREIGN KEY (DiscountRuleId) REFERENCES DiscountRules(DiscountRuleId);
    PRINT 'Added foreign key constraint FK_SalesInvoices_DiscountRules';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_SalesInvoices_DiscountRules already exists';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesInvoiceDetails_PricingRules')
BEGIN
    ALTER TABLE SalesInvoiceDetails 
    ADD CONSTRAINT FK_SalesInvoiceDetails_PricingRules 
    FOREIGN KEY (PricingRuleId) REFERENCES PricingRules(PricingRuleId);
    PRINT 'Added foreign key constraint FK_SalesInvoiceDetails_PricingRules';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_SalesInvoiceDetails_PricingRules already exists';
END

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_SalesInvoiceDetails_DiscountRules')
BEGIN
    ALTER TABLE SalesInvoiceDetails 
    ADD CONSTRAINT FK_SalesInvoiceDetails_DiscountRules 
    FOREIGN KEY (DiscountRuleId) REFERENCES DiscountRules(DiscountRuleId);
    PRINT 'Added foreign key constraint FK_SalesInvoiceDetails_DiscountRules';
END
ELSE
BEGIN
    PRINT 'Foreign key constraint FK_SalesInvoiceDetails_DiscountRules already exists';
END

PRINT 'Pricing & Discount integration with Sales forms completed successfully!';
