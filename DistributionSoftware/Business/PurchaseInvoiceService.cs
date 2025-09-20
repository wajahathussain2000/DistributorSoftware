using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class PurchaseInvoiceService : IPurchaseInvoiceService
    {
        private readonly IPurchaseInvoiceRepository _purchaseInvoiceRepository;

        public PurchaseInvoiceService()
        {
            var connectionString = ConfigurationManager.GetConnectionString("DefaultConnection");
            _purchaseInvoiceRepository = new PurchaseInvoiceRepository(connectionString);
        }

        public PurchaseInvoiceService(IPurchaseInvoiceRepository purchaseInvoiceRepository)
        {
            _purchaseInvoiceRepository = purchaseInvoiceRepository;
        }

        public int CreatePurchaseInvoice(PurchaseInvoice invoice)
        {
            try
            {
                if (!ValidatePurchaseInvoice(invoice))
                {
                    throw new InvalidOperationException("Purchase invoice validation failed");
                }

                return _purchaseInvoiceRepository.CreatePurchaseInvoice(invoice);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.CreatePurchaseInvoice", ex);
                throw;
            }
        }

        public bool UpdatePurchaseInvoice(PurchaseInvoice invoice)
        {
            try
            {
                if (!ValidatePurchaseInvoice(invoice))
                {
                    throw new InvalidOperationException("Purchase invoice validation failed");
                }

                return _purchaseInvoiceRepository.UpdatePurchaseInvoice(invoice);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.UpdatePurchaseInvoice", ex);
                throw;
            }
        }

        public bool DeletePurchaseInvoice(int invoiceId)
        {
            try
            {
                return _purchaseInvoiceRepository.DeletePurchaseInvoice(invoiceId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.DeletePurchaseInvoice", ex);
                throw;
            }
        }

        public PurchaseInvoice GetPurchaseInvoiceById(int invoiceId)
        {
            try
            {
                return _purchaseInvoiceRepository.GetPurchaseInvoiceById(invoiceId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.GetPurchaseInvoiceById", ex);
                throw;
            }
        }

        public PurchaseInvoice GetPurchaseInvoiceByNumber(string invoiceNumber)
        {
            try
            {
                return _purchaseInvoiceRepository.GetPurchaseInvoiceByNumber(invoiceNumber);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.GetPurchaseInvoiceByNumber", ex);
                throw;
            }
        }

        public List<PurchaseInvoice> GetAllPurchaseInvoices()
        {
            try
            {
                return _purchaseInvoiceRepository.GetAllPurchaseInvoices();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.GetAllPurchaseInvoices", ex);
                throw;
            }
        }

        public List<PurchaseInvoice> GetPurchaseInvoicesByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _purchaseInvoiceRepository.GetPurchaseInvoicesByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.GetPurchaseInvoicesByDateRange", ex);
                throw;
            }
        }

        public List<PurchaseInvoice> GetPurchaseInvoicesBySupplier(int supplierId)
        {
            try
            {
                return _purchaseInvoiceRepository.GetPurchaseInvoicesBySupplier(supplierId);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.GetPurchaseInvoicesBySupplier", ex);
                throw;
            }
        }

        public string GeneratePurchaseInvoiceNumber()
        {
            try
            {
                return _purchaseInvoiceRepository.GeneratePurchaseInvoiceNumber();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.GeneratePurchaseInvoiceNumber", ex);
                throw;
            }
        }

        public bool ValidatePurchaseInvoice(PurchaseInvoice invoice)
        {
            try
            {
                return invoice.IsValid();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.ValidatePurchaseInvoice", ex);
                return false;
            }
        }

        public List<string> GetValidationErrors(PurchaseInvoice invoice)
        {
            try
            {
                return invoice.GetValidationErrors();
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException("PurchaseInvoiceService.GetValidationErrors", ex);
                return new List<string> { "Error validating purchase invoice" };
            }
        }
    }
}
