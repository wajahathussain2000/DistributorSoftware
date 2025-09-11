-- Add additional seed data for ExpenseCategories
USE [DistributionDB]
GO

-- Insert additional expense categories if they don't exist
IF NOT EXISTS (SELECT 1 FROM ExpenseCategories WHERE CategoryName = 'Office Equipment')
BEGIN
    INSERT INTO [dbo].[ExpenseCategories] ([CategoryName], [CategoryCode], [Description], [IsActive], [CreatedBy])
    VALUES ('Office Equipment', 'OFF-EQP', 'Computers, printers, office furniture', 1, 1)
END

IF NOT EXISTS (SELECT 1 FROM ExpenseCategories WHERE CategoryName = 'Software & Licenses')
BEGIN
    INSERT INTO [dbo].[ExpenseCategories] ([CategoryName], [CategoryCode], [Description], [IsActive], [CreatedBy])
    VALUES ('Software & Licenses', 'SOFT-LIC', 'Software purchases and license renewals', 1, 1)
END

IF NOT EXISTS (SELECT 1 FROM ExpenseCategories WHERE CategoryName = 'Professional Services')
BEGIN
    INSERT INTO [dbo].[ExpenseCategories] ([CategoryName], [CategoryCode], [Description], [IsActive], [CreatedBy])
    VALUES ('Professional Services', 'PROF-SVC', 'Legal, accounting, consulting services', 1, 1)
END

IF NOT EXISTS (SELECT 1 FROM ExpenseCategories WHERE CategoryName = 'Insurance')
BEGIN
    INSERT INTO [dbo].[ExpenseCategories] ([CategoryName], [CategoryCode], [Description], [IsActive], [CreatedBy])
    VALUES ('Insurance', 'INS', 'Business insurance premiums', 1, 1)
END

IF NOT EXISTS (SELECT 1 FROM ExpenseCategories WHERE CategoryName = 'Rent & Lease')
BEGIN
    INSERT INTO [dbo].[ExpenseCategories] ([CategoryName], [CategoryCode], [Description], [IsActive], [CreatedBy])
    VALUES ('Rent & Lease', 'RENT', 'Office rent and equipment leases', 1, 1)
END

-- Update existing categories to ensure they have proper codes
UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'OFF-SUP' 
WHERE [CategoryName] = 'Office Supplies' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'TRAVEL' 
WHERE [CategoryName] = 'Travel & Transportation' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'MEALS' 
WHERE [CategoryName] = 'Meals & Entertainment' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'UTIL' 
WHERE [CategoryName] = 'Utilities' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'MAINT' 
WHERE [CategoryName] = 'Maintenance' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'MKT' 
WHERE [CategoryName] = 'Marketing' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'TRAIN' 
WHERE [CategoryName] = 'Training' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

UPDATE [dbo].[ExpenseCategories] 
SET [CategoryCode] = 'MISC' 
WHERE [CategoryName] = 'Miscellaneous' AND ([CategoryCode] IS NULL OR [CategoryCode] = '')

-- Show final count
SELECT COUNT(*) as TotalCategories FROM ExpenseCategories WHERE IsActive = 1
GO
