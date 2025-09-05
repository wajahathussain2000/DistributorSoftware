USE [DistributionDB]
GO

-- Create StockAdjustments table
CREATE TABLE [dbo].[StockAdjustments](
    [AdjustmentId] INT IDENTITY(1,1) NOT NULL,
    [ProductId] INT NOT NULL,
    [SystemQuantity] INT NOT NULL,
    [PhysicalQuantity] INT NOT NULL,
    [Difference] INT NOT NULL,
    [Reason] NVARCHAR(255) NOT NULL,
    [AdjustmentType] NVARCHAR(20) CHECK (AdjustmentType IN ('Increase','Decrease')),
    [CreatedBy] NVARCHAR(50) NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT (GETDATE()),

    CONSTRAINT [PK_StockAdjustments] PRIMARY KEY CLUSTERED ([AdjustmentId] ASC),
    CONSTRAINT [FK_StockAdjustments_Products] FOREIGN KEY ([ProductId]) 
        REFERENCES [dbo].[Products]([ProductId])
        ON DELETE CASCADE ON UPDATE CASCADE
);
GO

-- Create index for better performance
CREATE INDEX [IX_StockAdjustments_ProductId] ON [dbo].[StockAdjustments] ([ProductId]);
CREATE INDEX [IX_StockAdjustments_CreatedDate] ON [dbo].[StockAdjustments] ([CreatedDate]);
GO

-- Insert sample data for testing
INSERT INTO [dbo].[StockAdjustments] ([ProductId], [SystemQuantity], [PhysicalQuantity], [Difference], [Reason], [AdjustmentType], [CreatedBy], [CreatedDate])
VALUES 
(1, 100, 95, -5, 'Physical count adjustment - damaged items found', 'Decrease', 'Admin', GETDATE()),
(2, 50, 52, 2, 'Found items during inventory count', 'Increase', 'Admin', GETDATE()),
(3, 200, 200, 0, 'Regular inventory verification', 'Increase', 'Admin', GETDATE());
GO

PRINT 'StockAdjustments table created successfully with sample data!';
GO
