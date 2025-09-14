-- =============================================
-- Distribution Software Database Seed Data
-- =============================================

USE DistributionDB;
GO

-- =============================================
-- Seed Tax Categories
-- =============================================
IF NOT EXISTS (SELECT * FROM TaxCategories WHERE TaxCategoryCode = 'STD')
BEGIN
    INSERT INTO TaxCategories (TaxCategoryCode, TaxCategoryName, Description, IsActive, IsSystemCategory, CreatedBy)
    VALUES 
    ('STD', 'Standard Rate', 'Standard tax rate for most products', 1, 1, 1),
    ('EXEMPT', 'Exempt', 'Tax exempt products', 1, 1, 1),
    ('ZERO', 'Zero Rated', 'Zero rated products', 1, 1, 1),
    ('REDUCED', 'Reduced Rate', 'Reduced tax rate for specific products', 1, 1, 1);
    
    PRINT 'Tax Categories seeded successfully';
END
ELSE
    PRINT 'Tax Categories already seeded';
GO

-- =============================================
-- Seed Tax Rates
-- =============================================
IF NOT EXISTS (SELECT * FROM TaxRates WHERE TaxRateCode = 'STD_17')
BEGIN
    DECLARE @StdCategoryId INT = (SELECT TaxCategoryId FROM TaxCategories WHERE TaxCategoryCode = 'STD');
    DECLARE @ExemptCategoryId INT = (SELECT TaxCategoryId FROM TaxCategories WHERE TaxCategoryCode = 'EXEMPT');
    DECLARE @ZeroCategoryId INT = (SELECT TaxCategoryId FROM TaxCategories WHERE TaxCategoryCode = 'ZERO');
    
    INSERT INTO TaxRates (TaxCategoryId, TaxRateName, TaxPercentage, TaxRateCode, Description, IsActive, IsSystemRate, CreatedBy)
    VALUES 
    (@StdCategoryId, 'Sales Tax 17%', 17.00, 'STD_17', 'Standard sales tax rate', 1, 1, 1),
    (@StdCategoryId, 'Sales Tax 15%', 15.00, 'STD_15', 'Reduced sales tax rate', 1, 1, 1),
    (@ExemptCategoryId, 'Exempt Tax', 0.00, 'EXEMPT_0', 'Tax exempt rate', 1, 1, 1),
    (@ZeroCategoryId, 'Zero Rated Tax', 0.00, 'ZERO_0', 'Zero rated tax', 1, 1, 1);
    
    PRINT 'Tax Rates seeded successfully';
END
ELSE
    PRINT 'Tax Rates already seeded';
GO

-- =============================================
-- Seed Bank Accounts
-- =============================================
IF NOT EXISTS (SELECT * FROM BankAccounts WHERE AccountNumber = 'ACC001')
BEGIN
    INSERT INTO BankAccounts (BankName, AccountNumber, AccountHolderName, AccountType, Branch, Address, Phone, Email, IsActive, CreatedBy)
    VALUES 
    ('Allied Bank Limited', 'ACC001', 'Distribution Company Ltd', 'BUSINESS', 'Main Branch', '123 Main Street, Karachi', '+92-21-1234567', 'banking@distco.com', 1, 1),
    ('Habib Bank Limited', 'ACC002', 'Distribution Company Ltd', 'CURRENT', 'Gulshan Branch', '456 Gulshan Avenue, Karachi', '+92-21-2345678', 'hbl@distco.com', 1, 1),
    ('MCB Bank Limited', 'ACC003', 'Distribution Company Ltd', 'SAVINGS', 'Clifton Branch', '789 Clifton Road, Karachi', '+92-21-3456789', 'mcb@distco.com', 1, 1);
    
    PRINT 'Bank Accounts seeded successfully';
END
ELSE
    PRINT 'Bank Accounts already seeded';
GO

-- =============================================
-- Seed Backup Schedules
-- =============================================
IF NOT EXISTS (SELECT * FROM BackupSchedules WHERE ScheduleName = 'Daily Full Backup')
BEGIN
    INSERT INTO BackupSchedules (ScheduleName, Description, BackupType, Frequency, BackupTime, BackupPath, CompressBackup, EncryptBackup, RetentionDays, IsActive, NotifyOnCompletion, CreatedBy)
    VALUES 
    ('Daily Full Backup', 'Daily full database backup', 'FULL', 'DAILY', '02:00:00', 'C:\Backups\Daily', 1, 0, 30, 1, 1, 1),
    ('Weekly Full Backup', 'Weekly full database backup', 'FULL', 'WEEKLY', '03:00:00', 'C:\Backups\Weekly', 1, 1, 90, 1, 1, 1),
    ('Monthly Archive', 'Monthly archive backup', 'FULL', 'MONTHLY', '04:00:00', 'C:\Backups\Monthly', 1, 1, 365, 1, 0, 1);
    
    PRINT 'Backup Schedules seeded successfully';
END
ELSE
    PRINT 'Backup Schedules already seeded';
GO

-- =============================================
-- Seed Roles
-- =============================================
IF NOT EXISTS (SELECT * FROM Roles WHERE RoleName = 'Administrator')
BEGIN
    INSERT INTO Roles (RoleName, Description, IsActive, IsSystemRole, Priority, CreatedBy)
    VALUES 
    ('Administrator', 'Full system access', 1, 1, 1000, 1),
    ('Manager', 'Management level access', 1, 1, 800, 1),
    ('Sales Staff', 'Sales and customer management', 1, 1, 600, 1),
    ('Inventory Staff', 'Inventory and stock management', 1, 1, 600, 1),
    ('Accountant', 'Financial and accounting access', 1, 1, 700, 1),
    ('Viewer', 'Read-only access', 1, 1, 100, 1);
    
    PRINT 'Roles seeded successfully';
END
ELSE
    PRINT 'Roles already seeded';
GO

-- =============================================
-- Seed Permissions
-- =============================================
IF NOT EXISTS (SELECT * FROM Permissions WHERE PermissionCode = 'USER_MANAGEMENT')
BEGIN
    INSERT INTO Permissions (PermissionCode, PermissionName, Description, Category, PermissionType, IsActive, IsSystemPermission, Priority, CreatedBy)
    VALUES 
    -- User Management
    ('USER_MANAGEMENT', 'User Management', 'Manage system users', 'USER_MANAGEMENT', 'ADMIN', 1, 1, 1000, 1),
    ('USER_VIEW', 'View Users', 'View user information', 'USER_MANAGEMENT', 'VIEW', 1, 1, 100, 1),
    ('USER_CREATE', 'Create Users', 'Create new users', 'USER_MANAGEMENT', 'CREATE', 1, 1, 200, 1),
    ('USER_UPDATE', 'Update Users', 'Update user information', 'USER_MANAGEMENT', 'UPDATE', 1, 1, 300, 1),
    ('USER_DELETE', 'Delete Users', 'Delete users', 'USER_MANAGEMENT', 'DELETE', 1, 1, 400, 1),
    
    -- Sales Management
    ('SALES_MANAGEMENT', 'Sales Management', 'Manage sales operations', 'SALES', 'ADMIN', 1, 1, 1000, 1),
    ('SALES_VIEW', 'View Sales', 'View sales data', 'SALES', 'VIEW', 1, 1, 100, 1),
    ('SALES_CREATE', 'Create Sales', 'Create sales invoices', 'SALES', 'CREATE', 1, 1, 200, 1),
    ('SALES_UPDATE', 'Update Sales', 'Update sales invoices', 'SALES', 'UPDATE', 1, 1, 300, 1),
    ('SALES_DELETE', 'Delete Sales', 'Delete sales invoices', 'SALES', 'DELETE', 1, 1, 400, 1),
    ('SALES_EXPORT', 'Export Sales', 'Export sales reports', 'SALES', 'EXPORT', 1, 1, 500, 1),
    
    -- Inventory Management
    ('INVENTORY_MANAGEMENT', 'Inventory Management', 'Manage inventory operations', 'INVENTORY', 'ADMIN', 1, 1, 1000, 1),
    ('INVENTORY_VIEW', 'View Inventory', 'View inventory data', 'INVENTORY', 'VIEW', 1, 1, 100, 1),
    ('INVENTORY_CREATE', 'Create Inventory', 'Create inventory records', 'INVENTORY', 'CREATE', 1, 1, 200, 1),
    ('INVENTORY_UPDATE', 'Update Inventory', 'Update inventory records', 'INVENTORY', 'UPDATE', 1, 1, 300, 1),
    ('INVENTORY_DELETE', 'Delete Inventory', 'Delete inventory records', 'INVENTORY', 'DELETE', 1, 1, 400, 1),
    
    -- Reports
    ('REPORTS_MANAGEMENT', 'Reports Management', 'Manage all reports', 'REPORTS', 'ADMIN', 1, 1, 1000, 1),
    ('REPORTS_VIEW', 'View Reports', 'View reports', 'REPORTS', 'VIEW', 1, 1, 100, 1),
    ('REPORTS_EXPORT', 'Export Reports', 'Export reports', 'REPORTS', 'EXPORT', 1, 1, 200, 1),
    
    -- System Settings
    ('SYSTEM_SETTINGS', 'System Settings', 'Manage system settings', 'SYSTEM', 'ADMIN', 1, 1, 1000, 1),
    ('SYSTEM_VIEW', 'View Settings', 'View system settings', 'SYSTEM', 'VIEW', 1, 1, 100, 1),
    ('SYSTEM_UPDATE', 'Update Settings', 'Update system settings', 'SYSTEM', 'UPDATE', 1, 1, 200, 1),
    
    -- Pricing & Discounts
    ('PRICING_MANAGEMENT', 'Pricing Management', 'Manage pricing rules', 'PRICING', 'ADMIN', 1, 1, 1000, 1),
    ('PRICING_VIEW', 'View Pricing', 'View pricing rules', 'PRICING', 'VIEW', 1, 1, 100, 1),
    ('PRICING_CREATE', 'Create Pricing', 'Create pricing rules', 'PRICING', 'CREATE', 1, 1, 200, 1),
    ('PRICING_UPDATE', 'Update Pricing', 'Update pricing rules', 'PRICING', 'UPDATE', 1, 1, 300, 1),
    
    -- Tax Configuration
    ('TAX_MANAGEMENT', 'Tax Management', 'Manage tax configuration', 'TAX', 'ADMIN', 1, 1, 1000, 1),
    ('TAX_VIEW', 'View Tax', 'View tax configuration', 'TAX', 'VIEW', 1, 1, 100, 1),
    ('TAX_CREATE', 'Create Tax', 'Create tax rules', 'TAX', 'CREATE', 1, 1, 200, 1),
    ('TAX_UPDATE', 'Update Tax', 'Update tax rules', 'TAX', 'UPDATE', 1, 1, 300, 1),
    
    -- Bank Reconciliation
    ('BANK_MANAGEMENT', 'Bank Management', 'Manage bank reconciliation', 'BANK', 'ADMIN', 1, 1, 1000, 1),
    ('BANK_VIEW', 'View Bank', 'View bank reconciliation', 'BANK', 'VIEW', 1, 1, 100, 1),
    ('BANK_CREATE', 'Create Bank', 'Create bank reconciliation', 'BANK', 'CREATE', 1, 1, 200, 1),
    ('BANK_UPDATE', 'Update Bank', 'Update bank reconciliation', 'BANK', 'UPDATE', 1, 1, 300, 1),
    
    -- Backup Management
    ('BACKUP_MANAGEMENT', 'Backup Management', 'Manage database backups', 'BACKUP', 'ADMIN', 1, 1, 1000, 1),
    ('BACKUP_VIEW', 'View Backup', 'View backup schedules', 'BACKUP', 'VIEW', 1, 1, 100, 1),
    ('BACKUP_CREATE', 'Create Backup', 'Create backup schedules', 'BACKUP', 'CREATE', 1, 1, 200, 1),
    ('BACKUP_UPDATE', 'Update Backup', 'Update backup schedules', 'BACKUP', 'UPDATE', 1, 1, 300, 1);
    
    PRINT 'Permissions seeded successfully';
END
ELSE
    PRINT 'Permissions already seeded';
GO

-- =============================================
-- Seed Role Permissions (Administrator gets all permissions)
-- =============================================
IF NOT EXISTS (SELECT * FROM RolePermissions WHERE RoleId = 1)
BEGIN
    DECLARE @AdminRoleId INT = (SELECT RoleId FROM Roles WHERE RoleName = 'Administrator');
    
    INSERT INTO RolePermissions (RoleId, PermissionId, IsActive, CreatedBy)
    SELECT @AdminRoleId, PermissionId, 1, 1 FROM Permissions WHERE IsActive = 1;
    
    PRINT 'Role Permissions seeded successfully';
END
ELSE
    PRINT 'Role Permissions already seeded';
GO

-- =============================================
-- Seed Sample Pricing Rules
-- =============================================
IF NOT EXISTS (SELECT * FROM PricingRules WHERE RuleName = 'Standard Markup')
BEGIN
    INSERT INTO PricingRules (RuleName, Description, PricingType, BaseValue, Priority, IsActive, CreatedBy)
    VALUES 
    ('Standard Markup', 'Standard 20% markup on all products', 'PERCENTAGE_MARKUP', 20.00, 100, 1, 1),
    ('Premium Products', '25% markup for premium products', 'PERCENTAGE_MARKUP', 25.00, 90, 1, 1),
    ('Bulk Discount', '10% discount for orders over 100 units', 'PERCENTAGE_MARKUP', -10.00, 80, 1, 1);
    
    PRINT 'Sample Pricing Rules seeded successfully';
END
ELSE
    PRINT 'Sample Pricing Rules already seeded';
GO

-- =============================================
-- Seed Sample Discount Rules
-- =============================================
IF NOT EXISTS (SELECT * FROM DiscountRules WHERE RuleName = 'New Customer Discount')
BEGIN
    INSERT INTO DiscountRules (RuleName, Description, DiscountType, DiscountValue, MinOrderAmount, Priority, IsActive, IsPromotional, CreatedBy)
    VALUES 
    ('New Customer Discount', '10% discount for new customers', 'PERCENTAGE', 10.00, 1000.00, 100, 1, 1, 1),
    ('Loyalty Discount', '5% discount for loyal customers', 'PERCENTAGE', 5.00, 500.00, 90, 1, 0, 1),
    ('Seasonal Sale', '15% discount during seasonal sale', 'PERCENTAGE', 15.00, 2000.00, 80, 1, 1, 1);
    
    PRINT 'Sample Discount Rules seeded successfully';
END
ELSE
    PRINT 'Sample Discount Rules already seeded';
GO

-- =============================================
-- Seed Sample Bank Statements
-- =============================================
IF NOT EXISTS (SELECT * FROM BankStatements WHERE Description = 'Sample Deposit')
BEGIN
    DECLARE @BankAccountId INT = (SELECT TOP 1 BankAccountId FROM BankAccounts WHERE IsActive = 1);
    
    INSERT INTO BankStatements (BankAccountId, TransactionDate, Description, ReferenceNumber, TransactionType, Amount, Balance, CreatedBy)
    VALUES 
    (@BankAccountId, GETDATE(), 'Sample Deposit', 'DEP001', 'CREDIT', 50000.00, 50000.00, 1),
    (@BankAccountId, GETDATE(), 'Sample Withdrawal', 'WTH001', 'DEBIT', 10000.00, 40000.00, 1),
    (@BankAccountId, GETDATE(), 'Sample Transfer', 'TRF001', 'CREDIT', 25000.00, 65000.00, 1);
    
    PRINT 'Sample Bank Statements seeded successfully';
END
ELSE
    PRINT 'Sample Bank Statements already seeded';
GO

PRINT 'All seed data inserted successfully!';
GO
