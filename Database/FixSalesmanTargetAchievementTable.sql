-- Fix SalesmanTargetAchievement Table Structure
-- This script drops the existing table and recreates it with the correct column names

USE [DistributionDB]
GO

-- Drop the existing table if it exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SalesmanTargetAchievement]') AND type in (N'U'))
BEGIN
    PRINT 'Dropping existing SalesmanTargetAchievement table...'
    DROP TABLE [dbo].[SalesmanTargetAchievement]
    PRINT 'Table dropped successfully.'
END
GO

-- Create the SalesmanTargetAchievement table with correct structure
CREATE TABLE [dbo].[SalesmanTargetAchievement](
    [AchievementId] [int] IDENTITY(1,1) NOT NULL,
    [TargetId] [int] NOT NULL,
    [SalesmanId] [int] NOT NULL,
    [AchievementDate] [datetime] NOT NULL,
    [AchievementPeriod] [nvarchar](20) NOT NULL,
    [ActualRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
    [ActualUnits] [int] NOT NULL DEFAULT(0),
    [ActualCustomers] [int] NOT NULL DEFAULT(0),
    [ActualInvoices] [int] NOT NULL DEFAULT(0),
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
    [OverallAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
    [IsAchievementMet] [bit] NOT NULL DEFAULT(0),
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
) ON [PRIMARY]
GO

PRINT 'SalesmanTargetAchievement table created successfully with correct structure!'
GO

-- Verify the table structure
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'SalesmanTargetAchievement'
ORDER BY ORDINAL_POSITION
GO

PRINT 'Table structure verification complete.'
