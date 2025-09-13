using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class SalesmanTargetRepository : ISalesmanTargetRepository
    {
        private readonly string _connectionString;

        public SalesmanTargetRepository()
        {
            _connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
        }

        public SalesmanTargetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateSalesmanTarget(SalesmanTarget target)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Create tables if they don't exist
                    CreateSalesmanTargetTablesIfNotExists(connection);
                    
                    var insertQuery = @"
                        INSERT INTO SalesmanTarget (
                            SalesmanId, TargetType, TargetPeriodStart, TargetPeriodEnd, TargetPeriodName,
                            RevenueTarget, UnitTarget, CustomerTarget, InvoiceTarget, ProductCategory,
                            CategoryRevenueTarget, CategoryUnitTarget, Status, CreatedBy, CreatedDate
                        )
                        VALUES (
                            @SalesmanId, @TargetType, @TargetPeriodStart, @TargetPeriodEnd, @TargetPeriodName,
                            @RevenueTarget, @UnitTarget, @CustomerTarget, @InvoiceTarget, @ProductCategory,
                            @CategoryRevenueTarget, @CategoryUnitTarget, @Status, @CreatedBy, GETDATE()
                        );
                        SELECT SCOPE_IDENTITY();";
                    
                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanId", target.SalesmanId);
                        command.Parameters.AddWithValue("@TargetType", target.TargetType);
                        command.Parameters.AddWithValue("@TargetPeriodStart", target.TargetPeriodStart);
                        command.Parameters.AddWithValue("@TargetPeriodEnd", target.TargetPeriodEnd);
                        command.Parameters.AddWithValue("@TargetPeriodName", target.TargetPeriodName);
                        command.Parameters.AddWithValue("@RevenueTarget", target.RevenueTarget);
                        command.Parameters.AddWithValue("@UnitTarget", target.UnitTarget);
                        command.Parameters.AddWithValue("@CustomerTarget", target.CustomerTarget);
                        command.Parameters.AddWithValue("@InvoiceTarget", target.InvoiceTarget);
                        command.Parameters.AddWithValue("@ProductCategory", (object)target.ProductCategory ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryRevenueTarget", target.CategoryRevenueTarget);
                        command.Parameters.AddWithValue("@CategoryUnitTarget", target.CategoryUnitTarget);
                        command.Parameters.AddWithValue("@Status", target.Status);
                        command.Parameters.AddWithValue("@CreatedBy", target.CreatedBy);
                        
                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating salesman target: {ex.Message}", ex);
            }
        }

        public bool UpdateSalesmanTarget(SalesmanTarget target)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var updateQuery = @"
                        UPDATE SalesmanTarget SET
                            TargetType = @TargetType,
                            TargetPeriodStart = @TargetPeriodStart,
                            TargetPeriodEnd = @TargetPeriodEnd,
                            TargetPeriodName = @TargetPeriodName,
                            RevenueTarget = @RevenueTarget,
                            UnitTarget = @UnitTarget,
                            CustomerTarget = @CustomerTarget,
                            InvoiceTarget = @InvoiceTarget,
                            ProductCategory = @ProductCategory,
                            CategoryRevenueTarget = @CategoryRevenueTarget,
                            CategoryUnitTarget = @CategoryUnitTarget,
                            Status = @Status,
                            PerformanceRating = @PerformanceRating,
                            ManagerComments = @ManagerComments,
                            SalesmanComments = @SalesmanComments,
                            MarketConditions = @MarketConditions,
                            Challenges = @Challenges,
                            BonusAmount = @BonusAmount,
                            CommissionAmount = @CommissionAmount,
                            IsBonusEligible = @IsBonusEligible,
                            IsCommissionEligible = @IsCommissionEligible,
                            ModifiedBy = @ModifiedBy,
                            ModifiedDate = GETDATE()
                        WHERE TargetId = @TargetId";
                    
                    using (var command = new SqlCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TargetId", target.TargetId);
                        command.Parameters.AddWithValue("@TargetType", target.TargetType);
                        command.Parameters.AddWithValue("@TargetPeriodStart", target.TargetPeriodStart);
                        command.Parameters.AddWithValue("@TargetPeriodEnd", target.TargetPeriodEnd);
                        command.Parameters.AddWithValue("@TargetPeriodName", target.TargetPeriodName);
                        command.Parameters.AddWithValue("@RevenueTarget", target.RevenueTarget);
                        command.Parameters.AddWithValue("@UnitTarget", target.UnitTarget);
                        command.Parameters.AddWithValue("@CustomerTarget", target.CustomerTarget);
                        command.Parameters.AddWithValue("@InvoiceTarget", target.InvoiceTarget);
                        command.Parameters.AddWithValue("@ProductCategory", (object)target.ProductCategory ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CategoryRevenueTarget", target.CategoryRevenueTarget);
                        command.Parameters.AddWithValue("@CategoryUnitTarget", target.CategoryUnitTarget);
                        command.Parameters.AddWithValue("@Status", target.Status);
                        command.Parameters.AddWithValue("@PerformanceRating", (object)target.PerformanceRating ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ManagerComments", (object)target.ManagerComments ?? DBNull.Value);
                        command.Parameters.AddWithValue("@SalesmanComments", (object)target.SalesmanComments ?? DBNull.Value);
                        command.Parameters.AddWithValue("@MarketConditions", (object)target.MarketConditions ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Challenges", (object)target.Challenges ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BonusAmount", target.BonusAmount);
                        command.Parameters.AddWithValue("@CommissionAmount", target.CommissionAmount);
                        command.Parameters.AddWithValue("@IsBonusEligible", target.IsBonusEligible);
                        command.Parameters.AddWithValue("@IsCommissionEligible", target.IsCommissionEligible);
                        command.Parameters.AddWithValue("@ModifiedBy", target.ModifiedBy);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating salesman target: {ex.Message}", ex);
            }
        }

        public bool DeleteSalesmanTarget(int targetId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var deleteQuery = "DELETE FROM SalesmanTarget WHERE TargetId = @TargetId";
                    
                    using (var command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TargetId", targetId);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting salesman target: {ex.Message}", ex);
            }
        }

        public SalesmanTarget GetSalesmanTargetById(int targetId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT t.*, s.SalesmanName, s.SalesmanCode
                        FROM SalesmanTarget t
                        LEFT JOIN Salesman s ON t.SalesmanId = s.SalesmanId
                        WHERE t.TargetId = @TargetId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TargetId", targetId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapSalesmanTargetFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman target: {ex.Message}", ex);
            }
            
            return null;
        }

        public List<SalesmanTarget> GetAllSalesmanTargets()
        {
            var targets = new List<SalesmanTarget>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Check if SalesmanTarget table exists
                    var checkTableQuery = @"
                        SELECT COUNT(*) 
                        FROM INFORMATION_SCHEMA.TABLES 
                        WHERE TABLE_NAME = 'SalesmanTarget'";
                    
                    using (var checkCommand = new SqlCommand(checkTableQuery, connection))
                    {
                        var tableExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                        
                        if (!tableExists)
                        {
                            Console.WriteLine("SalesmanTarget table does not exist, creating it...");
                            // Table doesn't exist, create it
                            CreateSalesmanTargetTablesIfNotExists(connection);
                            return targets; // Return empty list for now
                        }
                        else
                        {
                            Console.WriteLine("SalesmanTarget table exists");
                            
                            // Check if there are any records in the table
                            var countQuery = "SELECT COUNT(*) FROM SalesmanTarget";
                            int recordCount = 0;
                            using (var countCommand = new SqlCommand(countQuery, connection))
                            {
                                recordCount = Convert.ToInt32(countCommand.ExecuteScalar());
                                Console.WriteLine($"SalesmanTarget table has {recordCount} records");
                            }
                            
                            // Check if there are any achievements
                            var achievementCountQuery = "SELECT COUNT(*) FROM SalesmanTargetAchievement";
                            int achievementCount = 0;
                            using (var achievementCountCommand = new SqlCommand(achievementCountQuery, connection))
                            {
                                achievementCount = Convert.ToInt32(achievementCountCommand.ExecuteScalar());
                                Console.WriteLine($"SalesmanTargetAchievement table has {achievementCount} records");
                            }
                            
                            // Debug info logged to console
                            Console.WriteLine($"SalesmanTarget table has {recordCount} records, SalesmanTargetAchievement table has {achievementCount} records");
                        }
                    }
                    
                    // Query with achievement data using subqueries and calculated percentages
                    var query = @"
                        SELECT t.*,
                               COALESCE(s.SalesmanName, 'Unknown Salesman') as SalesmanName, 
                               COALESCE(s.SalesmanCode, 'SM000') as SalesmanCode,
                               ISNULL((SELECT SUM(a.ActualRevenue) FROM SalesmanTargetAchievement a WHERE a.TargetId = t.TargetId), 0) as CalculatedActualRevenue,
                               ISNULL((SELECT SUM(a.ActualUnits) FROM SalesmanTargetAchievement a WHERE a.TargetId = t.TargetId), 0) as CalculatedActualUnits,
                               ISNULL((SELECT SUM(a.ActualCustomers) FROM SalesmanTargetAchievement a WHERE a.TargetId = t.TargetId), 0) as CalculatedActualCustomers,
                               ISNULL((SELECT SUM(a.ActualInvoices) FROM SalesmanTargetAchievement a WHERE a.TargetId = t.TargetId), 0) as CalculatedActualInvoices,
                               CASE 
                                   WHEN t.RevenueTarget > 0 THEN 
                                       ROUND((ISNULL((SELECT SUM(a.ActualRevenue) FROM SalesmanTargetAchievement a WHERE a.TargetId = t.TargetId), 0) * 100.0 / t.RevenueTarget), 2)
                                   ELSE 0 
                               END as OverallAchievementPercentage
                        FROM SalesmanTarget t
                        LEFT JOIN Salesman s ON t.SalesmanId = s.SalesmanId
                        ORDER BY t.TargetPeriodStart DESC, t.CreatedDate DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            int rowCount = 0;
                            while (reader.Read())
                            {
                                var target = MapSalesmanTargetFromReader(reader);
                                targets.Add(target);
                                rowCount++;
                            }
                            Console.WriteLine($"GetAllSalesmanTargets: Loaded {rowCount} targets from database");
                            
                            // Query results logged to console
                            Console.WriteLine($"Query Results: Loaded {rowCount} targets from database, Targets list count: {targets.Count}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all salesman targets: {ex.Message}");
                // Return empty list instead of throwing exception
                return targets;
            }
            
            return targets;
        }

        public List<SalesmanTarget> GetSalesmanTargetsBySalesman(int salesmanId)
        {
            var targets = new List<SalesmanTarget>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT t.*, s.SalesmanName, s.SalesmanCode
                        FROM SalesmanTarget t
                        LEFT JOIN Salesman s ON t.SalesmanId = s.SalesmanId
                        WHERE t.SalesmanId = @SalesmanId
                        ORDER BY t.TargetPeriodStart DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanId", salesmanId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                targets.Add(MapSalesmanTargetFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by salesman: {ex.Message}", ex);
            }
            
            return targets;
        }

        public List<SalesmanTarget> GetSalesmanTargetsByPeriod(DateTime startDate, DateTime endDate)
        {
            var targets = new List<SalesmanTarget>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT t.*, s.SalesmanName, s.SalesmanCode
                        FROM SalesmanTarget t
                        LEFT JOIN Salesman s ON t.SalesmanId = s.SalesmanId
                        WHERE t.TargetPeriodStart >= @StartDate AND t.TargetPeriodEnd <= @EndDate
                        ORDER BY t.TargetPeriodStart DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                targets.Add(MapSalesmanTargetFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by period: {ex.Message}", ex);
            }
            
            return targets;
        }

        public List<SalesmanTarget> GetSalesmanTargetsByStatus(string status)
        {
            var targets = new List<SalesmanTarget>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT t.*, s.SalesmanName, s.SalesmanCode
                        FROM SalesmanTarget t
                        LEFT JOIN Salesman s ON t.SalesmanId = s.SalesmanId
                        WHERE t.Status = @Status
                        ORDER BY t.TargetPeriodStart DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Status", status);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                targets.Add(MapSalesmanTargetFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by status: {ex.Message}", ex);
            }
            
            return targets;
        }

        public List<SalesmanTarget> GetSalesmanTargetsByType(string targetType)
        {
            var targets = new List<SalesmanTarget>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = @"
                        SELECT t.*, s.SalesmanName, s.SalesmanCode
                        FROM SalesmanTarget t
                        LEFT JOIN Salesman s ON t.SalesmanId = s.SalesmanId
                        WHERE t.TargetType = @TargetType
                        ORDER BY t.TargetPeriodStart DESC";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TargetType", targetType);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                targets.Add(MapSalesmanTargetFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting salesman targets by type: {ex.Message}", ex);
            }
            
            return targets;
        }

        private void CreateSalesmanTargetTablesIfNotExists(SqlConnection connection)
        {
            try
            {
                // Check if SalesmanTarget table exists
                var checkTableQuery = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = 'SalesmanTarget'";
                
                using (var command = new SqlCommand(checkTableQuery, connection))
                {
                    var tableExists = Convert.ToInt32(command.ExecuteScalar()) > 0;
                    
                    if (!tableExists)
                    {
                        // Create the SalesmanTarget table
                        var createTableQuery = @"
                            CREATE TABLE [dbo].[SalesmanTarget](
                                [TargetId] [int] IDENTITY(1,1) NOT NULL,
                                [SalesmanId] [int] NOT NULL,
                                [TargetType] [nvarchar](20) NOT NULL,
                                [TargetPeriodStart] [datetime] NOT NULL,
                                [TargetPeriodEnd] [datetime] NOT NULL,
                                [TargetPeriodName] [nvarchar](100) NOT NULL,
                                [RevenueTarget] [decimal](18,2) NOT NULL DEFAULT(0),
                                [UnitTarget] [int] NOT NULL DEFAULT(0),
                                [CustomerTarget] [int] NOT NULL DEFAULT(0),
                                [InvoiceTarget] [int] NOT NULL DEFAULT(0),
                                [ProductCategory] [nvarchar](100) NULL,
                                [CategoryRevenueTarget] [decimal](18,2) NOT NULL DEFAULT(0),
                                [CategoryUnitTarget] [int] NOT NULL DEFAULT(0),
                                [ActualRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
                                [ActualUnits] [int] NOT NULL DEFAULT(0),
                                [ActualCustomers] [int] NOT NULL DEFAULT(0),
                                [ActualInvoices] [int] NOT NULL DEFAULT(0),
                                [ActualCategoryRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
                                [ActualCategoryUnits] [int] NOT NULL DEFAULT(0),
                                [RevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [UnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CustomerAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [InvoiceAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CategoryRevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CategoryUnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [RevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
                                [UnitVariance] [int] NOT NULL DEFAULT(0),
                                [CustomerVariance] [int] NOT NULL DEFAULT(0),
                                [InvoiceVariance] [int] NOT NULL DEFAULT(0),
                                [CategoryRevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
                                [CategoryUnitVariance] [int] NOT NULL DEFAULT(0),
                                [Status] [nvarchar](20) NOT NULL DEFAULT('DRAFT'),
                                [PerformanceRating] [nvarchar](20) NOT NULL DEFAULT('AVERAGE'),
                                [ManagerComments] [nvarchar](max) NULL,
                                [SalesmanComments] [nvarchar](max) NULL,
                                [MarketConditions] [nvarchar](max) NULL,
                                [Challenges] [nvarchar](max) NULL,
                                [BonusAmount] [decimal](18,2) NOT NULL DEFAULT(0),
                                [CommissionAmount] [decimal](18,2) NOT NULL DEFAULT(0),
                                [IsBonusEligible] [bit] NOT NULL DEFAULT(0),
                                [IsCommissionEligible] [bit] NOT NULL DEFAULT(0),
                                [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
                                [CreatedBy] [int] NULL,
                                [ModifiedDate] [datetime] NULL,
                                [ModifiedBy] [int] NULL,
                                [ApprovedDate] [datetime] NULL,
                                [ApprovedBy] [int] NULL,
                                CONSTRAINT [PK_SalesmanTarget] PRIMARY KEY CLUSTERED ([TargetId] ASC)
                            )";
                        
                        using (var createCommand = new SqlCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                }
                
                // Check if SalesmanTargetAchievement table exists
                var checkAchievementTableQuery = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = 'SalesmanTargetAchievement'";
                
                using (var command = new SqlCommand(checkAchievementTableQuery, connection))
                {
                    var achievementTableExists = Convert.ToInt32(command.ExecuteScalar()) > 0;
                    
                    if (!achievementTableExists)
                    {
                        // Create the SalesmanTargetAchievement table
                        var createAchievementTableQuery = @"
                            CREATE TABLE [dbo].[SalesmanTargetAchievement](
                                [AchievementId] [int] IDENTITY(1,1) NOT NULL,
                                [TargetId] [int] NOT NULL,
                                [SalesmanId] [int] NOT NULL,
                                [AchievementDate] [datetime] NOT NULL,
                                [AchievementPeriod] [nvarchar](20) NOT NULL,
                                [ActualRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
                                [ActualUnits] [int] NOT NULL DEFAULT(0),
                                [ActualCustomers] [int] NOT NULL DEFAULT(0),
                                [ActualInvoices] [int] NOT NULL DEFAULT(0),
                                [ProductCategory] [nvarchar](100) NULL,
                                [CategoryRevenueAchieved] [decimal](18,2) NOT NULL DEFAULT(0),
                                [CategoryUnitsSold] [int] NOT NULL DEFAULT(0),
                                [RevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [UnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CustomerAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [InvoiceAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CategoryRevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CategoryUnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [RevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
                                [UnitVariance] [int] NOT NULL DEFAULT(0),
                                [CustomerVariance] [int] NOT NULL DEFAULT(0),
                                [InvoiceVariance] [int] NOT NULL DEFAULT(0),
                                [CategoryRevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
                                [CategoryUnitVariance] [int] NOT NULL DEFAULT(0),
                                [AchievementNotes] [nvarchar](max) NULL,
                                [Challenges] [nvarchar](max) NULL,
                                [MarketConditions] [nvarchar](max) NULL,
                                [CustomerFeedback] [nvarchar](max) NULL,
                                [OverallAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [IsAchievementMet] [bit] NOT NULL DEFAULT(0),
                                [Status] [nvarchar](20) NOT NULL DEFAULT('RECORDED'),
                                [IsVerified] [bit] NOT NULL DEFAULT(0),
                                [IsApproved] [bit] NOT NULL DEFAULT(0),
                                [VerifiedDate] [datetime] NULL,
                                [VerifiedBy] [int] NULL,
                                [ApprovedDate] [datetime] NULL,
                                [ApprovedBy] [int] NULL,
                                [VerificationNotes] [nvarchar](max) NULL,
                                [ApprovalNotes] [nvarchar](max) NULL,
                                [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
                                [CreatedBy] [int] NULL,
                                [ModifiedDate] [datetime] NULL,
                                [ModifiedBy] [int] NULL,
                                CONSTRAINT [PK_SalesmanTargetAchievement] PRIMARY KEY CLUSTERED ([AchievementId] ASC),
                                CONSTRAINT [FK_SalesmanTargetAchievement_Target] FOREIGN KEY([TargetId]) REFERENCES [dbo].[SalesmanTarget] ([TargetId]) ON DELETE CASCADE
                            )";
                        
                        using (var createCommand = new SqlCommand(createAchievementTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        // Table exists, but force recreate it to ensure correct structure
                        // Drop the old table and recreate with correct structure
                        var dropTableQuery = "DROP TABLE [dbo].[SalesmanTargetAchievement]";
                        using (var dropCommand = new SqlCommand(dropTableQuery, connection))
                        {
                            dropCommand.ExecuteNonQuery();
                        }
                        
                        // Now create the table with correct structure
                        var createAchievementTableQuery = @"
                            CREATE TABLE [dbo].[SalesmanTargetAchievement](
                                [AchievementId] [int] IDENTITY(1,1) NOT NULL,
                                [TargetId] [int] NOT NULL,
                                [SalesmanId] [int] NOT NULL,
                                [AchievementDate] [datetime] NOT NULL,
                                [AchievementPeriod] [nvarchar](20) NOT NULL,
                                [ActualRevenue] [decimal](18,2) NOT NULL DEFAULT(0),
                                [ActualUnits] [int] NOT NULL DEFAULT(0),
                                [ActualCustomers] [int] NOT NULL DEFAULT(0),
                                [ActualInvoices] [int] NOT NULL DEFAULT(0),
                                [ProductCategory] [nvarchar](100) NULL,
                                [CategoryRevenueAchieved] [decimal](18,2) NOT NULL DEFAULT(0),
                                [CategoryUnitsSold] [int] NOT NULL DEFAULT(0),
                                [RevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [UnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CustomerAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [InvoiceAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CategoryRevenueAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [CategoryUnitAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [RevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
                                [UnitVariance] [int] NOT NULL DEFAULT(0),
                                [CustomerVariance] [int] NOT NULL DEFAULT(0),
                                [InvoiceVariance] [int] NOT NULL DEFAULT(0),
                                [CategoryRevenueVariance] [decimal](18,2) NOT NULL DEFAULT(0),
                                [CategoryUnitVariance] [int] NOT NULL DEFAULT(0),
                                [AchievementNotes] [nvarchar](max) NULL,
                                [Challenges] [nvarchar](max) NULL,
                                [MarketConditions] [nvarchar](max) NULL,
                                [CustomerFeedback] [nvarchar](max) NULL,
                                [OverallAchievementPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [IsAchievementMet] [bit] NOT NULL DEFAULT(0),
                                [Status] [nvarchar](20) NOT NULL DEFAULT('RECORDED'),
                                [IsVerified] [bit] NOT NULL DEFAULT(0),
                                [IsApproved] [bit] NOT NULL DEFAULT(0),
                                [VerifiedDate] [datetime] NULL,
                                [VerifiedBy] [int] NULL,
                                [ApprovedDate] [datetime] NULL,
                                [ApprovedBy] [int] NULL,
                                [VerificationNotes] [nvarchar](max) NULL,
                                [ApprovalNotes] [nvarchar](max) NULL,
                                [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
                                [CreatedBy] [int] NULL,
                                [ModifiedDate] [datetime] NULL,
                                [ModifiedBy] [int] NULL,
                                CONSTRAINT [PK_SalesmanTargetAchievement] PRIMARY KEY CLUSTERED ([AchievementId] ASC),
                                CONSTRAINT [FK_SalesmanTargetAchievement_Target] FOREIGN KEY([TargetId]) REFERENCES [dbo].[SalesmanTarget] ([TargetId]) ON DELETE CASCADE
                            )";
                        
                        using (var createCommand = new SqlCommand(createAchievementTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If we can't create tables, that's okay - let the main error handling deal with it
            }
        }

        private SalesmanTarget MapSalesmanTargetFromReader(SqlDataReader reader)
        {
            return new SalesmanTarget
            {
                TargetId = reader.GetInt32(reader.GetOrdinal("TargetId")),
                SalesmanId = reader.GetInt32(reader.GetOrdinal("SalesmanId")),
                SalesmanName = reader.IsDBNull(reader.GetOrdinal("SalesmanName")) ? null : reader.GetString(reader.GetOrdinal("SalesmanName")),
                SalesmanCode = reader.IsDBNull(reader.GetOrdinal("SalesmanCode")) ? null : reader.GetString(reader.GetOrdinal("SalesmanCode")),
                TargetType = reader.GetString(reader.GetOrdinal("TargetType")),
                TargetPeriodStart = reader.GetDateTime(reader.GetOrdinal("TargetPeriodStart")),
                TargetPeriodEnd = reader.GetDateTime(reader.GetOrdinal("TargetPeriodEnd")),
                TargetPeriodName = reader.GetString(reader.GetOrdinal("TargetPeriodName")),
                RevenueTarget = reader.GetDecimal(reader.GetOrdinal("RevenueTarget")),
                UnitTarget = reader.GetInt32(reader.GetOrdinal("UnitTarget")),
                CustomerTarget = reader.GetInt32(reader.GetOrdinal("CustomerTarget")),
                InvoiceTarget = reader.GetInt32(reader.GetOrdinal("InvoiceTarget")),
                ProductCategory = reader.IsDBNull(reader.GetOrdinal("ProductCategory")) ? null : reader.GetString(reader.GetOrdinal("ProductCategory")),
                CategoryRevenueTarget = reader.GetDecimal(reader.GetOrdinal("CategoryRevenueTarget")),
                CategoryUnitTarget = reader.GetInt32(reader.GetOrdinal("CategoryUnitTarget")),
                ActualRevenue = reader.IsDBNull(reader.GetOrdinal("CalculatedActualRevenue")) ? 0 : reader.GetDecimal(reader.GetOrdinal("CalculatedActualRevenue")),
                ActualUnits = reader.IsDBNull(reader.GetOrdinal("CalculatedActualUnits")) ? 0 : reader.GetInt32(reader.GetOrdinal("CalculatedActualUnits")),
                ActualCustomers = reader.IsDBNull(reader.GetOrdinal("CalculatedActualCustomers")) ? 0 : reader.GetInt32(reader.GetOrdinal("CalculatedActualCustomers")),
                ActualInvoices = reader.IsDBNull(reader.GetOrdinal("CalculatedActualInvoices")) ? 0 : reader.GetInt32(reader.GetOrdinal("CalculatedActualInvoices")),
                ActualCategoryRevenue = reader.GetDecimal(reader.GetOrdinal("ActualCategoryRevenue")),
                ActualCategoryUnits = reader.GetInt32(reader.GetOrdinal("ActualCategoryUnits")),
                RevenueAchievementPercentage = reader.IsDBNull(reader.GetOrdinal("RevenueAchievementPercentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("RevenueAchievementPercentage")),
                UnitAchievementPercentage = reader.IsDBNull(reader.GetOrdinal("UnitAchievementPercentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("UnitAchievementPercentage")),
                CustomerAchievementPercentage = reader.IsDBNull(reader.GetOrdinal("CustomerAchievementPercentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("CustomerAchievementPercentage")),
                InvoiceAchievementPercentage = reader.IsDBNull(reader.GetOrdinal("InvoiceAchievementPercentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("InvoiceAchievementPercentage")),
                CategoryRevenueAchievementPercentage = reader.GetDecimal(reader.GetOrdinal("CategoryRevenueAchievementPercentage")),
                CategoryUnitAchievementPercentage = reader.GetDecimal(reader.GetOrdinal("CategoryUnitAchievementPercentage")),
                RevenueVariance = reader.GetDecimal(reader.GetOrdinal("RevenueVariance")),
                UnitVariance = reader.GetInt32(reader.GetOrdinal("UnitVariance")),
                CustomerVariance = reader.GetInt32(reader.GetOrdinal("CustomerVariance")),
                InvoiceVariance = reader.GetInt32(reader.GetOrdinal("InvoiceVariance")),
                CategoryRevenueVariance = reader.GetDecimal(reader.GetOrdinal("CategoryRevenueVariance")),
                CategoryUnitVariance = reader.GetInt32(reader.GetOrdinal("CategoryUnitVariance")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                PerformanceRating = reader.GetString(reader.GetOrdinal("PerformanceRating")),
                ManagerComments = reader.IsDBNull(reader.GetOrdinal("ManagerComments")) ? null : reader.GetString(reader.GetOrdinal("ManagerComments")),
                SalesmanComments = reader.IsDBNull(reader.GetOrdinal("SalesmanComments")) ? null : reader.GetString(reader.GetOrdinal("SalesmanComments")),
                MarketConditions = reader.IsDBNull(reader.GetOrdinal("MarketConditions")) ? null : reader.GetString(reader.GetOrdinal("MarketConditions")),
                Challenges = reader.IsDBNull(reader.GetOrdinal("Challenges")) ? null : reader.GetString(reader.GetOrdinal("Challenges")),
                BonusAmount = reader.GetDecimal(reader.GetOrdinal("BonusAmount")),
                CommissionAmount = reader.GetDecimal(reader.GetOrdinal("CommissionAmount")),
                IsBonusEligible = reader.GetBoolean(reader.GetOrdinal("IsBonusEligible")),
                IsCommissionEligible = reader.GetBoolean(reader.GetOrdinal("IsCommissionEligible")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? 0 : reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("ModifiedBy")),
                ApprovedDate = reader.IsDBNull(reader.GetOrdinal("ApprovedDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("ApprovedDate")),
                ApprovedBy = reader.IsDBNull(reader.GetOrdinal("ApprovedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("ApprovedBy")),
                OverallAchievementPercentage = reader.IsDBNull(reader.GetOrdinal("OverallAchievementPercentage")) ? 0 : reader.GetDecimal(reader.GetOrdinal("OverallAchievementPercentage"))
            };
        }

        // Achievement methods will be implemented in the next part
        public int CreateSalesmanTargetAchievement(SalesmanTargetAchievement achievement)
        {
            try
            {
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // Check if table exists first
                    var checkTableQuery = @"
                        SELECT COUNT(*) 
                        FROM INFORMATION_SCHEMA.TABLES 
                        WHERE TABLE_NAME = 'SalesmanTargetAchievement'";
                    
                    using (var checkCommand = new SqlCommand(checkTableQuery, connection))
                    {
                        var tableExists = Convert.ToInt32(checkCommand.ExecuteScalar()) > 0;
                        
                        if (!tableExists)
                        {
                            CreateSalesmanTargetTablesIfNotExists(connection);
                        }
                    }
                    
                    var query = @"
                        INSERT INTO SalesmanTargetAchievement (
                            TargetId, SalesmanId, ActualRevenue, ActualUnits, ActualCustomers, 
                            ActualInvoices, AchievementDate, AchievementPeriod, 
                            RevenueAchievementPercentage, UnitAchievementPercentage, 
                            CustomerAchievementPercentage, InvoiceAchievementPercentage, 
                            OverallAchievementPercentage, IsAchievementMet, AchievementNotes, 
                            Challenges, Status, CreatedDate, CreatedBy
                        )
                        VALUES (
                            @TargetId, @SalesmanId, @ActualRevenue, @ActualUnits, @ActualCustomers, 
                            @ActualInvoices, @AchievementDate, @AchievementPeriod, 
                            @RevenueAchievementPercentage, @UnitAchievementPercentage, 
                            @CustomerAchievementPercentage, @InvoiceAchievementPercentage, 
                            @OverallAchievementPercentage, @IsAchievementMet, @AchievementNotes, 
                            @Challenges, @Status, @CreatedDate, @CreatedBy
                        );
                        SELECT SCOPE_IDENTITY();";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@TargetId", achievement.TargetId);
                        command.Parameters.AddWithValue("@SalesmanId", achievement.SalesmanId);
                        command.Parameters.AddWithValue("@ActualRevenue", achievement.ActualRevenue);
                        command.Parameters.AddWithValue("@ActualUnits", achievement.ActualUnits);
                        command.Parameters.AddWithValue("@ActualCustomers", achievement.ActualCustomers);
                        command.Parameters.AddWithValue("@ActualInvoices", achievement.ActualInvoices);
                        command.Parameters.AddWithValue("@AchievementDate", achievement.AchievementDate);
                        command.Parameters.AddWithValue("@AchievementPeriod", achievement.AchievementPeriod);
                        command.Parameters.AddWithValue("@RevenueAchievementPercentage", achievement.RevenueAchievementPercentage);
                        command.Parameters.AddWithValue("@UnitAchievementPercentage", achievement.UnitAchievementPercentage);
                        command.Parameters.AddWithValue("@CustomerAchievementPercentage", achievement.CustomerAchievementPercentage);
                        command.Parameters.AddWithValue("@InvoiceAchievementPercentage", achievement.InvoiceAchievementPercentage);
                        command.Parameters.AddWithValue("@OverallAchievementPercentage", achievement.OverallAchievementPercentage);
                        command.Parameters.AddWithValue("@IsAchievementMet", achievement.IsAchievementMet);
                        command.Parameters.AddWithValue("@AchievementNotes", achievement.AchievementNotes ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Challenges", achievement.Challenges ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", achievement.Status);
                        command.Parameters.AddWithValue("@CreatedDate", achievement.CreatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", achievement.CreatedBy);
                        
                        var result = command.ExecuteScalar();
                        var achievementId = Convert.ToInt32(result);
                        return achievementId;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating salesman target achievement: {ex.Message}");
                return 0;
            }
        }

        public bool UpdateSalesmanTargetAchievement(SalesmanTargetAchievement achievement)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var query = @"
                        UPDATE SalesmanTargetAchievement 
                        SET ActualRevenue = @ActualRevenue, 
                            ActualUnits = @ActualUnits, 
                            ActualCustomers = @ActualCustomers, 
                            ActualInvoices = @ActualInvoices, 
                            AchievementDate = @AchievementDate, 
                            AchievementPeriod = @AchievementPeriod, 
                            RevenueAchievementPercentage = @RevenueAchievementPercentage, 
                            UnitAchievementPercentage = @UnitAchievementPercentage, 
                            CustomerAchievementPercentage = @CustomerAchievementPercentage, 
                            InvoiceAchievementPercentage = @InvoiceAchievementPercentage, 
                            OverallAchievementPercentage = @OverallAchievementPercentage, 
                            IsAchievementMet = @IsAchievementMet, 
                            AchievementNotes = @AchievementNotes, 
                            Challenges = @Challenges, 
                            Status = @Status, 
                            ModifiedDate = @ModifiedDate, 
                            ModifiedBy = @ModifiedBy
                        WHERE AchievementId = @AchievementId";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@AchievementId", achievement.AchievementId);
                        command.Parameters.AddWithValue("@ActualRevenue", achievement.ActualRevenue);
                        command.Parameters.AddWithValue("@ActualUnits", achievement.ActualUnits);
                        command.Parameters.AddWithValue("@ActualCustomers", achievement.ActualCustomers);
                        command.Parameters.AddWithValue("@ActualInvoices", achievement.ActualInvoices);
                        command.Parameters.AddWithValue("@AchievementDate", achievement.AchievementDate);
                        command.Parameters.AddWithValue("@AchievementPeriod", achievement.AchievementPeriod);
                        command.Parameters.AddWithValue("@RevenueAchievementPercentage", achievement.RevenueAchievementPercentage);
                        command.Parameters.AddWithValue("@UnitAchievementPercentage", achievement.UnitAchievementPercentage);
                        command.Parameters.AddWithValue("@CustomerAchievementPercentage", achievement.CustomerAchievementPercentage);
                        command.Parameters.AddWithValue("@InvoiceAchievementPercentage", achievement.InvoiceAchievementPercentage);
                        command.Parameters.AddWithValue("@OverallAchievementPercentage", achievement.OverallAchievementPercentage);
                        command.Parameters.AddWithValue("@IsAchievementMet", achievement.IsAchievementMet);
                        command.Parameters.AddWithValue("@AchievementNotes", achievement.AchievementNotes ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Challenges", achievement.Challenges ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Status", achievement.Status);
                        command.Parameters.AddWithValue("@ModifiedDate", achievement.ModifiedDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", achievement.ModifiedBy ?? (object)DBNull.Value);
                        
                        var rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating salesman target achievement: {ex.Message}");
                return false;
            }
        }

        public bool DeleteSalesmanTargetAchievement(int achievementId)
        {
            // Implementation will be added
            return false;
        }

        public SalesmanTargetAchievement GetSalesmanTargetAchievementById(int achievementId)
        {
            // Implementation will be added
            return null;
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievements(int targetId)
        {
            // Implementation will be added
            return new List<SalesmanTargetAchievement>();
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsBySalesman(int salesmanId)
        {
            // Implementation will be added
            return new List<SalesmanTargetAchievement>();
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsByDate(DateTime date)
        {
            // Implementation will be added
            return new List<SalesmanTargetAchievement>();
        }

        public List<SalesmanTargetAchievement> GetSalesmanTargetAchievementsByPeriod(DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return new List<SalesmanTargetAchievement>();
        }

        // Performance Analysis methods
        public List<SalesmanTarget> GetTopPerformers(int count, DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return new List<SalesmanTarget>();
        }

        public List<SalesmanTarget> GetUnderPerformers(int count, DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return new List<SalesmanTarget>();
        }

        public decimal GetAverageAchievementPercentage(int salesmanId, DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }

        public decimal GetTotalRevenueAchieved(int salesmanId, DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }

        public int GetTotalUnitsSold(int salesmanId, DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }

        // Reporting methods
        public List<SalesmanTarget> GetSalesmanTargetReport(DateTime startDate, DateTime endDate, int? salesmanId, string targetType, string status)
        {
            // Implementation will be added
            return new List<SalesmanTarget>();
        }

        public List<SalesmanTargetAchievement> GetSalesmanAchievementReport(DateTime startDate, DateTime endDate, int? salesmanId, string status)
        {
            // Implementation will be added
            return new List<SalesmanTargetAchievement>();
        }

        // Validation methods
        public bool ValidateSalesmanTarget(SalesmanTarget target)
        {
            if (target == null) return false;
            if (target.SalesmanId <= 0) return false;
            if (string.IsNullOrEmpty(target.TargetType)) return false;
            if (target.TargetPeriodStart >= target.TargetPeriodEnd) return false;
            if (target.RevenueTarget <= 0 && target.UnitTarget <= 0) return false;
            
            return true;
        }

        public bool CheckTargetOverlap(int salesmanId, DateTime startDate, DateTime endDate, int? excludeTargetId = null)
        {
            // Implementation will be added
            return false;
        }

        public bool CheckSalesmanExists(int salesmanId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    var query = @"
                        SELECT COUNT(*) 
                        FROM Salesman 
                        WHERE SalesmanId = @SalesmanId AND IsActive = 1";
                    
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SalesmanId", salesmanId);
                        
                        var count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking salesman existence: {ex.Message}");
                return false;
            }
        }

        // Approval and Status Management methods
        public bool ApproveSalesmanTarget(int targetId, int approvedBy, string approvalNotes)
        {
            // Implementation will be added
            return false;
        }

        public bool RejectSalesmanTarget(int targetId, int rejectedBy, string rejectionReason)
        {
            // Implementation will be added
            return false;
        }

        public bool ActivateSalesmanTarget(int targetId)
        {
            // Implementation will be added
            return false;
        }

        public bool CompleteSalesmanTarget(int targetId)
        {
            // Implementation will be added
            return false;
        }

        // Achievement Verification methods
        public bool VerifySalesmanAchievement(int achievementId, int verifiedBy, string verificationNotes)
        {
            // Implementation will be added
            return false;
        }

        public bool ApproveSalesmanAchievement(int achievementId, int approvedBy, string approvalNotes)
        {
            // Implementation will be added
            return false;
        }

        // Bonus and Commission methods
        public bool UpdateBonusAmount(int targetId, decimal bonusAmount)
        {
            // Implementation will be added
            return false;
        }

        public bool UpdateCommissionAmount(int targetId, decimal commissionAmount)
        {
            // Implementation will be added
            return false;
        }

        public bool SetBonusEligibility(int targetId, bool isEligible)
        {
            // Implementation will be added
            return false;
        }

        public bool SetCommissionEligibility(int targetId, bool isEligible)
        {
            // Implementation will be added
            return false;
        }

        // Statistics methods
        public int GetTargetCount(DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }

        public int GetAchievementCount(DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }

        public decimal GetTotalTargetRevenue(DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }

        public decimal GetTotalAchievedRevenue(DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }

        public decimal GetOverallAchievementPercentage(DateTime startDate, DateTime endDate)
        {
            // Implementation will be added
            return 0;
        }
    }
}

