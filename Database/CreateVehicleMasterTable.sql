-- Create VehicleMaster table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='VehicleMaster' AND xtype='U')
BEGIN
    CREATE TABLE VehicleMaster (
        VehicleId INT IDENTITY(1,1) PRIMARY KEY,
        VehicleNo NVARCHAR(50) NOT NULL UNIQUE,
        VehicleType NVARCHAR(50),
        DriverName NVARCHAR(100),
        DriverContact NVARCHAR(20),
        TransporterName NVARCHAR(100),
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        CreatedBy INT NOT NULL,
        ModifiedDate DATETIME NULL,
        ModifiedBy INT NULL,
        Remarks NVARCHAR(500) NULL
    );

    -- Create index on VehicleNo for better performance
    CREATE INDEX IX_VehicleMaster_VehicleNo ON VehicleMaster(VehicleNo);
    
    -- Create index on IsActive for filtering active vehicles
    CREATE INDEX IX_VehicleMaster_IsActive ON VehicleMaster(IsActive);

    PRINT 'VehicleMaster table created successfully';
END
ELSE
BEGIN
    PRINT 'VehicleMaster table already exists';
END

-- Add VehicleID column to DeliveryChallan table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('DeliveryChallan') AND name = 'VehicleId')
BEGIN
    ALTER TABLE DeliveryChallan 
    ADD VehicleId INT NULL;

    -- Add foreign key constraint
    ALTER TABLE DeliveryChallan 
    ADD CONSTRAINT FK_DeliveryChallan_VehicleMaster 
    FOREIGN KEY (VehicleId) REFERENCES VehicleMaster(VehicleId);

    -- Create index on VehicleId for better performance
    CREATE INDEX IX_DeliveryChallan_VehicleId ON DeliveryChallan(VehicleId);

    PRINT 'VehicleId column added to DeliveryChallan table successfully';
END
ELSE
BEGIN
    PRINT 'VehicleId column already exists in DeliveryChallan table';
END

-- Insert sample data for testing
INSERT INTO VehicleMaster (VehicleNo, VehicleType, DriverName, DriverContact, TransporterName, IsActive, CreatedBy, Remarks)
VALUES 
    ('TN-01-AB-1234', 'Truck', 'Rajesh Kumar', '9876543210', 'ABC Transport', 1, 1, 'Heavy duty truck for long distance'),
    ('TN-02-CD-5678', 'Van', 'Suresh Singh', '9876543211', 'XYZ Logistics', 1, 1, 'Small van for local delivery'),
    ('TN-03-EF-9012', 'Bike', 'Manoj Patel', '9876543212', 'Quick Delivery', 1, 1, 'Motorcycle for quick deliveries'),
    ('TN-04-GH-3456', 'Truck', 'Vikram Sharma', '9876543213', 'Speed Transport', 1, 1, 'Medium truck for city delivery'),
    ('TN-05-IJ-7890', 'Van', 'Amit Kumar', '9876543214', 'Reliable Logistics', 0, 1, 'Out of service - maintenance');

PRINT 'Sample vehicle data inserted successfully';
