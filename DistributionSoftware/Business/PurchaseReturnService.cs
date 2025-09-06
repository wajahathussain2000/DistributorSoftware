using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DistributionSoftware.Business
{
    public class PurchaseReturnService : IPurchaseReturnService
    {
        private readonly IPurchaseReturnRepository _purchaseReturnRepository;
        private readonly IPurchaseReturnItemRepository _purchaseReturnItemRepository;

        public PurchaseReturnService(IPurchaseReturnRepository purchaseReturnRepository, 
                                   IPurchaseReturnItemRepository purchaseReturnItemRepository)
        {
            _purchaseReturnRepository = purchaseReturnRepository;
            _purchaseReturnItemRepository = purchaseReturnItemRepository;
        }

        public async Task<PurchaseReturn> GetPurchaseReturnByIdAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnByIdAsync: Getting purchase return {purchaseReturnId}");
                
                var purchaseReturn = await _purchaseReturnRepository.GetByIdAsync(purchaseReturnId);
                if (purchaseReturn != null)
                {
                    purchaseReturn.Items = await _purchaseReturnItemRepository.GetByPurchaseReturnIdAsync(purchaseReturnId);
                }
                
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnByIdAsync: {(purchaseReturn != null ? "Success" : "Not found")}");
                return purchaseReturn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnByIdAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<PurchaseReturn> GetPurchaseReturnByNumberAsync(string returnNumber)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnByNumberAsync: Getting purchase return {returnNumber}");
                
                var purchaseReturn = await _purchaseReturnRepository.GetByReturnNumberAsync(returnNumber);
                if (purchaseReturn != null)
                {
                    purchaseReturn.Items = await _purchaseReturnItemRepository.GetByPurchaseReturnIdAsync(purchaseReturn.PurchaseReturnId);
                }
                
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnByNumberAsync: {(purchaseReturn != null ? "Success" : "Not found")}");
                return purchaseReturn;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnByNumberAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<List<PurchaseReturn>> GetAllPurchaseReturnsAsync()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnService.GetAllPurchaseReturnsAsync: Getting all purchase returns");
                
                var purchaseReturns = await _purchaseReturnRepository.GetAllAsync();
                
                Debug.WriteLine($"PurchaseReturnService.GetAllPurchaseReturnsAsync: Retrieved {purchaseReturns.Count} purchase returns");
                return purchaseReturns;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetAllPurchaseReturnsAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<List<PurchaseReturn>> GetPurchaseReturnsBySupplierAsync(int supplierId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsBySupplierAsync: Getting purchase returns for supplier {supplierId}");
                
                var purchaseReturns = await _purchaseReturnRepository.GetBySupplierIdAsync(supplierId);
                
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsBySupplierAsync: Retrieved {purchaseReturns.Count} purchase returns");
                return purchaseReturns;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsBySupplierAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<List<PurchaseReturn>> GetPurchaseReturnsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsByDateRangeAsync: Getting purchase returns from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}");
                
                var purchaseReturns = await _purchaseReturnRepository.GetByDateRangeAsync(startDate, endDate);
                
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsByDateRangeAsync: Retrieved {purchaseReturns.Count} purchase returns");
                return purchaseReturns;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsByDateRangeAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<List<PurchaseReturn>> GetPurchaseReturnsByStatusAsync(string status)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsByStatusAsync: Getting purchase returns with status {status}");
                
                var purchaseReturns = await _purchaseReturnRepository.GetByStatusAsync(status);
                
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsByStatusAsync: Retrieved {purchaseReturns.Count} purchase returns");
                return purchaseReturns;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnsByStatusAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<string> GenerateNextReturnNumberAsync()
        {
            try
            {
                Debug.WriteLine("PurchaseReturnService.GenerateNextReturnNumberAsync: Generating next return number");
                
                var returnNumber = await _purchaseReturnRepository.GetNextReturnNumberAsync();
                
                Debug.WriteLine($"PurchaseReturnService.GenerateNextReturnNumberAsync: Generated return number {returnNumber}");
                return returnNumber;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GenerateNextReturnNumberAsync: Error - {ex.Message}");
                throw;
            }
        }

        public Task<string> GenerateBarcodeAsync(string returnNumber)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GenerateBarcodeAsync: Generating barcode for {returnNumber}");
                
                // Generate barcode similar to return number format
                // Extract the numeric part from return number (e.g., "PR-20250906-00001" -> "2025090600001")
                var numericPart = returnNumber.Replace("PR-", "").Replace("-", "");
                var barcode = $"BC-{numericPart.Substring(0, 8)}-{numericPart.Substring(8)}";
                
                Debug.WriteLine($"PurchaseReturnService.GenerateBarcodeAsync: Generated barcode {barcode}");
                return Task.FromResult(barcode);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GenerateBarcodeAsync: Error - {ex.Message}");
                // Fallback to simple format
                var barcode = $"BC-{DateTime.Now:yyyyMMdd}-00001";
                return Task.FromResult(barcode);
            }
        }

        public async Task<int> CreatePurchaseReturnAsync(PurchaseReturn purchaseReturn)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.CreatePurchaseReturnAsync: Creating purchase return {purchaseReturn.ReturnNumber}");
                
                // Validate the purchase return
                if (!await ValidatePurchaseReturnAsync(purchaseReturn))
                {
                    throw new InvalidOperationException("Purchase return validation failed");
                }
                
                // Calculate net amount
                purchaseReturn.NetReturnAmount = await CalculateNetReturnAmountAsync(purchaseReturn);
                
                // Create the purchase return
                var purchaseReturnId = await _purchaseReturnRepository.CreateAsync(purchaseReturn);
                
                // Create items if any
                if (purchaseReturn.Items != null && purchaseReturn.Items.Any())
                {
                    foreach (var item in purchaseReturn.Items)
                    {
                        item.PurchaseReturnId = purchaseReturnId;
                        await _purchaseReturnItemRepository.CreateAsync(item);
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnService.CreatePurchaseReturnAsync: Successfully created purchase return with ID {purchaseReturnId}");
                return purchaseReturnId;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.CreatePurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdatePurchaseReturnAsync(PurchaseReturn purchaseReturn)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.UpdatePurchaseReturnAsync: Updating purchase return {purchaseReturn.ReturnNumber}");
                
                // Validate the purchase return
                if (!await ValidatePurchaseReturnAsync(purchaseReturn))
                {
                    throw new InvalidOperationException("Purchase return validation failed");
                }
                
                // Calculate net amount
                purchaseReturn.NetReturnAmount = await CalculateNetReturnAmountAsync(purchaseReturn);
                
                // Update the purchase return
                var success = await _purchaseReturnRepository.UpdateAsync(purchaseReturn);
                
                if (success && purchaseReturn.Items != null)
                {
                    // Remove existing items
                    await _purchaseReturnItemRepository.DeleteByPurchaseReturnIdAsync(purchaseReturn.PurchaseReturnId);
                    
                    // Add updated items
                    foreach (var item in purchaseReturn.Items)
                    {
                        item.PurchaseReturnId = purchaseReturn.PurchaseReturnId;
                        await _purchaseReturnItemRepository.CreateAsync(item);
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnService.UpdatePurchaseReturnAsync: Update {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.UpdatePurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeletePurchaseReturnAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.DeletePurchaseReturnAsync: Deleting purchase return {purchaseReturnId}");
                
                // Check if purchase return is posted
                var purchaseReturn = await _purchaseReturnRepository.GetByIdAsync(purchaseReturnId);
                if (purchaseReturn != null && purchaseReturn.Status == "Posted")
                {
                    throw new InvalidOperationException("Cannot delete posted purchase return");
                }
                
                var success = await _purchaseReturnRepository.DeleteAsync(purchaseReturnId);
                
                Debug.WriteLine($"PurchaseReturnService.DeletePurchaseReturnAsync: Delete {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.DeletePurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> SaveDraftAsync(PurchaseReturn purchaseReturn)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.SaveDraftAsync: Saving draft for purchase return {purchaseReturn.ReturnNumber}");
                
                purchaseReturn.Status = "Draft";
                
                if (purchaseReturn.PurchaseReturnId == 0)
                {
                    await CreatePurchaseReturnAsync(purchaseReturn);
                }
                else
                {
                    await UpdatePurchaseReturnAsync(purchaseReturn);
                }
                
                Debug.WriteLine($"PurchaseReturnService.SaveDraftAsync: Draft saved successfully");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.SaveDraftAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> PostPurchaseReturnAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.PostPurchaseReturnAsync: Posting purchase return {purchaseReturnId}");
                
                // Validate that purchase return has items
                var items = await _purchaseReturnItemRepository.GetByPurchaseReturnIdAsync(purchaseReturnId);
                if (!items.Any())
                {
                    throw new InvalidOperationException("Cannot post purchase return without items");
                }
                
                var success = await _purchaseReturnRepository.PostPurchaseReturnAsync(purchaseReturnId);
                
                Debug.WriteLine($"PurchaseReturnService.PostPurchaseReturnAsync: Post {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.PostPurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CancelPurchaseReturnAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.CancelPurchaseReturnAsync: Cancelling purchase return {purchaseReturnId}");
                
                var success = await _purchaseReturnRepository.CancelPurchaseReturnAsync(purchaseReturnId);
                
                Debug.WriteLine($"PurchaseReturnService.CancelPurchaseReturnAsync: Cancel {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.CancelPurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<decimal> CalculateNetReturnAmountAsync(PurchaseReturn purchaseReturn)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.CalculateNetReturnAmountAsync: Calculating net amount for purchase return {purchaseReturn.ReturnNumber}");
                
                decimal subtotal = 0;
                
                if (purchaseReturn.Items != null && purchaseReturn.Items.Any())
                {
                    subtotal = purchaseReturn.Items.Sum(item => item.LineTotal);
                }
                
                var netAmount = subtotal + purchaseReturn.TaxAdjust - purchaseReturn.DiscountAdjust + purchaseReturn.FreightAdjust;
                
                Debug.WriteLine($"PurchaseReturnService.CalculateNetReturnAmountAsync: Calculated net amount {netAmount}");
                return netAmount;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.CalculateNetReturnAmountAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ValidatePurchaseReturnAsync(PurchaseReturn purchaseReturn)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.ValidatePurchaseReturnAsync: Validating purchase return {purchaseReturn.ReturnNumber}");
                
                // Basic validation
                if (string.IsNullOrEmpty(purchaseReturn.ReturnNumber))
                {
                    Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: Return number is required");
                    return false;
                }
                
                if (purchaseReturn.SupplierId <= 0)
                {
                    Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: Valid supplier is required");
                    return false;
                }
                
                if (purchaseReturn.ReturnDate == DateTime.MinValue)
                {
                    Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: Return date is required");
                    return false;
                }
                
                if (purchaseReturn.Items == null || !purchaseReturn.Items.Any())
                {
                    Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: At least one item is required");
                    return false;
                }
                
                // Validate items
                foreach (var item in purchaseReturn.Items)
                {
                    if (item.ProductId <= 0)
                    {
                        Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: Valid product is required for all items");
                        return false;
                    }
                    
                    if (item.Quantity <= 0)
                    {
                        Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: Quantity must be greater than 0");
                        return false;
                    }
                    
                    if (item.UnitPrice < 0)
                    {
                        Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: Unit price cannot be negative");
                        return false;
                    }
                }
                
                Debug.WriteLine("PurchaseReturnService.ValidatePurchaseReturnAsync: Validation successful");
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.ValidatePurchaseReturnAsync: Error - {ex.Message}");
                return false;
            }
        }

        public async Task<List<PurchaseReturnItem>> GetPurchaseReturnItemsAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnItemsAsync: Getting items for purchase return {purchaseReturnId}");
                
                var items = await _purchaseReturnItemRepository.GetByPurchaseReturnIdAsync(purchaseReturnId);
                
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnItemsAsync: Retrieved {items.Count} items");
                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnItemsAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddPurchaseReturnItemAsync(PurchaseReturnItem item)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.AddPurchaseReturnItemAsync: Adding item {item.ProductName}");
                
                // Calculate line total
                item.LineTotal = await _purchaseReturnItemRepository.CalculateLineTotalAsync(item.ProductId, item.Quantity, item.UnitPrice);
                
                var itemId = await _purchaseReturnItemRepository.CreateAsync(item);
                var success = itemId > 0;
                
                Debug.WriteLine($"PurchaseReturnService.AddPurchaseReturnItemAsync: Add {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.AddPurchaseReturnItemAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdatePurchaseReturnItemAsync(PurchaseReturnItem item)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.UpdatePurchaseReturnItemAsync: Updating item {item.ProductName}");
                
                // Calculate line total
                item.LineTotal = await _purchaseReturnItemRepository.CalculateLineTotalAsync(item.ProductId, item.Quantity, item.UnitPrice);
                
                var success = await _purchaseReturnItemRepository.UpdateAsync(item);
                
                Debug.WriteLine($"PurchaseReturnService.UpdatePurchaseReturnItemAsync: Update {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.UpdatePurchaseReturnItemAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemovePurchaseReturnItemAsync(int itemId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.RemovePurchaseReturnItemAsync: Removing item {itemId}");
                
                var success = await _purchaseReturnItemRepository.DeleteAsync(itemId);
                
                Debug.WriteLine($"PurchaseReturnService.RemovePurchaseReturnItemAsync: Remove {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.RemovePurchaseReturnItemAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveAllPurchaseReturnItemsAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.RemoveAllPurchaseReturnItemsAsync: Removing all items for purchase return {purchaseReturnId}");
                
                var success = await _purchaseReturnItemRepository.DeleteByPurchaseReturnIdAsync(purchaseReturnId);
                
                Debug.WriteLine($"PurchaseReturnService.RemoveAllPurchaseReturnItemsAsync: Remove all {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.RemoveAllPurchaseReturnItemsAsync: Error - {ex.Message}");
                throw;
            }
        }
    }
}
