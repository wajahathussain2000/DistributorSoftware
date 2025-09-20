using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class PurchaseInvoiceRepository : IPurchaseInvoiceRepository
    {
        private readonly string _connectionString;

        public PurchaseInvoiceRepository()
        {
            _connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
        }

        public PurchaseInvoiceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreatePurchaseInvoice(PurchaseInvoice invoice)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            INSERT INTO PurchaseInvoices (
                                InvoiceNumber, SupplierId, InvoiceDate, DueDate, 
                                SubTotal, TaxAmount, DiscountAmount, TotalAmount, 
                                PaidAmount, BalanceAmount, PaymentMode, Status, 
                                Remarks, CreatedDate, CreatedBy, TaxableAmount, 
                                TaxPercentage, DiscountPercentage, ChangeAmount, 
                                Barcode, BarcodeImage, PrintStatus, PrintDate, 
                                CashierId, TransactionTime, PurchaseAccountId, 
                                CashAccountId, PayableAccountId, TaxAccountId, 
                                JournalVoucherId, TaxCategoryId, PricingRuleId, 
                                DiscountRuleId
                            ) VALUES (
                                @InvoiceNumber, @SupplierId, @InvoiceDate, @DueDate, 
                                @SubTotal, @TaxAmount, @DiscountAmount, @TotalAmount, 
                                @PaidAmount, @BalanceAmount, @PaymentMode, @Status, 
                                @Remarks, @CreatedDate, @CreatedBy, @TaxableAmount, 
                                @TaxPercentage, @DiscountPercentage, @ChangeAmount, 
                                @Barcode, @BarcodeImage, @PrintStatus, @PrintDate, 
                                @CashierId, @TransactionTime, @PurchaseAccountId, 
                                @CashAccountId, @PayableAccountId, @TaxAccountId, 
                                @JournalVoucherId, @TaxCategoryId, @PricingRuleId, 
                                @DiscountRuleId
                            );
                            SELECT SCOPE_IDENTITY();";

                        command.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                        command.Parameters.AddWithValue("@SupplierId", invoice.SupplierId);
                        command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                        command.Parameters.AddWithValue("@DueDate", invoice.DueDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
                        command.Parameters.AddWithValue("@TaxAmount", invoice.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountAmount", invoice.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                        command.Parameters.AddWithValue("@PaidAmount", invoice.PaidAmount);
                        command.Parameters.AddWithValue("@BalanceAmount", invoice.BalanceAmount);
                        command.Parameters.AddWithValue("@PaymentMode", invoice.PaymentMode);
                        command.Parameters.AddWithValue("@Status", invoice.Status);
                        command.Parameters.AddWithValue("@Remarks", invoice.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CreatedDate", invoice.CreatedDate);
                        command.Parameters.AddWithValue("@CreatedBy", invoice.CreatedBy);
                        command.Parameters.AddWithValue("@TaxableAmount", invoice.TaxableAmount);
                        command.Parameters.AddWithValue("@TaxPercentage", invoice.TaxPercentage);
                        command.Parameters.AddWithValue("@DiscountPercentage", invoice.DiscountPercentage);
                        command.Parameters.AddWithValue("@ChangeAmount", invoice.ChangeAmount);
                        command.Parameters.AddWithValue("@Barcode", invoice.Barcode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BarcodeImage", invoice.BarcodeImage ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PrintStatus", invoice.PrintStatus);
                        command.Parameters.AddWithValue("@PrintDate", invoice.PrintDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CashierId", invoice.CashierId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TransactionTime", invoice.TransactionTime ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PurchaseAccountId", invoice.PurchaseAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CashAccountId", invoice.CashAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PayableAccountId", invoice.PayableAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TaxAccountId", invoice.TaxAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@JournalVoucherId", invoice.JournalVoucherId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TaxCategoryId", invoice.TaxCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PricingRuleId", invoice.PricingRuleId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DiscountRuleId", invoice.DiscountRuleId ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.CreatePurchaseInvoice", ex);
                throw;
            }
        }

        public bool UpdatePurchaseInvoice(PurchaseInvoice invoice)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = @"
                            UPDATE PurchaseInvoices SET 
                                InvoiceNumber = @InvoiceNumber,
                                SupplierId = @SupplierId,
                                InvoiceDate = @InvoiceDate,
                                DueDate = @DueDate,
                                SubTotal = @SubTotal,
                                TaxAmount = @TaxAmount,
                                DiscountAmount = @DiscountAmount,
                                TotalAmount = @TotalAmount,
                                PaidAmount = @PaidAmount,
                                BalanceAmount = @BalanceAmount,
                                PaymentMode = @PaymentMode,
                                Status = @Status,
                                Remarks = @Remarks,
                                ModifiedDate = @ModifiedDate,
                                ModifiedBy = @ModifiedBy,
                                TaxableAmount = @TaxableAmount,
                                TaxPercentage = @TaxPercentage,
                                DiscountPercentage = @DiscountPercentage,
                                ChangeAmount = @ChangeAmount,
                                Barcode = @Barcode,
                                BarcodeImage = @BarcodeImage,
                                PrintStatus = @PrintStatus,
                                PrintDate = @PrintDate,
                                CashierId = @CashierId,
                                TransactionTime = @TransactionTime,
                                PurchaseAccountId = @PurchaseAccountId,
                                CashAccountId = @CashAccountId,
                                PayableAccountId = @PayableAccountId,
                                TaxAccountId = @TaxAccountId,
                                JournalVoucherId = @JournalVoucherId,
                                TaxCategoryId = @TaxCategoryId,
                                PricingRuleId = @PricingRuleId,
                                DiscountRuleId = @DiscountRuleId
                            WHERE PurchaseInvoiceId = @PurchaseInvoiceId";

                        command.Parameters.AddWithValue("@PurchaseInvoiceId", invoice.PurchaseInvoiceId);
                        command.Parameters.AddWithValue("@InvoiceNumber", invoice.InvoiceNumber);
                        command.Parameters.AddWithValue("@SupplierId", invoice.SupplierId);
                        command.Parameters.AddWithValue("@InvoiceDate", invoice.InvoiceDate);
                        command.Parameters.AddWithValue("@DueDate", invoice.DueDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@SubTotal", invoice.SubTotal);
                        command.Parameters.AddWithValue("@TaxAmount", invoice.TaxAmount);
                        command.Parameters.AddWithValue("@DiscountAmount", invoice.DiscountAmount);
                        command.Parameters.AddWithValue("@TotalAmount", invoice.TotalAmount);
                        command.Parameters.AddWithValue("@PaidAmount", invoice.PaidAmount);
                        command.Parameters.AddWithValue("@BalanceAmount", invoice.BalanceAmount);
                        command.Parameters.AddWithValue("@PaymentMode", invoice.PaymentMode);
                        command.Parameters.AddWithValue("@Status", invoice.Status);
                        command.Parameters.AddWithValue("@Remarks", invoice.Remarks ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedDate", invoice.ModifiedDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ModifiedBy", invoice.ModifiedBy ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TaxableAmount", invoice.TaxableAmount);
                        command.Parameters.AddWithValue("@TaxPercentage", invoice.TaxPercentage);
                        command.Parameters.AddWithValue("@DiscountPercentage", invoice.DiscountPercentage);
                        command.Parameters.AddWithValue("@ChangeAmount", invoice.ChangeAmount);
                        command.Parameters.AddWithValue("@Barcode", invoice.Barcode ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@BarcodeImage", invoice.BarcodeImage ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PrintStatus", invoice.PrintStatus);
                        command.Parameters.AddWithValue("@PrintDate", invoice.PrintDate ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CashierId", invoice.CashierId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TransactionTime", invoice.TransactionTime ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PurchaseAccountId", invoice.PurchaseAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@CashAccountId", invoice.CashAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PayableAccountId", invoice.PayableAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TaxAccountId", invoice.TaxAccountId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@JournalVoucherId", invoice.JournalVoucherId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@TaxCategoryId", invoice.TaxCategoryId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@PricingRuleId", invoice.PricingRuleId ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@DiscountRuleId", invoice.DiscountRuleId ?? (object)DBNull.Value);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.UpdatePurchaseInvoice", ex);
                throw;
            }
        }

        public bool DeletePurchaseInvoice(int invoiceId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "DELETE FROM PurchaseInvoices WHERE PurchaseInvoiceId = @PurchaseInvoiceId";
                        command.Parameters.AddWithValue("@PurchaseInvoiceId", invoiceId);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.DeletePurchaseInvoice", ex);
                throw;
            }
        }

        public PurchaseInvoice GetPurchaseInvoiceById(int invoiceId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM PurchaseInvoices WHERE PurchaseInvoiceId = @PurchaseInvoiceId";
                        command.Parameters.AddWithValue("@PurchaseInvoiceId", invoiceId);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapPurchaseInvoice(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.GetPurchaseInvoiceById", ex);
                throw;
            }
        }

        public PurchaseInvoice GetPurchaseInvoiceByNumber(string invoiceNumber)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM PurchaseInvoices WHERE InvoiceNumber = @InvoiceNumber";
                        command.Parameters.AddWithValue("@InvoiceNumber", invoiceNumber);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapPurchaseInvoice(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.GetPurchaseInvoiceByNumber", ex);
                throw;
            }
        }

        public List<PurchaseInvoice> GetAllPurchaseInvoices()
        {
            try
            {
                var invoices = new List<PurchaseInvoice>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM PurchaseInvoices ORDER BY InvoiceDate DESC";

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                invoices.Add(MapPurchaseInvoice(reader));
                            }
                        }
                    }
                }
                return invoices;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.GetAllPurchaseInvoices", ex);
                throw;
            }
        }

        public List<PurchaseInvoice> GetPurchaseInvoicesByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                var invoices = new List<PurchaseInvoice>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM PurchaseInvoices WHERE InvoiceDate BETWEEN @StartDate AND @EndDate ORDER BY InvoiceDate DESC";
                        command.Parameters.AddWithValue("@StartDate", startDate);
                        command.Parameters.AddWithValue("@EndDate", endDate);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                invoices.Add(MapPurchaseInvoice(reader));
                            }
                        }
                    }
                }
                return invoices;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.GetPurchaseInvoicesByDateRange", ex);
                throw;
            }
        }

        public List<PurchaseInvoice> GetPurchaseInvoicesBySupplier(int supplierId)
        {
            try
            {
                var invoices = new List<PurchaseInvoice>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM PurchaseInvoices WHERE SupplierId = @SupplierId ORDER BY InvoiceDate DESC";
                        command.Parameters.AddWithValue("@SupplierId", supplierId);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                invoices.Add(MapPurchaseInvoice(reader));
                            }
                        }
                    }
                }
                return invoices;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.GetPurchaseInvoicesBySupplier", ex);
                throw;
            }
        }

        public string GeneratePurchaseInvoiceNumber()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT COUNT(*) FROM PurchaseInvoices WHERE YEAR(InvoiceDate) = YEAR(GETDATE())";
                        var count = Convert.ToInt32(command.ExecuteScalar()) + 1;
                        return $"PINV{DateTime.Now:yyyyMM}{count:D4}";
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceRepository.GeneratePurchaseInvoiceNumber", ex);
                throw;
            }
        }

        private PurchaseInvoice MapPurchaseInvoice(SqlDataReader reader)
        {
            return new PurchaseInvoice
            {
                PurchaseInvoiceId = Convert.ToInt32(reader["PurchaseInvoiceId"]),
                InvoiceNumber = reader["InvoiceNumber"].ToString(),
                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                InvoiceDate = Convert.ToDateTime(reader["InvoiceDate"]),
                DueDate = reader["DueDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["DueDate"]),
                SubTotal = Convert.ToDecimal(reader["SubTotal"]),
                TaxAmount = Convert.ToDecimal(reader["TaxAmount"]),
                DiscountAmount = Convert.ToDecimal(reader["DiscountAmount"]),
                TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                PaidAmount = Convert.ToDecimal(reader["PaidAmount"]),
                BalanceAmount = Convert.ToDecimal(reader["BalanceAmount"]),
                PaymentMode = reader["PaymentMode"].ToString(),
                Status = reader["Status"].ToString(),
                Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                CreatedBy = Convert.ToInt32(reader["CreatedBy"]),
                ModifiedDate = reader["ModifiedDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["ModifiedDate"]),
                ModifiedBy = reader["ModifiedBy"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["ModifiedBy"]),
                TaxableAmount = Convert.ToDecimal(reader["TaxableAmount"]),
                TaxPercentage = Convert.ToDecimal(reader["TaxPercentage"]),
                DiscountPercentage = Convert.ToDecimal(reader["DiscountPercentage"]),
                ChangeAmount = Convert.ToDecimal(reader["ChangeAmount"]),
                Barcode = reader["Barcode"] == DBNull.Value ? null : reader["Barcode"].ToString(),
                BarcodeImage = reader["BarcodeImage"] == DBNull.Value ? null : reader["BarcodeImage"].ToString(),
                PrintStatus = Convert.ToBoolean(reader["PrintStatus"]),
                PrintDate = reader["PrintDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["PrintDate"]),
                CashierId = reader["CashierId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CashierId"]),
                TransactionTime = reader["TransactionTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["TransactionTime"]),
                PurchaseAccountId = reader["PurchaseAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PurchaseAccountId"]),
                CashAccountId = reader["CashAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["CashAccountId"]),
                PayableAccountId = reader["PayableAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PayableAccountId"]),
                TaxAccountId = reader["TaxAccountId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TaxAccountId"]),
                JournalVoucherId = reader["JournalVoucherId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["JournalVoucherId"]),
                TaxCategoryId = reader["TaxCategoryId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["TaxCategoryId"]),
                PricingRuleId = reader["PricingRuleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["PricingRuleId"]),
                DiscountRuleId = reader["DiscountRuleId"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["DiscountRuleId"])
            };
        }
    }
}
