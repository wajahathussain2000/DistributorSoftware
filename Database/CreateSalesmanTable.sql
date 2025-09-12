-- Create Salesman Table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Salesman' AND xtype='U')
BEGIN
    CREATE TABLE Salesman (
        SalesmanId INT IDENTITY(1,1) PRIMARY KEY,
        SalesmanCode NVARCHAR(20) NOT NULL UNIQUE,
        SalesmanName NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100),
        Phone NVARCHAR(20),
        Address NVARCHAR(500),
        Territory NVARCHAR(100),
        CommissionRate DECIMAL(5,2) DEFAULT 0.00,
        IsActive BIT DEFAULT 1,
        CreatedDate DATETIME DEFAULT GETDATE(),
        CreatedBy INT,
        ModifiedDate DATETIME,
        ModifiedBy INT
    );
    
    PRINT 'Salesman table created successfully.';
END
ELSE
BEGIN
    PRINT 'Salesman table already exists.';
END
GO

-- Insert Sample Salesman Data
IF EXISTS (SELECT * FROM sysobjects WHERE name='Salesman' AND xtype='U')
BEGIN
    -- Check if data already exists
    IF NOT EXISTS (SELECT 1 FROM Salesman)
    BEGIN
        INSERT INTO Salesman (SalesmanCode, SalesmanName, Email, Phone, Address, Territory, CommissionRate, IsActive, CreatedBy)
        VALUES 
        ('SM001', 'John Smith', 'john.smith@company.com', '+1-555-0101', '123 Main St, New York, NY', 'North Region', 5.50, 1, 1),
        ('SM002', 'Sarah Johnson', 'sarah.johnson@company.com', '+1-555-0102', '456 Oak Ave, Los Angeles, CA', 'West Region', 6.00, 1, 1),
        ('SM003', 'Michael Brown', 'michael.brown@company.com', '+1-555-0103', '789 Pine St, Chicago, IL', 'Central Region', 5.75, 1, 1),
        ('SM004', 'Emily Davis', 'emily.davis@company.com', '+1-555-0104', '321 Elm St, Houston, TX', 'South Region', 5.25, 1, 1),
        ('SM005', 'David Wilson', 'david.wilson@company.com', '+1-555-0105', '654 Maple Dr, Phoenix, AZ', 'Southwest Region', 6.25, 1, 1),
        ('SM006', 'Lisa Anderson', 'lisa.anderson@company.com', '+1-555-0106', '987 Cedar Ln, Philadelphia, PA', 'Northeast Region', 5.00, 1, 1),
        ('SM007', 'Robert Taylor', 'robert.taylor@company.com', '+1-555-0107', '147 Birch St, San Antonio, TX', 'South Region', 5.80, 1, 1),
        ('SM008', 'Jennifer Martinez', 'jennifer.martinez@company.com', '+1-555-0108', '258 Spruce Ave, San Diego, CA', 'West Region', 6.50, 1, 1),
        ('SM009', 'Christopher Garcia', 'christopher.garcia@company.com', '+1-555-0109', '369 Willow Rd, Dallas, TX', 'Central Region', 5.30, 1, 1),
        ('SM010', 'Amanda Rodriguez', 'amanda.rodriguez@company.com', '+1-555-0110', '741 Poplar St, San Jose, CA', 'West Region', 6.00, 1, 1);
        
        PRINT 'Sample salesman data inserted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Salesman data already exists.';
    END
END
GO

-- Create Indexes for better performance
IF EXISTS (SELECT * FROM sysobjects WHERE name='Salesman' AND xtype='U')
BEGIN
    -- Index on SalesmanCode for quick lookups
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Salesman_SalesmanCode' AND object_id = OBJECT_ID('Salesman'))
    BEGIN
        CREATE INDEX IX_Salesman_SalesmanCode ON Salesman(SalesmanCode);
        PRINT 'Index IX_Salesman_SalesmanCode created.';
    END
    
    -- Index on Territory for filtering
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Salesman_Territory' AND object_id = OBJECT_ID('Salesman'))
    BEGIN
        CREATE INDEX IX_Salesman_Territory ON Salesman(Territory);
        PRINT 'Index IX_Salesman_Territory created.';
    END
    
    -- Index on IsActive for filtering active salesmen
    IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Salesman_IsActive' AND object_id = OBJECT_ID('Salesman'))
    BEGIN
        CREATE INDEX IX_Salesman_IsActive ON Salesman(IsActive);
        PRINT 'Index IX_Salesman_IsActive created.';
    END
END
GO

PRINT 'Salesman table setup completed successfully!';
