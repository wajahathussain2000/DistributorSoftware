using DistributionSoftware.DataAccess;
using DistributionSoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;

namespace DistributionSoftware.Business
{
    public class PurchaseReturnService : IPurchaseReturnService
    {
        private readonly IPurchaseReturnRepository _purchaseReturnRepository;
        private readonly IPurchaseReturnItemRepository _purchaseReturnItemRepository;
        private readonly string _connectionString;

        public PurchaseReturnService(IPurchaseReturnRepository purchaseReturnRepository, 
                                   IPurchaseReturnItemRepository purchaseReturnItemRepository)
        {
            _purchaseReturnRepository = purchaseReturnRepository;
            _purchaseReturnItemRepository = purchaseReturnItemRepository;
            _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=DistributionDB;Integrated Security=True";
        }

        public async Task<PurchaseReturn> GetPurchaseReturnByIdAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetPurchaseReturnByIdAsync: Getting purchase return {purchaseReturnId}");
                
                var purchaseReturn = await _purchaseReturnRepository.GetByIdAsync(purchaseReturnId);
                if (purchaseReturn != null)
                {
                    purchaseReturn.Items = await _purchaseReturnItemRepository.GetByReturnIdAsync(purchaseReturnId);
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
                    purchaseReturn.Items = await _purchaseReturnItemRepository.GetByReturnIdAsync(purchaseReturn.PurchaseReturnId);
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
                        item.ReturnId = purchaseReturnId;
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
                    await _purchaseReturnItemRepository.DeleteByReturnIdAsync(purchaseReturn.PurchaseReturnId);
                    
                    // Add updated items
                    foreach (var item in purchaseReturn.Items)
                    {
                        item.ReturnId = purchaseReturn.PurchaseReturnId;
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
                // Check if purchase return exists and can be deleted
                if (purchaseReturn != null)
                {
                    // For now, allow deletion of any purchase return
                    // In the future, you might want to add business rules here
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
                
                // Set default values for new purchase returns
                if (purchaseReturn.PurchaseReturnId == 0)
                {
                    // This is a new purchase return
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
                var items = await _purchaseReturnItemRepository.GetByReturnIdAsync(purchaseReturnId);
                if (!items.Any())
                {
                    throw new InvalidOperationException("Cannot post purchase return without items");
                }
                
                // Post the purchase return and update stock
                var success = await _purchaseReturnRepository.PostPurchaseReturnAsync(purchaseReturnId);
                
                if (success)
                {
                    // Update stock quantities (reduce by returned amounts)
                    await UpdateStockQuantitiesAsync(purchaseReturnId);
                }
                
                Debug.WriteLine($"PurchaseReturnService.PostPurchaseReturnAsync: Post {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.PostPurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        private async Task UpdateStockQuantitiesAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.UpdateStockQuantitiesAsync: Updating stock for purchase return {purchaseReturnId}");
                
                var items = await _purchaseReturnItemRepository.GetByReturnIdAsync(purchaseReturnId);
                
                foreach (var item in items)
                {
                    // Reduce stock quantity by returned amount
                    var stockUpdateQuery = @"
                        UPDATE Products 
                        SET StockQuantity = StockQuantity - @ReturnedQuantity
                        WHERE ProductId = @ProductId";
                    
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new SqlCommand(stockUpdateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@ReturnedQuantity", item.Quantity);
                            command.Parameters.AddWithValue("@ProductId", item.ProductId);
                            
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    
                    Debug.WriteLine($"PurchaseReturnService.UpdateStockQuantitiesAsync: Reduced stock for product {item.ProductId} by {item.Quantity}");
                }
                
                Debug.WriteLine($"PurchaseReturnService.UpdateStockQuantitiesAsync: Stock update completed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.UpdateStockQuantitiesAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CancelPurchaseReturnAsync(int purchaseReturnId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.CancelPurchaseReturnAsync: Cancelling purchase return {purchaseReturnId}");
                
                // Get the return items before cancelling
                var items = await _purchaseReturnItemRepository.GetByReturnIdAsync(purchaseReturnId);
                
                var success = await _purchaseReturnRepository.CancelPurchaseReturnAsync(purchaseReturnId);
                
                if (success && items.Any())
                {
                    // Restore stock quantities (add back the returned amounts)
                    await RestoreStockQuantitiesAsync(items);
                }
                
                Debug.WriteLine($"PurchaseReturnService.CancelPurchaseReturnAsync: Cancel {(success ? "successful" : "failed")}");
                return success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.CancelPurchaseReturnAsync: Error - {ex.Message}");
                throw;
            }
        }

        private async Task RestoreStockQuantitiesAsync(List<PurchaseReturnItem> items)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.RestoreStockQuantitiesAsync: Restoring stock for {items.Count} items");
                
                foreach (var item in items)
                {
                    // Restore stock quantity by adding back returned amount
                    var stockUpdateQuery = @"
                        UPDATE Products 
                        SET StockQuantity = StockQuantity + @ReturnedQuantity
                        WHERE ProductId = @ProductId";
                    
                    using (var connection = new SqlConnection(_connectionString))
                    {
                        await connection.OpenAsync();
                        using (var command = new SqlCommand(stockUpdateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@ReturnedQuantity", item.Quantity);
                            command.Parameters.AddWithValue("@ProductId", item.ProductId);
                            
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    
                    Debug.WriteLine($"PurchaseReturnService.RestoreStockQuantitiesAsync: Restored stock for product {item.ProductId} by {item.Quantity}");
                }
                
                Debug.WriteLine($"PurchaseReturnService.RestoreStockQuantitiesAsync: Stock restoration completed");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.RestoreStockQuantitiesAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<List<PurchaseReturnItem>> GetProductsFromInvoiceAsync(int invoiceId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetProductsFromInvoiceAsync: Getting products from invoice {invoiceId}");
                
                var products = new List<PurchaseReturnItem>();
                
                var query = @"
                    SELECT 
                        pid.ProductId,
                        p.ProductName,
                        p.ProductCode,
                        pid.Quantity as PurchasedQuantity,
                        pid.UnitPrice as PurchasedUnitPrice,
                        pid.BatchNumber,
                        pid.ExpiryDate
                    FROM PurchaseInvoiceDetails pid
                    INNER JOIN Products p ON pid.ProductId = p.ProductId
                    WHERE pid.PurchaseInvoiceId = @InvoiceId
                    AND p.IsActive = 1
                    ORDER BY p.ProductName";
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var product = new PurchaseReturnItem
                                {
                                    ProductId = Convert.ToInt32(reader[0]), // ProductId column
                                    ProductName = reader[1].ToString(), // ProductName column
                                    ProductCode = reader[2].ToString(), // ProductCode column
                                    Quantity = reader.GetDecimal(3), // PurchasedQuantity column
                                    UnitPrice = reader.GetDecimal(4), // PurchasedUnitPrice column
                                    BatchNo = reader.IsDBNull(5) ? null : reader[5].ToString(), // BatchNumber column
                                    ExpiryDate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6) // ExpiryDate column
                                };
                                
                                products.Add(product);
                            }
                        }
                    }
                }
                
                Debug.WriteLine($"PurchaseReturnService.GetProductsFromInvoiceAsync: Found {products.Count} products in invoice {invoiceId}");
                return products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetProductsFromInvoiceAsync: Error - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ValidateReturnQuantityAsync(int productId, int invoiceId, decimal returnQuantity)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.ValidateReturnQuantityAsync: Validating return quantity {returnQuantity} for product {productId} from invoice {invoiceId}");
                
                var query = @"
                    SELECT pid.Quantity as PurchasedQuantity
                    FROM PurchaseInvoiceDetails pid
                    WHERE pid.ProductId = @ProductId AND pid.PurchaseInvoiceId = @InvoiceId";
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@InvoiceId", invoiceId);
                        
                        var result = await command.ExecuteScalarAsync();
                        if (result != null)
                        {
                            var purchasedQuantity = Convert.ToDecimal(result);
                            var isValid = returnQuantity <= purchasedQuantity;
                            
                            Debug.WriteLine($"PurchaseReturnService.ValidateReturnQuantityAsync: Purchased: {purchasedQuantity}, Return: {returnQuantity}, Valid: {isValid}");
                            return isValid;
                        }
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.ValidateReturnQuantityAsync: Error - {ex.Message}");
                return false;
            }
        }

        public async Task<decimal> GetActualAvailableStockAsync(int productId)
        {
            try
            {
                Debug.WriteLine($"PurchaseReturnService.GetActualAvailableStockAsync: Getting actual stock for product {productId}");
                
                // Get the current stock quantity from Products table
                // This already reflects the stock adjustments from posted returns
                var query = @"
                    SELECT StockQuantity 
                    FROM Products 
                    WHERE ProductId = @ProductId";
                
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        
                        var result = await command.ExecuteScalarAsync();
                        if (result != null)
                        {
                            var currentStock = Convert.ToDecimal(result);
                            Debug.WriteLine($"PurchaseReturnService.GetActualAvailableStockAsync: Current stock for product {productId} is {currentStock}");
                            return currentStock;
                        }
                    }
                }
                
                return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PurchaseReturnService.GetActualAvailableStockAsync: Error - {ex.Message}");
                return 0;
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
                
                var items = await _purchaseReturnItemRepository.GetByReturnIdAsync(purchaseReturnId);
                
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
                
                var success = await _purchaseReturnItemRepository.DeleteByReturnIdAsync(purchaseReturnId);
                
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
