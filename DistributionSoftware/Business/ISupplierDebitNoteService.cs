using System;
using System.Collections.Generic;
using DistributionSoftware.Models;

namespace DistributionSoftware.Business
{
    public interface ISupplierDebitNoteService
    {
        // Debit Note CRUD
        int CreateSupplierDebitNote(SupplierDebitNote debitNote);
        bool UpdateSupplierDebitNote(SupplierDebitNote debitNote);
        bool DeleteSupplierDebitNote(int debitNoteId);
        SupplierDebitNote GetSupplierDebitNoteById(int debitNoteId);
        SupplierDebitNote GetSupplierDebitNoteByNumber(string debitNoteNo);
        List<SupplierDebitNote> GetAllSupplierDebitNotes();
        List<SupplierDebitNote> GetSupplierDebitNotesByDateRange(DateTime startDate, DateTime endDate);
        List<SupplierDebitNote> GetSupplierDebitNotesBySupplier(int supplierId);
        List<SupplierDebitNote> GetSupplierDebitNotesByStatus(string status);
        
        // Debit Note Items
        bool CreateSupplierDebitNoteItem(SupplierDebitNoteItem item);
        bool UpdateSupplierDebitNoteItem(SupplierDebitNoteItem item);
        bool DeleteSupplierDebitNoteItem(int itemId);
        List<SupplierDebitNoteItem> GetSupplierDebitNoteItems(int debitNoteId);
        
        // Business Logic
        string GenerateDebitNoteNumber();
        string GenerateDebitNoteBarcode();
        bool ProcessDebitNote(SupplierDebitNote debitNote);
        bool ApproveDebitNote(int debitNoteId, int approvedBy);
        bool RejectDebitNote(int debitNoteId, int rejectedBy, string rejectionReason);
        bool CalculateDebitNoteTotals(SupplierDebitNote debitNote);
        bool ValidateDebitNote(SupplierDebitNote debitNote);
        
        // Barcode and Printing
        bool GenerateDebitNoteBarcode(SupplierDebitNote debitNote);
        string GenerateDebitNotePrintout(SupplierDebitNote debitNote);
        
        // Reports
        List<SupplierDebitNote> GetSupplierDebitNoteReport(DateTime startDate, DateTime endDate, int? supplierId, string status);
        decimal GetTotalDebitNoteAmount(DateTime startDate, DateTime endDate);
        int GetDebitNoteCount(DateTime startDate, DateTime endDate);
        
        // Validation
        bool CheckDebitNoteNumberExists(string debitNoteNo);
        bool ValidateDebitNoteItems(List<SupplierDebitNoteItem> items);
    }
}
