@echo off
REM =============================================
REM Chart of Accounts Seed Data Execution Script
REM Distribution Software Database Setup
REM =============================================

echo.
echo ================================================
echo   Chart of Accounts Seed Data Setup
echo   Distribution Software Database
echo ================================================
echo.

REM Set SQL Server connection parameters
REM Modify these values according to your SQL Server setup
set SERVER_NAME=localhost
set DATABASE_NAME=DistributionDB
set SQL_USER=sa
set SQL_PASSWORD=YourPassword123

REM Alternative: Use Windows Authentication (uncomment the line below and comment out the SQL_USER/SQL_PASSWORD lines)
REM set USE_WINDOWS_AUTH=1

echo Checking SQL Server connection...
echo Server: %SERVER_NAME%
echo Database: %DATABASE_NAME%
echo.

REM Check if sqlcmd is available
sqlcmd -? >nul 2>&1
if %errorlevel% neq 0 (
    echo ERROR: sqlcmd is not available. Please install SQL Server Command Line Utilities.
    echo Download from: https://docs.microsoft.com/en-us/sql/tools/sqlcmd-utility
    pause
    exit /b 1
)

echo sqlcmd found. Proceeding with database setup...
echo.

REM Create the database if it doesn't exist
echo Creating database if it doesn't exist...
if defined USE_WINDOWS_AUTH (
    sqlcmd -S %SERVER_NAME% -E -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '%DATABASE_NAME%') CREATE DATABASE [%DATABASE_NAME%]"
) else (
    sqlcmd -S %SERVER_NAME% -U %SQL_USER% -P %SQL_PASSWORD% -Q "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '%DATABASE_NAME%') CREATE DATABASE [%DATABASE_NAME%]"
)

if %errorlevel% neq 0 (
    echo ERROR: Failed to create database. Please check your connection parameters.
    pause
    exit /b 1
)

echo Database created/verified successfully.
echo.

REM Run the table creation script
echo Creating Chart of Accounts table...
if defined USE_WINDOWS_AUTH (
    sqlcmd -S %SERVER_NAME% -E -d %DATABASE_NAME% -i "ChartOfAccounts_Simple.sql"
) else (
    sqlcmd -S %SERVER_NAME% -U %SQL_USER% -P %SQL_PASSWORD% -d %DATABASE_NAME% -i "ChartOfAccounts_Simple.sql"
)

if %errorlevel% neq 0 (
    echo ERROR: Failed to create Chart of Accounts table.
    pause
    exit /b 1
)

echo Table created successfully.
echo.

REM Run the seed data script
echo Loading seed data...
if defined USE_WINDOWS_AUTH (
    sqlcmd -S %SERVER_NAME% -E -d %DATABASE_NAME% -i "ChartOfAccounts_SeedData.sql"
) else (
    sqlcmd -S %SERVER_NAME% -U %SQL_USER% -P %SQL_PASSWORD% -d %DATABASE_NAME% -i "ChartOfAccounts_SeedData.sql"
)

if %errorlevel% neq 0 (
    echo ERROR: Failed to load seed data.
    pause
    exit /b 1
)

echo Seed data loaded successfully.
echo.

REM Verify the data
echo Verifying Chart of Accounts data...
if defined USE_WINDOWS_AUTH (
    sqlcmd -S %SERVER_NAME% -E -d %DATABASE_NAME% -Q "SELECT COUNT(*) as 'Total Accounts' FROM ChartOfAccounts; SELECT AccountCode, AccountName, AccountType, AccountLevel FROM ChartOfAccounts ORDER BY AccountCode;"
) else (
    sqlcmd -S %SERVER_NAME% -U %SQL_USER% -P %SQL_PASSWORD% -d %DATABASE_NAME% -Q "SELECT COUNT(*) as 'Total Accounts' FROM ChartOfAccounts; SELECT AccountCode, AccountName, AccountType, AccountLevel FROM ChartOfAccounts ORDER BY AccountCode;"
)

echo.
echo ================================================
echo   Chart of Accounts Setup Completed Successfully!
echo ================================================
echo.
echo You can now:
echo 1. Open your Distribution Software application
echo 2. Navigate to Dashboard ^> Accounting ^> Chart of Accounts
echo 3. View the hierarchical account structure
echo.
pause
