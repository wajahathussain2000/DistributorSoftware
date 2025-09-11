-- Create Delivery Schedule Tables
-- Run this script in DistributionDB database

USE DistributionDB;
GO

-- Create DeliverySchedules table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliverySchedules' AND xtype='U')
BEGIN
    CREATE TABLE DeliverySchedules (
        ScheduleId INT IDENTITY(1,1) PRIMARY KEY,
        ScheduleRef NVARCHAR(50) UNIQUE NOT NULL,
        ScheduledDateTime DATETIME NOT NULL,
        VehicleId INT NULL,
        VehicleNo NVARCHAR(50) NULL,
        RouteId INT NULL,
        DriverName NVARCHAR(100) NULL,
        DriverContact NVARCHAR(20) NULL,
        Status NVARCHAR(20) DEFAULT 'Scheduled',
        DispatchDateTime DATETIME NULL,
        DeliveredDateTime DATETIME NULL,
        ReturnedDateTime DATETIME NULL,
        Remarks NVARCHAR(500) NULL,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NOT NULL,
        ModifiedDate DATETIME NULL,
        ModifiedBy INT NULL,
        RowVersion TIMESTAMP
    );
    
    PRINT 'DeliverySchedules table created successfully';
END
ELSE
BEGIN
    PRINT 'DeliverySchedules table already exists';
END
GO

-- Create DeliveryScheduleItems table (many-to-many relationship between schedules and challans)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliveryScheduleItems' AND xtype='U')
BEGIN
    CREATE TABLE DeliveryScheduleItems (
        ScheduleItemId INT IDENTITY(1,1) PRIMARY KEY,
        ScheduleId INT NOT NULL,
        ChallanId INT NOT NULL,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NOT NULL
    );
    
    PRINT 'DeliveryScheduleItems table created successfully';
END
ELSE
BEGIN
    PRINT 'DeliveryScheduleItems table already exists';
END
GO

-- Create DeliveryScheduleAttachments table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliveryScheduleAttachments' AND xtype='U')
BEGIN
    CREATE TABLE DeliveryScheduleAttachments (
        AttachmentId INT IDENTITY(1,1) PRIMARY KEY,
        ScheduleId INT NOT NULL,
        FileName NVARCHAR(255) NOT NULL,
        FilePath NVARCHAR(500) NOT NULL,
        FileType NVARCHAR(50) NULL,
        FileSize INT NULL,
        AttachmentType NVARCHAR(50) NULL, -- POD, MANIFEST, OTHER
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT NOT NULL
    );
    
    PRINT 'DeliveryScheduleAttachments table created successfully';
END
ELSE
BEGIN
    PRINT 'DeliveryScheduleAttachments table already exists';
END
GO

-- Create DeliveryScheduleHistory table for audit trail
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DeliveryScheduleHistory' AND xtype='U')
BEGIN
    CREATE TABLE DeliveryScheduleHistory (
        HistoryId INT IDENTITY(1,1) PRIMARY KEY,
        ScheduleId INT NOT NULL,
        ChangedAt DATETIME DEFAULT GETDATE(),
        ChangedBy INT NOT NULL,
        OldStatus NVARCHAR(20) NULL,
        NewStatus NVARCHAR(20) NOT NULL,
        Remarks NVARCHAR(500) NULL,
        DispatchDateTime DATETIME NULL,
        DriverName NVARCHAR(100) NULL
    );
    
    PRINT 'DeliveryScheduleHistory table created successfully';
END
ELSE
BEGIN
    PRINT 'DeliveryScheduleHistory table already exists';
END
GO

-- Add foreign key constraints
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliverySchedules_VehicleMaster')
BEGIN
    ALTER TABLE DeliverySchedules
    ADD CONSTRAINT FK_DeliverySchedules_VehicleMaster
    FOREIGN KEY (VehicleId) REFERENCES VehicleMaster(VehicleId);
    
    PRINT 'Foreign key FK_DeliverySchedules_VehicleMaster added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliverySchedules_VehicleMaster already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliverySchedules_RouteMaster')
BEGIN
    ALTER TABLE DeliverySchedules
    ADD CONSTRAINT FK_DeliverySchedules_RouteMaster
    FOREIGN KEY (RouteId) REFERENCES RouteMaster(RouteID);
    
    PRINT 'Foreign key FK_DeliverySchedules_RouteMaster added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliverySchedules_RouteMaster already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliveryScheduleItems_DeliverySchedules')
BEGIN
    ALTER TABLE DeliveryScheduleItems
    ADD CONSTRAINT FK_DeliveryScheduleItems_DeliverySchedules
    FOREIGN KEY (ScheduleId) REFERENCES DeliverySchedules(ScheduleId) ON DELETE CASCADE;
    
    PRINT 'Foreign key FK_DeliveryScheduleItems_DeliverySchedules added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliveryScheduleItems_DeliverySchedules already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliveryScheduleItems_DeliveryChallans')
BEGIN
    ALTER TABLE DeliveryScheduleItems
    ADD CONSTRAINT FK_DeliveryScheduleItems_DeliveryChallans
    FOREIGN KEY (ChallanId) REFERENCES DeliveryChallans(ChallanId);
    
    PRINT 'Foreign key FK_DeliveryScheduleItems_DeliveryChallans added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliveryScheduleItems_DeliveryChallans already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliveryScheduleAttachments_DeliverySchedules')
BEGIN
    ALTER TABLE DeliveryScheduleAttachments
    ADD CONSTRAINT FK_DeliveryScheduleAttachments_DeliverySchedules
    FOREIGN KEY (ScheduleId) REFERENCES DeliverySchedules(ScheduleId) ON DELETE CASCADE;
    
    PRINT 'Foreign key FK_DeliveryScheduleAttachments_DeliverySchedules added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliveryScheduleAttachments_DeliverySchedules already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE name = 'FK_DeliveryScheduleHistory_DeliverySchedules')
BEGIN
    ALTER TABLE DeliveryScheduleHistory
    ADD CONSTRAINT FK_DeliveryScheduleHistory_DeliverySchedules
    FOREIGN KEY (ScheduleId) REFERENCES DeliverySchedules(ScheduleId) ON DELETE CASCADE;
    
    PRINT 'Foreign key FK_DeliveryScheduleHistory_DeliverySchedules added successfully';
END
ELSE
BEGIN
    PRINT 'Foreign key FK_DeliveryScheduleHistory_DeliverySchedules already exists';
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliverySchedules_ScheduleRef')
BEGIN
    CREATE INDEX IX_DeliverySchedules_ScheduleRef ON DeliverySchedules(ScheduleRef);
    PRINT 'Index IX_DeliverySchedules_ScheduleRef created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliverySchedules_ScheduleRef already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliverySchedules_Status')
BEGIN
    CREATE INDEX IX_DeliverySchedules_Status ON DeliverySchedules(Status);
    PRINT 'Index IX_DeliverySchedules_Status created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliverySchedules_Status already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliverySchedules_ScheduledDateTime')
BEGIN
    CREATE INDEX IX_DeliverySchedules_ScheduledDateTime ON DeliverySchedules(ScheduledDateTime);
    PRINT 'Index IX_DeliverySchedules_ScheduledDateTime created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliverySchedules_ScheduledDateTime already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliveryScheduleItems_ScheduleId')
BEGIN
    CREATE INDEX IX_DeliveryScheduleItems_ScheduleId ON DeliveryScheduleItems(ScheduleId);
    PRINT 'Index IX_DeliveryScheduleItems_ScheduleId created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliveryScheduleItems_ScheduleId already exists';
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_DeliveryScheduleItems_ChallanId')
BEGIN
    CREATE INDEX IX_DeliveryScheduleItems_ChallanId ON DeliveryScheduleItems(ChallanId);
    PRINT 'Index IX_DeliveryScheduleItems_ChallanId created successfully';
END
ELSE
BEGIN
    PRINT 'Index IX_DeliveryScheduleItems_ChallanId already exists';
END
GO

-- Insert sample data for testing
IF NOT EXISTS (SELECT * FROM DeliverySchedules WHERE ScheduleRef = 'DS000001')
BEGIN
    INSERT INTO DeliverySchedules (
        ScheduleRef, ScheduledDateTime, VehicleId, VehicleNo, RouteId, 
        DriverName, DriverContact, Status, Remarks, CreatedBy
    ) VALUES (
        'DS000001', DATEADD(day, 1, GETDATE()), 1, 'TN-01-AB-1234', 1,
        'Rajesh Kumar', '9876543210', 'Scheduled', 'Sample delivery schedule', 1
    );
    
    DECLARE @ScheduleId INT = SCOPE_IDENTITY();
    
    -- Add sample challan to schedule
    INSERT INTO DeliveryScheduleItems (ScheduleId, ChallanId, CreatedBy)
    VALUES (@ScheduleId, 1, 1);
    
    -- Add sample history entry
    INSERT INTO DeliveryScheduleHistory (ScheduleId, ChangedBy, OldStatus, NewStatus, Remarks)
    VALUES (@ScheduleId, 1, NULL, 'Scheduled', 'Schedule created');
    
    PRINT 'Sample delivery schedule data inserted successfully';
END
ELSE
BEGIN
    PRINT 'Sample delivery schedule data already exists';
END
GO

PRINT 'Delivery Schedule tables setup completed successfully!';
