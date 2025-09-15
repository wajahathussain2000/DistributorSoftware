# =============================================
# Chart of Accounts Seed Data Execution Script
# Distribution Software Database Setup
# PowerShell Version
# =============================================

param(
    [string]$ServerName = "localhost",
    [string]$DatabaseName = "DistributionDB",
    [string]$SqlUser = "",
    [string]$SqlPassword = "",
    [switch]$UseWindowsAuth = $true,
    [switch]$Force = $false
)

# Set console colors
$Host.UI.RawUI.ForegroundColor = "White"

Write-Host ""
Write-Host "================================================" -ForegroundColor Cyan
Write-Host "  Chart of Accounts Seed Data Setup" -ForegroundColor Cyan
Write-Host "  Distribution Software Database" -ForegroundColor Cyan
Write-Host "  PowerShell Version" -ForegroundColor Cyan
Write-Host "================================================" -ForegroundColor Cyan
Write-Host ""

# Check if SQL Server module is available
try {
    Import-Module SqlServer -ErrorAction Stop
    Write-Host "SQL Server PowerShell module found." -ForegroundColor Green
} catch {
    Write-Host "SQL Server PowerShell module not found. Using sqlcmd instead." -ForegroundColor Yellow
    $UseSqlCmd = $true
}

# Build connection string
if ($UseWindowsAuth) {
    $ConnectionString = "Server=$ServerName;Database=$DatabaseName;Integrated Security=true;TrustServerCertificate=true;"
    Write-Host "Using Windows Authentication" -ForegroundColor Green
} else {
    if ([string]::IsNullOrEmpty($SqlUser) -or [string]::IsNullOrEmpty($SqlPassword)) {
        Write-Host "ERROR: SQL User and Password are required when not using Windows Authentication." -ForegroundColor Red
        Write-Host "Usage: .\RunSeedData.ps1 -ServerName 'localhost' -DatabaseName 'DistributionDB' -SqlUser 'sa' -SqlPassword 'YourPassword' -UseWindowsAuth:`$false" -ForegroundColor Yellow
        exit 1
    }
    $ConnectionString = "Server=$ServerName;Database=$DatabaseName;User Id=$SqlUser;Password=$SqlPassword;TrustServerCertificate=true;"
    Write-Host "Using SQL Server Authentication" -ForegroundColor Green
}

Write-Host "Server: $ServerName" -ForegroundColor White
Write-Host "Database: $DatabaseName" -ForegroundColor White
Write-Host ""

# Function to execute SQL commands
function Execute-SqlCommand {
    param(
        [string]$Query,
        [string]$ScriptFile = $null
    )
    
    try {
        if ($UseSqlCmd) {
            # Use sqlcmd
            $cmd = "sqlcmd"
            $args = @("-S", $ServerName, "-d", $DatabaseName)
            
            if ($UseWindowsAuth) {
                $args += "-E"
            } else {
                $args += @("-U", $SqlUser, "-P", $SqlPassword)
            }
            
            if ($ScriptFile) {
                $args += @("-i", $ScriptFile)
            } else {
                $args += @("-Q", $Query)
            }
            
            $result = & $cmd $args
            if ($LASTEXITCODE -ne 0) {
                throw "sqlcmd execution failed with exit code $LASTEXITCODE"
            }
            return $result
        } else {
            # Use PowerShell SqlServer module
            if ($ScriptFile) {
                $sql = Get-Content $ScriptFile -Raw
                Invoke-Sqlcmd -ConnectionString $ConnectionString -Query $sql
            } else {
                Invoke-Sqlcmd -ConnectionString $ConnectionString -Query $Query
            }
        }
    } catch {
        Write-Host "ERROR: Failed to execute SQL command: $($_.Exception.Message)" -ForegroundColor Red
        throw
    }
}

# Check if database exists and create if needed
Write-Host "Checking database existence..." -ForegroundColor Yellow
try {
    $dbExists = Execute-SqlCommand "SELECT COUNT(*) FROM sys.databases WHERE name = '$DatabaseName'"
    if ($dbExists -eq 0) {
        Write-Host "Creating database: $DatabaseName" -ForegroundColor Yellow
        Execute-SqlCommand "CREATE DATABASE [$DatabaseName]"
        Write-Host "Database created successfully." -ForegroundColor Green
    } else {
        Write-Host "Database already exists." -ForegroundColor Green
    }
} catch {
    Write-Host "ERROR: Failed to create/verify database." -ForegroundColor Red
    exit 1
}

# Check if ChartOfAccounts table exists
Write-Host "Checking Chart of Accounts table..." -ForegroundColor Yellow
try {
    $tableExists = Execute-SqlCommand "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'ChartOfAccounts'"
    if ($tableExists -eq 0) {
        Write-Host "Creating Chart of Accounts table..." -ForegroundColor Yellow
        Execute-SqlCommand -ScriptFile "ChartOfAccounts_Simple.sql"
        Write-Host "Table created successfully." -ForegroundColor Green
    } else {
        Write-Host "Chart of Accounts table already exists." -ForegroundColor Green
        if (-not $Force) {
            $response = Read-Host "Do you want to clear existing data and reload? (y/N)"
            if ($response -ne "y" -and $response -ne "Y") {
                Write-Host "Skipping seed data loading." -ForegroundColor Yellow
                exit 0
            }
        }
    }
} catch {
    Write-Host "ERROR: Failed to create Chart of Accounts table." -ForegroundColor Red
    exit 1
}

# Load seed data
Write-Host "Loading seed data..." -ForegroundColor Yellow
try {
    Execute-SqlCommand -ScriptFile "ChartOfAccounts_SeedData.sql"
    Write-Host "Seed data loaded successfully." -ForegroundColor Green
} catch {
    Write-Host "ERROR: Failed to load seed data." -ForegroundColor Red
    exit 1
}

# Verify the data
Write-Host "Verifying Chart of Accounts data..." -ForegroundColor Yellow
try {
    $accountCount = Execute-SqlCommand "SELECT COUNT(*) as TotalAccounts FROM ChartOfAccounts"
    Write-Host "Total accounts created: $accountCount" -ForegroundColor Green
    
    Write-Host ""
    Write-Host "Account Structure:" -ForegroundColor Cyan
    Execute-SqlCommand "SELECT AccountCode, AccountName, AccountType, AccountLevel, CASE WHEN ParentAccountId IS NULL THEN 'ROOT' ELSE CAST(ParentAccountId AS VARCHAR(10)) END AS ParentAccount FROM ChartOfAccounts ORDER BY AccountCode"
    
} catch {
    Write-Host "ERROR: Failed to verify data." -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "================================================" -ForegroundColor Green
Write-Host "  Chart of Accounts Setup Completed Successfully!" -ForegroundColor Green
Write-Host "================================================" -ForegroundColor Green
Write-Host ""
Write-Host "You can now:" -ForegroundColor White
Write-Host "1. Open your Distribution Software application" -ForegroundColor White
Write-Host "2. Navigate to Dashboard > Accounting > Chart of Accounts" -ForegroundColor White
Write-Host "3. View the hierarchical account structure" -ForegroundColor White
Write-Host ""

# Usage examples
Write-Host "Usage Examples:" -ForegroundColor Cyan
Write-Host "Windows Auth: .\RunSeedData.ps1" -ForegroundColor Yellow
Write-Host "SQL Auth: .\RunSeedData.ps1 -ServerName 'localhost' -DatabaseName 'DistributionDB' -SqlUser 'sa' -SqlPassword 'YourPassword' -UseWindowsAuth:`$false" -ForegroundColor Yellow
Write-Host "Force reload: .\RunSeedData.ps1 -Force" -ForegroundColor Yellow
Write-Host ""

Read-Host "Press Enter to continue"
