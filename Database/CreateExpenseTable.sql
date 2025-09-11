-- Create Expenses table
USE [DistributionDB]
GO

-- Drop table if exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Expenses]') AND type in (N'U'))
    DROP TABLE [dbo].[Expenses]
GO

-- Create Expenses table
CREATE TABLE [dbo].[Expenses](
    [ExpenseId] [int] IDENTITY(1,1) NOT NULL,
    [ExpenseCode] [nvarchar](50) NOT NULL,
    [Barcode] [nvarchar](100) NOT NULL,
    [CategoryId] [int] NOT NULL,
    [ExpenseDate] [date] NOT NULL,
    [Amount] [decimal](18, 2) NOT NULL,
    [Description] [nvarchar](500) NULL,
    [ReferenceNumber] [nvarchar](100) NULL,
    [PaymentMethod] [nvarchar](50) NULL,
    [ImagePath] [nvarchar](500) NULL,
    [ImageData] [varbinary](max) NULL,
    [Status] [nvarchar](20) NOT NULL DEFAULT ('PENDING'),
    [ApprovedBy] [int] NULL,
    [ApprovedDate] [datetime] NULL,
    [Remarks] [nvarchar](500) NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
    [CreatedBy] [int] NULL,
    [ModifiedDate] [datetime] NULL,
    [ModifiedBy] [int] NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED 
    (
        [ExpenseId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Add unique constraint on ExpenseCode
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [UK_Expenses_ExpenseCode] UNIQUE ([ExpenseCode])
GO

-- Add unique constraint on Barcode
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [UK_Expenses_Barcode] UNIQUE ([Barcode])
GO

-- Add foreign key constraint to ExpenseCategories
ALTER TABLE [dbo].[Expenses]
ADD CONSTRAINT [FK_Expenses_ExpenseCategories] 
FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[ExpenseCategories]([CategoryId])
GO

-- Insert sample data
INSERT INTO [dbo].[Expenses] ([ExpenseCode], [Barcode], [CategoryId], [ExpenseDate], [Amount], [Description], [ReferenceNumber], [PaymentMethod], [Status], [CreatedBy])
VALUES 
    ('EXP-2025-0001', 'EXP001-20250101-001', 1, '2025-01-01', 150.00, 'Office supplies purchase', 'REF-001', 'Cash', 'PENDING', 1),
    ('EXP-2025-0002', 'EXP002-20250101-002', 2, '2025-01-01', 75.50, 'Travel expenses', 'REF-002', 'Credit Card', 'APPROVED', 1),
    ('EXP-2025-0003', 'EXP003-20250101-003', 3, '2025-01-01', 200.00, 'Equipment maintenance', 'REF-003', 'Bank Transfer', 'PENDING', 1)
GO
