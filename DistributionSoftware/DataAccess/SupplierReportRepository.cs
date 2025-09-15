using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using DistributionSoftware.Models;
using DistributionSoftware.Common;

namespace DistributionSoftware.DataAccess
{
    public class SupplierReportRepository
    {
        private readonly string _connectionString;

        public SupplierReportRepository(string connectionString = null)
        {
            _connectionString = connectionString ?? ConfigurationManager.DistributionConnectionString;
        }

        #region Supplier Ledger Report

        public List<SupplierLedgerReportData> GetSupplierLedgerReportData(
            int? supplierId, DateTime? startDate, DateTime? endDate, string transactionType)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("sp_GetSupplierLedgerReport", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@SupplierId", supplierId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StartDate", startDate ?? DateTime.Now.AddMonths(-1));
                    command.Parameters.AddWithValue("@EndDate", endDate ?? DateTime.Now);
                    command.Parameters.AddWithValue("@TransactionType", string.IsNullOrEmpty(transactionType) ? (object)DBNull.Value : transactionType);

                    var reportData = new List<SupplierLedgerReportData>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reportData.Add(new SupplierLedgerReportData
                            {
                                TransactionId = reader["TransactionId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionId"]),
                                TransactionDate = reader["TransactionDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["TransactionDate"]),
                                TransactionType = reader["TransactionType"] == DBNull.Value ? "" : reader["TransactionType"].ToString(),
                                Description = reader["Description"] == DBNull.Value ? "" : reader["Description"].ToString(),
                                Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                                RunningBalance = reader["RunningBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["RunningBalance"]),
                                ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                                DebitAmount = reader["DebitAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DebitAmount"]),
                                CreditAmount = reader["CreditAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["CreditAmount"]),
                                SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SupplierId"]),
                                SupplierCode = reader["SupplierCode"] == DBNull.Value ? "" : reader["SupplierCode"].ToString(),
                                SupplierName = reader["SupplierName"] == DBNull.Value ? "" : reader["SupplierName"].ToString(),
                                ContactPerson = reader["ContactPerson"] == DBNull.Value ? "" : reader["ContactPerson"].ToString(),
                                Phone = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString(),
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                                City = reader["City"] == DBNull.Value ? "" : reader["City"].ToString(),
                                State = reader["State"] == DBNull.Value ? "" : reader["State"].ToString(),
                                ReferenceDocument = reader["ReferenceDocument"] == DBNull.Value ? "" : reader["ReferenceDocument"].ToString(),
                                TransactionCategory = reader["TransactionCategory"] == DBNull.Value ? "" : reader["TransactionCategory"].ToString(),
                                DaysSinceTransaction = reader["DaysSinceTransaction"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DaysSinceTransaction"]),
                                TransactionStatus = reader["TransactionStatus"] == DBNull.Value ? "" : reader["TransactionStatus"].ToString()
                            });
                        }
                    }
                    return reportData;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("SupplierReportRepository.GetSupplierLedgerReportData", ex);
                throw;
            }
        }

        #endregion

        #region Supplier Balance Report

        public List<SupplierBalanceReportData> GetSupplierBalanceReportData(
            int? supplierId, DateTime? asOfDate, bool showOnlyOutstanding = false)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("sp_GetSupplierBalanceReport", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@SupplierId", supplierId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@AsOfDate", asOfDate ?? DateTime.Now);
                    command.Parameters.AddWithValue("@ShowOnlyOutstanding", showOnlyOutstanding);

                    var reportData = new List<SupplierBalanceReportData>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reportData.Add(new SupplierBalanceReportData
                            {
                                SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SupplierId"]),
                                SupplierCode = reader["SupplierCode"] == DBNull.Value ? "" : reader["SupplierCode"].ToString(),
                                SupplierName = reader["SupplierName"] == DBNull.Value ? "" : reader["SupplierName"].ToString(),
                                ContactPerson = reader["ContactPerson"] == DBNull.Value ? "" : reader["ContactPerson"].ToString(),
                                Phone = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString(),
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                                City = reader["City"] == DBNull.Value ? "" : reader["City"].ToString(),
                                State = reader["State"] == DBNull.Value ? "" : reader["State"].ToString(),
                                PaymentTermsDays = reader["PaymentTermsDays"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["PaymentTermsDays"]),
                                TotalDebits = reader["TotalDebits"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalDebits"]),
                                TotalCredits = reader["TotalCredits"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["TotalCredits"]),
                                CurrentBalance = reader["CurrentBalance"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["CurrentBalance"]),
                                LastTransactionDate = reader["LastTransactionDate"] == DBNull.Value ? null : (DateTime?)reader["LastTransactionDate"],
                                TransactionCount = reader["TransactionCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["TransactionCount"]),
                                DaysOutstanding = reader["DaysOutstanding"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DaysOutstanding"]),
                                AgingCategory = reader["AgingCategory"] == DBNull.Value ? "" : reader["AgingCategory"].ToString(),
                                RiskLevel = reader["RiskLevel"] == DBNull.Value ? "" : reader["RiskLevel"].ToString(),
                                PaymentStatus = reader["PaymentStatus"] == DBNull.Value ? "" : reader["PaymentStatus"].ToString()
                            });
                        }
                    }
                    return reportData;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("SupplierReportRepository.GetSupplierBalanceReportData", ex);
                throw;
            }
        }

        #endregion

        #region Supplier Payment History Report

        public List<SupplierPaymentHistoryReportData> GetSupplierPaymentHistoryReportData(
            int? supplierId, DateTime? startDate, DateTime? endDate, string paymentMethod)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = new SqlCommand("sp_GetSupplierPaymentHistoryReport", connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    command.Parameters.AddWithValue("@SupplierId", supplierId ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@StartDate", startDate ?? DateTime.Now.AddMonths(-3));
                    command.Parameters.AddWithValue("@EndDate", endDate ?? DateTime.Now);
                    command.Parameters.AddWithValue("@PaymentMethod", string.IsNullOrEmpty(paymentMethod) ? (object)DBNull.Value : paymentMethod);

                    var reportData = new List<SupplierPaymentHistoryReportData>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reportData.Add(new SupplierPaymentHistoryReportData
                            {
                                PaymentId = reader["PaymentId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PaymentId"]),
                                PaymentNumber = reader["PaymentNumber"] == DBNull.Value ? "" : reader["PaymentNumber"].ToString(),
                                PaymentDate = reader["PaymentDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["PaymentDate"]),
                                Amount = reader["Amount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["Amount"]),
                                PaymentMethod = reader["PaymentMethod"] == DBNull.Value ? "" : reader["PaymentMethod"].ToString(),
                                ReferenceNumber = reader["ReferenceNumber"] == DBNull.Value ? "" : reader["ReferenceNumber"].ToString(),
                                Notes = reader["Notes"] == DBNull.Value ? "" : reader["Notes"].ToString(),
                                Status = reader["Status"] == DBNull.Value ? "" : reader["Status"].ToString(),
                                CreatedDate = reader["CreatedDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["CreatedDate"]),
                                CreatedBy = reader["CreatedBy"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["CreatedBy"]),
                                SupplierId = reader["SupplierId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["SupplierId"]),
                                SupplierCode = reader["SupplierCode"] == DBNull.Value ? "" : reader["SupplierCode"].ToString(),
                                SupplierName = reader["SupplierName"] == DBNull.Value ? "" : reader["SupplierName"].ToString(),
                                ContactPerson = reader["ContactPerson"] == DBNull.Value ? "" : reader["ContactPerson"].ToString(),
                                Phone = reader["Phone"] == DBNull.Value ? "" : reader["Phone"].ToString(),
                                Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString(),
                                City = reader["City"] == DBNull.Value ? "" : reader["City"].ToString(),
                                State = reader["State"] == DBNull.Value ? "" : reader["State"].ToString(),
                                TransactionId = reader["TransactionId"] == DBNull.Value ? null : (int?)Convert.ToInt32(reader["TransactionId"]),
                                RelatedTransactionDate = reader["RelatedTransactionDate"] == DBNull.Value ? null : (DateTime?)reader["RelatedTransactionDate"],
                                TransactionDescription = reader["TransactionDescription"] == DBNull.Value ? "" : reader["TransactionDescription"].ToString(),
                                RunningBalance = reader["RunningBalance"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(reader["RunningBalance"]),
                                PaymentTypeDescription = reader["PaymentTypeDescription"] == DBNull.Value ? "" : reader["PaymentTypeDescription"].ToString(),
                                PaymentStatusDescription = reader["PaymentStatusDescription"] == DBNull.Value ? "" : reader["PaymentStatusDescription"].ToString(),
                                OutstandingBeforePayment = reader["OutstandingBeforePayment"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["OutstandingBeforePayment"]),
                                PaymentEfficiency = reader["PaymentEfficiency"] == DBNull.Value ? "" : reader["PaymentEfficiency"].ToString(),
                                DaysToPayment = reader["DaysToPayment"] == DBNull.Value ? 0 : Convert.ToInt32(reader["DaysToPayment"])
                            });
                        }
                    }
                    return reportData;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("SupplierReportRepository.GetSupplierPaymentHistoryReportData", ex);
                throw;
            }
        }

        #endregion

        #region Helper Methods

        public List<Supplier> GetActiveSuppliers()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var sql = "SELECT SupplierId, SupplierCode, SupplierName FROM Suppliers WHERE IsActive = 1 ORDER BY SupplierName";
                    
                    var suppliers = new List<Supplier>();
                    using (var command = new SqlCommand(sql, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            suppliers.Add(new Supplier
                            {
                                SupplierId = Convert.ToInt32(reader["SupplierId"]),
                                SupplierCode = reader["SupplierCode"].ToString(),
                                SupplierName = reader["SupplierName"].ToString()
                            });
                        }
                    }
                    return suppliers;
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("SupplierReportRepository.GetActiveSuppliers", ex);
                throw;
            }
        }

        #endregion
    }
}
