using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class SupplierDebitNoteRepository : ISupplierDebitNoteRepository
    {
        private readonly string _connectionString;

        public SupplierDebitNoteRepository()
        {
            _connectionString = ConfigurationManager.GetConnectionString("DistributionConnection");
        }

        public SupplierDebitNoteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateSupplierDebitNote(SupplierDebitNote debitNote)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_CreateSupplierDebitNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@DebitNoteNo", debitNote.DebitNoteNo);
                        command.Parameters.AddWithValue("@DebitNoteBarcode", debitNote.DebitNoteBarcode);
                        command.Parameters.AddWithValue("@SupplierId", debitNote.SupplierId);
                        command.Parameters.AddWithValue("@ReferencePurchaseId", (object)debitNote.ReferencePurchaseId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReferenceGRNId", (object)debitNote.ReferenceGRNId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DebitDate", debitNote.DebitDate);
                        command.Parameters.AddWithValue("@Reason", debitNote.Reason);
                        command.Parameters.AddWithValue("@SubTotal", debitNote.SubTotal);
                        command.Parameters.AddWithValue("@TaxAmount", debitNote.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountAmount", debitNote.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", debitNote.TotalAmount);
                        command.Parameters.AddWithValue("@Status", debitNote.Status);
                        command.Parameters.AddWithValue("@Remarks", (object)debitNote.Remarks ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", (object)debitNote.CreatedBy ?? DBNull.Value);
                        command.Parameters.AddWithValue("@BarcodeImage", (object)debitNote.BarcodeImage ?? DBNull.Value);
                        
                        var debitNoteIdParam = new SqlParameter("@DebitNoteId", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(debitNoteIdParam);
                        
                        command.ExecuteNonQuery();
                        
                        return Convert.ToInt32(debitNoteIdParam.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure doesn't exist, use direct SQL INSERT
                if (ex.Message.Contains("Could not find stored procedure"))
                {
                    return CreateSupplierDebitNoteDirect(debitNote);
                }
                throw new Exception($"Error creating supplier debit note: {ex.Message}", ex);
            }
        }

        private int CreateSupplierDebitNoteDirect(SupplierDebitNote debitNote)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // First, try to create the table if it doesn't exist
                    CreateSupplierDebitNoteTableIfNotExists(connection);
                    
                    var insertQuery = @"
                        INSERT INTO SupplierDebitNote (
                            DebitNoteNumber, Barcode, SupplierId, DebitDate, Reason, 
                            SubTotal, TaxAmount, DiscountAmount, TotalAmount, Status, 
                            Remarks, ReferencePurchaseId, ReferenceGRNId, CreatedBy, 
                            CreatedDate, UpdatedBy, UpdatedDate
                        )
                        VALUES (
                            @DebitNoteNumber, @Barcode, @SupplierId, @DebitDate, @Reason,
                            @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount, @Status,
                            @Remarks, @ReferencePurchaseId, @ReferenceGRNId, @CreatedBy,
                            GETDATE(), @CreatedBy, GETDATE()
                        );
                        SELECT SCOPE_IDENTITY();";
                    
                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@DebitNoteNumber", debitNote.DebitNoteNo);
                        command.Parameters.AddWithValue("@Barcode", debitNote.DebitNoteBarcode);
                        command.Parameters.AddWithValue("@SupplierId", debitNote.SupplierId);
                        command.Parameters.AddWithValue("@DebitDate", debitNote.DebitDate);
                        command.Parameters.AddWithValue("@Reason", debitNote.Reason);
                        command.Parameters.AddWithValue("@SubTotal", debitNote.SubTotal);
                        command.Parameters.AddWithValue("@TaxAmount", debitNote.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountAmount", debitNote.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", debitNote.TotalAmount);
                        command.Parameters.AddWithValue("@Status", debitNote.Status);
                        command.Parameters.AddWithValue("@Remarks", (object)debitNote.Remarks ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReferencePurchaseId", (object)debitNote.ReferencePurchaseId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReferenceGRNId", (object)debitNote.ReferenceGRNId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedBy", (object)debitNote.CreatedBy ?? DBNull.Value);
                        
                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Invalid object name"))
                {
                    throw new Exception("Database tables for Supplier Debit Note are not created yet. Please run the database setup script first.", ex);
                }
                throw new Exception($"Error creating supplier debit note (direct method): {ex.Message}", ex);
            }
        }

        private void CreateSupplierDebitNoteTableIfNotExists(SqlConnection connection)
        {
            try
            {
                // Check if table exists
                var checkTableQuery = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = 'SupplierDebitNote'";
                
                using (var command = new SqlCommand(checkTableQuery, connection))
                {
                    var tableExists = Convert.ToInt32(command.ExecuteScalar()) > 0;
                    
                    if (!tableExists)
                    {
                        // Create the table
                        var createTableQuery = @"
                            CREATE TABLE [dbo].[SupplierDebitNote](
                                [DebitNoteId] [int] IDENTITY(1,1) NOT NULL,
                                [DebitNoteNumber] [nvarchar](50) NOT NULL,
                                [Barcode] [nvarchar](100) NULL,
                                [SupplierId] [int] NOT NULL,
                                [DebitDate] [datetime] NOT NULL,
                                [Reason] [nvarchar](100) NOT NULL,
                                [SubTotal] [decimal](18,2) NOT NULL DEFAULT(0),
                                [TaxAmount] [decimal](18,2) NOT NULL DEFAULT(0),
                                [DiscountAmount] [decimal](18,2) NOT NULL DEFAULT(0),
                                [TotalAmount] [decimal](18,2) NOT NULL DEFAULT(0),
                                [Status] [nvarchar](20) NOT NULL DEFAULT('DRAFT'),
                                [Remarks] [nvarchar](max) NULL,
                                [ReferencePurchaseId] [int] NULL,
                                [ReferenceGRNId] [int] NULL,
                                [CreatedBy] [int] NULL,
                                [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
                                [UpdatedBy] [int] NULL,
                                [UpdatedDate] [datetime] NULL,
                                [ApprovedBy] [int] NULL,
                                [ApprovalDate] [datetime] NULL,
                                [BarcodeImage] [varbinary](max) NULL,
                                CONSTRAINT [PK_SupplierDebitNote] PRIMARY KEY CLUSTERED ([DebitNoteId] ASC)
                            )";
                        
                        using (var createCommand = new SqlCommand(createTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                }
                
                // Check if items table exists
                var checkItemsTableQuery = @"
                    SELECT COUNT(*) 
                    FROM INFORMATION_SCHEMA.TABLES 
                    WHERE TABLE_NAME = 'SupplierDebitNoteItem'";
                
                using (var command = new SqlCommand(checkItemsTableQuery, connection))
                {
                    var itemsTableExists = Convert.ToInt32(command.ExecuteScalar()) > 0;
                    
                    if (!itemsTableExists)
                    {
                        // Create the items table
                        var createItemsTableQuery = @"
                            CREATE TABLE [dbo].[SupplierDebitNoteItem](
                                [DebitNoteItemId] [int] IDENTITY(1,1) NOT NULL,
                                [DebitNoteId] [int] NOT NULL,
                                [ProductId] [int] NOT NULL,
                                [Quantity] [decimal](18,2) NOT NULL,
                                [UnitPrice] [decimal](18,2) NOT NULL,
                                [TaxPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [TaxAmount] [decimal](18,2) NOT NULL DEFAULT(0),
                                [DiscountPercentage] [decimal](5,2) NOT NULL DEFAULT(0),
                                [DiscountAmount] [decimal](18,2) NOT NULL DEFAULT(0),
                                [TotalAmount] [decimal](18,2) NOT NULL,
                                [Reason] [nvarchar](max) NULL,
                                [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
                                CONSTRAINT [PK_SupplierDebitNoteItem] PRIMARY KEY CLUSTERED ([DebitNoteItemId] ASC),
                                CONSTRAINT [FK_SupplierDebitNoteItem_DebitNote] FOREIGN KEY([DebitNoteId]) REFERENCES [dbo].[SupplierDebitNote] ([DebitNoteId]) ON DELETE CASCADE
                            )";
                        
                        using (var createCommand = new SqlCommand(createItemsTableQuery, connection))
                        {
                            createCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If we can't create tables, that's okay - let the main error handling deal with it
                // This is just a convenience method
            }
        }

        public bool UpdateSupplierDebitNote(SupplierDebitNote debitNote)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_UpdateSupplierDebitNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@DebitNoteId", debitNote.DebitNoteId);
                        command.Parameters.AddWithValue("@SupplierId", debitNote.SupplierId);
                        command.Parameters.AddWithValue("@ReferencePurchaseId", (object)debitNote.ReferencePurchaseId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ReferenceGRNId", (object)debitNote.ReferenceGRNId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@DebitDate", debitNote.DebitDate);
                        command.Parameters.AddWithValue("@Reason", debitNote.Reason);
                        command.Parameters.AddWithValue("@SubTotal", debitNote.SubTotal);
                        command.Parameters.AddWithValue("@TaxAmount", debitNote.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountAmount", debitNote.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", debitNote.TotalAmount);
                        command.Parameters.AddWithValue("@Status", debitNote.Status);
                        command.Parameters.AddWithValue("@Remarks", (object)debitNote.Remarks ?? DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", (object)debitNote.ModifiedBy ?? DBNull.Value);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating supplier debit note: {ex.Message}", ex);
            }
        }

        public bool DeleteSupplierDebitNote(int debitNoteId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_DeleteSupplierDebitNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteId", debitNoteId);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting supplier debit note: {ex.Message}", ex);
            }
        }

        public SupplierDebitNote GetSupplierDebitNoteById(int debitNoteId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNoteById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteId", debitNoteId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapSupplierDebitNoteFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting supplier debit note: {ex.Message}", ex);
            }
            
            return null;
        }

        public SupplierDebitNote GetSupplierDebitNoteByNumber(string debitNoteNo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNoteByNumber", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteNo", debitNoteNo);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapSupplierDebitNoteFromReader(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting supplier debit note by number: {ex.Message}", ex);
            }
            
            return null;
        }

        public List<SupplierDebitNote> GetAllSupplierDebitNotes()
        {
            var debitNotes = new List<SupplierDebitNote>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetAllSupplierDebitNotes", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                debitNotes.Add(MapSupplierDebitNoteFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting all supplier debit notes: {ex.Message}", ex);
            }
            
            return debitNotes;
        }

        public List<SupplierDebitNote> GetSupplierDebitNotesByDateRange(DateTime startDate, DateTime endDate)
        {
            var debitNotes = new List<SupplierDebitNote>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNotesByDateRange", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                debitNotes.Add(MapSupplierDebitNoteFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting supplier debit notes by date range: {ex.Message}", ex);
            }
            
            return debitNotes;
        }

        public List<SupplierDebitNote> GetSupplierDebitNotesBySupplier(int supplierId)
        {
            var debitNotes = new List<SupplierDebitNote>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNotesBySupplier", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                debitNotes.Add(MapSupplierDebitNoteFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting supplier debit notes by supplier: {ex.Message}", ex);
            }
            
            return debitNotes;
        }

        public List<SupplierDebitNote> GetSupplierDebitNotesByStatus(string status)
        {
            var debitNotes = new List<SupplierDebitNote>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNotesByStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Status", status);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                debitNotes.Add(MapSupplierDebitNoteFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting supplier debit notes by status: {ex.Message}", ex);
            }
            
            return debitNotes;
        }

        public bool CreateSupplierDebitNoteItem(SupplierDebitNoteItem item)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_CreateSupplierDebitNoteItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@DebitNoteId", item.DebitNoteId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@TaxPercentage", item.TaxPercentage);
                        command.Parameters.AddWithValue("@TaxAmount", item.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountPercentage", item.DiscountPercentage);
                        command.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                        command.Parameters.AddWithValue("@Reason", (object)item.Reason ?? DBNull.Value);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure doesn't exist, use direct SQL INSERT
                if (ex.Message.Contains("Could not find stored procedure"))
                {
                    return CreateSupplierDebitNoteItemDirect(item);
                }
                throw new Exception($"Error creating supplier debit note item: {ex.Message}", ex);
            }
        }

        private bool CreateSupplierDebitNoteItemDirect(SupplierDebitNoteItem item)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    
                    // First, try to create the table if it doesn't exist
                    CreateSupplierDebitNoteTableIfNotExists(connection);
                    
                    var insertQuery = @"
                        INSERT INTO SupplierDebitNoteItem (
                            DebitNoteId, ProductId, Quantity, UnitPrice, TaxPercentage,
                            TaxAmount, DiscountPercentage, DiscountAmount, TotalAmount,
                            Reason, CreatedDate
                        )
                        VALUES (
                            @DebitNoteId, @ProductId, @Quantity, @UnitPrice, @TaxPercentage,
                            @TaxAmount, @DiscountPercentage, @DiscountAmount, @TotalAmount,
                            @Reason, GETDATE()
                        )";
                    
                    using (var command = new SqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@DebitNoteId", item.DebitNoteId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@TaxPercentage", item.TaxPercentage);
                        command.Parameters.AddWithValue("@TaxAmount", item.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountPercentage", item.DiscountPercentage);
                        command.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                        command.Parameters.AddWithValue("@Reason", (object)item.Reason ?? DBNull.Value);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating supplier debit note item (direct method): {ex.Message}", ex);
            }
        }

        public bool UpdateSupplierDebitNoteItem(SupplierDebitNoteItem item)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_UpdateSupplierDebitNoteItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        command.Parameters.AddWithValue("@DebitNoteItemId", item.DebitNoteItemId);
                        command.Parameters.AddWithValue("@ProductId", item.ProductId);
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@UnitPrice", item.UnitPrice);
                        command.Parameters.AddWithValue("@TaxPercentage", item.TaxPercentage);
                        command.Parameters.AddWithValue("@TaxAmount", item.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountPercentage", item.DiscountPercentage);
                        command.Parameters.AddWithValue("@DiscountAmount", item.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", item.TotalAmount);
                        command.Parameters.AddWithValue("@Reason", (object)item.Reason ?? DBNull.Value);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating supplier debit note item: {ex.Message}", ex);
            }
        }

        public bool DeleteSupplierDebitNoteItem(int itemId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_DeleteSupplierDebitNoteItem", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteItemId", itemId);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting supplier debit note item: {ex.Message}", ex);
            }
        }

        public List<SupplierDebitNoteItem> GetSupplierDebitNoteItems(int debitNoteId)
        {
            var items = new List<SupplierDebitNoteItem>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNoteItems", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteId", debitNoteId);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                items.Add(MapSupplierDebitNoteItemFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting supplier debit note items: {ex.Message}", ex);
            }
            
            return items;
        }

        public string GenerateDebitNoteNumber()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GenerateSupplierDebitNoteNumber", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader["NextDebitNoteNo"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // If stored procedure doesn't exist, generate a simple number
                var timestamp = DateTime.Now.ToString("yyyyMMdd");
                var random = new Random().Next(1000, 9999);
                return $"DN{timestamp}{random}";
            }
            
            return $"DN{DateTime.Now:yyyyMMdd}001";
        }

        public bool ApproveDebitNote(int debitNoteId, int approvedBy)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_ApproveSupplierDebitNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteId", debitNoteId);
                        command.Parameters.AddWithValue("@ApprovedBy", approvedBy);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error approving supplier debit note: {ex.Message}", ex);
            }
        }

        public bool RejectDebitNote(int debitNoteId, int rejectedBy, string rejectionReason)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_RejectSupplierDebitNote", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteId", debitNoteId);
                        command.Parameters.AddWithValue("@RejectedBy", rejectedBy);
                        command.Parameters.AddWithValue("@RejectionReason", rejectionReason);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error rejecting supplier debit note: {ex.Message}", ex);
            }
        }

        public bool UpdateSupplierBalance(int supplierId, decimal amount)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_UpdateSupplierBalance", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SupplierId", supplierId);
                        command.Parameters.AddWithValue("@Amount", amount);
                        
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating supplier balance: {ex.Message}", ex);
            }
        }

        public List<SupplierDebitNote> GetSupplierDebitNoteReport(DateTime startDate, DateTime endDate, int? supplierId, string status)
        {
            var debitNotes = new List<SupplierDebitNote>();
            
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNoteReport", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        command.Parameters.AddWithValue("@SupplierId", (object)supplierId ?? DBNull.Value);
                        command.Parameters.AddWithValue("@Status", (object)status ?? DBNull.Value);
                        
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                debitNotes.Add(MapSupplierDebitNoteFromReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting supplier debit note report: {ex.Message}", ex);
            }
            
            return debitNotes;
        }

        public decimal GetTotalDebitNoteAmount(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetTotalSupplierDebitNoteAmount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        
                        var result = command.ExecuteScalar();
                        return result != null ? Convert.ToDecimal(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting total debit note amount: {ex.Message}", ex);
            }
        }

        public int GetDebitNoteCount(DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_GetSupplierDebitNoteCount", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);
                        
                        var result = command.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting debit note count: {ex.Message}", ex);
            }
        }

        public bool ValidateDebitNote(SupplierDebitNote debitNote)
        {
            if (debitNote == null) return false;
            if (string.IsNullOrEmpty(debitNote.DebitNoteNo)) return false;
            if (debitNote.SupplierId <= 0) return false;
            if (debitNote.DebitDate == DateTime.MinValue) return false;
            if (string.IsNullOrEmpty(debitNote.Reason)) return false;
            if (debitNote.TotalAmount <= 0) return false;
            if (debitNote.Items == null || debitNote.Items.Count == 0) return false;
            
            return true;
        }

        public bool CheckDebitNoteNumberExists(string debitNoteNo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("sp_CheckSupplierDebitNoteNumberExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@DebitNoteNo", debitNoteNo);
                        
                        var result = command.ExecuteScalar();
                        return result != null && Convert.ToInt32(result) > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking debit note number exists: {ex.Message}", ex);
            }
        }

        private SupplierDebitNote MapSupplierDebitNoteFromReader(SqlDataReader reader)
        {
            return new SupplierDebitNote
            {
                DebitNoteId = reader.GetInt32(reader.GetOrdinal("DebitNoteId")),
                DebitNoteNo = reader.GetString(reader.GetOrdinal("DebitNoteNo")),
                DebitNoteBarcode = reader.GetString(reader.GetOrdinal("DebitNoteBarcode")),
                SupplierId = reader.GetInt32(reader.GetOrdinal("SupplierId")),
                SupplierName = reader.IsDBNull(reader.GetOrdinal("SupplierName")) ? null : reader.GetString(reader.GetOrdinal("SupplierName")),
                SupplierCode = reader.IsDBNull(reader.GetOrdinal("SupplierCode")) ? null : reader.GetString(reader.GetOrdinal("SupplierCode")),
                ReferencePurchaseId = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("ReferencePurchaseId")),
                ReferencePurchaseNo = reader.IsDBNull(reader.GetOrdinal("ReferencePurchaseNo")) ? null : reader.GetString(reader.GetOrdinal("ReferencePurchaseNo")),
                ReferenceGRNId = reader.IsDBNull(reader.GetOrdinal("ReferenceGRNId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("ReferenceGRNId")),
                ReferenceGRNNo = reader.IsDBNull(reader.GetOrdinal("ReferenceGRNNo")) ? null : reader.GetString(reader.GetOrdinal("ReferenceGRNNo")),
                DebitDate = reader.GetDateTime(reader.GetOrdinal("DebitDate")),
                Reason = reader.GetString(reader.GetOrdinal("Reason")),
                SubTotal = reader.GetDecimal(reader.GetOrdinal("SubTotal")),
                TaxAmount = reader.GetDecimal(reader.GetOrdinal("TaxAmount")),
                DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount")),
                TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                ApprovedBy = reader.IsDBNull(reader.GetOrdinal("ApprovedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("ApprovedBy")),
                ApprovedByName = reader.IsDBNull(reader.GetOrdinal("ApprovedByName")) ? null : reader.GetString(reader.GetOrdinal("ApprovedByName")),
                ApprovalDate = reader.IsDBNull(reader.GetOrdinal("ApprovalDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("ApprovalDate")),
                Remarks = reader.IsDBNull(reader.GetOrdinal("Remarks")) ? null : reader.GetString(reader.GetOrdinal("Remarks")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                CreatedBy = reader.IsDBNull(reader.GetOrdinal("CreatedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("CreatedBy")),
                CreatedByName = reader.IsDBNull(reader.GetOrdinal("CreatedByName")) ? null : reader.GetString(reader.GetOrdinal("CreatedByName")),
                ModifiedDate = reader.IsDBNull(reader.GetOrdinal("ModifiedDate")) ? null : (DateTime?)reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                ModifiedBy = reader.IsDBNull(reader.GetOrdinal("ModifiedBy")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("ModifiedBy")),
                ModifiedByName = reader.IsDBNull(reader.GetOrdinal("ModifiedByName")) ? null : reader.GetString(reader.GetOrdinal("ModifiedByName")),
                BarcodeImage = reader.IsDBNull(reader.GetOrdinal("BarcodeImage")) ? null : (byte[])reader[reader.GetOrdinal("BarcodeImage")]
            };
        }

        private SupplierDebitNoteItem MapSupplierDebitNoteItemFromReader(SqlDataReader reader)
        {
            return new SupplierDebitNoteItem
            {
                DebitNoteItemId = reader.GetInt32(reader.GetOrdinal("DebitNoteItemId")),
                DebitNoteId = reader.GetInt32(reader.GetOrdinal("DebitNoteId")),
                ProductId = reader.GetInt32(reader.GetOrdinal("ProductId")),
                ProductCode = reader.IsDBNull(reader.GetOrdinal("ProductCode")) ? null : reader.GetString(reader.GetOrdinal("ProductCode")),
                ProductName = reader.IsDBNull(reader.GetOrdinal("ProductName")) ? null : reader.GetString(reader.GetOrdinal("ProductName")),
                ProductDescription = reader.IsDBNull(reader.GetOrdinal("ProductDescription")) ? null : reader.GetString(reader.GetOrdinal("ProductDescription")),
                Quantity = reader.GetDecimal(reader.GetOrdinal("Quantity")),
                UnitPrice = reader.GetDecimal(reader.GetOrdinal("UnitPrice")),
                TaxPercentage = reader.GetDecimal(reader.GetOrdinal("TaxPercentage")),
                TaxAmount = reader.GetDecimal(reader.GetOrdinal("TaxAmount")),
                DiscountPercentage = reader.GetDecimal(reader.GetOrdinal("DiscountPercentage")),
                DiscountAmount = reader.GetDecimal(reader.GetOrdinal("DiscountAmount")),
                TotalAmount = reader.GetDecimal(reader.GetOrdinal("TotalAmount")),
                Reason = reader.IsDBNull(reader.GetOrdinal("Reason")) ? null : reader.GetString(reader.GetOrdinal("Reason")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            };
        }
    }
}
