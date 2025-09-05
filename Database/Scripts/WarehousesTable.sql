USE [DistributionDB]
GO

-- Create Warehouses table
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
GO

-- Create index for better performance
CREATE INDEX [IX_Warehouses_WarehouseName] ON [dbo].[Warehouses] ([WarehouseName]);
CREATE INDEX [IX_Warehouses_IsActive] ON [dbo].[Warehouses] ([IsActive]);
GO

-- Insert sample data for testing
INSERT INTO [dbo].[Warehouses] ([WarehouseName], [Address], [ContactPerson], [Phone], [Email], [Capacity], [Description], [IsActive], [CreatedDate])
VALUES 
('Main Warehouse', '123 Main Street, City Center', 'John Smith', '+1-555-0101', 'john.smith@company.com', '10,000 sq ft', 'Primary warehouse for all products', 1, GETDATE()),
('Secondary Warehouse', '456 Industrial Ave, Business District', 'Sarah Johnson', '+1-555-0102', 'sarah.johnson@company.com', '8,000 sq ft', 'Secondary storage facility', 1, GETDATE()),
('North Branch', '789 North Road, Suburb Area', 'Mike Wilson', '+1-555-0103', 'mike.wilson@company.com', '5,000 sq ft', 'Northern distribution center', 1, GETDATE()),
('South Branch', '321 South Blvd, Downtown', 'Lisa Brown', '+1-555-0104', 'lisa.brown@company.com', '6,000 sq ft', 'Southern distribution center', 1, GETDATE()),
('Central Storage', '654 Central Park, Midtown', 'David Lee', '+1-555-0105', 'david.lee@company.com', '7,500 sq ft', 'Central storage facility', 1, GETDATE());
GO

PRINT 'Warehouses table created successfully with sample data!';
GO
