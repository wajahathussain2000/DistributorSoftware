-- Create RouteMaster table
-- This table stores all delivery routes for vehicles

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RouteMaster' AND xtype='U')
BEGIN
    CREATE TABLE RouteMaster (
        RouteID INT IDENTITY(1,1) PRIMARY KEY,
        RouteName NVARCHAR(100) UNIQUE NOT NULL,
        StartLocation NVARCHAR(200),
        EndLocation NVARCHAR(200),
        Distance DECIMAL(10,2),
        EstimatedTime NVARCHAR(50),
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NULL,
        ModifiedDate DATETIME NULL,
        ModifiedBy INT NULL
    );

    -- Add indexes for better performance
    CREATE INDEX IX_RouteMaster_RouteName ON RouteMaster(RouteName);
    CREATE INDEX IX_RouteMaster_IsActive ON RouteMaster(IsActive);
    CREATE INDEX IX_RouteMaster_CreatedDate ON RouteMaster(CreatedDate);

    PRINT 'RouteMaster table created successfully';
END
ELSE
BEGIN
    PRINT 'RouteMaster table already exists';
END

-- Insert sample data for testing
IF NOT EXISTS (SELECT 1 FROM RouteMaster WHERE RouteName = 'Main City Route')
BEGIN
    INSERT INTO RouteMaster (RouteName, StartLocation, EndLocation, Distance, EstimatedTime, IsActive, CreatedBy)
    VALUES 
    ('Main City Route', 'Downtown Office', 'City Center', 15.5, '45 minutes', 1, 1),
    ('Suburban Route', 'Warehouse', 'Suburban Area', 25.0, '60 minutes', 1, 1),
    ('Industrial Route', 'Factory District', 'Port Area', 35.2, '90 minutes', 1, 1),
    ('Express Route', 'Airport', 'Business District', 20.0, '30 minutes', 1, 1),
    ('Local Delivery', 'Local Warehouse', 'Nearby Areas', 8.5, '25 minutes', 1, 1);

    PRINT 'Sample route data inserted successfully';
END
ELSE
BEGIN
    PRINT 'Sample route data already exists';
END

