# Database Update Script for Distribution Software
# This script creates the Warehouses and Units tables

Write-Host "Starting Database Updates..." -ForegroundColor Green

# SQL Server connection details
$server = "localhost"
$database = "DistributionDB"
$connectionString = "Server=$server;Database=$database;Integrated Security=true;"

try {
    # Create SQL connection
    $connection = New-Object System.Data.SqlClient.SqlConnection
    $connection.ConnectionString = $connectionString
    $connection.Open()
    
    Write-Host "Connected to database successfully!" -ForegroundColor Green
    
    # Create Warehouses table
    Write-Host "Creating Warehouses table..." -ForegroundColor Yellow
    $warehousesSQL = @"
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
        
        INSERT INTO [dbo].[Warehouses] ([WarehouseName], [Address], [ContactPerson], [Phone], [Email], [Capacity], [Description], [IsActive], [CreatedDate])
        VALUES 
        ('Main Warehouse', '123 Main Street, City Center', 'John Smith', '+1-555-0101', 'john.smith@company.com', '10,000 sq ft', 'Primary warehouse for all products', 1, GETDATE()),
        ('Secondary Warehouse', '456 Industrial Ave, Business District', 'Sarah Johnson', '+1-555-0102', 'sarah.johnson@company.com', '8,000 sq ft', 'Secondary storage facility', 1, GETDATE()),
        ('North Branch', '789 North Road, Suburb Area', 'Mike Wilson', '+1-555-0103', 'mike.wilson@company.com', '5,000 sq ft', 'Northern distribution center', 1, GETDATE()),
        ('South Branch', '321 South Blvd, Downtown', 'Lisa Brown', '+1-555-0104', 'lisa.brown@company.com', '6,000 sq ft', 'Southern distribution center', 1, GETDATE()),
        ('Central Storage', '654 Central Park, Midtown', 'David Lee', '+1-555-0105', 'david.lee@company.com', '7,500 sq ft', 'Central storage facility', 1, GETDATE());
        
        PRINT 'Warehouses table created successfully with sample data!';
    END
    ELSE
    BEGIN
        PRINT 'Warehouses table already exists!';
    END
"@
    
    $command = New-Object System.Data.SqlClient.SqlCommand($warehousesSQL, $connection)
    $command.ExecuteNonQuery()
    Write-Host "Warehouses table created/verified successfully!" -ForegroundColor Green
    
    # Create Units table
    Write-Host "Creating Units table..." -ForegroundColor Yellow
    $unitsSQL = @"
    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Units' AND xtype='U')
    BEGIN
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
        
        PRINT 'Units table created successfully with sample data!';
    END
    ELSE
    BEGIN
        PRINT 'Units table already exists!';
    END
"@
    
    $command = New-Object System.Data.SqlClient.SqlCommand($unitsSQL, $connection)
    $command.ExecuteNonQuery()
    Write-Host "Units table created/verified successfully!" -ForegroundColor Green
    
    # Create StockAdjustments table if it doesn't exist
    Write-Host "Creating StockAdjustments table..." -ForegroundColor Yellow
    $stockAdjustmentsSQL = @"
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
        
        INSERT INTO [dbo].[StockAdjustments] ([ProductId], [SystemQuantity], [PhysicalQuantity], [Difference], [Reason], [AdjustmentType], [CreatedBy], [CreatedDate])
        VALUES 
        (1, 100, 95, -5, 'Physical count adjustment - damaged items found', 'Decrease', 'Admin', GETDATE()),
        (2, 50, 52, 2, 'Found items during inventory count', 'Increase', 'Admin', GETDATE()),
        (3, 200, 200, 0, 'Regular inventory verification', 'Increase', 'Admin', GETDATE());
        
        PRINT 'StockAdjustments table created successfully with sample data!';
    END
    ELSE
    BEGIN
        PRINT 'StockAdjustments table already exists!';
    END
"@
    
    $command = New-Object System.Data.SqlClient.SqlCommand($stockAdjustmentsSQL, $connection)
    $command.ExecuteNonQuery()
    Write-Host "StockAdjustments table created/verified successfully!" -ForegroundColor Green
    
    $connection.Close()
    Write-Host "Database updates completed successfully!" -ForegroundColor Green
    
} catch {
    Write-Host "Error: $($_.Exception.Message)" -ForegroundColor Red
    if ($connection -and $connection.State -eq 'Open') {
        $connection.Close()
    }
}

Write-Host "Press any key to continue..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
