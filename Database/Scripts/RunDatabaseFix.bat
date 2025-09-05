@echo off
echo ========================================
echo Database Fix Script for Distribution Software
echo ========================================
echo.

echo Please run the following SQL script in SQL Server Management Studio:
echo.
echo File: Database\Scripts\FixDatabaseTables.sql
echo.
echo This will fix the missing columns in the Units table.
echo.

echo Press any key to open the SQL file...
pause > nul

start notepad "%~dp0FixDatabaseTables.sql"

echo.
echo After running the SQL script, the forms should work properly.
echo Press any key to exit...
pause > nul
