# Supplier Debit Note Workflow

## Business Process Flow

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                           SUPPLIER DEBIT NOTE WORKFLOW                          │
└─────────────────────────────────────────────────────────────────────────────────┘

┌─────────────┐       ┌─────────────┐       ┌─────────────┐       ┌─────────────┐
│   SUPPLIER  │       │    BUYER    │       │   ACCOUNTS   │       │   DATABASE  │
│             │       │             │       │   PAYABLE    │       │             │
└─────┬───────┘       └──────┬──────┘       └──────┬──────┘       └──────┬──────┘
      │                     │                      │                      │
      │ 1. Send Invoice     │                      │                      │
      │    Amount: 1000     │                      │                      │
      ├────────────────────►│                      │                      │
      │                     │                      │                      │
      │                     │ 2. Create Payable    │                      │
      │                     │    Balance: 1000      │                      │
      │                     ├─────────────────────►│                      │
      │                     │                      │                      │
      │                     │                      │ 3. Store Invoice     │
      │                     │                      │    in Database      │
      │                     │                      ├─────────────────────►│
      │                     │                      │                      │
      │                     │ 4. Problem Found     │                      │
      │                     │    (Damage/Shortage) │                      │
      │                     │                      │                      │
      │                     │ 5. Create Debit Note │                      │
      │                     │    Amount: 200        │                      │
      │                     ├─────────────────────►│                      │
      │                     │                      │                      │
      │                     │                      │ 6. Update Balance   │
      │                     │                      │    1000 - 200 = 800 │
      │                     │                      ├─────────────────────►│
      │                     │                      │                      │
      │                     │ 7. Send Debit Note   │                      │
      │                     │    to Supplier       │                      │
      │                     ├─────────────────────►│                      │
      │                     │                      │                      │
      │ 8. Acknowledge     │                      │                      │
      │    New Balance: 800│                      │                      │
      ◄─────────────────────┤                      │                      │
      │                     │                      │                      │
      │                     │ 9. Update Records    │                      │
      │                     │    Supplier Balance  │                      │
      │                     │    = 800             │                      │
      │                     │                      │                      │
```

## Database Schema Relationships

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                              DATABASE STRUCTURE                                │
└─────────────────────────────────────────────────────────────────────────────────┘

┌─────────────────┐       ┌─────────────────┐       ┌─────────────────┐
│   SUPPLIERS     │       │   INVOICES      │       │ SUPPLIER DEBIT  │
│                 │       │                 │       │     NOTES       │
├─────────────────┤       ├─────────────────┤       ├─────────────────┤
│ SupplierId (PK) │◄──────┤ SupplierId (FK) │       │ SupplierId (FK) │
│ SupplierName    │       │ InvoiceId (PK)  │◄──────┤ OriginalInvoice │
│ SupplierCode    │       │ InvoiceNo       │       │ DebitNoteId (PK)│
│ ContactInfo     │       │ InvoiceAmount   │       │ DebitNoteNo     │
│ PaymentTerms    │       │ InvoiceDate     │       │ TotalAmount     │
└─────────────────┘       └─────────────────┘       │ BalanceImpact   │
                                                     │ Status          │
                                                     └─────────────────┘
                                                              │
                                                              │
                                                     ┌─────────────────┐
                                                     │ SUPPLIER BALANCE │
                                                     │                 │
                                                     ├─────────────────┤
                                                     │ SupplierId (FK) │
                                                     │ TransactionType │
                                                     │ Amount          │
                                                     │ RunningBalance  │
                                                     │ TransactionDate │
                                                     └─────────────────┘
```

## Key Business Rules

### 1. Invoice Creation
- Supplier sends invoice for goods/services
- System creates payable record
- Balance increases by invoice amount

### 2. Problem Identification
- Goods damaged, short, or overcharged
- Quality issues discovered
- Price discrepancies found

### 3. Debit Note Creation
- Buyer creates debit note
- Links back to original invoice
- Specifies reason and amount
- Reduces payable balance

### 4. Balance Calculation
```
New Balance = Original Invoice Amount - Debit Note Amount
Example: 1000 - 200 = 800
```

### 5. Supplier Acknowledgment
- Supplier receives debit note
- Updates their records
- Confirms new balance

## Status Workflow

```
DRAFT → PENDING → APPROVED → ACKNOWLEDGED
  ↓         ↓         ↓           ↓
Create   Submit   Manager    Supplier
Note     for      Approves   Confirms
         Review              Receipt
```

## Amount Calculations

### Debit Note Total
```
SubTotal = Σ(Quantity × UnitPrice)
TaxAmount = SubTotal × TaxRate
DiscountAmount = SubTotal × DiscountRate
TotalAmount = SubTotal + TaxAmount - DiscountAmount
```

### Balance Impact
```
BalanceImpact = TotalAmount (always reduces payable)
NewPayableBalance = CurrentBalance - BalanceImpact
```

## Database Triggers (Recommended)

1. **Auto-calculate totals** when items are added/modified
2. **Update supplier balance** when debit note is approved
3. **Validate amounts** against original invoice
4. **Generate unique barcodes** for each debit note
5. **Log all status changes** for audit trail

## Integration Points

- **Purchase Module**: Link to purchase orders/invoices
- **Inventory Module**: Update stock levels for returned items
- **Accounting Module**: Post journal entries for balance changes
- **Reporting Module**: Generate supplier balance reports
- **Notification Module**: Alert suppliers of new debit notes
