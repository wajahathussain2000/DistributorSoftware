using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistributionSoftware.Models;

namespace DistributionSoftware.DataAccess
{
    /// <summary>
    /// Repository interface for Chart of Accounts data access operations
    /// </summary>
    public interface IChartOfAccountRepository
    {
        #region Basic CRUD Operations
        
        /// <summary>
        /// Creates a new chart of account entry
        /// </summary>
        /// <param name="account">The account to create</param>
        /// <returns>The ID of the created account</returns>
        int CreateChartOfAccount(ChartOfAccount account);
        
        /// <summary>
        /// Updates an existing chart of account entry
        /// </summary>
        /// <param name="account">The account to update</param>
        /// <returns>True if successful, false otherwise</returns>
        bool UpdateChartOfAccount(ChartOfAccount account);
        
        /// <summary>
        /// Deletes a chart of account entry
        /// </summary>
        /// <param name="accountId">The ID of the account to delete</param>
        /// <returns>True if successful, false otherwise</returns>
        bool DeleteChartOfAccount(int accountId);
        
        /// <summary>
        /// Gets a chart of account by ID
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <returns>The account if found, null otherwise</returns>
        ChartOfAccount GetChartOfAccountById(int accountId);
        
        /// <summary>
        /// Gets a chart of account by account code
        /// </summary>
        /// <param name="accountCode">The account code</param>
        /// <returns>The account if found, null otherwise</returns>
        ChartOfAccount GetChartOfAccountByCode(string accountCode);
        
        /// <summary>
        /// Gets all chart of accounts
        /// </summary>
        /// <returns>List of all accounts</returns>
        List<ChartOfAccount> GetAllChartOfAccounts();
        
        #endregion
        
        #region Filtered Retrieval Operations
        
        /// <summary>
        /// Gets accounts by account type
        /// </summary>
        /// <param name="accountType">The account type (ASSET, LIABILITY, EQUITY, REVENUE, EXPENSE)</param>
        /// <returns>List of accounts of the specified type</returns>
        List<ChartOfAccount> GetChartOfAccountsByType(string accountType);
        
        /// <summary>
        /// Gets accounts by account category
        /// </summary>
        /// <param name="accountCategory">The account category</param>
        /// <returns>List of accounts of the specified category</returns>
        List<ChartOfAccount> GetChartOfAccountsByCategory(string accountCategory);
        
        /// <summary>
        /// Gets active accounts only
        /// </summary>
        /// <returns>List of active accounts</returns>
        List<ChartOfAccount> GetActiveChartOfAccounts();
        
        /// <summary>
        /// Gets accounts by parent account ID
        /// </summary>
        /// <param name="parentAccountId">The parent account ID</param>
        /// <returns>List of child accounts</returns>
        List<ChartOfAccount> GetChartOfAccountsByParent(int parentAccountId);
        
        /// <summary>
        /// Gets root level accounts (no parent)
        /// </summary>
        /// <returns>List of root level accounts</returns>
        List<ChartOfAccount> GetRootLevelAccounts();
        
        /// <summary>
        /// Gets accounts by account level
        /// </summary>
        /// <param name="accountLevel">The account level</param>
        /// <returns>List of accounts at the specified level</returns>
        List<ChartOfAccount> GetChartOfAccountsByLevel(int accountLevel);
        
        #endregion
        
        #region Search and Validation Operations
        
        /// <summary>
        /// Searches accounts by name or code
        /// </summary>
        /// <param name="searchTerm">The search term</param>
        /// <returns>List of matching accounts</returns>
        List<ChartOfAccount> SearchChartOfAccounts(string searchTerm);
        
        /// <summary>
        /// Checks if account code exists
        /// </summary>
        /// <param name="accountCode">The account code to check</param>
        /// <param name="excludeAccountId">Account ID to exclude from check (for updates)</param>
        /// <returns>True if exists, false otherwise</returns>
        bool AccountCodeExists(string accountCode, int? excludeAccountId = null);
        
        /// <summary>
        /// Checks if account name exists
        /// </summary>
        /// <param name="accountName">The account name to check</param>
        /// <param name="excludeAccountId">Account ID to exclude from check (for updates)</param>
        /// <returns>True if exists, false otherwise</returns>
        bool AccountNameExists(string accountName, int? excludeAccountId = null);
        
        /// <summary>
        /// Validates account hierarchy
        /// </summary>
        /// <param name="accountId">The account ID to validate</param>
        /// <param name="parentAccountId">The proposed parent account ID</param>
        /// <returns>True if valid hierarchy, false otherwise</returns>
        bool ValidateAccountHierarchy(int accountId, int? parentAccountId);
        
        #endregion
        
        #region Hierarchical Operations
        
        /// <summary>
        /// Gets the full account hierarchy
        /// </summary>
        /// <returns>List of accounts with hierarchy information</returns>
        List<ChartOfAccount> GetAccountHierarchy();
        
        /// <summary>
        /// Gets account path (parent > child > grandchild)
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <returns>Account path string</returns>
        string GetAccountPath(int accountId);
        
        /// <summary>
        /// Gets all descendant accounts
        /// </summary>
        /// <param name="parentAccountId">The parent account ID</param>
        /// <returns>List of all descendant accounts</returns>
        List<ChartOfAccount> GetAllDescendantAccounts(int parentAccountId);
        
        /// <summary>
        /// Gets account tree structure
        /// </summary>
        /// <param name="rootAccountId">The root account ID (null for all roots)</param>
        /// <returns>Account tree structure</returns>
        List<ChartOfAccount> GetAccountTree(int? rootAccountId = null);
        
        #endregion
        
        #region Business Logic Operations
        
        /// <summary>
        /// Gets accounts suitable for journal entry debit
        /// </summary>
        /// <returns>List of accounts that can be debited</returns>
        List<ChartOfAccount> GetDebitAccounts();
        
        /// <summary>
        /// Gets accounts suitable for journal entry credit
        /// </summary>
        /// <returns>List of accounts that can be credited</returns>
        List<ChartOfAccount> GetCreditAccounts();
        
        /// <summary>
        /// Gets accounts by normal balance
        /// </summary>
        /// <param name="normalBalance">The normal balance (DEBIT or CREDIT)</param>
        /// <returns>List of accounts with the specified normal balance</returns>
        List<ChartOfAccount> GetAccountsByNormalBalance(string normalBalance);
        
        /// <summary>
        /// Gets system accounts (cannot be deleted)
        /// </summary>
        /// <returns>List of system accounts</returns>
        List<ChartOfAccount> GetSystemAccounts();
        
        /// <summary>
        /// Gets user-defined accounts (can be deleted)
        /// </summary>
        /// <returns>List of user-defined accounts</returns>
        List<ChartOfAccount> GetUserDefinedAccounts();
        
        #endregion
        
        #region Reporting Operations
        
        /// <summary>
        /// Gets account summary for reporting
        /// </summary>
        /// <returns>Account summary data</returns>
        List<ChartOfAccount> GetAccountSummary();
        
        /// <summary>
        /// Gets accounts with transaction counts
        /// </summary>
        /// <returns>Accounts with usage statistics</returns>
        List<ChartOfAccount> GetAccountsWithUsageStats();
        
        /// <summary>
        /// Gets inactive accounts
        /// </summary>
        /// <returns>List of inactive accounts</returns>
        List<ChartOfAccount> GetInactiveAccounts();
        
        #endregion
        
        #region Bulk Operations
        
        /// <summary>
        /// Bulk updates account status
        /// </summary>
        /// <param name="accountIds">List of account IDs</param>
        /// <param name="isActive">New active status</param>
        /// <returns>Number of accounts updated</returns>
        int BulkUpdateAccountStatus(List<int> accountIds, bool isActive);
        
        /// <summary>
        /// Bulk deletes accounts (only user-defined accounts)
        /// </summary>
        /// <param name="accountIds">List of account IDs to delete</param>
        /// <returns>Number of accounts deleted</returns>
        int BulkDeleteAccounts(List<int> accountIds);
        
        #endregion
    }
}
