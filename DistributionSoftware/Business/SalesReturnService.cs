using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class SalesReturnService : ISalesReturnService
    {
        private readonly ISalesReturnRepository _salesReturnRepository;
        private readonly IStockMovementService _stockMovementService;

        public SalesReturnService()
        {
            _salesReturnRepository = new SalesReturnRepository();
            _stockMovementService = new StockMovementService();
        }

        public int CreateSalesReturn(SalesReturn salesReturn)
        {
            try
            {
                // Set default values
                salesReturn.CreatedDate = DateTime.Now;
                salesReturn.CreatedBy = UserSession.CurrentUserId > 0 ? UserSession.CurrentUserId : 1; // Default to user ID 1 if no user logged in
                salesReturn.Status = "PENDING";
                
                // Generate barcode if not provided
                if (string.IsNullOrEmpty(salesReturn.ReturnBarcode))
                {
                    salesReturn.ReturnBarcode = GenerateReturnBarcode();
                }

                // Calculate totals if not set
                if (salesReturn.SalesReturnItems != null && salesReturn.SalesReturnItems.Any())
                {
                    salesReturn.SubTotal = CalculateSubTotal(salesReturn.SalesReturnItems.ToList());
                    salesReturn.TaxAmount = CalculateTaxAmount(salesReturn.SalesReturnItems.ToList());
                    salesReturn.DiscountAmount = CalculateDiscountAmount(salesReturn.SalesReturnItems.ToList());
                    salesReturn.TotalAmount = CalculateTotalAmount(salesReturn.SalesReturnItems.ToList());
                }

                // Create the main sales return record
                var returnId = _salesReturnRepository.Create(salesReturn);
                
                // Save individual return items
                if (returnId > 0 && salesReturn.SalesReturnItems != null && salesReturn.SalesReturnItems.Any())
                {
                    foreach (var item in salesReturn.SalesReturnItems)
                    {
                        item.ReturnId = returnId;
                        var itemSaved = _salesReturnRepository.CreateReturnItem(item);
                        if (!itemSaved)
                        {
                            throw new Exception($"Failed to save return item for product {item.ProductId}");
                        }
                    }
                }

                return returnId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating sales return: {ex.Message}", ex);
            }
        }

        public SalesReturn GetSalesReturnById(int returnId)
        {
            try
            {
                return _salesReturnRepository.GetById(returnId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sales return: {ex.Message}", ex);
            }
        }

        public List<SalesReturn> GetAllSalesReturns()
        {
            try
            {
                return _salesReturnRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving sales returns: {ex.Message}", ex);
            }
        }

        public bool UpdateSalesReturn(SalesReturn salesReturn)
        {
            try
            {
                salesReturn.ModifiedDate = DateTime.Now;
                salesReturn.ModifiedBy = UserSession.CurrentUserId;

                return _salesReturnRepository.Update(salesReturn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating sales return: {ex.Message}", ex);
            }
        }

        public bool DeleteSalesReturn(int returnId)
        {
            try
            {
                return _salesReturnRepository.Delete(returnId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting sales return: {ex.Message}", ex);
            }
        }

        public string GenerateNewReturnNumber()
        {
            try
            {
                // Generate a simple return number based on current date and time
                return $"SR{DateTime.Now:yyyyMMddHHmmss}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating return number: {ex.Message}", ex);
            }
        }

        public string GenerateReturnBarcode()
        {
            try
            {
                // Generate a unique barcode for the return
                // Format: SR + timestamp + random number
                var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                var random = new Random().Next(1000, 9999);
                return $"SR{timestamp}{random}";
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating return barcode: {ex.Message}", ex);
            }
        }

        public List<SalesReturnItem> GetSalesReturnItems(int returnId)
        {
            try
            {
                return _salesReturnRepository.GetReturnItems(returnId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving return items: {ex.Message}", ex);
            }
        }

        public decimal CalculateTotalAmount(List<SalesReturnItem> items)
        {
            if (items == null || !items.Any())
                return 0;

            decimal subtotal = CalculateSubTotal(items);
            decimal taxAmount = CalculateTaxAmount(items);
            decimal discountAmount = CalculateDiscountAmount(items);

            return subtotal + taxAmount - discountAmount;
        }

        public decimal CalculateTaxAmount(List<SalesReturnItem> items)
        {
            if (items == null || !items.Any())
                return 0;

            return items.Sum(item => item.TaxAmount);
        }

        public decimal CalculateDiscountAmount(List<SalesReturnItem> items)
        {
            if (items == null || !items.Any())
                return 0;

            return items.Sum(item => item.DiscountAmount);
        }

        public decimal CalculateSubTotal(List<SalesReturnItem> items)
        {
            if (items == null || !items.Any())
                return 0;

            return items.Sum(item => item.Quantity * item.UnitPrice);
        }

        public bool UpdateStockForSalesReturn(int returnId, int updatedByUserId)
        {
            try
            {
                return _salesReturnRepository.UpdateStockForSalesReturn(returnId, updatedByUserId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating stock for sales return: {ex.Message}", ex);
            }
        }
    }
}