using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Service interface for Chart of Accounts business logic operations
    /// </summary>
    public interface IChartOfAccountService
    {
        #region Basic CRUD Operations
        
        /// <summary>
        /// Creates a new chart of account
        /// </summary>
        /// <param name="account">The account to create</param>
        /// <returns>The ID of the created account</returns>
        int CreateChartOfAccount(ChartOfAccount account);
        
        /// <summary>
        /// Updates an existing chart of account
        /// </summary>
        /// <param name="account">The account to update</param>
        /// <returns>True if successful, false otherwise</returns>
        bool UpdateChartOfAccount(ChartOfAccount account);
        
        /// <summary>
        /// Deletes a chart of account
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
        
        #region Business Logic Operations
        
        /// <summary>
        /// Validates chart of account data
        /// </summary>
        /// <param name="account">The account to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        bool ValidateChartOfAccount(ChartOfAccount account);
        
        /// <summary>
        /// Gets validation errors for an account
        /// </summary>
        /// <param name="account">The account to validate</param>
        /// <returns>List of validation errors</returns>
        List<string> GetValidationErrors(ChartOfAccount account);
        
        /// <summary>
        /// Generates next available account code
        /// </summary>
        /// <param name="accountType">The account type</param>
        /// <param name="parentAccountId">The parent account ID</param>
        /// <returns>Next available account code</returns>
        string GenerateAccountCode(string accountType, int? parentAccountId = null);
        
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
        
        /// <summary>
        /// Moves an account to a new parent
        /// </summary>
        /// <param name="accountId">The account ID to move</param>
        /// <param name="newParentAccountId">The new parent account ID</param>
        /// <returns>True if successful, false otherwise</returns>
        bool MoveAccount(int accountId, int? newParentAccountId);
        
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
        
        /// <summary>
        /// Gets account type summary
        /// </summary>
        /// <returns>Summary of accounts by type</returns>
        Dictionary<string, int> GetAccountTypeSummary();
        
        /// <summary>
        /// Gets account category summary
        /// </summary>
        /// <returns>Summary of accounts by category</returns>
        Dictionary<string, int> GetAccountCategorySummary();
        
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
        
        /// <summary>
        /// Imports accounts from external source
        /// </summary>
        /// <param name="accounts">List of accounts to import</param>
        /// <returns>Number of accounts imported</returns>
        int ImportAccounts(List<ChartOfAccount> accounts);
        
        /// <summary>
        /// Exports accounts to external format
        /// </summary>
        /// <param name="accountIds">List of account IDs to export (null for all)</param>
        /// <returns>List of accounts for export</returns>
        List<ChartOfAccount> ExportAccounts(List<int> accountIds = null);
        
        #endregion
        
        #region Account Type and Category Management
        
        /// <summary>
        /// Gets all available account types
        /// </summary>
        /// <returns>List of account types</returns>
        List<string> GetAccountTypes();
        
        /// <summary>
        /// Gets all available account categories
        /// </summary>
        /// <returns>List of account categories</returns>
        List<string> GetAccountCategories();
        
        /// <summary>
        /// Gets account categories by type
        /// </summary>
        /// <param name="accountType">The account type</param>
        /// <returns>List of categories for the specified type</returns>
        List<string> GetAccountCategoriesByType(string accountType);
        
        /// <summary>
        /// Gets normal balance for account type
        /// </summary>
        /// <param name="accountType">The account type</param>
        /// <returns>Normal balance (DEBIT or CREDIT)</returns>
        string GetNormalBalanceForAccountType(string accountType);
        
        #endregion

        #region Enhanced Validation Methods

        /// <summary>
        /// Validates account code format and business rules
        /// </summary>
        /// <param name="accountCode">The account code to validate</param>
        /// <param name="accountType">The account type</param>
        /// <param name="parentAccountId">The parent account ID</param>
        /// <param name="excludeAccountId">Account ID to exclude from uniqueness check</param>
        /// <returns>True if valid</returns>
        bool ValidateAccountCode(string accountCode, string accountType, int? parentAccountId = null, int? excludeAccountId = null);

        /// <summary>
        /// Validates account name uniqueness and business rules
        /// </summary>
        /// <param name="accountName">The account name to validate</param>
        /// <param name="excludeAccountId">Account ID to exclude from uniqueness check</param>
        /// <returns>True if valid</returns>
        bool ValidateAccountName(string accountName, int? excludeAccountId = null);

        /// <summary>
        /// Validates account type and category compatibility
        /// </summary>
        /// <param name="accountType">The account type</param>
        /// <param name="accountCategory">The account category</param>
        /// <returns>True if valid</returns>
        bool ValidateAccountTypeAndCategory(string accountType, string accountCategory);

        /// <summary>
        /// Validates account hierarchy depth and business rules
        /// </summary>
        /// <param name="parentAccountId">The parent account ID</param>
        /// <param name="maxDepth">Maximum allowed depth</param>
        /// <returns>True if valid</returns>
        bool ValidateAccountHierarchyDepth(int? parentAccountId, int maxDepth = 5);

        /// <summary>
        /// Validates if account can be deleted based on business rules
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <returns>True if can be deleted</returns>
        bool CanDeleteAccount(int accountId);

        /// <summary>
        /// Validates account for transaction usage
        /// </summary>
        /// <param name="accountId">The account ID</param>
        /// <param name="transactionType">The transaction type</param>
        /// <returns>True if valid for transaction</returns>
        bool ValidateAccountForTransaction(int accountId, string transactionType);

        /// <summary>
        /// Gets comprehensive validation errors for an account
        /// </summary>
        /// <param name="account">The account to validate</param>
        /// <returns>List of validation errors</returns>
        List<string> GetComprehensiveValidationErrors(ChartOfAccount account);

        #endregion
        
        #region Default Account Methods
        
        /// <summary>
        /// Gets the default bank account
        /// </summary>
        /// <returns>Default bank account</returns>
        ChartOfAccount GetDefaultBankAccount();
        
        /// <summary>
        /// Gets the default cash account
        /// </summary>
        /// <returns>Default cash account</returns>
        ChartOfAccount GetDefaultCashAccount();
        
        /// <summary>
        /// Gets the default mobile account
        /// </summary>
        /// <returns>Default mobile account</returns>
        ChartOfAccount GetDefaultMobileAccount();
        
        #endregion
    }
}
