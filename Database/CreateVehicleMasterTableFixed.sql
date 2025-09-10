-- Create VehicleMaster table (corrected for existing database)
USE DistributionDB;
GO

-- Create VehicleMaster table if it doesn't exist
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

-- Add VehicleID column to DeliveryChallans table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('DeliveryChallans') AND name = 'VehicleId')
BEGIN
    ALTER TABLE DeliveryChallans 
    ADD VehicleId INT NULL;

    -- Add foreign key constraint
    ALTER TABLE DeliveryChallans 
    ADD CONSTRAINT FK_DeliveryChallans_VehicleMaster 
    FOREIGN KEY (VehicleId) REFERENCES VehicleMaster(VehicleId);

    -- Create index on VehicleId for better performance
    CREATE INDEX IX_DeliveryChallans_VehicleId ON DeliveryChallans(VehicleId);

    PRINT 'VehicleId column added to DeliveryChallans table successfully';
END
ELSE
BEGIN
    PRINT 'VehicleId column already exists in DeliveryChallans table';
END

-- Insert sample data for testing (only if table is empty)
IF NOT EXISTS (SELECT 1 FROM VehicleMaster)
BEGIN
    INSERT INTO VehicleMaster (VehicleNo, VehicleType, DriverName, DriverContact, TransporterName, IsActive, CreatedBy, Remarks)
    VALUES 
        ('TN-01-AB-1234', 'Truck', 'Rajesh Kumar', '9876543210', 'ABC Transport', 1, 1, 'Heavy duty truck for long distance'),
        ('TN-02-CD-5678', 'Van', 'Suresh Singh', '9876543211', 'XYZ Logistics', 1, 1, 'Small van for local delivery'),
        ('TN-03-EF-9012', 'Bike', 'Manoj Patel', '9876543212', 'Quick Delivery', 1, 1, 'Motorcycle for quick deliveries'),
        ('TN-04-GH-3456', 'Truck', 'Vikram Sharma', '9876543213', 'Speed Transport', 1, 1, 'Medium truck for city delivery'),
        ('TN-05-IJ-7890', 'Van', 'Amit Kumar', '9876543214', 'Reliable Logistics', 0, 1, 'Out of service - maintenance'),
        ('TN-06-KL-2468', 'Car', 'Deepak Verma', '9876543215', 'Express Cargo', 1, 1, 'Small car for urgent deliveries'),
        ('TN-07-MN-1357', 'Truck', 'Ravi Kumar', '9876543216', 'Heavy Haulage', 1, 1, 'Large truck for bulk transport'),
        ('TN-08-OP-9753', 'Van', 'Sunil Sharma', '9876543217', 'City Logistics', 1, 1, 'Medium van for city distribution');

    PRINT 'Sample vehicle data inserted successfully';
END
ELSE
BEGIN
    PRINT 'VehicleMaster table already contains data';
END

-- Verify the setup
SELECT 'VehicleMaster Table Structure:' AS Info;
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE,
    COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'VehicleMaster'
ORDER BY ORDINAL_POSITION;

SELECT 'Sample Data:' AS Info;
SELECT TOP 5 * FROM VehicleMaster;

SELECT 'DeliveryChallans Table Check:' AS Info;
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'DeliveryChallans' 
AND COLUMN_NAME = 'VehicleId';

PRINT 'Vehicle Master setup completed successfully!';
