USE [DistributionDB]
GO

-- Create Units table
CREATE TABLE [dbo].[Units](
    [UnitId] INT IDENTITY(1,1) NOT NULL,
    [UnitName] NVARCHAR(50) NOT NULL,
    [UnitSymbol] NVARCHAR(10) NULL,
    [UnitType] NVARCHAR(20) NULL,
    [Description] NVARCHAR(255) NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT (GETDATE()),
    [ModifiedDate] DATETIME NULL,

    CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED ([UnitId] ASC)
);
GO

-- Create index for better performance
CREATE INDEX [IX_Units_UnitName] ON [dbo].[Units] ([UnitName]);
CREATE INDEX [IX_Units_IsActive] ON [dbo].[Units] ([IsActive]);
GO

-- Insert sample data for testing
INSERT INTO [dbo].[Units] ([UnitName], [UnitSymbol], [UnitType], [Description], [IsActive], [CreatedDate])
VALUES 
('Piece', 'pc', 'Count', 'Individual items', 1, GETDATE()),
('Box', 'box', 'Count', 'Box of items', 1, GETDATE()),
('Kilogram', 'kg', 'Weight', 'Weight measurement', 1, GETDATE()),
('Liter', 'L', 'Volume', 'Volume measurement', 1, GETDATE()),
('Meter', 'm', 'Length', 'Length measurement', 1, GETDATE()),
('Pack', 'pack', 'Count', 'Pack of items', 1, GETDATE()),
('Dozen', 'dz', 'Count', '12 items', 1, GETDATE()),
('Carton', 'ctn', 'Count', 'Carton of items', 1, GETDATE()),
('Gram', 'g', 'Weight', 'Small weight measurement', 1, GETDATE()),
('Milliliter', 'ml', 'Volume', 'Small volume measurement', 1, GETDATE()),
('Centimeter', 'cm', 'Length', 'Small length measurement', 1, GETDATE()),
('Pair', 'pr', 'Count', 'Two items', 1, GETDATE());
GO

PRINT 'Units table created successfully with sample data!';
GO
