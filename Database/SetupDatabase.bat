@echo off
echo =============================================
echo Distribution Software Database Setup
echo =============================================
echo.

echo Creating database tables...
sqlcmd -S localhost\SQLEXPRESS -E -i "CreateTables.sql"
if %errorlevel% neq 0 (
    echo Error creating tables!
    pause
    exit /b 1
)

echo.
echo Seeding database with sample data...
sqlcmd -S localhost\SQLEXPRESS -E -i "SeedData.sql"
if %errorlevel% neq 0 (
    echo Error seeding data!
    pause
    exit /b 1
)

echo.
echo =============================================
echo Database setup completed successfully!
echo =============================================
pause
