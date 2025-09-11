-- Create ExpenseCategories table
USE [DistributionDB]
GO

-- Drop table if exists
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpenseCategories]') AND type in (N'U'))
    DROP TABLE [dbo].[ExpenseCategories]
GO

-- Create ExpenseCategories table
CREATE TABLE [dbo].[ExpenseCategories](
    [CategoryId] [int] IDENTITY(1,1) NOT NULL,
    [CategoryName] [nvarchar](100) NOT NULL,
    [CategoryCode] [nvarchar](20) NULL,
    [Description] [nvarchar](255) NULL,
    [IsActive] [bit] NOT NULL DEFAULT ((1)),
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
    [CreatedBy] [int] NULL,
    [ModifiedDate] [datetime] NULL,
    [ModifiedBy] [int] NULL,
    CONSTRAINT [PK_ExpenseCategories] PRIMARY KEY CLUSTERED 
    (
        [CategoryId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

-- Add unique constraint on CategoryName
ALTER TABLE [dbo].[ExpenseCategories]
ADD CONSTRAINT [UK_ExpenseCategories_CategoryName] UNIQUE ([CategoryName])
GO

-- Add unique constraint on CategoryCode (if provided)
ALTER TABLE [dbo].[ExpenseCategories]
ADD CONSTRAINT [UK_ExpenseCategories_CategoryCode] UNIQUE ([CategoryCode])
GO

-- Insert sample data
INSERT INTO [dbo].[ExpenseCategories] ([CategoryName], [CategoryCode], [Description], [IsActive], [CreatedBy])
VALUES 
    ('Office Supplies', 'OFF-SUP', 'Office stationery and supplies', 1, 1),
    ('Travel & Transportation', 'TRAVEL', 'Travel expenses and transportation costs', 1, 1),
    ('Utilities', 'UTIL', 'Electricity, water, internet bills', 1, 1),
    ('Marketing & Advertising', 'MKT', 'Marketing campaigns and advertising expenses', 1, 1),
    ('Maintenance & Repairs', 'MAINT', 'Equipment maintenance and repair costs', 1, 1),
    ('Professional Services', 'PROF', 'Legal, accounting, consulting fees', 1, 1),
    ('Insurance', 'INS', 'Business insurance premiums', 1, 1),
    ('Rent & Lease', 'RENT', 'Office rent and equipment lease payments', 1, 1),
    ('Training & Development', 'TRAIN', 'Employee training and development costs', 1, 1),
    ('Miscellaneous', 'MISC', 'Other business expenses', 1, 1)
GO

PRINT 'ExpenseCategories table created successfully with sample data'
GO
