-- Fix Database Issues for Salesman Target & Achievement Form
-- This script fixes the missing columns and tables

USE DistributionDB;
GO

-- First, let's check if Users table exists and add missing columns
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Users')
BEGIN
    -- Add Role column if it doesn't exist
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'Role')
    BEGIN
        ALTER TABLE [dbo].[Users] ADD [Role] [nvarchar](50) NULL;
        PRINT 'Added Role column to Users table';
    END
    
    -- Add EmployeeCode column if it doesn't exist
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'EmployeeCode')
    BEGIN
        ALTER TABLE [dbo].[Users] ADD [EmployeeCode] [nvarchar](50) NULL;
        PRINT 'Added EmployeeCode column to Users table';
    END
    
    -- Add IsActive column if it doesn't exist
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'IsActive')
    BEGIN
        ALTER TABLE [dbo].[Users] ADD [IsActive] [bit] NOT NULL DEFAULT(1);
        PRINT 'Added IsActive column to Users table';
    END
    
    -- Update existing users with default values
    UPDATE [dbo].[Users] SET [Role] = 'Admin' WHERE [Role] IS NULL;
    UPDATE [dbo].[Users] SET [EmployeeCode] = 'EMP' + CAST([UserId] AS NVARCHAR(10)) WHERE [EmployeeCode] IS NULL;
    UPDATE [dbo].[Users] SET [IsActive] = 1 WHERE [IsActive] IS NULL;
    
    PRINT 'Updated existing users with default values';
END
ELSE
BEGIN
    -- Create Users table if it doesn't exist
    CREATE TABLE [dbo].[Users](
        [UserId] [int] IDENTITY(1,1) NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [Username] [nvarchar](50) NOT NULL,
        [Password] [nvarchar](255) NOT NULL,
        [Email] [nvarchar](100) NULL,
        [Phone] [nvarchar](20) NULL,
        [Role] [nvarchar](50) NOT NULL DEFAULT('User'),
        [EmployeeCode] [nvarchar](50) NULL,
        [IsActive] [bit] NOT NULL DEFAULT(1),
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [LastLoginDate] [datetime] NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
    );
    
    -- Insert default admin user
    INSERT INTO [dbo].[Users] ([FirstName], [LastName], [Username], [Password], [Role], [EmployeeCode])
    VALUES ('System', 'Administrator', 'admin', 'admin123', 'Admin', 'ADMIN001');
    
    -- Insert sample salesman users
    INSERT INTO [dbo].[Users] ([FirstName], [LastName], [Username], [Password], [Role], [EmployeeCode])
    VALUES 
    ('John', 'Smith', 'jsmith', 'password123', 'Salesman', 'SALES001'),
    ('Sarah', 'Johnson', 'sjohnson', 'password123', 'Salesman', 'SALES002'),
    ('Mike', 'Brown', 'mbrown', 'password123', 'Salesman', 'SALES003');
    
    PRINT 'Created Users table with sample data';
END
GO

-- Create SalesmanTarget table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTarget')
BEGIN
    CREATE TABLE [dbo].[SalesmanTarget](
        [TargetId] [int] IDENTITY(1,1) NOT NULL,
        [SalesmanId] [int] NOT NULL,
        [TargetType] [nvarchar](20) NOT NULL,
        [TargetPeriodStart] [datetime] NOT NULL,
        [TargetPeriodEnd] [datetime] NOT NULL,
        [TargetPeriodName] [nvarchar](100) NOT NULL,
        [RevenueTarget] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitTarget] [int] NOT NULL DEFAULT(0),
        [CustomerTarget] [int] NOT NULL DEFAULT(0),
        [InvoiceTarget] [int] NOT NULL DEFAULT(0),
        [ProductCategory] [nvarchar](100) NULL,
        [CategoryRevenueTarget] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitTarget] [int] NOT NULL DEFAULT(0),
        [ActualRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
        [ActualUnits] [int] NOT NULL DEFAULT(0),
        [ActualCustomers] [int] NOT NULL DEFAULT(0),
        [ActualInvoices] [int] NOT NULL DEFAULT(0),
        [ActualCategoryRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
        [ActualCategoryUnits] [int] NOT NULL DEFAULT(0),
        [RevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [UnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CustomerAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [InvoiceAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryRevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryUnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [RevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitVariance] [int] NOT NULL DEFAULT(0),
        [CustomerVariance] [int] NOT NULL DEFAULT(0),
        [InvoiceVariance] [int] NOT NULL DEFAULT(0),
        [CategoryRevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitVariance] [int] NOT NULL DEFAULT(0),
        [Status] [nvarchar](20) NOT NULL DEFAULT('DRAFT'),
        [PerformanceRating] [nvarchar](20) NOT NULL DEFAULT('AVERAGE'),
        [ManagerComments] [nvarchar](max) NULL,
        [SalesmanComments] [nvarchar](max) NULL,
        [MarketConditions] [nvarchar](max) NULL,
        [Challenges] [nvarchar](max) NULL,
        [BonusAmount] [decimal](18,2) NOT NULL DEFAULT(0),
        [CommissionAmount] [decimal](18,2) NOT NULL DEFAULT(0),
        [IsBonusEligible] [bit] NOT NULL DEFAULT(0),
        [IsCommissionEligible] [bit] NOT NULL DEFAULT(0),
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [CreatedBy] [int] NULL,
        [ModifiedDate] [datetime] NULL,
        [ModifiedBy] [int] NULL,
        [ApprovedDate] [datetime] NULL,
        [ApprovedBy] [int] NULL,
        CONSTRAINT [PK_SalesmanTarget] PRIMARY KEY CLUSTERED ([TargetId] ASC)
    );
    
    PRINT 'Created SalesmanTarget table';
END
GO

-- Create SalesmanTargetAchievement table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTargetAchievement')
BEGIN
    CREATE TABLE [dbo].[SalesmanTargetAchievement](
        [AchievementId] [int] IDENTITY(1,1) NOT NULL,
        [TargetId] [int] NOT NULL,
        [SalesmanId] [int] NOT NULL,
        [AchievementDate] [datetime] NOT NULL,
        [AchievementPeriod] [nvarchar](20) NOT NULL,
        [RevenueAchieved] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitsSold] [int] NOT NULL DEFAULT(0),
        [CustomersServed] [int] NOT NULL DEFAULT(0),
        [InvoicesGenerated] [int] NOT NULL DEFAULT(0),
        [ProductCategory] [nvarchar](100) NULL,
        [CategoryRevenueAchieved] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitsSold] [int] NOT NULL DEFAULT(0),
        [RevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [UnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CustomerAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [InvoiceAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryRevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryUnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [RevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitVariance] [int] NOT NULL DEFAULT(0),
        [CustomerVariance] [int] NOT NULL DEFAULT(0),
        [InvoiceVariance] [int] NOT NULL DEFAULT(0),
        [CategoryRevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitVariance] [int] NOT NULL DEFAULT(0),
        [AchievementNotes] [nvarchar](max) NULL,
        [Challenges] [nvarchar](max) NULL,
        [MarketConditions] [nvarchar](max) NULL,
        [CustomerFeedback] [nvarchar](max) NULL,
        [Status] [nvarchar](20) NOT NULL DEFAULT('RECORDED'),
        [IsVerified] [bit] NOT NULL DEFAULT(0),
        [IsApproved] [bit] NOT NULL DEFAULT(0),
        [VerifiedDate] [datetime] NULL,
        [VerifiedBy] [int] NULL,
        [ApprovedDate] [datetime] NULL,
        [ApprovedBy] [int] NULL,
        [VerificationNotes] [nvarchar](max) NULL,
        [ApprovalNotes] [nvarchar](max) NULL,
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [CreatedBy] [int] NULL,
        [ModifiedDate] [datetime] NULL,
        [ModifiedBy] [int] NULL,
        CONSTRAINT [PK_SalesmanTargetAchievement] PRIMARY KEY CLUSTERED ([AchievementId] ASC),
        CONSTRAINT [FK_SalesmanTargetAchievement_Target] FOREIGN KEY([TargetId]) REFERENCES [dbo].[SalesmanTarget] ([TargetId]) ON DELETE CASCADE
    );
    
    PRINT 'Created SalesmanTargetAchievement table';
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_SalesmanId')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_SalesmanId] ON [dbo].[SalesmanTarget] ([SalesmanId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_TargetType')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_TargetType] ON [dbo].[SalesmanTarget] ([TargetType]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_Status')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Status] ON [dbo].[SalesmanTarget] ([Status]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_Period')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Period] ON [dbo].[SalesmanTarget] ([TargetPeriodStart], [TargetPeriodEnd]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_TargetId')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_TargetId] ON [dbo].[SalesmanTargetAchievement] ([TargetId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_SalesmanId')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_SalesmanId] ON [dbo].[SalesmanTargetAchievement] ([SalesmanId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_Date')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Date] ON [dbo].[SalesmanTargetAchievement] ([AchievementDate]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_Status')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Status] ON [dbo].[SalesmanTargetAchievement] ([Status]);

PRINT 'Created indexes for better performance';
GO

-- Insert sample data if tables are empty
IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[SalesmanTarget])
BEGIN
    INSERT INTO [dbo].[SalesmanTarget] (
        [SalesmanId], [TargetType], [TargetPeriodStart], [TargetPeriodEnd], [TargetPeriodName],
        [RevenueTarget], [UnitTarget], [CustomerTarget], [InvoiceTarget],
        [Status], [PerformanceRating], [CreatedBy]
    ) VALUES 
    (2, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 100000.00, 500, 50, 100, 'ACTIVE', 'AVERAGE', 1),
    (3, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 150000.00, 750, 75, 150, 'ACTIVE', 'GOOD', 1),
    (4, 'QUARTERLY', '2024-01-01', '2024-03-31', 'Q1 2024', 300000.00, 1500, 150, 300, 'ACTIVE', 'AVERAGE', 1);
    
    PRINT 'Inserted sample SalesmanTarget data';
END

IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[SalesmanTargetAchievement])
BEGIN
    INSERT INTO [dbo].[SalesmanTargetAchievement] (
        [TargetId], [SalesmanId], [AchievementDate], [AchievementPeriod],
        [RevenueAchieved], [UnitsSold], [CustomersServed], [InvoicesGenerated],
        [Status], [CreatedBy]
    ) VALUES 
    (1, 2, '2024-01-15', 'DAILY', 50000.00, 250, 25, 50, 'RECORDED', 1),
    (1, 2, '2024-01-20', 'DAILY', 30000.00, 150, 15, 30, 'RECORDED', 1),
    (2, 3, '2024-01-10', 'DAILY', 75000.00, 375, 38, 75, 'RECORDED', 1);
    
    PRINT 'Inserted sample SalesmanTargetAchievement data';
END
GO

PRINT 'Database issues fixed successfully!';
PRINT 'Users table updated with Role, EmployeeCode, and IsActive columns';
PRINT 'SalesmanTarget and SalesmanTargetAchievement tables created';
PRINT 'Sample data inserted for testing';
-- This script fixes the missing columns and tables

USE DistributionDB;
GO

-- First, let's check if Users table exists and add missing columns
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'Users')
BEGIN
    -- Add Role column if it doesn't exist
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'Role')
    BEGIN
        ALTER TABLE [dbo].[Users] ADD [Role] [nvarchar](50) NULL;
        PRINT 'Added Role column to Users table';
    END
    
    -- Add EmployeeCode column if it doesn't exist
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'EmployeeCode')
    BEGIN
        ALTER TABLE [dbo].[Users] ADD [EmployeeCode] [nvarchar](50) NULL;
        PRINT 'Added EmployeeCode column to Users table';
    END
    
    -- Add IsActive column if it doesn't exist
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Users') AND name = 'IsActive')
    BEGIN
        ALTER TABLE [dbo].[Users] ADD [IsActive] [bit] NOT NULL DEFAULT(1);
        PRINT 'Added IsActive column to Users table';
    END
    
    -- Update existing users with default values
    UPDATE [dbo].[Users] SET [Role] = 'Admin' WHERE [Role] IS NULL;
    UPDATE [dbo].[Users] SET [EmployeeCode] = 'EMP' + CAST([UserId] AS NVARCHAR(10)) WHERE [EmployeeCode] IS NULL;
    UPDATE [dbo].[Users] SET [IsActive] = 1 WHERE [IsActive] IS NULL;
    
    PRINT 'Updated existing users with default values';
END
ELSE
BEGIN
    -- Create Users table if it doesn't exist
    CREATE TABLE [dbo].[Users](
        [UserId] [int] IDENTITY(1,1) NOT NULL,
        [FirstName] [nvarchar](50) NOT NULL,
        [LastName] [nvarchar](50) NOT NULL,
        [Username] [nvarchar](50) NOT NULL,
        [Password] [nvarchar](255) NOT NULL,
        [Email] [nvarchar](100) NULL,
        [Phone] [nvarchar](20) NULL,
        [Role] [nvarchar](50) NOT NULL DEFAULT('User'),
        [EmployeeCode] [nvarchar](50) NULL,
        [IsActive] [bit] NOT NULL DEFAULT(1),
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [LastLoginDate] [datetime] NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
    );
    
    -- Insert default admin user
    INSERT INTO [dbo].[Users] ([FirstName], [LastName], [Username], [Password], [Role], [EmployeeCode])
    VALUES ('System', 'Administrator', 'admin', 'admin123', 'Admin', 'ADMIN001');
    
    -- Insert sample salesman users
    INSERT INTO [dbo].[Users] ([FirstName], [LastName], [Username], [Password], [Role], [EmployeeCode])
    VALUES 
    ('John', 'Smith', 'jsmith', 'password123', 'Salesman', 'SALES001'),
    ('Sarah', 'Johnson', 'sjohnson', 'password123', 'Salesman', 'SALES002'),
    ('Mike', 'Brown', 'mbrown', 'password123', 'Salesman', 'SALES003');
    
    PRINT 'Created Users table with sample data';
END
GO

-- Create SalesmanTarget table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTarget')
BEGIN
    CREATE TABLE [dbo].[SalesmanTarget](
        [TargetId] [int] IDENTITY(1,1) NOT NULL,
        [SalesmanId] [int] NOT NULL,
        [TargetType] [nvarchar](20) NOT NULL,
        [TargetPeriodStart] [datetime] NOT NULL,
        [TargetPeriodEnd] [datetime] NOT NULL,
        [TargetPeriodName] [nvarchar](100) NOT NULL,
        [RevenueTarget] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitTarget] [int] NOT NULL DEFAULT(0),
        [CustomerTarget] [int] NOT NULL DEFAULT(0),
        [InvoiceTarget] [int] NOT NULL DEFAULT(0),
        [ProductCategory] [nvarchar](100) NULL,
        [CategoryRevenueTarget] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitTarget] [int] NOT NULL DEFAULT(0),
        [ActualRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
        [ActualUnits] [int] NOT NULL DEFAULT(0),
        [ActualCustomers] [int] NOT NULL DEFAULT(0),
        [ActualInvoices] [int] NOT NULL DEFAULT(0),
        [ActualCategoryRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
        [ActualCategoryUnits] [int] NOT NULL DEFAULT(0),
        [RevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [UnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CustomerAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [InvoiceAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryRevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryUnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [RevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitVariance] [int] NOT NULL DEFAULT(0),
        [CustomerVariance] [int] NOT NULL DEFAULT(0),
        [InvoiceVariance] [int] NOT NULL DEFAULT(0),
        [CategoryRevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitVariance] [int] NOT NULL DEFAULT(0),
        [Status] [nvarchar](20) NOT NULL DEFAULT('DRAFT'),
        [PerformanceRating] [nvarchar](20) NOT NULL DEFAULT('AVERAGE'),
        [ManagerComments] [nvarchar](max) NULL,
        [SalesmanComments] [nvarchar](max) NULL,
        [MarketConditions] [nvarchar](max) NULL,
        [Challenges] [nvarchar](max) NULL,
        [BonusAmount] [decimal](18,2) NOT NULL DEFAULT(0),
        [CommissionAmount] [decimal](18,2) NOT NULL DEFAULT(0),
        [IsBonusEligible] [bit] NOT NULL DEFAULT(0),
        [IsCommissionEligible] [bit] NOT NULL DEFAULT(0),
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [CreatedBy] [int] NULL,
        [ModifiedDate] [datetime] NULL,
        [ModifiedBy] [int] NULL,
        [ApprovedDate] [datetime] NULL,
        [ApprovedBy] [int] NULL,
        CONSTRAINT [PK_SalesmanTarget] PRIMARY KEY CLUSTERED ([TargetId] ASC)
    );
    
    PRINT 'Created SalesmanTarget table';
END
GO

-- Create SalesmanTargetAchievement table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTargetAchievement')
BEGIN
    CREATE TABLE [dbo].[SalesmanTargetAchievement](
        [AchievementId] [int] IDENTITY(1,1) NOT NULL,
        [TargetId] [int] NOT NULL,
        [SalesmanId] [int] NOT NULL,
        [AchievementDate] [datetime] NOT NULL,
        [AchievementPeriod] [nvarchar](20) NOT NULL,
        [RevenueAchieved] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitsSold] [int] NOT NULL DEFAULT(0),
        [CustomersServed] [int] NOT NULL DEFAULT(0),
        [InvoicesGenerated] [int] NOT NULL DEFAULT(0),
        [ProductCategory] [nvarchar](100) NULL,
        [CategoryRevenueAchieved] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitsSold] [int] NOT NULL DEFAULT(0),
        [RevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [UnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CustomerAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [InvoiceAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryRevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [CategoryUnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
        [RevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [UnitVariance] [int] NOT NULL DEFAULT(0),
        [CustomerVariance] [int] NOT NULL DEFAULT(0),
        [InvoiceVariance] [int] NOT NULL DEFAULT(0),
        [CategoryRevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
        [CategoryUnitVariance] [int] NOT NULL DEFAULT(0),
        [AchievementNotes] [nvarchar](max) NULL,
        [Challenges] [nvarchar](max) NULL,
        [MarketConditions] [nvarchar](max) NULL,
        [CustomerFeedback] [nvarchar](max) NULL,
        [Status] [nvarchar](20) NOT NULL DEFAULT('RECORDED'),
        [IsVerified] [bit] NOT NULL DEFAULT(0),
        [IsApproved] [bit] NOT NULL DEFAULT(0),
        [VerifiedDate] [datetime] NULL,
        [VerifiedBy] [int] NULL,
        [ApprovedDate] [datetime] NULL,
        [ApprovedBy] [int] NULL,
        [VerificationNotes] [nvarchar](max) NULL,
        [ApprovalNotes] [nvarchar](max) NULL,
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [CreatedBy] [int] NULL,
        [ModifiedDate] [datetime] NULL,
        [ModifiedBy] [int] NULL,
        CONSTRAINT [PK_SalesmanTargetAchievement] PRIMARY KEY CLUSTERED ([AchievementId] ASC),
        CONSTRAINT [FK_SalesmanTargetAchievement_Target] FOREIGN KEY([TargetId]) REFERENCES [dbo].[SalesmanTarget] ([TargetId]) ON DELETE CASCADE
    );
    
    PRINT 'Created SalesmanTargetAchievement table';
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_SalesmanId')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_SalesmanId] ON [dbo].[SalesmanTarget] ([SalesmanId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_TargetType')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_TargetType] ON [dbo].[SalesmanTarget] ([TargetType]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_Status')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Status] ON [dbo].[SalesmanTarget] ([Status]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTarget_Period')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Period] ON [dbo].[SalesmanTarget] ([TargetPeriodStart], [TargetPeriodEnd]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_TargetId')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_TargetId] ON [dbo].[SalesmanTargetAchievement] ([TargetId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_SalesmanId')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_SalesmanId] ON [dbo].[SalesmanTargetAchievement] ([SalesmanId]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_Date')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Date] ON [dbo].[SalesmanTargetAchievement] ([AchievementDate]);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SalesmanTargetAchievement_Status')
    CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Status] ON [dbo].[SalesmanTargetAchievement] ([Status]);

PRINT 'Created indexes for better performance';
GO

-- Insert sample data if tables are empty
IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[SalesmanTarget])
BEGIN
    INSERT INTO [dbo].[SalesmanTarget] (
        [SalesmanId], [TargetType], [TargetPeriodStart], [TargetPeriodEnd], [TargetPeriodName],
        [RevenueTarget], [UnitTarget], [CustomerTarget], [InvoiceTarget],
        [Status], [PerformanceRating], [CreatedBy]
    ) VALUES 
    (2, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 100000.00, 500, 50, 100, 'ACTIVE', 'AVERAGE', 1),
    (3, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 150000.00, 750, 75, 150, 'ACTIVE', 'GOOD', 1),
    (4, 'QUARTERLY', '2024-01-01', '2024-03-31', 'Q1 2024', 300000.00, 1500, 150, 300, 'ACTIVE', 'AVERAGE', 1);
    
    PRINT 'Inserted sample SalesmanTarget data';
END

IF NOT EXISTS (SELECT TOP 1 * FROM [dbo].[SalesmanTargetAchievement])
BEGIN
    INSERT INTO [dbo].[SalesmanTargetAchievement] (
        [TargetId], [SalesmanId], [AchievementDate], [AchievementPeriod],
        [RevenueAchieved], [UnitsSold], [CustomersServed], [InvoicesGenerated],
        [Status], [CreatedBy]
    ) VALUES 
    (1, 2, '2024-01-15', 'DAILY', 50000.00, 250, 25, 50, 'RECORDED', 1),
    (1, 2, '2024-01-20', 'DAILY', 30000.00, 150, 15, 30, 'RECORDED', 1),
    (2, 3, '2024-01-10', 'DAILY', 75000.00, 375, 38, 75, 'RECORDED', 1);
    
    PRINT 'Inserted sample SalesmanTargetAchievement data';
END
GO

PRINT 'Database issues fixed successfully!';
PRINT 'Users table updated with Role, EmployeeCode, and IsActive columns';
PRINT 'SalesmanTarget and SalesmanTargetAchievement tables created';
PRINT 'Sample data inserted for testing';


