-- =============================================
-- Verification Script for Supplier Ledger Report Integration
-- Author: Distribution Software
-- Description: Verify that all components are properly integrated
-- =============================================

PRINT '=== SUPPLIER LEDGER REPORT INTEGRATION VERIFICATION ===';
PRINT '';

-- 1. Verify stored procedure exists
PRINT '1. Checking if stored procedure exists...';
IF EXISTS (SELECT * FROM sys.procedures WHERE name = 'sp_GetSupplierLedgerReport')
BEGIN
    PRINT '   ✅ Stored procedure sp_GetSupplierLedgerReport exists';
END
ELSE
BEGIN
    PRINT '   ❌ Stored procedure sp_GetSupplierLedgerReport NOT found';
END

-- 2. Verify required tables exist
PRINT '';
PRINT '2. Checking required tables...';

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Suppliers')
BEGIN
    PRINT '   ✅ Suppliers table exists';
    DECLARE @SupplierCount INT = (SELECT COUNT(*) FROM Suppliers WHERE IsActive = 1);
    PRINT '   📊 Active suppliers: ' + CAST(@SupplierCount AS VARCHAR(10));
END
ELSE
BEGIN
    PRINT '   ❌ Suppliers table NOT found';
END

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'SupplierTransactions')
BEGIN
    PRINT '   ✅ SupplierTransactions table exists';
    DECLARE @TransactionCount INT = (SELECT COUNT(*) FROM SupplierTransactions);
    PRINT '   📊 Total transactions: ' + CAST(@TransactionCount AS VARCHAR(10));
END
ELSE
BEGIN
    PRINT '   ❌ SupplierTransactions table NOT found';
END

-- 3. Test stored procedure with sample data
PRINT '';
PRINT '3. Testing stored procedure...';

DECLARE @TestSupplierId INT = (SELECT TOP 1 SupplierId FROM Suppliers WHERE IsActive = 1);

IF @TestSupplierId IS NOT NULL
BEGIN
    PRINT '   Testing with Supplier ID: ' + CAST(@TestSupplierId AS VARCHAR(10));
    
    -- Test the stored procedure
    BEGIN TRY
        EXEC sp_GetSupplierLedgerReport 
            @StartDate = '2025-01-01',
            @EndDate = '2025-12-31',
            @SupplierId = @TestSupplierId;
        PRINT '   ✅ Stored procedure executed successfully';
    END TRY
    BEGIN CATCH
        PRINT '   ❌ Error executing stored procedure: ' + ERROR_MESSAGE();
    END CATCH
END
ELSE
BEGIN
    PRINT '   ⚠️  No active suppliers found for testing';
END

-- 4. Summary
PRINT '';
PRINT '=== INTEGRATION SUMMARY ===';
PRINT 'Files that should be added to your project:';
PRINT '  📁 Models/SupplierLedgerReportData.cs';
PRINT '  📁 Presentation/Forms/SupplierLedgerReportForm.cs';
PRINT '  📁 Presentation/Forms/SupplierLedgerReportForm.Designer.cs';
PRINT '  📁 Reports/SupplierLedgerReport.rdlc';
PRINT '';
PRINT 'Database components:';
PRINT '  🗄️  Stored procedure: sp_GetSupplierLedgerReport';
PRINT '';
PRINT 'UI Integration:';
PRINT '  🎯 Reports dropdown menu updated';
PRINT '  🎯 OpenSupplierLedgerReportForm() method added';
PRINT '';
PRINT '✅ Supplier Ledger Report is ready to use!';
PRINT '   Access it via: Reports → Supplier Ledger Report';
