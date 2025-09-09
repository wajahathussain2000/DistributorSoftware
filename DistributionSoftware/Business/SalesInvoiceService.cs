using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class SalesInvoiceService : ISalesInvoiceService
    {
        private readonly ISalesInvoiceRepository _salesInvoiceRepository;

        public SalesInvoiceService()
        {
            _salesInvoiceRepository = new SalesInvoiceRepository();
        }

        public SalesInvoiceService(ISalesInvoiceRepository salesInvoiceRepository)
        {
            _salesInvoiceRepository = salesInvoiceRepository;
        }

        public int CreateSalesInvoice(SalesInvoice invoice)
        {
            try
            {
                // Validate invoice
                if (!ValidateInvoice(invoice))
                {
                    throw new InvalidOperationException("Invalid invoice data");
                }

                // Calculate totals
                CalculateInvoiceTotals(invoice);

                // Reserve stock
                if (!ReserveStockForInvoice(invoice))
                {
                    throw new InvalidOperationException("Insufficient stock for one or more items");
                }

                // Generate invoice number if not provided
                if (string.IsNullOrEmpty(invoice.InvoiceNumber))
                {
                    invoice.InvoiceNumber = GenerateInvoiceNumber();
                }

                // Generate barcode
                GenerateInvoiceBarcode(invoice);
                
                // Set invoice status to DRAFT (payment comes first)
                invoice.Status = "DRAFT";
                
                // Create invoice
                var invoiceId = _salesInvoiceRepository.CreateSalesInvoice(invoice);
                invoice.SalesInvoiceId = invoiceId;

                // DON'T reduce stock yet - wait for payment confirmation
                // ProcessStockReduction(invoice); // Moved to ProcessPayment method

                return invoiceId;
            }
            catch (Exception ex)
            {
                // Release reserved stock on failure
                ReleaseStockForInvoice(invoice);
                throw; // Re-throw to maintain original behavior
            }
        }

        public bool UpdateSalesInvoice(SalesInvoice invoice)
        {
            if (!ValidateInvoice(invoice))
            {
                return false;
            }

            CalculateInvoiceTotals(invoice);
            return _salesInvoiceRepository.UpdateSalesInvoice(invoice);
        }

        public bool DeleteSalesInvoice(int invoiceId)
        {
            var invoice = GetSalesInvoiceById(invoiceId);
            if (invoice != null)
            {
                // Release reserved stock
                ReleaseStockForInvoice(invoice);
            }

            return _salesInvoiceRepository.DeleteSalesInvoice(invoiceId);
        }

        public SalesInvoice GetSalesInvoiceById(int invoiceId)
        {
            var invoice = _salesInvoiceRepository.GetSalesInvoiceById(invoiceId);
            if (invoice != null)
            {
                invoice.Items = GetSalesInvoiceDetails(invoiceId);
                invoice.Payments = GetSalesPayments(invoiceId);
            }
            return invoice;
        }

        public SalesInvoice GetSalesInvoiceByNumber(string invoiceNumber)
        {
            var invoice = _salesInvoiceRepository.GetSalesInvoiceByNumber(invoiceNumber);
            if (invoice != null)
            {
                invoice.Items = GetSalesInvoiceDetails(invoice.SalesInvoiceId);
                invoice.Payments = GetSalesPayments(invoice.SalesInvoiceId);
            }
            return invoice;
        }

        public List<SalesInvoice> GetAllSalesInvoices()
        {
            return _salesInvoiceRepository.GetAllSalesInvoices();
        }

        public List<SalesInvoice> GetSalesInvoicesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _salesInvoiceRepository.GetSalesInvoicesByDateRange(startDate, endDate);
        }

        public List<SalesInvoice> GetSalesInvoicesByCustomer(int customerId)
        {
            return _salesInvoiceRepository.GetSalesInvoicesByCustomer(customerId);
        }

        public List<SalesInvoice> GetSalesInvoicesByStatus(string status)
        {
            return _salesInvoiceRepository.GetSalesInvoicesByStatus(status);
        }

        public bool CreateSalesInvoiceDetail(SalesInvoiceDetail detail)
        {
            return _salesInvoiceRepository.CreateSalesInvoiceDetail(detail);
        }

        public bool UpdateSalesInvoiceDetail(SalesInvoiceDetail detail)
        {
            return _salesInvoiceRepository.UpdateSalesInvoiceDetail(detail);
        }

        public bool DeleteSalesInvoiceDetail(int detailId)
        {
            return _salesInvoiceRepository.DeleteSalesInvoiceDetail(detailId);
        }

        public List<SalesInvoiceDetail> GetSalesInvoiceDetails(int invoiceId)
        {
            return _salesInvoiceRepository.GetSalesInvoiceDetails(invoiceId);
        }

        public bool CreateSalesPayment(SalesPayment payment)
        {
            return _salesInvoiceRepository.CreateSalesPayment(payment);
        }

        public bool UpdateSalesPayment(SalesPayment payment)
        {
            return _salesInvoiceRepository.UpdateSalesPayment(payment);
        }

        public bool DeleteSalesPayment(int paymentId)
        {
            return _salesInvoiceRepository.DeleteSalesPayment(paymentId);
        }

        public List<SalesPayment> GetSalesPayments(int invoiceId)
        {
            return _salesInvoiceRepository.GetSalesPayments(invoiceId);
        }

        public string GenerateInvoiceNumber()
        {
            return _salesInvoiceRepository.GenerateInvoiceNumber();
        }

        public bool ProcessPayment(SalesInvoice invoice, SalesPayment payment)
        {
            try
            {
                // Validate payment
                if (payment.Amount <= 0)
                {
                    return false;
                }

                // Validate invoice
                if (invoice.SalesInvoiceId <= 0)
                {
                    return false;
                }

                // Create payment
                payment.SalesInvoiceId = invoice.SalesInvoiceId;
                payment.PaymentDate = DateTime.Now;
                payment.CreatedDate = DateTime.Now;
                payment.Status = "COMPLETED";

                var paymentCreated = CreateSalesPayment(payment);

            if (paymentCreated)
            {
                // Update invoice paid amount
                invoice.PaidAmount += payment.Amount;
                invoice.ChangeAmount = invoice.PaidAmount - invoice.TotalAmount;

                // Update invoice status based on payment
                if (invoice.PaidAmount >= invoice.TotalAmount)
                {
                    invoice.Status = "PAID";
                    // NOW reduce stock - payment confirmed!
                    ProcessStockReduction(invoice);
                }
                else
                {
                    invoice.Status = "PARTIAL_PAID";
                }

                UpdateSalesInvoice(invoice);
            }

            return paymentCreated;
            }
            catch (Exception ex)
            {
                // Log the error for debugging (can be removed in production)
                System.Diagnostics.Debug.WriteLine($"ProcessPayment Error: {ex.Message}");
                return false;
            }
        }

        public bool CalculateInvoiceTotals(SalesInvoice invoice)
        {
            if (invoice.Items == null || !invoice.Items.Any())
            {
                return false;
            }

            // Calculate subtotal
            invoice.Subtotal = invoice.Items.Sum(item => item.Quantity * item.UnitPrice);

            // Apply order-level discount
            if (invoice.DiscountPercentage > 0)
            {
                invoice.DiscountAmount = invoice.Subtotal * (invoice.DiscountPercentage / 100);
            }

            // Calculate taxable amount
            invoice.TaxableAmount = invoice.Subtotal - invoice.DiscountAmount;

            // Calculate tax (GST Pakistan)
            invoice.TaxAmount = invoice.TaxableAmount * (invoice.TaxPercentage / 100);

            // Calculate total
            invoice.TotalAmount = invoice.TaxableAmount + invoice.TaxAmount;

            return true;
        }

        public bool ValidateInvoice(SalesInvoice invoice)
        {
            if (invoice == null)
                return false;

            if (invoice.CustomerId < 0) // Allow 0 for Walk-in Customer
                return false;

            if (invoice.Items == null || !invoice.Items.Any())
                return false;

            if (invoice.TotalAmount < 0)
                return false;

            // Validate each item
            foreach (var item in invoice.Items)
            {
                if (item.ProductId <= 0)
                    return false;

                if (item.Quantity <= 0)
                    return false;

                if (item.UnitPrice < 0)
                    return false;
            }

            return true;
        }

        public bool ReserveStockForInvoice(SalesInvoice invoice)
        {
            if (invoice.Items == null)
                return false;

            foreach (var item in invoice.Items)
            {
                if (!_salesInvoiceRepository.ReserveStock(item.ProductId, item.Quantity))
                {
                    // Release previously reserved stock
                    foreach (var reservedItem in invoice.Items.TakeWhile(x => x != item))
                    {
                        _salesInvoiceRepository.ReleaseStock(reservedItem.ProductId, reservedItem.Quantity);
                    }
                    return false;
                }
            }

            return true;
        }

        public bool ReleaseStockForInvoice(SalesInvoice invoice)
        {
            if (invoice.Items == null)
                return false;

            foreach (var item in invoice.Items)
            {
                _salesInvoiceRepository.ReleaseStock(item.ProductId, item.Quantity);
            }

            return true;
        }

        public bool ConfirmStockForInvoice(SalesInvoice invoice)
        {
            if (invoice.Items == null)
                return false;

            foreach (var item in invoice.Items)
            {
                if (!_salesInvoiceRepository.ConfirmStock(item.ProductId, item.Quantity))
                {
                    return false;
                }
            }

            return true;
        }

        public bool GenerateInvoiceBarcode(SalesInvoice invoice)
        {
            if (string.IsNullOrEmpty(invoice.InvoiceNumber))
                return false;

            invoice.Barcode = invoice.InvoiceNumber;
            // Generate barcode image (implement with barcode library)
            invoice.BarcodeImage = GenerateBarcodeImage(invoice.Barcode);

            // Don't call UpdateInvoiceBarcode here - the invoice hasn't been created yet
            // The barcode will be saved when the invoice is created
            return true;
        }

        public bool UpdatePrintStatus(int invoiceId, bool printStatus)
        {
            return _salesInvoiceRepository.UpdatePrintStatus(invoiceId, printStatus, printStatus ? DateTime.Now : (DateTime?)null);
        }

        public string GenerateThermalReceipt(SalesInvoice invoice)
        {
            var receipt = new System.Text.StringBuilder();
            
            // Company header
            receipt.AppendLine("           [COMPANY LOGO]");
            receipt.AppendLine("        Your Company Name");
            receipt.AppendLine("     123 Main Street, Karachi");
            receipt.AppendLine("     Phone: +92-21-1234567");
            receipt.AppendLine("     GST#: 1234567890123");
            receipt.AppendLine("     NTN#: 1234567890123");
            receipt.AppendLine("─────────────────────────────");
            
            // Invoice details
            receipt.AppendLine($"Invoice#: {invoice.InvoiceNumber}");
            receipt.AppendLine($"Date: {invoice.InvoiceDate:dd/MM/yyyy}    Time: {invoice.TransactionTime:HH:mm:ss}");
            receipt.AppendLine($"Cashier: {invoice.CashierName ?? "System"}");
            receipt.AppendLine($"Customer: {invoice.CustomerName ?? "Walk-in Customer"}");
            receipt.AppendLine("─────────────────────────────");
            
            // Items
            receipt.AppendLine("Items:");
            foreach (var item in invoice.Items)
            {
                receipt.AppendLine($"{item.ProductCode} {item.ProductName}");
                receipt.AppendLine($"        {item.Quantity}×{item.UnitPrice:N2} = {item.LineTotal:N2}");
            }
            
            // Totals
            receipt.AppendLine("─────────────────────────────");
            receipt.AppendLine($"Subtotal: {invoice.Subtotal,20:N2}");
            receipt.AppendLine($"Discount: {invoice.DiscountAmount,20:N2}");
            receipt.AppendLine($"Taxable: {invoice.TaxableAmount,20:N2}");
            receipt.AppendLine($"GST ({invoice.TaxPercentage}%): {invoice.TaxAmount,20:N2}");
            receipt.AppendLine($"Total: {invoice.TotalAmount,20:N2}");
            receipt.AppendLine("─────────────────────────────");
            
            // Payment
            receipt.AppendLine($"Payment: {invoice.PaymentMode} {invoice.TotalAmount,20:N2}");
            receipt.AppendLine($"Change: {invoice.ChangeAmount,20:N2}");
            receipt.AppendLine("─────────────────────────────");
            
            // Barcode
            receipt.AppendLine($"[BARCODE: {invoice.Barcode}]");
            receipt.AppendLine("");
            receipt.AppendLine("Thank you for your business!");
            receipt.AppendLine("Visit us again soon");
            
            return receipt.ToString();
        }

        public List<SalesInvoice> GetSalesReport(DateTime startDate, DateTime endDate, int? customerId, string status)
        {
            return _salesInvoiceRepository.GetSalesReport(startDate, endDate, customerId, status);
        }

        private void ProcessStockReduction(SalesInvoice invoice)
        {
            // ERP Logic: After successful invoice creation, reduce actual stock quantities
            foreach (var item in invoice.Items)
            {
                // Reduce stock quantity and clear reserved quantity
                _salesInvoiceRepository.ReduceStockQuantity(item.ProductId, item.Quantity);
            }
        }

        public decimal GetTotalSales(DateTime startDate, DateTime endDate)
        {
            return _salesInvoiceRepository.GetTotalSales(startDate, endDate);
        }

        public int GetInvoiceCount(DateTime startDate, DateTime endDate)
        {
            return _salesInvoiceRepository.GetInvoiceCount(startDate, endDate);
        }

        public bool ProcessQuickSale(List<SalesInvoiceDetail> items, string paymentMode, decimal paidAmount)
        {
            var invoice = new SalesInvoice
            {
                CustomerId = 1, // Default walk-in customer
                CustomerName = "Walk-in Customer",
                PaymentMode = paymentMode,
                Items = items,
                CreatedBy = 1, // Current user
                Status = "DRAFT"
            };

            var invoiceId = CreateSalesInvoice(invoice);
            if (invoiceId > 0)
            {
                var payment = new SalesPayment
                {
                    PaymentMode = paymentMode,
                    Amount = paidAmount,
                    PaymentDate = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    Status = "COMPLETED",
                    CreatedBy = 1
                };

                return ProcessPayment(invoice, payment);
            }

            return false;
        }

        public bool HoldInvoice(SalesInvoice invoice)
        {
            invoice.Status = "HOLD";
            return UpdateSalesInvoice(invoice);
        }

        public bool RetrieveHeldInvoice(int invoiceId)
        {
            var invoice = GetSalesInvoiceById(invoiceId);
            if (invoice != null && invoice.Status == "HOLD")
            {
                invoice.Status = "DRAFT";
                return UpdateSalesInvoice(invoice);
            }
            return false;
        }

        public List<SalesInvoice> GetHeldInvoices()
        {
            return GetSalesInvoicesByStatus("HOLD");
        }

        private byte[] GenerateBarcodeImage(string barcode)
        {
            try
            {
                // Create a simple barcode image
                using (var bitmap = new System.Drawing.Bitmap(300, 80))
                using (var graphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    graphics.Clear(System.Drawing.Color.White);
                    
                    // Draw barcode lines (simple pattern)
                    var rand = new Random(barcode.GetHashCode()); // Use text hash for consistent pattern
                    int startX = 20;
                    int lineSpacing = 3;
                    
                    for (int i = 0; i < barcode.Length * 4; i++)
                    {
                        int height = rand.Next(20, 60);
                        int x = startX + i * lineSpacing;
                        graphics.DrawLine(System.Drawing.Pens.Black, x, 10, x, 10 + height);
                    }
                    
                    // Convert bitmap to byte array
                    using (var stream = new System.IO.MemoryStream())
                    {
                        bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                        return stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                return null; // Return null if generation fails
            }
        }
    }
}
