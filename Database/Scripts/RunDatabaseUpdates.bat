@echo off
echo ========================================
echo Database Update Script for Distribution Software
echo ========================================
echo.

echo Running PowerShell script to update database...
powershell -ExecutionPolicy Bypass -File "%~dp0RunDatabaseUpdates.ps1"

echo.
echo Database update process completed!
echo Press any key to exit...
pause > nul
