-- Add stored procedures for User Activity and Login History Reports
-- These procedures should be run on the DistributionDB database

USE DistributionDB;
GO

-- Create stored procedure for User Activity Report
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetUserActivityReport')
    DROP PROCEDURE sp_GetUserActivityReport;
GO

CREATE PROCEDURE sp_GetUserActivityReport
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @UserId INT = NULL,
    @RoleId INT = NULL,
    @ActivityType NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @StartDate IS NULL SET @StartDate = DATEADD(DAY, -30, GETDATE());
    IF @EndDate IS NULL SET @EndDate = GETDATE();
    
    SELECT 
        ual.[LogId],
        u.[Username],
        u.[Email],
        u.[FirstName] + ' ' + u.[LastName] AS [FullName],
        r.[RoleName],
        ual.[ActivityType],
        ual.[ActivityDescription],
        ual.[Module],
        ual.[IPAddress],
        ual.[ActivityDate],
        ual.[AdditionalData]
    FROM [dbo].[UserActivityLog] ual
    INNER JOIN [dbo].[Users] u ON ual.[UserId] = u.[UserId]
    INNER JOIN [dbo].[UserRoles] ur ON u.[UserId] = ur.[UserId]
    INNER JOIN [dbo].[Roles] r ON ur.[RoleId] = r.[RoleId]
    WHERE ual.[ActivityDate] BETWEEN @StartDate AND @EndDate
    AND (@UserId IS NULL OR ual.[UserId] = @UserId)
    AND (@RoleId IS NULL OR ur.[RoleId] = @RoleId)
    AND (@ActivityType IS NULL OR ual.[ActivityType] = @ActivityType)
    ORDER BY ual.[ActivityDate] DESC;
END
GO

-- Create stored procedure for Login History Report
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GetLoginHistoryReport')
    DROP PROCEDURE sp_GetLoginHistoryReport;
GO

CREATE PROCEDURE sp_GetLoginHistoryReport
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL,
    @UserId INT = NULL,
    @LoginStatus NVARCHAR(20) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @StartDate IS NULL SET @StartDate = DATEADD(DAY, -30, GETDATE());
    IF @EndDate IS NULL SET @EndDate = GETDATE();
    
    SELECT 
        lh.[LoginId],
        u.[Username],
        u.[Email],
        u.[FirstName] + ' ' + u.[LastName] AS [FullName],
        r.[RoleName],
        lh.[LoginDate],
        lh.[LogoutDate],
        lh.[IPAddress],
        lh.[UserAgent],
        lh.[LoginStatus],
        lh.[FailureReason],
        lh.[SessionDuration]
    FROM [dbo].[LoginHistory] lh
    INNER JOIN [dbo].[Users] u ON lh.[UserId] = u.[UserId]
    INNER JOIN [dbo].[UserRoles] ur ON u.[UserId] = ur.[UserId]
    INNER JOIN [dbo].[Roles] r ON ur.[RoleId] = r.[RoleId]
    WHERE lh.[LoginDate] BETWEEN @StartDate AND @EndDate
    AND (@UserId IS NULL OR lh.[UserId] = @UserId)
    AND (@LoginStatus IS NULL OR lh.[LoginStatus] = @LoginStatus)
    ORDER BY lh.[LoginDate] DESC;
END
GO

-- Create stored procedure for logging user activity (if not exists)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_LogUserActivity')
BEGIN
    CREATE PROCEDURE sp_LogUserActivity
        @UserId INT,
        @ActivityType NVARCHAR(50),
        @ActivityDescription NVARCHAR(500) = NULL,
        @Module NVARCHAR(100) = NULL,
        @IPAddress NVARCHAR(45) = NULL,
        @UserAgent NVARCHAR(500) = NULL,
        @AdditionalData NVARCHAR(MAX) = NULL
    AS
    BEGIN
        SET NOCOUNT ON;
        
        INSERT INTO [dbo].[UserActivityLog] 
        ([UserId], [ActivityType], [ActivityDescription], [Module], [IPAddress], [UserAgent], [ActivityDate], [AdditionalData])
        VALUES (@UserId, @ActivityType, @ActivityDescription, @Module, @IPAddress, @UserAgent, GETDATE(), @AdditionalData);
        
        SELECT SCOPE_IDENTITY() AS LogId;
    END
END
GO

PRINT 'User Activity and Login History Report stored procedures created successfully!';
