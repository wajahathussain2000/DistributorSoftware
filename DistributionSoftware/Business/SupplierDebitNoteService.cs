using System;
using System.Collections.Generic;
using System.Linq;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class SupplierDebitNoteService : ISupplierDebitNoteService
    {
        private readonly ISupplierDebitNoteRepository _supplierDebitNoteRepository;

        public SupplierDebitNoteService()
        {
            _supplierDebitNoteRepository = new SupplierDebitNoteRepository();
        }

        public SupplierDebitNoteService(ISupplierDebitNoteRepository supplierDebitNoteRepository)
        {
            _supplierDebitNoteRepository = supplierDebitNoteRepository;
        }

        public int CreateSupplierDebitNote(SupplierDebitNote debitNote)
        {
            try
            {
                // Validate debit note
                if (!ValidateDebitNote(debitNote))
                {
                    throw new InvalidOperationException("Invalid debit note data");
                }

                // Calculate totals
                CalculateDebitNoteTotals(debitNote);

                // Generate debit note number if not provided
                if (string.IsNullOrEmpty(debitNote.DebitNoteNo))
                {
                    debitNote.DebitNoteNo = GenerateDebitNoteNumber();
                }

                // Generate barcode
                if (string.IsNullOrEmpty(debitNote.DebitNoteBarcode))
                {
                    debitNote.DebitNoteBarcode = GenerateDebitNoteBarcode();
                }

                // Generate barcode image
                GenerateDebitNoteBarcode(debitNote);

                // Set status to DRAFT
                debitNote.Status = "DRAFT";

                // Create debit note
                var debitNoteId = _supplierDebitNoteRepository.CreateSupplierDebitNote(debitNote);
                debitNote.DebitNoteId = debitNoteId;

                // Create items
                if (debitNote.Items != null && debitNote.Items.Any())
                {
                    foreach (var item in debitNote.Items)
                    {
                        item.DebitNoteId = debitNoteId;
                        CreateSupplierDebitNoteItem(item);
                    }
                }

                return debitNoteId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating supplier debit note: {ex.Message}", ex);
            }
        }

        public bool UpdateSupplierDebitNote(SupplierDebitNote debitNote)
        {
            if (!ValidateDebitNote(debitNote))
            {
                return false;
            }

            CalculateDebitNoteTotals(debitNote);
            return _supplierDebitNoteRepository.UpdateSupplierDebitNote(debitNote);
        }

        public bool DeleteSupplierDebitNote(int debitNoteId)
        {
            return _supplierDebitNoteRepository.DeleteSupplierDebitNote(debitNoteId);
        }

        public SupplierDebitNote GetSupplierDebitNoteById(int debitNoteId)
        {
            var debitNote = _supplierDebitNoteRepository.GetSupplierDebitNoteById(debitNoteId);
            if (debitNote != null)
            {
                debitNote.Items = GetSupplierDebitNoteItems(debitNoteId);
            }
            return debitNote;
        }

        public SupplierDebitNote GetSupplierDebitNoteByNumber(string debitNoteNo)
        {
            var debitNote = _supplierDebitNoteRepository.GetSupplierDebitNoteByNumber(debitNoteNo);
            if (debitNote != null)
            {
                debitNote.Items = GetSupplierDebitNoteItems(debitNote.DebitNoteId);
            }
            return debitNote;
        }

        public List<SupplierDebitNote> GetAllSupplierDebitNotes()
        {
            return _supplierDebitNoteRepository.GetAllSupplierDebitNotes();
        }

        public List<SupplierDebitNote> GetSupplierDebitNotesByDateRange(DateTime startDate, DateTime endDate)
        {
            return _supplierDebitNoteRepository.GetSupplierDebitNotesByDateRange(startDate, endDate);
        }

        public List<SupplierDebitNote> GetSupplierDebitNotesBySupplier(int supplierId)
        {
            return _supplierDebitNoteRepository.GetSupplierDebitNotesBySupplier(supplierId);
        }

        public List<SupplierDebitNote> GetSupplierDebitNotesByStatus(string status)
        {
            return _supplierDebitNoteRepository.GetSupplierDebitNotesByStatus(status);
        }

        public bool CreateSupplierDebitNoteItem(SupplierDebitNoteItem item)
        {
            return _supplierDebitNoteRepository.CreateSupplierDebitNoteItem(item);
        }

        public bool UpdateSupplierDebitNoteItem(SupplierDebitNoteItem item)
        {
            return _supplierDebitNoteRepository.UpdateSupplierDebitNoteItem(item);
        }

        public bool DeleteSupplierDebitNoteItem(int itemId)
        {
            return _supplierDebitNoteRepository.DeleteSupplierDebitNoteItem(itemId);
        }

        public List<SupplierDebitNoteItem> GetSupplierDebitNoteItems(int debitNoteId)
        {
            return _supplierDebitNoteRepository.GetSupplierDebitNoteItems(debitNoteId);
        }

        public string GenerateDebitNoteNumber()
        {
            try
            {
                return _supplierDebitNoteRepository.GenerateDebitNoteNumber();
            }
            catch (Exception ex)
            {
                // Fallback to simple generation if stored procedure doesn't exist
                var timestamp = DateTime.Now.ToString("yyyyMMdd");
                var random = new Random().Next(1000, 9999);
                return $"DN{timestamp}{random}";
            }
        }

        public string GenerateDebitNoteBarcode()
        {
            // Generate unique barcode based on timestamp
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            var random = new Random().Next(1000, 9999);
            return $"DN{timestamp}{random}";
        }

        public bool ProcessDebitNote(SupplierDebitNote debitNote)
        {
            try
            {
                // Validate debit note
                if (!ValidateDebitNote(debitNote))
                {
                    return false;
                }

                // Calculate totals
                CalculateDebitNoteTotals(debitNote);

                // Generate barcode if not exists
                if (string.IsNullOrEmpty(debitNote.DebitNoteBarcode))
                {
                    debitNote.DebitNoteBarcode = GenerateDebitNoteBarcode();
                }

                // Generate barcode image
                GenerateDebitNoteBarcode(debitNote);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error processing debit note: {ex.Message}", ex);
            }
        }

        public bool ApproveDebitNote(int debitNoteId, int approvedBy)
        {
            try
            {
                var result = _supplierDebitNoteRepository.ApproveDebitNote(debitNoteId, approvedBy);
                
                if (result)
                {
                    // Update supplier balance
                    var debitNote = GetSupplierDebitNoteById(debitNoteId);
                    if (debitNote != null)
                    {
                        _supplierDebitNoteRepository.UpdateSupplierBalance(debitNote.SupplierId, debitNote.TotalAmount);
                    }
                }
                
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error approving debit note: {ex.Message}", ex);
            }
        }

        public bool RejectDebitNote(int debitNoteId, int rejectedBy, string rejectionReason)
        {
            return _supplierDebitNoteRepository.RejectDebitNote(debitNoteId, rejectedBy, rejectionReason);
        }

        public bool CalculateDebitNoteTotals(SupplierDebitNote debitNote)
        {
            if (debitNote.Items == null || !debitNote.Items.Any())
            {
                return false;
            }

            // Calculate subtotal
            debitNote.SubTotal = debitNote.Items.Sum(item => item.Quantity * item.UnitPrice);

            // Apply order-level discount
            if (debitNote.DiscountAmount > 0)
            {
                debitNote.SubTotal -= debitNote.DiscountAmount;
            }

            // Calculate tax
            debitNote.TaxAmount = debitNote.Items.Sum(item => item.TaxAmount);

            // Calculate total
            debitNote.TotalAmount = debitNote.SubTotal + debitNote.TaxAmount;

            return true;
        }

        public bool ValidateDebitNote(SupplierDebitNote debitNote)
        {
            if (debitNote == null)
                return false;

            if (debitNote.SupplierId <= 0)
                return false;

            if (debitNote.DebitDate == DateTime.MinValue)
                return false;

            if (string.IsNullOrEmpty(debitNote.Reason))
                return false;

            if (debitNote.Items == null || !debitNote.Items.Any())
                return false;

            // Validate each item
            foreach (var item in debitNote.Items)
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

        public bool GenerateDebitNoteBarcode(SupplierDebitNote debitNote)
        {
            if (string.IsNullOrEmpty(debitNote.DebitNoteBarcode))
                return false;

            try
            {
                // Generate barcode image
                debitNote.BarcodeImage = GenerateBarcodeImage(debitNote.DebitNoteBarcode);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating barcode: {ex.Message}", ex);
            }
        }

        public string GenerateDebitNotePrintout(SupplierDebitNote debitNote)
        {
            var printout = new System.Text.StringBuilder();
            
            // Company header
            printout.AppendLine("           [COMPANY LOGO]");
            printout.AppendLine("        Your Company Name");
            printout.AppendLine("     123 Main Street, Karachi");
            printout.AppendLine("     Phone: +92-21-1234567");
            printout.AppendLine("     GST#: 1234567890123");
            printout.AppendLine("     NTN#: 1234567890123");
            printout.AppendLine("─────────────────────────────");
            
            // Debit Note details
            printout.AppendLine($"Debit Note#: {debitNote.DebitNoteNo}");
            printout.AppendLine($"Date: {debitNote.DebitDate:dd/MM/yyyy}");
            printout.AppendLine($"Supplier: {debitNote.SupplierName}");
            printout.AppendLine($"Supplier Code: {debitNote.SupplierCode}");
            if (!string.IsNullOrEmpty(debitNote.ReferencePurchaseNo))
                printout.AppendLine($"Reference Purchase: {debitNote.ReferencePurchaseNo}");
            if (!string.IsNullOrEmpty(debitNote.ReferenceGRNNo))
                printout.AppendLine($"Reference GRN: {debitNote.ReferenceGRNNo}");
            printout.AppendLine($"Reason: {debitNote.Reason}");
            printout.AppendLine("─────────────────────────────");
            
            // Items
            printout.AppendLine("Items:");
            foreach (var item in debitNote.Items)
            {
                printout.AppendLine($"{item.ProductCode} {item.ProductName}");
                printout.AppendLine($"        {item.Quantity}×{item.UnitPrice:N2} = {item.LineTotal:N2}");
                if (!string.IsNullOrEmpty(item.Reason))
                    printout.AppendLine($"        Reason: {item.Reason}");
            }
            
            // Totals
            printout.AppendLine("─────────────────────────────");
            printout.AppendLine($"Subtotal: {debitNote.SubTotal,20:N2}");
            printout.AppendLine($"Discount: {debitNote.DiscountAmount,20:N2}");
            printout.AppendLine($"Tax: {debitNote.TaxAmount,20:N2}");
            printout.AppendLine($"Total: {debitNote.TotalAmount,20:N2}");
            printout.AppendLine("─────────────────────────────");
            
            // Status
            printout.AppendLine($"Status: {debitNote.StatusText}");
            if (!string.IsNullOrEmpty(debitNote.Remarks))
                printout.AppendLine($"Remarks: {debitNote.Remarks}");
            
            // Barcode
            printout.AppendLine($"[BARCODE: {debitNote.DebitNoteBarcode}]");
            printout.AppendLine("");
            printout.AppendLine("This debit note increases the supplier balance.");
            printout.AppendLine("Please acknowledge receipt of this debit note.");
            
            return printout.ToString();
        }

        public List<SupplierDebitNote> GetSupplierDebitNoteReport(DateTime startDate, DateTime endDate, int? supplierId, string status)
        {
            return _supplierDebitNoteRepository.GetSupplierDebitNoteReport(startDate, endDate, supplierId, status);
        }

        public decimal GetTotalDebitNoteAmount(DateTime startDate, DateTime endDate)
        {
            return _supplierDebitNoteRepository.GetTotalDebitNoteAmount(startDate, endDate);
        }

        public int GetDebitNoteCount(DateTime startDate, DateTime endDate)
        {
            return _supplierDebitNoteRepository.GetDebitNoteCount(startDate, endDate);
        }

        public bool CheckDebitNoteNumberExists(string debitNoteNo)
        {
            return _supplierDebitNoteRepository.CheckDebitNoteNumberExists(debitNoteNo);
        }

        public bool ValidateDebitNoteItems(List<SupplierDebitNoteItem> items)
        {
            if (items == null || !items.Any())
                return false;

            foreach (var item in items)
            {
                if (item.ProductId <= 0)
                    return false;

                if (item.Quantity <= 0)
                    return false;

                if (item.UnitPrice < 0)
                    return false;

                if (item.TotalAmount < 0)
                    return false;
            }

            return true;
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
