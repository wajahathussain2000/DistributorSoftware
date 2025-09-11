-- Add sample expense data
USE [DistributionDB]
GO

-- Insert sample expenses
INSERT INTO [dbo].[Expenses] ([ExpenseCode], [Barcode], [CategoryId], [ExpenseDate], [Amount], [Description], [ReferenceNumber], [PaymentMethod], [Status], [CreatedBy])
VALUES 
    ('EXP-2025-0001', 'EXP20250101123456789001', 1, '2025-01-01', 150.00, 'Office supplies purchase - pens, paper, notebooks', 'REF-001', 'Cash', 'PENDING', 1),
    ('EXP-2025-0002', 'EXP20250101123456789002', 2, '2025-01-01', 75.50, 'Travel expenses - client meeting', 'REF-002', 'Credit Card', 'APPROVED', 1),
    ('EXP-2025-0003', 'EXP20250101123456789003', 3, '2025-01-01', 200.00, 'Equipment maintenance - printer repair', 'REF-003', 'Bank Transfer', 'PENDING', 1),
    ('EXP-2025-0004', 'EXP20250101123456789004', 4, '2025-01-02', 120.00, 'Monthly electricity bill', 'REF-004', 'Bank Transfer', 'APPROVED', 1),
    ('EXP-2025-0005', 'EXP20250101123456789005', 5, '2025-01-02', 85.00, 'Office cleaning supplies', 'REF-005', 'Cash', 'PENDING', 1),
    ('EXP-2025-0006', 'EXP20250101123456789006', 6, '2025-01-03', 300.00, 'Marketing materials - brochures and flyers', 'REF-006', 'Credit Card', 'APPROVED', 1),
    ('EXP-2025-0007', 'EXP20250101123456789007', 7, '2025-01-03', 250.00, 'Employee training course', 'REF-007', 'Bank Transfer', 'PENDING', 1),
    ('EXP-2025-0008', 'EXP20250101123456789008', 8, '2025-01-04', 45.00, 'Miscellaneous office expenses', 'REF-008', 'Cash', 'DRAFT', 1),
    ('EXP-2025-0009', 'EXP20250101123456789009', 9, '2025-01-04', 500.00, 'New computer monitor', 'REF-009', 'Credit Card', 'APPROVED', 1),
    ('EXP-2025-0010', 'EXP20250101123456789010', 10, '2025-01-05', 180.00, 'Software license renewal', 'REF-010', 'Bank Transfer', 'PENDING', 1)

-- Show final count
SELECT COUNT(*) as TotalExpenses FROM Expenses
GO
