-- Create Delivery Schedule Stored Procedures
-- Run this script in DistributionDB database

USE DistributionDB;
GO

-- Stored procedure to create a new delivery schedule
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CreateDeliverySchedule')
    DROP PROCEDURE sp_CreateDeliverySchedule;
GO

CREATE PROCEDURE sp_CreateDeliverySchedule
    @ScheduleRef NVARCHAR(50),
    @ScheduledDateTime DATETIME,
    @VehicleId INT = NULL,
    @VehicleNo NVARCHAR(50) = NULL,
    @RouteId INT = NULL,
    @DriverName NVARCHAR(100) = NULL,
    @DriverContact NVARCHAR(20) = NULL,
    @Remarks NVARCHAR(500) = NULL,
    @CreatedBy INT,
    @ChallanIds NVARCHAR(MAX) = NULL, -- Comma-separated list of challan IDs
    @ScheduleId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Insert the delivery schedule
        INSERT INTO DeliverySchedules (
            ScheduleRef, ScheduledDateTime, VehicleId, VehicleNo, RouteId,
            DriverName, DriverContact, Remarks, CreatedBy
        )
        VALUES (
            @ScheduleRef, @ScheduledDateTime, @VehicleId, @VehicleNo, @RouteId,
            @DriverName, @DriverContact, @Remarks, @CreatedBy
        );
        
        SET @ScheduleId = SCOPE_IDENTITY();
        
        -- Add challans to schedule if provided
        IF @ChallanIds IS NOT NULL AND LEN(@ChallanIds) > 0
        BEGIN
            DECLARE @ChallanId INT;
            DECLARE @Pos INT = 1;
            DECLARE @NextPos INT;
            
            WHILE @Pos <= LEN(@ChallanIds)
            BEGIN
                SET @NextPos = CHARINDEX(',', @ChallanIds, @Pos);
                IF @NextPos = 0
                    SET @NextPos = LEN(@ChallanIds) + 1;
                
                SET @ChallanId = CAST(SUBSTRING(@ChallanIds, @Pos, @NextPos - @Pos) AS INT);
                
                -- Check if challan is not already scheduled
                IF NOT EXISTS (
                    SELECT 1 FROM DeliveryScheduleItems dsi
                    INNER JOIN DeliverySchedules ds ON dsi.ScheduleId = ds.ScheduleId
                    WHERE dsi.ChallanId = @ChallanId 
                    AND ds.Status IN ('Scheduled', 'Dispatched')
                )
                BEGIN
                    INSERT INTO DeliveryScheduleItems (ScheduleId, ChallanId, CreatedBy)
                    VALUES (@ScheduleId, @ChallanId, @CreatedBy);
                END
                
                SET @Pos = @NextPos + 1;
            END
        END
        
        -- Add history entry
        INSERT INTO DeliveryScheduleHistory (ScheduleId, ChangedBy, OldStatus, NewStatus, Remarks)
        VALUES (@ScheduleId, @CreatedBy, NULL, 'Scheduled', 'Schedule created');
        
        COMMIT TRANSACTION;
        
        SELECT @ScheduleId AS ScheduleId;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- Stored procedure to update delivery schedule status
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateScheduleStatus')
    DROP PROCEDURE sp_UpdateScheduleStatus;
GO

CREATE PROCEDURE sp_UpdateScheduleStatus
    @ScheduleId INT,
    @NewStatus NVARCHAR(20),
    @PerformedBy INT,
    @DispatchDateTime DATETIME = NULL,
    @DriverName NVARCHAR(100) = NULL,
    @Remarks NVARCHAR(500) = NULL,
    @RowVersion TIMESTAMP = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Check if schedule exists and get current status
        DECLARE @CurrentStatus NVARCHAR(20);
        DECLARE @CurrentRowVersion TIMESTAMP;
        
        SELECT @CurrentStatus = Status, @CurrentRowVersion = RowVersion
        FROM DeliverySchedules
        WHERE ScheduleId = @ScheduleId;
        
        IF @CurrentStatus IS NULL
        BEGIN
            RAISERROR('Delivery schedule not found.', 16, 1);
            RETURN;
        END
        
        -- Check row version for concurrency
        IF @RowVersion IS NOT NULL AND @CurrentRowVersion != @RowVersion
        BEGIN
            RAISERROR('Record has been modified by another user. Please reload and try again.', 16, 1);
            RETURN;
        END
        
        -- Validate status transition
        IF @CurrentStatus = 'Scheduled' AND @NewStatus NOT IN ('Dispatched', 'Cancelled')
        BEGIN
            RAISERROR('Invalid status transition from Scheduled to %s', 16, 1, @NewStatus);
            RETURN;
        END
        
        IF @CurrentStatus = 'Dispatched' AND @NewStatus NOT IN ('Delivered', 'Returned')
        BEGIN
            RAISERROR('Invalid status transition from Dispatched to %s', 16, 1, @NewStatus);
            RETURN;
        END
        
        IF @CurrentStatus IN ('Delivered', 'Returned') AND @NewStatus NOT IN ('Scheduled')
        BEGIN
            RAISERROR('Invalid status transition from %s to %s', 16, 1, @CurrentStatus, @NewStatus);
            RETURN;
        END
        
        -- Update the schedule
        UPDATE DeliverySchedules
        SET Status = @NewStatus,
            ModifiedDate = GETDATE(),
            ModifiedBy = @PerformedBy,
            DispatchDateTime = CASE 
                WHEN @NewStatus = 'Dispatched' AND @DispatchDateTime IS NOT NULL 
                THEN @DispatchDateTime 
                ELSE DispatchDateTime 
            END,
            DeliveredDateTime = CASE 
                WHEN @NewStatus = 'Delivered' 
                THEN GETDATE() 
                ELSE DeliveredDateTime 
            END,
            ReturnedDateTime = CASE 
                WHEN @NewStatus = 'Returned' 
                THEN GETDATE() 
                ELSE ReturnedDateTime 
            END,
            DriverName = CASE 
                WHEN @DriverName IS NOT NULL 
                THEN @DriverName 
                ELSE DriverName 
            END,
            Remarks = CASE 
                WHEN @Remarks IS NOT NULL 
                THEN @Remarks 
                ELSE Remarks 
            END
        WHERE ScheduleId = @ScheduleId;
        
        -- Add history entry
        INSERT INTO DeliveryScheduleHistory (
            ScheduleId, ChangedBy, OldStatus, NewStatus, Remarks, 
            DispatchDateTime, DriverName
        )
        VALUES (
            @ScheduleId, @PerformedBy, @CurrentStatus, @NewStatus, @Remarks,
            @DispatchDateTime, @DriverName
        );
        
        COMMIT TRANSACTION;
        
        SELECT 1 AS Success;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- Stored procedure to get delivery schedules with filters
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetDeliverySchedules')
    DROP PROCEDURE sp_GetDeliverySchedules;
GO

CREATE PROCEDURE sp_GetDeliverySchedules
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @Status NVARCHAR(20) = NULL,
    @VehicleId INT = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 50
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;
    
    SELECT 
        ds.ScheduleId,
        ds.ScheduleRef,
        ds.ScheduledDateTime,
        ds.VehicleId,
        ds.VehicleNo,
        vm.VehicleType,
        ds.RouteId,
        rm.RouteName,
        ds.DriverName,
        ds.DriverContact,
        ds.Status,
        ds.DispatchDateTime,
        ds.DeliveredDateTime,
        ds.ReturnedDateTime,
        ds.Remarks,
        ds.CreatedDate,
        ds.CreatedBy,
        ds.ModifiedDate,
        ds.ModifiedBy,
        ds.RowVersion,
        COUNT(*) OVER() AS TotalRecords
    FROM DeliverySchedules ds
    LEFT JOIN VehicleMaster vm ON ds.VehicleId = vm.VehicleId
    LEFT JOIN RouteMaster rm ON ds.RouteId = rm.RouteID
    WHERE 
        (@StartDate IS NULL OR ds.ScheduledDateTime >= @StartDate)
        AND (@EndDate IS NULL OR ds.ScheduledDateTime <= @EndDate)
        AND (@Status IS NULL OR ds.Status = @Status)
        AND (@VehicleId IS NULL OR ds.VehicleId = @VehicleId)
    ORDER BY ds.ScheduledDateTime DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO

-- Stored procedure to get available challans for scheduling
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetAvailableChallans')
    DROP PROCEDURE sp_GetAvailableChallans;
GO

CREATE PROCEDURE sp_GetAvailableChallans
    @ExcludeScheduleId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        dc.ChallanId,
        dc.ChallanNo,
        dc.CustomerName,
        dc.CustomerAddress,
        dc.ChallanDate,
        dc.Status,
        COUNT(dci.ChallanItemId) AS ItemCount,
        SUM(dci.TotalAmount) AS TotalAmount
    FROM DeliveryChallans dc
    LEFT JOIN DeliveryChallanItems dci ON dc.ChallanId = dci.ChallanId
    WHERE dc.Status IN ('Open', 'ReadyForDispatch')
        AND (@ExcludeScheduleId IS NULL OR dc.ChallanId NOT IN (
            SELECT ChallanId FROM DeliveryScheduleItems dsi
            INNER JOIN DeliverySchedules ds ON dsi.ScheduleId = ds.ScheduleId
            WHERE ds.ScheduleId != @ExcludeScheduleId 
            AND ds.Status IN ('Scheduled', 'Dispatched')
        ))
    GROUP BY dc.ChallanId, dc.ChallanNo, dc.CustomerName, dc.CustomerAddress, 
             dc.ChallanDate, dc.Status
    ORDER BY dc.ChallanDate DESC;
END
GO

-- Stored procedure to get schedule details with challans
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetScheduleDetails')
    DROP PROCEDURE sp_GetScheduleDetails;
GO

CREATE PROCEDURE sp_GetScheduleDetails
    @ScheduleId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Get schedule details
    SELECT 
        ds.ScheduleId,
        ds.ScheduleRef,
        ds.ScheduledDateTime,
        ds.VehicleId,
        ds.VehicleNo,
        vm.VehicleType,
        ds.RouteId,
        rm.RouteName,
        ds.DriverName,
        ds.DriverContact,
        ds.Status,
        ds.DispatchDateTime,
        ds.DeliveredDateTime,
        ds.ReturnedDateTime,
        ds.Remarks,
        ds.CreatedDate,
        ds.CreatedBy,
        ds.ModifiedDate,
        ds.ModifiedBy,
        ds.RowVersion
    FROM DeliverySchedules ds
    LEFT JOIN VehicleMaster vm ON ds.VehicleId = vm.VehicleId
    LEFT JOIN RouteMaster rm ON ds.RouteId = rm.RouteID
    WHERE ds.ScheduleId = @ScheduleId;
    
    -- Get schedule challans
    SELECT 
        dc.ChallanId,
        dc.ChallanNo,
        dc.CustomerName,
        dc.CustomerAddress,
        dc.ChallanDate,
        dc.Status AS ChallanStatus
    FROM DeliveryScheduleItems dsi
    INNER JOIN DeliveryChallans dc ON dsi.ChallanId = dc.ChallanId
    WHERE dsi.ScheduleId = @ScheduleId
    ORDER BY dc.ChallanDate;
    
    -- Get schedule history
    SELECT 
        dsh.HistoryId,
        dsh.ChangedAt,
        dsh.ChangedBy,
        u.Username,
        dsh.OldStatus,
        dsh.NewStatus,
        dsh.Remarks,
        dsh.DispatchDateTime,
        dsh.DriverName
    FROM DeliveryScheduleHistory dsh
    LEFT JOIN Users u ON dsh.ChangedBy = u.UserId
    WHERE dsh.ScheduleId = @ScheduleId
    ORDER BY dsh.ChangedAt DESC;
    
    -- Get schedule attachments
    SELECT 
        dsa.AttachmentId,
        dsa.FileName,
        dsa.FilePath,
        dsa.FileType,
        dsa.FileSize,
        dsa.AttachmentType,
        dsa.CreatedDate,
        dsa.CreatedBy
    FROM DeliveryScheduleAttachments dsa
    WHERE dsa.ScheduleId = @ScheduleId
    ORDER BY dsa.CreatedDate DESC;
END
GO

PRINT 'Delivery Schedule stored procedures created successfully!';
