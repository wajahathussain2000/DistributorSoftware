USE [DistributionDB]
GO

-- Fix Units table if it exists but is missing columns
IF EXISTS (SELECT * FROM sysobjects WHERE name='Units' AND xtype='U')
BEGIN
    -- Check if UnitSymbol column exists, if not add it
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND name = 'UnitSymbol')
    BEGIN
        ALTER TABLE [dbo].[Units] ADD [UnitSymbol] NVARCHAR(10) NULL;
        PRINT 'Added UnitSymbol column to Units table';
    END
    
    -- Check if UnitType column exists, if not add it
    IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[Units]') AND name = 'UnitType')
    BEGIN
        ALTER TABLE [dbo].[Units] ADD [UnitType] NVARCHAR(20) NULL;
        PRINT 'Added UnitType column to Units table';
    END
    
    -- Update existing records with UnitSymbol and UnitType
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'pc', [UnitType] = 'Count' WHERE [UnitName] = 'Piece';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'box', [UnitType] = 'Count' WHERE [UnitName] = 'Box';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'kg', [UnitType] = 'Weight' WHERE [UnitName] = 'Kilogram';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'L', [UnitType] = 'Volume' WHERE [UnitName] = 'Liter';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'm', [UnitType] = 'Length' WHERE [UnitName] = 'Meter';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'pack', [UnitType] = 'Count' WHERE [UnitName] = 'Pack';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'dz', [UnitType] = 'Count' WHERE [UnitName] = 'Dozen';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'ctn', [UnitType] = 'Count' WHERE [UnitName] = 'Carton';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'g', [UnitType] = 'Weight' WHERE [UnitName] = 'Gram';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'ml', [UnitType] = 'Volume' WHERE [UnitName] = 'Milliliter';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'cm', [UnitType] = 'Length' WHERE [UnitName] = 'Centimeter';
    UPDATE [dbo].[Units] SET [UnitSymbol] = 'pr', [UnitType] = 'Count' WHERE [UnitName] = 'Pair';
    
    PRINT 'Updated Units table with UnitSymbol and UnitType values';
END
ELSE
BEGIN
    -- Create Units table if it doesn't exist
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
    
    CREATE INDEX [IX_Units_UnitName] ON [dbo].[Units] ([UnitName]);
    CREATE INDEX [IX_Units_IsActive] ON [dbo].[Units] ([IsActive]);
    
    -- Insert sample data
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
    
    PRINT 'Created Units table with sample data';
END

-- Create Warehouses table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Warehouses' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Warehouses](
        [WarehouseId] INT IDENTITY(1,1) NOT NULL,
        [WarehouseName] NVARCHAR(100) NOT NULL,
        [Address] NVARCHAR(255) NULL,
        [ContactPerson] NVARCHAR(100) NULL,
        [Phone] NVARCHAR(20) NULL,
        [Email] NVARCHAR(100) NULL,
        [Capacity] NVARCHAR(50) NULL,
        [Description] NVARCHAR(500) NULL,
        [IsActive] BIT NOT NULL DEFAULT 1,
        [CreatedDate] DATETIME NOT NULL DEFAULT (GETDATE()),
        [ModifiedDate] DATETIME NULL,

        CONSTRAINT [PK_Warehouses] PRIMARY KEY CLUSTERED ([WarehouseId] ASC)
    );
    
    CREATE INDEX [IX_Warehouses_WarehouseName] ON [dbo].[Warehouses] ([WarehouseName]);
    CREATE INDEX [IX_Warehouses_IsActive] ON [dbo].[Warehouses] ([IsActive]);
    
    -- Insert sample data
    INSERT INTO [dbo].[Warehouses] ([WarehouseName], [Address], [ContactPerson], [Phone], [Email], [Capacity], [Description], [IsActive], [CreatedDate])
    VALUES 
    ('Main Warehouse', '123 Main Street, City Center', 'John Smith', '+1-555-0101', 'john.smith@company.com', '10,000 sq ft', 'Primary warehouse for all products', 1, GETDATE()),
    ('Secondary Warehouse', '456 Industrial Ave, Business District', 'Sarah Johnson', '+1-555-0102', 'sarah.johnson@company.com', '8,000 sq ft', 'Secondary storage facility', 1, GETDATE()),
    ('North Branch', '789 North Road, Suburb Area', 'Mike Wilson', '+1-555-0103', 'mike.wilson@company.com', '5,000 sq ft', 'Northern distribution center', 1, GETDATE()),
    ('South Branch', '321 South Blvd, Downtown', 'Lisa Brown', '+1-555-0104', 'lisa.brown@company.com', '6,000 sq ft', 'Southern distribution center', 1, GETDATE()),
    ('Central Storage', '654 Central Park, Midtown', 'David Lee', '+1-555-0105', 'david.lee@company.com', '7,500 sq ft', 'Central storage facility', 1, GETDATE());
    
    PRINT 'Created Warehouses table with sample data';
END

-- Create StockAdjustments table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='StockAdjustments' AND xtype='U')
BEGIN
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
    
    CREATE INDEX [IX_StockAdjustments_ProductId] ON [dbo].[StockAdjustments] ([ProductId]);
    CREATE INDEX [IX_StockAdjustments_CreatedDate] ON [dbo].[StockAdjustments] ([CreatedDate]);
    
    -- Insert sample data
    INSERT INTO [dbo].[StockAdjustments] ([ProductId], [SystemQuantity], [PhysicalQuantity], [Difference], [Reason], [AdjustmentType], [CreatedBy], [CreatedDate])
    VALUES 
    (1, 100, 95, -5, 'Physical count adjustment - damaged items found', 'Decrease', 'Admin', GETDATE()),
    (2, 50, 52, 2, 'Found items during inventory count', 'Increase', 'Admin', GETDATE()),
    (3, 200, 200, 0, 'Regular inventory verification', 'Increase', 'Admin', GETDATE());
    
    PRINT 'Created StockAdjustments table with sample data';
END

PRINT 'Database tables fixed successfully!'
GO
