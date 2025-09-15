-- =============================================
-- Run Chart of Accounts Seed Data Script
-- Distribution Software Database Setup
-- =============================================

-- First, run the table creation script if not already done
-- EXEC sp_executesql N'-- Run ChartOfAccounts_Simple.sql first if table doesn''t exist'

-- Then run the seed data
PRINT 'Starting Chart of Accounts seed data insertion...'
PRINT '================================================'

-- Run the seed data script
:r ChartOfAccounts_SeedData.sql

PRINT '================================================'
PRINT 'Chart of Accounts seed data insertion completed!'
PRINT '================================================'

-- Verify the data was inserted correctly
SELECT 
    AccountCode,
    AccountName,
    AccountType,
    AccountCategory,
    AccountLevel,
    CASE WHEN ParentAccountId IS NULL THEN 'ROOT' ELSE CAST(ParentAccountId AS VARCHAR(10)) END AS ParentAccount,
    NormalBalance,
    IsActive
FROM ChartOfAccounts 
ORDER BY AccountCode;

PRINT 'Total accounts in Chart of Accounts: ' + CAST((SELECT COUNT(*) FROM ChartOfAccounts) AS VARCHAR(10))
GO
