using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    /// <summary>
    /// Service implementation for Chart of Accounts business logic operations
    /// </summary>
    public class ChartOfAccountService : IChartOfAccountService
    {
        private readonly IChartOfAccountRepository _chartOfAccountRepository;

        public ChartOfAccountService()
        {
            _chartOfAccountRepository = new ChartOfAccountRepository();
        }

        public ChartOfAccountService(IChartOfAccountRepository chartOfAccountRepository)
        {
            _chartOfAccountRepository = chartOfAccountRepository;
        }

        #region Basic CRUD Operations

        public int CreateChartOfAccount(ChartOfAccount account)
        {
            try
            {
                // Validate account data
                if (!ValidateChartOfAccount(account))
                {
                    var errors = GetValidationErrors(account);
                    throw new ArgumentException($"Invalid account data: {string.Join(", ", errors)}");
                }

                // Check for duplicate account code
                if (AccountCodeExists(account.AccountCode))
                {
                    throw new InvalidOperationException($"Account code '{account.AccountCode}' already exists.");
                }

                // Check for duplicate account name
                if (AccountNameExists(account.AccountName))
                {
                    throw new InvalidOperationException($"Account name '{account.AccountName}' already exists.");
                }

                // Validate hierarchy if parent is specified
                if (account.ParentAccountId.HasValue)
                {
                    if (!ValidateAccountHierarchy(0, account.ParentAccountId))
                    {
                        throw new InvalidOperationException("Invalid account hierarchy.");
                    }
                }

                // Set audit fields
                account.CreatedDate = DateTime.Now;
                account.CreatedBy = UserSession.CurrentUserId;
                account.CreatedByName = UserSession.GetDisplayName();

                // Generate account code if not provided
                if (string.IsNullOrEmpty(account.AccountCode))
                {
                    account.AccountCode = GenerateAccountCode(account.AccountType, account.ParentAccountId);
                }

                // Set account level
                if (account.ParentAccountId.HasValue)
                {
                    var parentAccount = GetChartOfAccountById(account.ParentAccountId.Value);
                    if (parentAccount != null)
                    {
                        account.AccountLevel = parentAccount.AccountLevel + 1;
                    }
                }
                else
                {
                    account.AccountLevel = 1;
                }

                // Set default values
                if (string.IsNullOrEmpty(account.NormalBalance))
                {
                    account.NormalBalance = GetNormalBalanceForAccountType(account.AccountType);
                }

                return _chartOfAccountRepository.CreateChartOfAccount(account);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.CreateChartOfAccount", ex);
                throw;
            }
        }

        public bool UpdateChartOfAccount(ChartOfAccount account)
        {
            try
            {
                // Validate account data
                if (!ValidateChartOfAccount(account))
                {
                    var errors = GetValidationErrors(account);
                    throw new ArgumentException($"Invalid account data: {string.Join(", ", errors)}");
                }

                // Check for duplicate account code (excluding current account)
                if (AccountCodeExists(account.AccountCode, account.AccountId))
                {
                    throw new InvalidOperationException($"Account code '{account.AccountCode}' already exists.");
                }

                // Check for duplicate account name (excluding current account)
                if (AccountNameExists(account.AccountName, account.AccountId))
                {
                    throw new InvalidOperationException($"Account name '{account.AccountName}' already exists.");
                }

                // Validate hierarchy if parent is specified
                if (account.ParentAccountId.HasValue)
                {
                    if (!ValidateAccountHierarchy(account.AccountId, account.ParentAccountId))
                    {
                        throw new InvalidOperationException("Invalid account hierarchy.");
                    }
                }

                // Set audit fields
                account.ModifiedDate = DateTime.Now;
                account.ModifiedBy = UserSession.CurrentUserId;
                account.ModifiedByName = UserSession.GetDisplayName();

                // Update account level if parent changed
                if (account.ParentAccountId.HasValue)
                {
                    var parentAccount = GetChartOfAccountById(account.ParentAccountId.Value);
                    if (parentAccount != null)
                    {
                        account.AccountLevel = parentAccount.AccountLevel + 1;
                    }
                }
                else
                {
                    account.AccountLevel = 1;
                }

                return _chartOfAccountRepository.UpdateChartOfAccount(account);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.UpdateChartOfAccount", ex);
                throw;
            }
        }

        public bool DeleteChartOfAccount(int accountId)
        {
            try
            {
                var account = GetChartOfAccountById(accountId);
                if (account == null)
                {
                    throw new ArgumentException("Account not found.");
                }

                // Check if it's a system account
                if (account.IsSystemAccount)
                {
                    throw new InvalidOperationException("Cannot delete system accounts.");
                }

                // Check if account has child accounts
                var childAccounts = GetChartOfAccountsByParent(accountId);
                if (childAccounts.Any())
                {
                    throw new InvalidOperationException("Cannot delete account with child accounts. Please delete child accounts first.");
                }

                // TODO: Check if account has transactions
                // This would require integration with journal entries

                return _chartOfAccountRepository.DeleteChartOfAccount(accountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.DeleteChartOfAccount", ex);
                throw;
            }
        }

        public ChartOfAccount GetChartOfAccountById(int accountId)
        {
            try
            {
                return _chartOfAccountRepository.GetChartOfAccountById(accountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetChartOfAccountById", ex);
                throw;
            }
        }

        public ChartOfAccount GetChartOfAccountByCode(string accountCode)
        {
            try
            {
                return _chartOfAccountRepository.GetChartOfAccountByCode(accountCode);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetChartOfAccountByCode", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAllChartOfAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetAllChartOfAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAllChartOfAccounts", ex);
                throw;
            }
        }

        #endregion

        #region Filtered Retrieval Operations

        public List<ChartOfAccount> GetChartOfAccountsByType(string accountType)
        {
            try
            {
                return _chartOfAccountRepository.GetChartOfAccountsByType(accountType);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetChartOfAccountsByType", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetChartOfAccountsByCategory(string accountCategory)
        {
            try
            {
                return _chartOfAccountRepository.GetChartOfAccountsByCategory(accountCategory);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetChartOfAccountsByCategory", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetActiveChartOfAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetActiveChartOfAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetActiveChartOfAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetChartOfAccountsByParent(int parentAccountId)
        {
            try
            {
                return _chartOfAccountRepository.GetChartOfAccountsByParent(parentAccountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetChartOfAccountsByParent", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetRootLevelAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetRootLevelAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetRootLevelAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetChartOfAccountsByLevel(int accountLevel)
        {
            try
            {
                return _chartOfAccountRepository.GetChartOfAccountsByLevel(accountLevel);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetChartOfAccountsByLevel", ex);
                throw;
            }
        }

        #endregion

        #region Search and Validation Operations

        public List<ChartOfAccount> SearchChartOfAccounts(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return GetActiveChartOfAccounts();
                }

                return _chartOfAccountRepository.SearchChartOfAccounts(searchTerm);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.SearchChartOfAccounts", ex);
                throw;
            }
        }

        public bool AccountCodeExists(string accountCode, int? excludeAccountId = null)
        {
            try
            {
                return _chartOfAccountRepository.AccountCodeExists(accountCode, excludeAccountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.IsAccountCodeUnique", ex);
                throw;
            }
        }

        public bool AccountNameExists(string accountName, int? excludeAccountId = null)
        {
            try
            {
                return _chartOfAccountRepository.AccountNameExists(accountName, excludeAccountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.IsAccountNameUnique", ex);
                throw;
            }
        }

        public bool ValidateAccountHierarchy(int accountId, int? parentAccountId)
        {
            try
            {
                // Check for circular reference
                if (parentAccountId.HasValue && accountId > 0)
                {
                    if (accountId == parentAccountId.Value)
                    {
                        return false; // Cannot be parent of itself
                    }

                    // Check if parent is a descendant of the account
                    var descendants = GetAllDescendantAccounts(accountId);
                    if (descendants.Any(d => d.AccountId == parentAccountId.Value))
                    {
                        return false; // Circular reference
                    }
                }

                return _chartOfAccountRepository.ValidateAccountHierarchy(accountId, parentAccountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ValidateAccountHierarchy", ex);
                throw;
            }
        }

        #endregion

        #region Business Logic Operations

        public bool ValidateChartOfAccount(ChartOfAccount account)
        {
            try
            {
                if (account == null) return false;

                var errors = GetValidationErrors(account);
                return !errors.Any();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ValidateChartOfAccount", ex);
                return false;
            }
        }

        public List<string> GetValidationErrors(ChartOfAccount account)
        {
            var errors = new List<string>();

            if (account == null)
            {
                errors.Add("Account cannot be null");
                return errors;
            }

            if (string.IsNullOrWhiteSpace(account.AccountCode))
                errors.Add("Account Code is required");

            if (string.IsNullOrWhiteSpace(account.AccountName))
                errors.Add("Account Name is required");

            if (string.IsNullOrWhiteSpace(account.AccountType))
                errors.Add("Account Type is required");

            if (string.IsNullOrWhiteSpace(account.AccountCategory))
                errors.Add("Account Category is required");

            if (string.IsNullOrWhiteSpace(account.NormalBalance))
                errors.Add("Normal Balance is required");

            if (account.AccountLevel < 1)
                errors.Add("Account Level must be at least 1");

            // Validate account code format
            if (!string.IsNullOrWhiteSpace(account.AccountCode))
            {
                if (account.AccountCode.Length > 20)
                    errors.Add("Account Code cannot exceed 20 characters");

                if (!System.Text.RegularExpressions.Regex.IsMatch(account.AccountCode, @"^[0-9]+$"))
                    errors.Add("Account Code must contain only numbers");
            }

            // Validate account name length
            if (!string.IsNullOrWhiteSpace(account.AccountName) && account.AccountName.Length > 100)
                errors.Add("Account Name cannot exceed 100 characters");

            // Validate account type
            var validAccountTypes = GetAccountTypes();
            if (!string.IsNullOrWhiteSpace(account.AccountType) && !validAccountTypes.Contains(account.AccountType))
                errors.Add("Invalid Account Type");

            // Validate account category
            var validCategories = GetAccountCategories();
            if (!string.IsNullOrWhiteSpace(account.AccountCategory) && !validCategories.Contains(account.AccountCategory))
                errors.Add("Invalid Account Category");

            // Validate normal balance
            if (!string.IsNullOrWhiteSpace(account.NormalBalance) && 
                account.NormalBalance != "DEBIT" && account.NormalBalance != "CREDIT")
                errors.Add("Normal Balance must be either DEBIT or CREDIT");

            return errors;
        }

        public string GenerateAccountCode(string accountType, int? parentAccountId = null)
        {
            try
            {
                var accounts = GetChartOfAccountsByType(accountType);
                var maxCode = 0;

                foreach (var account in accounts)
                {
                    if (int.TryParse(account.AccountCode, out int code))
                    {
                        maxCode = Math.Max(maxCode, code);
                    }
                }

                // Generate next code based on account type
                var baseCode = GetBaseCodeForAccountType(accountType);
                var nextCode = maxCode + 10; // Increment by 10 to leave room for sub-accounts

                return nextCode.ToString();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GenerateAccountCode", ex);
                return "1000"; // Default fallback
            }
        }

        public List<ChartOfAccount> GetDebitAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetDebitAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetDebitAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetCreditAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetCreditAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetCreditAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAccountsByNormalBalance(string normalBalance)
        {
            try
            {
                return _chartOfAccountRepository.GetAccountsByNormalBalance(normalBalance);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountsByNormalBalance", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetSystemAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetSystemAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetSystemAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetUserDefinedAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetUserDefinedAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetUserDefinedAccounts", ex);
                throw;
            }
        }

        #endregion

        #region Hierarchical Operations

        public List<ChartOfAccount> GetAccountHierarchy()
        {
            try
            {
                return _chartOfAccountRepository.GetAccountHierarchy();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountHierarchy", ex);
                throw;
            }
        }

        public string GetAccountPath(int accountId)
        {
            try
            {
                return _chartOfAccountRepository.GetAccountPath(accountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountPath", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAllDescendantAccounts(int parentAccountId)
        {
            try
            {
                return _chartOfAccountRepository.GetAllDescendantAccounts(parentAccountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetDescendantAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAccountTree(int? rootAccountId = null)
        {
            try
            {
                return _chartOfAccountRepository.GetAccountTree(rootAccountId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountTree", ex);
                throw;
            }
        }

        public bool MoveAccount(int accountId, int? newParentAccountId)
        {
            try
            {
                var account = GetChartOfAccountById(accountId);
                if (account == null)
                {
                    throw new ArgumentException("Account not found.");
                }

                // Validate new hierarchy
                if (!ValidateAccountHierarchy(accountId, newParentAccountId))
                {
                    throw new InvalidOperationException("Invalid account hierarchy.");
                }

                // Update account
                account.ParentAccountId = newParentAccountId;
                
                // Update account level
                if (newParentAccountId.HasValue)
                {
                    var parentAccount = GetChartOfAccountById(newParentAccountId.Value);
                    if (parentAccount != null)
                    {
                        account.AccountLevel = parentAccount.AccountLevel + 1;
                    }
                }
                else
                {
                    account.AccountLevel = 1;
                }

                return UpdateChartOfAccount(account);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.MoveAccount", ex);
                throw;
            }
        }

        #endregion

        #region Reporting Operations

        public List<ChartOfAccount> GetAccountSummary()
        {
            try
            {
                return _chartOfAccountRepository.GetAccountSummary();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountSummary", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetAccountsWithUsageStats()
        {
            try
            {
                return _chartOfAccountRepository.GetAccountsWithUsageStats();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountsWithUsageStats", ex);
                throw;
            }
        }

        public List<ChartOfAccount> GetInactiveAccounts()
        {
            try
            {
                return _chartOfAccountRepository.GetInactiveAccounts();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetInactiveAccounts", ex);
                throw;
            }
        }

        public Dictionary<string, int> GetAccountTypeSummary()
        {
            try
            {
                var accounts = GetAllChartOfAccounts();
                return accounts.GroupBy(a => a.AccountType)
                              .ToDictionary(g => g.Key, g => g.Count());
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountTypeSummary", ex);
                throw;
            }
        }

        public Dictionary<string, int> GetAccountCategorySummary()
        {
            try
            {
                var accounts = GetAllChartOfAccounts();
                return accounts.GroupBy(a => a.AccountCategory)
                              .ToDictionary(g => g.Key, g => g.Count());
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.GetAccountCategorySummary", ex);
                throw;
            }
        }

        #endregion

        #region Bulk Operations

        public int BulkUpdateAccountStatus(List<int> accountIds, bool isActive)
        {
            try
            {
                return _chartOfAccountRepository.BulkUpdateAccountStatus(accountIds, isActive);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.BulkUpdateAccountStatus", ex);
                throw;
            }
        }

        public int BulkDeleteAccounts(List<int> accountIds)
        {
            try
            {
                // Validate that all accounts can be deleted
                foreach (var accountId in accountIds)
                {
                    var account = GetChartOfAccountById(accountId);
                    if (account != null && account.IsSystemAccount)
                    {
                        throw new InvalidOperationException($"Cannot delete system account: {account.AccountName}");
                    }
                }

                return _chartOfAccountRepository.BulkDeleteAccounts(accountIds);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.BulkDeleteAccounts", ex);
                throw;
            }
        }

        public int ImportAccounts(List<ChartOfAccount> accounts)
        {
            try
            {
                var importedCount = 0;
                foreach (var account in accounts)
                {
                    try
                    {
                        CreateChartOfAccount(account);
                        importedCount++;
                    }
                    catch (Exception ex)
                    {
                        DebugHelper.WriteException($"ChartOfAccountService.ImportAccounts - Account {account.AccountCode}", ex);
                        // Continue with other accounts
                    }
                }
                return importedCount;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ImportAccounts", ex);
                throw;
            }
        }

        public List<ChartOfAccount> ExportAccounts(List<int> accountIds = null)
        {
            try
            {
                if (accountIds == null || !accountIds.Any())
                {
                    return GetAllChartOfAccounts();
                }

                var accounts = new List<ChartOfAccount>();
                foreach (var accountId in accountIds)
                {
                    var account = GetChartOfAccountById(accountId);
                    if (account != null)
                    {
                        accounts.Add(account);
                    }
                }
                return accounts;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ExportAccounts", ex);
                throw;
            }
        }

        #endregion

        #region Account Type and Category Management

        public List<string> GetAccountTypes()
        {
            return new List<string> { "ASSET", "LIABILITY", "EQUITY", "REVENUE", "EXPENSE" };
        }

        public List<string> GetAccountCategories()
        {
            return new List<string>
            {
                "CURRENT_ASSET", "FIXED_ASSET", "CURRENT_LIABILITY", "LONG_TERM_LIABILITY",
                "OWNERS_EQUITY", "SALES_REVENUE", "OTHER_REVENUE", "COST_OF_GOODS_SOLD",
                "OPERATING_EXPENSE", "ADMINISTRATIVE_EXPENSE"
            };
        }

        public List<string> GetAccountCategoriesByType(string accountType)
        {
            switch (accountType?.ToUpper())
            {
                case "ASSET":
                    return new List<string> { "CURRENT_ASSET", "FIXED_ASSET" };
                case "LIABILITY":
                    return new List<string> { "CURRENT_LIABILITY", "LONG_TERM_LIABILITY" };
                case "EQUITY":
                    return new List<string> { "OWNERS_EQUITY" };
                case "REVENUE":
                    return new List<string> { "SALES_REVENUE", "OTHER_REVENUE" };
                case "EXPENSE":
                    return new List<string> { "COST_OF_GOODS_SOLD", "OPERATING_EXPENSE", "ADMINISTRATIVE_EXPENSE" };
                default:
                    return new List<string>();
            }
        }

        public string GetNormalBalanceForAccountType(string accountType)
        {
            switch (accountType?.ToUpper())
            {
                case "ASSET":
                case "EXPENSE":
                    return "DEBIT";
                case "LIABILITY":
                case "EQUITY":
                case "REVENUE":
                    return "CREDIT";
                default:
                    return "DEBIT";
            }
        }

        #endregion

        #region Enhanced Validation Methods

        /// <summary>
        /// Validates account code format and business rules
        /// </summary>
        public bool ValidateAccountCode(string accountCode, string accountType, int? parentAccountId = null, int? excludeAccountId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accountCode))
                    return false;

                // Check if code already exists
                if (AccountCodeExists(accountCode, excludeAccountId))
                    return false;

                // Validate format (numeric only)
                if (!System.Text.RegularExpressions.Regex.IsMatch(accountCode, @"^[0-9]+$"))
                    return false;

                // Validate length
                if (accountCode.Length > 20)
                    return false;

                // Validate account code range based on type
                if (int.TryParse(accountCode, out int code))
                {
                    var baseCode = GetBaseCodeForAccountType(accountType);
                    var maxCode = baseCode + 999; // Allow 1000 accounts per type

                    if (code < baseCode || code > maxCode)
                        return false;

                    // Validate parent-child code relationship
                    if (parentAccountId.HasValue)
                    {
                        var parentAccount = GetChartOfAccountById(parentAccountId.Value);
                        if (parentAccount != null)
                        {
                            // Child account code should be within parent's range
                            var parentCode = int.Parse(parentAccount.AccountCode);
                            var childMinCode = parentCode + 1;
                            var childMaxCode = parentCode + 99;

                            if (code < childMinCode || code > childMaxCode)
                                return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ValidateAccountCode", ex);
                return false;
            }
        }

        /// <summary>
        /// Validates account name uniqueness and business rules
        /// </summary>
        public bool ValidateAccountName(string accountName, int? excludeAccountId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accountName))
                    return false;

                // Check if name already exists
                if (AccountNameExists(accountName, excludeAccountId))
                    return false;

                // Validate length
                if (accountName.Length > 100)
                    return false;

                // Check for reserved names
                var reservedNames = GetReservedAccountNames();
                if (reservedNames.Contains(accountName.ToUpper()))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ValidateAccountName", ex);
                return false;
            }
        }

        /// <summary>
        /// Validates account type and category compatibility
        /// </summary>
        public bool ValidateAccountTypeAndCategory(string accountType, string accountCategory)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(accountType) || string.IsNullOrWhiteSpace(accountCategory))
                    return false;

                var validCategories = GetAccountCategoriesByType(accountType);
                return validCategories.Contains(accountCategory);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ValidateAccountTypeAndCategory", ex);
                return false;
            }
        }

        /// <summary>
        /// Validates account hierarchy depth and business rules
        /// </summary>
        public bool ValidateAccountHierarchyDepth(int? parentAccountId, int maxDepth = 5)
        {
            try
            {
                if (!parentAccountId.HasValue)
                    return true; // Root level account

                var depth = 1;
                var currentParentId = parentAccountId;

                while (currentParentId.HasValue)
                {
                    var parentAccount = GetChartOfAccountById(currentParentId.Value);
                    if (parentAccount == null)
                        break;

                    depth++;
                    currentParentId = parentAccount.ParentAccountId;

                    if (depth > maxDepth)
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ValidateAccountHierarchyDepth", ex);
                return false;
            }
        }

        /// <summary>
        /// Validates if account can be deleted based on business rules
        /// </summary>
        public bool CanDeleteAccount(int accountId)
        {
            try
            {
                var account = GetChartOfAccountById(accountId);
                if (account == null)
                    return false;

                // Cannot delete system accounts
                if (account.IsSystemAccount)
                    return false;

                // Cannot delete if has child accounts
                var childAccounts = GetChartOfAccountsByParent(accountId);
                if (childAccounts.Any())
                    return false;

                // TODO: Cannot delete if has transactions
                // This will be implemented when journal entries are integrated
                // if (HasTransactions(accountId))
                //     return false;

                return true;
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.CanDeleteAccount", ex);
                return false;
            }
        }

        /// <summary>
        /// Validates account for transaction usage
        /// </summary>
        public bool ValidateAccountForTransaction(int accountId, string transactionType)
        {
            try
            {
                var account = GetChartOfAccountById(accountId);
                if (account == null || !account.IsActive)
                    return false;

                // Business rules for transaction types
                switch (transactionType?.ToUpper())
                {
                    case "SALES":
                        return account.AccountType == "REVENUE";
                    case "PURCHASE":
                        return account.AccountType == "EXPENSE" || account.AccountType == "ASSET";
                    case "PAYMENT":
                        return account.AccountType == "ASSET" || account.AccountType == "LIABILITY";
                    case "RECEIPT":
                        return account.AccountType == "ASSET";
                    case "EXPENSE":
                        return account.AccountType == "EXPENSE";
                    default:
                        return true; // Allow for general transactions
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("ChartOfAccountService.ValidateAccountForTransaction", ex);
                return false;
            }
        }

        /// <summary>
        /// Gets comprehensive validation errors for an account
        /// </summary>
        public List<string> GetComprehensiveValidationErrors(ChartOfAccount account)
        {
            var errors = new List<string>();

            if (account == null)
            {
                errors.Add("Account cannot be null");
                return errors;
            }

            // Basic validation errors
            errors.AddRange(GetValidationErrors(account));

            // Enhanced validation errors
            if (!ValidateAccountCode(account.AccountCode, account.AccountType, account.ParentAccountId, account.AccountId))
            {
                if (AccountCodeExists(account.AccountCode, account.AccountId))
                    errors.Add("Account Code already exists");
                else if (!System.Text.RegularExpressions.Regex.IsMatch(account.AccountCode, @"^[0-9]+$"))
                    errors.Add("Account Code must contain only numbers");
                else if (account.AccountCode.Length > 20)
                    errors.Add("Account Code cannot exceed 20 characters");
                else
                    errors.Add("Account Code format is invalid for the specified account type");
            }

            if (!ValidateAccountName(account.AccountName, account.AccountId))
            {
                if (AccountNameExists(account.AccountName, account.AccountId))
                    errors.Add("Account Name already exists");
                else if (account.AccountName.Length > 100)
                    errors.Add("Account Name cannot exceed 100 characters");
                else
                    errors.Add("Account Name contains reserved words or invalid characters");
            }

            if (!ValidateAccountTypeAndCategory(account.AccountType, account.AccountCategory))
            {
                errors.Add("Account Category is not valid for the specified Account Type");
            }

            if (!ValidateAccountHierarchyDepth(account.ParentAccountId))
            {
                errors.Add("Account hierarchy depth exceeds maximum allowed level (5)");
            }

            return errors;
        }

        /// <summary>
        /// Gets reserved account names that cannot be used
        /// </summary>
        private List<string> GetReservedAccountNames()
        {
            return new List<string>
            {
                "CASH", "BANK", "ACCOUNTS RECEIVABLE", "ACCOUNTS PAYABLE", "SALES", "PURCHASES",
                "INVENTORY", "EQUIPMENT", "BUILDING", "LAND", "CAPITAL", "RETAINED EARNINGS",
                "DRAWINGS", "SALARIES", "RENT", "UTILITIES", "INSURANCE", "DEPRECIATION",
                "SYSTEM", "ADMIN", "ROOT", "DEFAULT", "TEMP", "TEST"
            };
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets base code for account type
        /// </summary>
        /// <param name="accountType">The account type</param>
        /// <returns>Base code for the account type</returns>
        private int GetBaseCodeForAccountType(string accountType)
        {
            switch (accountType?.ToUpper())
            {
                case "ASSET":
                    return 1000;
                case "LIABILITY":
                    return 2000;
                case "EQUITY":
                    return 3000;
                case "REVENUE":
                    return 4000;
                case "EXPENSE":
                    return 5000;
                default:
                    return 1000;
            }
        }

        #endregion
    }
}
