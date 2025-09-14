-- =============================================
-- Distribution Software Database Tables Creation
-- =============================================

USE DistributionDB;
GO

-- =============================================
-- Pricing Rules Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PricingRules' AND xtype='U')
BEGIN
    CREATE TABLE PricingRules (
        PricingRuleId INT IDENTITY(1,1) PRIMARY KEY,
        RuleName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        ProductId INT NULL,
        CategoryId INT NULL,
        CustomerId INT NULL,
        CustomerCategoryId INT NULL,
        PricingType NVARCHAR(50) NOT NULL, -- FIXED_PRICE, PERCENTAGE_MARKUP, PERCENTAGE_MARGIN, QUANTITY_BREAK
        BaseValue DECIMAL(18,4) NOT NULL,
        MinQuantity DECIMAL(18,4) NULL,
        MaxQuantity DECIMAL(18,4) NULL,
        Priority INT NOT NULL DEFAULT 100,
        IsActive BIT NOT NULL DEFAULT 1,
        EffectiveFrom DATETIME NULL,
        EffectiveTo DATETIME NULL,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
        FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    );
    
    PRINT 'PricingRules table created successfully';
END
ELSE
    PRINT 'PricingRules table already exists';
GO

-- =============================================
-- Discount Rules Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DiscountRules' AND xtype='U')
BEGIN
    CREATE TABLE DiscountRules (
        DiscountRuleId INT IDENTITY(1,1) PRIMARY KEY,
        RuleName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        ProductId INT NULL,
        CategoryId INT NULL,
        CustomerId INT NULL,
        CustomerCategoryId INT NULL,
        DiscountType NVARCHAR(50) NOT NULL, -- PERCENTAGE, FIXED_AMOUNT, QUANTITY_BREAK
        DiscountValue DECIMAL(18,4) NOT NULL,
        MinQuantity DECIMAL(18,4) NULL,
        MaxQuantity DECIMAL(18,4) NULL,
        MinOrderAmount DECIMAL(18,4) NULL,
        MaxDiscountAmount DECIMAL(18,4) NULL,
        Priority INT NOT NULL DEFAULT 100,
        IsActive BIT NOT NULL DEFAULT 1,
        IsPromotional BIT NOT NULL DEFAULT 0,
        EffectiveFrom DATETIME NULL,
        EffectiveTo DATETIME NULL,
        UsageLimitPerCustomer INT NULL,
        TotalUsageLimit INT NULL,
        CurrentUsageCount INT NOT NULL DEFAULT 0,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
        FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
    );
    
    PRINT 'DiscountRules table created successfully';
END
ELSE
    PRINT 'DiscountRules table already exists';
GO

-- =============================================
-- Tax Categories Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TaxCategories' AND xtype='U')
BEGIN
    CREATE TABLE TaxCategories (
        TaxCategoryId INT IDENTITY(1,1) PRIMARY KEY,
        TaxCategoryCode NVARCHAR(20) NOT NULL UNIQUE,
        TaxCategoryName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        IsActive BIT NOT NULL DEFAULT 1,
        IsSystemCategory BIT NOT NULL DEFAULT 0,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL
    );
    
    PRINT 'TaxCategories table created successfully';
END
ELSE
    PRINT 'TaxCategories table already exists';
GO

-- =============================================
-- Tax Rates Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='TaxRates' AND xtype='U')
BEGIN
    CREATE TABLE TaxRates (
        TaxRateId INT IDENTITY(1,1) PRIMARY KEY,
        TaxCategoryId INT NOT NULL,
        TaxRateName NVARCHAR(100) NOT NULL,
        TaxPercentage DECIMAL(5,2) NOT NULL,
        TaxRateCode NVARCHAR(20) NOT NULL,
        Description NVARCHAR(500),
        IsActive BIT NOT NULL DEFAULT 1,
        IsSystemRate BIT NOT NULL DEFAULT 0,
        EffectiveFrom DATETIME NULL,
        EffectiveTo DATETIME NULL,
        IsCompound BIT NOT NULL DEFAULT 0,
        IsInclusive BIT NOT NULL DEFAULT 0,
        TaxAccountId INT NULL,
        TaxPayableAccountId INT NULL,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        FOREIGN KEY (TaxCategoryId) REFERENCES TaxCategories(TaxCategoryId),
        FOREIGN KEY (TaxAccountId) REFERENCES ChartOfAccounts(AccountId),
        FOREIGN KEY (TaxPayableAccountId) REFERENCES ChartOfAccounts(AccountId)
    );
    
    PRINT 'TaxRates table created successfully';
END
ELSE
    PRINT 'TaxRates table already exists';
GO

-- =============================================
-- Bank Accounts Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BankAccounts' AND xtype='U')
BEGIN
    CREATE TABLE BankAccounts (
        BankAccountId INT IDENTITY(1,1) PRIMARY KEY,
        BankName NVARCHAR(100) NOT NULL,
        AccountNumber NVARCHAR(50) NOT NULL,
        AccountHolderName NVARCHAR(100) NOT NULL,
        AccountType NVARCHAR(50) NOT NULL, -- CURRENT, SAVINGS, BUSINESS
        Branch NVARCHAR(100),
        Address NVARCHAR(500),
        Phone NVARCHAR(20),
        Email NVARCHAR(100),
        ChartOfAccountId INT NULL,
        CurrentBalance DECIMAL(18,4) NOT NULL DEFAULT 0,
        LastReconciliationDate DATETIME NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        FOREIGN KEY (ChartOfAccountId) REFERENCES ChartOfAccounts(AccountId)
    );
    
    PRINT 'BankAccounts table created successfully';
END
ELSE
    PRINT 'BankAccounts table already exists';
GO

-- =============================================
-- Bank Statements Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BankStatements' AND xtype='U')
BEGIN
    CREATE TABLE BankStatements (
        BankStatementId INT IDENTITY(1,1) PRIMARY KEY,
        BankAccountId INT NOT NULL,
        TransactionDate DATETIME NOT NULL,
        Description NVARCHAR(500) NOT NULL,
        ReferenceNumber NVARCHAR(100),
        TransactionType NVARCHAR(20) NOT NULL, -- DEBIT, CREDIT
        Amount DECIMAL(18,4) NOT NULL,
        Balance DECIMAL(18,4) NOT NULL,
        IsReconciled BIT NOT NULL DEFAULT 0,
        ReconciliationDate DATETIME NULL,
        ReconciledBy INT NULL,
        ReconciledByName NVARCHAR(100),
        MatchedAccountId INT NULL,
        MatchedJournalVoucherId INT NULL,
        MatchingNotes NVARCHAR(500),
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        FOREIGN KEY (BankAccountId) REFERENCES BankAccounts(BankAccountId),
        FOREIGN KEY (MatchedAccountId) REFERENCES ChartOfAccounts(AccountId),
        FOREIGN KEY (MatchedJournalVoucherId) REFERENCES JournalVouchers(JournalVoucherId)
    );
    
    PRINT 'BankStatements table created successfully';
END
ELSE
    PRINT 'BankStatements table already exists';
GO

-- =============================================
-- Bank Reconciliations Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BankReconciliations' AND xtype='U')
BEGIN
    CREATE TABLE BankReconciliations (
        ReconciliationId INT IDENTITY(1,1) PRIMARY KEY,
        BankAccountId INT NOT NULL,
        ReconciliationDate DATETIME NOT NULL,
        StatementBalance DECIMAL(18,4) NOT NULL,
        BookBalance DECIMAL(18,4) NOT NULL,
        Difference DECIMAL(18,4) NOT NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT 'PENDING', -- PENDING, IN_PROGRESS, COMPLETED, DISCREPANCY
        Notes NVARCHAR(1000),
        IsCompleted BIT NOT NULL DEFAULT 0,
        CompletionDate DATETIME NULL,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        FOREIGN KEY (BankAccountId) REFERENCES BankAccounts(BankAccountId)
    );
    
    PRINT 'BankReconciliations table created successfully';
END
ELSE
    PRINT 'BankReconciliations table already exists';
GO

-- =============================================
-- Backup Schedules Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BackupSchedules' AND xtype='U')
BEGIN
    CREATE TABLE BackupSchedules (
        BackupScheduleId INT IDENTITY(1,1) PRIMARY KEY,
        ScheduleName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        BackupType NVARCHAR(50) NOT NULL, -- FULL, INCREMENTAL, DIFFERENTIAL
        Frequency NVARCHAR(50) NOT NULL, -- DAILY, WEEKLY, MONTHLY
        DayOfWeek INT NULL, -- 1=Sunday, 7=Saturday
        DayOfMonth INT NULL, -- 1-31
        BackupTime TIME NOT NULL,
        BackupPath NVARCHAR(500) NOT NULL,
        CompressBackup BIT NOT NULL DEFAULT 1,
        EncryptBackup BIT NOT NULL DEFAULT 0,
        EncryptionPassword NVARCHAR(100),
        RetentionDays INT NOT NULL DEFAULT 30,
        IsActive BIT NOT NULL DEFAULT 1,
        NotifyOnCompletion BIT NOT NULL DEFAULT 0,
        NotificationEmails NVARCHAR(500),
        LastExecutionDate DATETIME NULL,
        NextExecutionDate DATETIME NULL,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL
    );
    
    PRINT 'BackupSchedules table created successfully';
END
ELSE
    PRINT 'BackupSchedules table already exists';
GO

-- =============================================
-- Backup History Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BackupHistory' AND xtype='U')
BEGIN
    CREATE TABLE BackupHistory (
        BackupHistoryId INT IDENTITY(1,1) PRIMARY KEY,
        BackupScheduleId INT NOT NULL,
        StartTime DATETIME NOT NULL,
        EndTime DATETIME NULL,
        BackupFilePath NVARCHAR(500),
        FileSize BIGINT NULL,
        Status NVARCHAR(50) NOT NULL DEFAULT 'RUNNING', -- RUNNING, COMPLETED, FAILED, CANCELLED
        ErrorMessage NVARCHAR(1000),
        DurationSeconds INT NULL,
        IsManualBackup BIT NOT NULL DEFAULT 0,
        InitiatedBy INT NULL,
        InitiatedByName NVARCHAR(100),
        Notes NVARCHAR(500),
        
        FOREIGN KEY (BackupScheduleId) REFERENCES BackupSchedules(BackupScheduleId)
    );
    
    PRINT 'BackupHistory table created successfully';
END
ELSE
    PRINT 'BackupHistory table already exists';
GO

-- =============================================
-- Roles Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Roles' AND xtype='U')
BEGIN
    CREATE TABLE Roles (
        RoleId INT IDENTITY(1,1) PRIMARY KEY,
        RoleName NVARCHAR(50) NOT NULL UNIQUE,
        Description NVARCHAR(500),
        IsActive BIT NOT NULL DEFAULT 1,
        IsSystemRole BIT NOT NULL DEFAULT 0,
        Priority INT NOT NULL DEFAULT 100,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL
    );
    
    PRINT 'Roles table created successfully';
END
ELSE
    PRINT 'Roles table already exists';
GO

-- =============================================
-- Permissions Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Permissions' AND xtype='U')
BEGIN
    CREATE TABLE Permissions (
        PermissionId INT IDENTITY(1,1) PRIMARY KEY,
        PermissionCode NVARCHAR(50) NOT NULL UNIQUE,
        PermissionName NVARCHAR(100) NOT NULL,
        Description NVARCHAR(500),
        Category NVARCHAR(50) NOT NULL,
        PermissionType NVARCHAR(50) NOT NULL, -- VIEW, CREATE, UPDATE, DELETE, EXPORT, IMPORT, ADMIN
        IsActive BIT NOT NULL DEFAULT 1,
        IsSystemPermission BIT NOT NULL DEFAULT 0,
        Priority INT NOT NULL DEFAULT 100,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL
    );
    
    PRINT 'Permissions table created successfully';
END
ELSE
    PRINT 'Permissions table already exists';
GO

-- =============================================
-- User Roles Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRoles' AND xtype='U')
BEGIN
    CREATE TABLE UserRoles (
        UserRoleId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        RoleId INT NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        StartDate DATETIME NULL,
        EndDate DATETIME NULL,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        ModifiedBy INT NULL,
        ModifiedDate DATETIME NULL,
        
        FOREIGN KEY (UserId) REFERENCES Users(UserId),
        FOREIGN KEY (RoleId) REFERENCES Roles(RoleId),
        UNIQUE (UserId, RoleId)
    );
    
    PRINT 'UserRoles table created successfully';
END
ELSE
    PRINT 'UserRoles table already exists';
GO

-- =============================================
-- Role Permissions Table
-- =============================================
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RolePermissions' AND xtype='U')
BEGIN
    CREATE TABLE RolePermissions (
        RolePermissionId INT IDENTITY(1,1) PRIMARY KEY,
        RoleId INT NOT NULL,
        PermissionId INT NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        
        FOREIGN KEY (RoleId) REFERENCES Roles(RoleId),
        FOREIGN KEY (PermissionId) REFERENCES Permissions(PermissionId),
        UNIQUE (RoleId, PermissionId)
    );
    
    PRINT 'RolePermissions table created successfully';
END
ELSE
    PRINT 'RolePermissions table already exists';
GO

PRINT 'All tables created successfully!';
GO
