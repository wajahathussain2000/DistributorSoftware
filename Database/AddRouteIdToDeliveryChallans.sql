-- Add RouteId column to DeliveryChallans table
-- This script adds route selection capability to delivery challans

-- Check if RouteId column already exists
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_NAME = 'DeliveryChallans' AND COLUMN_NAME = 'RouteId')
BEGIN
    -- Add RouteId column
    ALTER TABLE DeliveryChallans 
    ADD RouteId INT NULL;

    -- Add foreign key constraint to RouteMaster table
    ALTER TABLE DeliveryChallans 
    ADD CONSTRAINT FK_DeliveryChallans_RouteMaster 
    FOREIGN KEY (RouteId) REFERENCES RouteMaster(RouteID);

    -- Add index for better performance
    CREATE INDEX IX_DeliveryChallans_RouteId ON DeliveryChallans(RouteId);

    PRINT 'RouteId column added to DeliveryChallans table successfully';
END
ELSE
BEGIN
    PRINT 'RouteId column already exists in DeliveryChallans table';
END

-- Update existing delivery challans to have NULL RouteId (optional)
-- UPDATE DeliveryChallans SET RouteId = NULL WHERE RouteId IS NULL;

