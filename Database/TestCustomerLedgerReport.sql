-- =============================================
-- Test Script for Customer Ledger Report
-- =============================================

-- Test 1: Get all customers ledger for 2025
PRINT 'Test 1: All Customers Ledger Report for 2025'
EXEC sp_GetCustomerLedgerReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = NULL

-- Test 2: Get specific customer ledger (replace with actual CustomerId)
PRINT 'Test 2: Specific Customer Ledger Report'
EXEC sp_GetCustomerLedgerReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-12-31',
    @CustomerId = 1

-- Test 3: Get customer ledger for a shorter period
PRINT 'Test 3: Customer Ledger Report for January 2025'
EXEC sp_GetCustomerLedgerReport 
    @StartDate = '2025-01-01',
    @EndDate = '2025-01-31',
    @CustomerId = NULL

-- Test 4: Check if CustomerTransactions table exists and has data
PRINT 'Test 4: Check CustomerTransactions table'
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CustomerTransactions')
BEGIN
    SELECT COUNT(*) AS CustomerTransactionCount FROM CustomerTransactions
    SELECT TOP 5 * FROM CustomerTransactions ORDER BY TransactionDate DESC
END
ELSE
BEGIN
    PRINT 'CustomerTransactions table does not exist. Creating sample data...'
    
    -- Create CustomerTransactions table if it doesn't exist
    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'CustomerTransactions')
    BEGIN
        CREATE TABLE CustomerTransactions (
            TransactionId BIGINT IDENTITY(1,1) PRIMARY KEY,
            CustomerId INT NOT NULL,
            TransactionDate DATETIME NOT NULL,
            TransactionType VARCHAR(50) NOT NULL, -- 'Sales', 'Payment', 'Return'
            Description VARCHAR(500),
            ReferenceNumber VARCHAR(100),
            DebitAmount DECIMAL(18,2),
            CreditAmount DECIMAL(18,2),
            Balance DECIMAL(18,2),
            IsActive BIT DEFAULT 1,
            CreatedDate DATETIME DEFAULT GETDATE(),
            CreatedBy INT,
            FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
        )
        
        PRINT 'CustomerTransactions table created successfully.'
    END
    
    -- Insert sample data if no data exists
    IF NOT EXISTS (SELECT TOP 1 * FROM CustomerTransactions)
    BEGIN
        -- Insert sample sales transactions
        INSERT INTO CustomerTransactions (CustomerId, TransactionDate, TransactionType, Description, ReferenceNumber, CreditAmount, IsActive)
        SELECT TOP 5 
            CustomerId, 
            DATEADD(DAY, -RAND() * 30, GETDATE()) as TransactionDate,
            'Sales' as TransactionType,
            'Sales Invoice' as Description,
            'INV-' + CAST(RAND() * 10000 AS VARCHAR) as ReferenceNumber,
            ROUND(RAND() * 1000 + 100, 2) as CreditAmount,
            1 as IsActive
        FROM Customers 
        WHERE IsActive = 1
        
        -- Insert sample payment transactions
        INSERT INTO CustomerTransactions (CustomerId, TransactionDate, TransactionType, Description, ReferenceNumber, DebitAmount, IsActive)
        SELECT TOP 3 
            CustomerId, 
            DATEADD(DAY, -RAND() * 15, GETDATE()) as TransactionDate,
            'Payment' as TransactionType,
            'Payment Received' as Description,
            'PAY-' + CAST(RAND() * 10000 AS VARCHAR) as ReferenceNumber,
            ROUND(RAND() * 500 + 50, 2) as DebitAmount,
            1 as IsActive
        FROM Customers 
        WHERE IsActive = 1
        
        PRINT 'Sample data inserted into CustomerTransactions table.'
    END
    
    -- Run the test again
    PRINT 'Re-running Test 1 with sample data:'
    EXEC sp_GetCustomerLedgerReport 
        @StartDate = '2025-01-01',
        @EndDate = '2025-12-31',
        @CustomerId = NULL
END

-- Test 5: Verify Customers table has data
PRINT 'Test 5: Check Customers table'
SELECT COUNT(*) AS CustomerCount FROM Customers WHERE IsActive = 1
SELECT TOP 5 CustomerId, CustomerCode, CustomerName FROM Customers WHERE IsActive = 1

PRINT 'Customer Ledger Report test completed successfully!'
