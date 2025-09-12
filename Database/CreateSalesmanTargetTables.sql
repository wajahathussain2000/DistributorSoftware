-- Create Salesman Target & Achievement Tables
-- This script creates the database tables for the Salesman Target & Achievement system

USE DistributionDB;
GO

-- Create SalesmanTarget table
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTarget')
    DROP TABLE [dbo].[SalesmanTarget];
GO

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
GO

-- Create SalesmanTargetAchievement table
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTargetAchievement')
    DROP TABLE [dbo].[SalesmanTargetAchievement];
GO

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
GO

-- Create indexes for better performance
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_SalesmanId] ON [dbo].[SalesmanTarget] ([SalesmanId]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_TargetType] ON [dbo].[SalesmanTarget] ([TargetType]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Status] ON [dbo].[SalesmanTarget] ([Status]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Period] ON [dbo].[SalesmanTarget] ([TargetPeriodStart], [TargetPeriodEnd]);

CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_TargetId] ON [dbo].[SalesmanTargetAchievement] ([TargetId]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_SalesmanId] ON [dbo].[SalesmanTargetAchievement] ([SalesmanId]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Date] ON [dbo].[SalesmanTargetAchievement] ([AchievementDate]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Status] ON [dbo].[SalesmanTargetAchievement] ([Status]);
GO

-- Insert sample data
INSERT INTO [dbo].[SalesmanTarget] (
    [SalesmanId], [TargetType], [TargetPeriodStart], [TargetPeriodEnd], [TargetPeriodName],
    [RevenueTarget], [UnitTarget], [CustomerTarget], [InvoiceTarget],
    [Status], [PerformanceRating], [CreatedBy]
) VALUES 
(1, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 100000.00, 500, 50, 100, 'ACTIVE', 'AVERAGE', 1),
(2, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 150000.00, 750, 75, 150, 'ACTIVE', 'GOOD', 1),
(1, 'QUARTERLY', '2024-01-01', '2024-03-31', 'Q1 2024', 300000.00, 1500, 150, 300, 'ACTIVE', 'AVERAGE', 1);
GO

-- Insert sample achievements
INSERT INTO [dbo].[SalesmanTargetAchievement] (
    [TargetId], [SalesmanId], [AchievementDate], [AchievementPeriod],
    [RevenueAchieved], [UnitsSold], [CustomersServed], [InvoicesGenerated],
    [Status], [CreatedBy]
) VALUES 
(1, 1, '2024-01-15', 'DAILY', 50000.00, 250, 25, 50, 'RECORDED', 1),
(1, 1, '2024-01-20', 'DAILY', 30000.00, 150, 15, 30, 'RECORDED', 1),
(2, 2, '2024-01-10', 'DAILY', 75000.00, 375, 38, 75, 'RECORDED', 1);
GO

PRINT 'Salesman Target & Achievement tables created successfully!';
PRINT 'Sample data inserted successfully!';
-- This script creates the database tables for the Salesman Target & Achievement system

USE DistributionDB;
GO

-- Create SalesmanTarget table
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTarget')
    DROP TABLE [dbo].[SalesmanTarget];
GO

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
GO

-- Create SalesmanTargetAchievement table
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'U' AND name = 'SalesmanTargetAchievement')
    DROP TABLE [dbo].[SalesmanTargetAchievement];
GO

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
GO

-- Create indexes for better performance
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_SalesmanId] ON [dbo].[SalesmanTarget] ([SalesmanId]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_TargetType] ON [dbo].[SalesmanTarget] ([TargetType]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Status] ON [dbo].[SalesmanTarget] ([Status]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTarget_Period] ON [dbo].[SalesmanTarget] ([TargetPeriodStart], [TargetPeriodEnd]);

CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_TargetId] ON [dbo].[SalesmanTargetAchievement] ([TargetId]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_SalesmanId] ON [dbo].[SalesmanTargetAchievement] ([SalesmanId]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Date] ON [dbo].[SalesmanTargetAchievement] ([AchievementDate]);
CREATE NONCLUSTERED INDEX [IX_SalesmanTargetAchievement_Status] ON [dbo].[SalesmanTargetAchievement] ([Status]);
GO

-- Insert sample data
INSERT INTO [dbo].[SalesmanTarget] (
    [SalesmanId], [TargetType], [TargetPeriodStart], [TargetPeriodEnd], [TargetPeriodName],
    [RevenueTarget], [UnitTarget], [CustomerTarget], [InvoiceTarget],
    [Status], [PerformanceRating], [CreatedBy]
) VALUES 
(1, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 100000.00, 500, 50, 100, 'ACTIVE', 'AVERAGE', 1),
(2, 'MONTHLY', '2024-01-01', '2024-01-31', 'January 2024', 150000.00, 750, 75, 150, 'ACTIVE', 'GOOD', 1),
(1, 'QUARTERLY', '2024-01-01', '2024-03-31', 'Q1 2024', 300000.00, 1500, 150, 300, 'ACTIVE', 'AVERAGE', 1);
GO

-- Insert sample achievements
INSERT INTO [dbo].[SalesmanTargetAchievement] (
    [TargetId], [SalesmanId], [AchievementDate], [AchievementPeriod],
    [RevenueAchieved], [UnitsSold], [CustomersServed], [InvoicesGenerated],
    [Status], [CreatedBy]
) VALUES 
(1, 1, '2024-01-15', 'DAILY', 50000.00, 250, 25, 50, 'RECORDED', 1),
(1, 1, '2024-01-20', 'DAILY', 30000.00, 150, 15, 30, 'RECORDED', 1),
(2, 2, '2024-01-10', 'DAILY', 75000.00, 375, 38, 75, 'RECORDED', 1);
GO

PRINT 'Salesman Target & Achievement tables created successfully!';
PRINT 'Sample data inserted successfully!';


