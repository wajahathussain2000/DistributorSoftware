-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Seed dummy data for Expense Categories and Expenses tables
-- =============================================

-- Create ExpenseCategories table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='ExpenseCategories' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[ExpenseCategories](
        [CategoryId] [int] IDENTITY(1,1) NOT NULL,
        [CategoryCode] [varchar](20) NOT NULL,
        [CategoryName] [varchar](100) NOT NULL,
        [Description] [varchar](500) NULL,
        [IsActive] [bit] NOT NULL DEFAULT(1),
        [CreatedBy] [int] NULL,
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [ModifiedBy] [int] NULL,
        [ModifiedDate] [datetime] NULL,
        CONSTRAINT [PK_ExpenseCategories] PRIMARY KEY CLUSTERED ([CategoryId] ASC)
    )
END

-- Create Expenses table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Expenses' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Expenses](
        [ExpenseId] [int] IDENTITY(1,1) NOT NULL,
        [ExpenseNumber] [varchar](50) NOT NULL,
        [ExpenseDate] [datetime] NOT NULL,
        [Description] [varchar](500) NOT NULL,
        [Amount] [decimal](18,2) NOT NULL,
        [CategoryId] [int] NOT NULL,
        [UserId] [int] NOT NULL,
        [PaymentMethod] [varchar](50) NOT NULL,
        [ReferenceNumber] [varchar](100) NULL,
        [ReceiptNumber] [varchar](100) NULL,
        [VendorName] [varchar](200) NULL,
        [VendorContact] [varchar](100) NULL,
        [VendorPhone] [varchar](20) NULL,
        [VendorEmail] [varchar](100) NULL,
        [Status] [varchar](20) NOT NULL DEFAULT('PENDING'),
        [ApprovedBy] [int] NULL,
        [ApprovedDate] [datetime] NULL,
        [Remarks] [varchar](1000) NULL,
        [CreatedBy] [int] NOT NULL,
        [CreatedDate] [datetime] NOT NULL DEFAULT(GETDATE()),
        [ModifiedBy] [int] NULL,
        [ModifiedDate] [datetime] NULL,
        CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED ([ExpenseId] ASC),
        CONSTRAINT [FK_Expenses_CategoryId] FOREIGN KEY([CategoryId]) REFERENCES [dbo].[ExpenseCategories] ([CategoryId]),
        CONSTRAINT [FK_Expenses_UserId] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([UserId]),
        CONSTRAINT [FK_Expenses_ApprovedBy] FOREIGN KEY([ApprovedBy]) REFERENCES [dbo].[Users] ([UserId]),
        CONSTRAINT [FK_Expenses_CreatedBy] FOREIGN KEY([CreatedBy]) REFERENCES [dbo].[Users] ([UserId]),
        CONSTRAINT [FK_Expenses_ModifiedBy] FOREIGN KEY([ModifiedBy]) REFERENCES [dbo].[Users] ([UserId])
    )
END

-- Clear existing data
DELETE FROM Expenses
DELETE FROM ExpenseCategories

-- Reset identity columns
DBCC CHECKIDENT ('ExpenseCategories', RESEED, 0)
DBCC CHECKIDENT ('Expenses', RESEED, 0)

-- Insert Expense Categories
INSERT INTO ExpenseCategories (CategoryCode, CategoryName, Description, IsActive, CreatedBy, CreatedDate)
VALUES 
('OFFICE', 'Office Supplies', 'Office stationery, equipment, and supplies', 1, 1, GETDATE()),
('TRAVEL', 'Travel & Transportation', 'Business travel, fuel, and transportation costs', 1, 1, GETDATE()),
('MEALS', 'Meals & Entertainment', 'Business meals, client entertainment, and hospitality', 1, 1, GETDATE()),
('UTILITIES', 'Utilities', 'Electricity, water, gas, internet, and phone bills', 1, 1, GETDATE()),
('MAINTENANCE', 'Maintenance & Repairs', 'Equipment maintenance, repairs, and servicing', 1, 1, GETDATE()),
('MARKETING', 'Marketing & Advertising', 'Promotional activities, advertising, and marketing materials', 1, 1, GETDATE()),
('PROFESSIONAL', 'Professional Services', 'Legal, accounting, consulting, and professional fees', 1, 1, GETDATE()),
('INSURANCE', 'Insurance', 'Business insurance premiums and coverage', 1, 1, GETDATE()),
('RENT', 'Rent & Lease', 'Office rent, equipment lease, and facility rental', 1, 1, GETDATE()),
('TRAINING', 'Training & Development', 'Employee training, courses, and professional development', 1, 1, GETDATE())

-- Insert dummy expense data
INSERT INTO Expenses (ExpenseNumber, ExpenseDate, Description, Amount, CategoryId, UserId, PaymentMethod, ReferenceNumber, ReceiptNumber, VendorName, VendorContact, VendorPhone, VendorEmail, Status, ApprovedBy, ApprovedDate, Remarks, CreatedBy, CreatedDate)
VALUES 
-- Office Supplies
('EXP-2025-001', '2025-01-15', 'Office stationery and supplies', 2500.00, 1, 1, 'CASH', 'REF-001', 'RCP-001', 'Stationery Plus', 'John Smith', '0300-1234567', 'john@stationeryplus.com', 'APPROVED', 2, '2025-01-16', 'Monthly office supplies', 1, '2025-01-15'),
('EXP-2025-002', '2025-01-18', 'Printer paper and toner cartridges', 4500.00, 1, 2, 'CARD', 'REF-002', 'RCP-002', 'Office Depot', 'Sarah Johnson', '0300-2345678', 'sarah@officedepot.com', 'PAID', 2, '2025-01-19', 'Urgent printing supplies', 2, '2025-01-18'),
('EXP-2025-003', '2025-01-22', 'Computer accessories and cables', 3200.00, 1, 3, 'BANK_TRANSFER', 'REF-003', 'RCP-003', 'Tech Solutions', 'Mike Wilson', '0300-3456789', 'mike@techsolutions.com', 'APPROVED', 2, '2025-01-23', 'IT equipment accessories', 3, '2025-01-22'),

-- Travel & Transportation
('EXP-2025-004', '2025-01-20', 'Business trip to Karachi', 8500.00, 2, 1, 'CARD', 'REF-004', 'RCP-004', 'Travel Agency', 'Ahmed Ali', '0300-4567890', 'ahmed@travelagency.com', 'PAID', 2, '2025-01-21', 'Client meeting in Karachi', 1, '2025-01-20'),
('EXP-2025-005', '2025-01-25', 'Fuel for delivery vehicles', 12000.00, 2, 4, 'CASH', 'REF-005', 'RCP-005', 'Shell Petrol Station', 'Station Manager', '0300-5678901', 'shell@station.com', 'APPROVED', 2, '2025-01-26', 'Monthly fuel expenses', 4, '2025-01-25'),
('EXP-2025-006', '2025-01-28', 'Vehicle maintenance and service', 15000.00, 2, 4, 'CHEQUE', 'REF-006', 'RCP-006', 'Auto Care Center', 'Hassan Khan', '0300-6789012', 'hassan@autocare.com', 'PENDING', NULL, NULL, 'Regular vehicle service', 4, '2025-01-28'),

-- Meals & Entertainment
('EXP-2025-007', '2025-01-16', 'Client lunch meeting', 3500.00, 3, 1, 'CARD', 'REF-007', 'RCP-007', 'Restaurant ABC', 'Manager', '0300-7890123', 'info@restaurantabc.com', 'APPROVED', 2, '2025-01-17', 'Business development lunch', 1, '2025-01-16'),
('EXP-2025-008', '2025-01-24', 'Team dinner celebration', 8500.00, 3, 2, 'CASH', 'REF-008', 'RCP-008', 'Fine Dining Restaurant', 'Chef Manager', '0300-8901234', 'chef@finedining.com', 'PAID', 2, '2025-01-25', 'Monthly team celebration', 2, '2025-01-24'),
('EXP-2025-009', '2025-01-30', 'Coffee and snacks for office', 1200.00, 3, 3, 'EASYPAISA', 'REF-009', 'RCP-009', 'Coffee Shop', 'Barista', '0300-9012345', 'coffee@shop.com', 'APPROVED', 2, '2025-01-31', 'Office refreshments', 3, '2025-01-30'),

-- Utilities
('EXP-2025-010', '2025-01-15', 'Monthly electricity bill', 25000.00, 4, 5, 'BANK_TRANSFER', 'REF-010', 'RCP-010', 'LESCO', 'Billing Department', '0300-0123456', 'billing@lesco.com', 'PAID', 2, '2025-01-16', 'Office electricity bill', 5, '2025-01-15'),
('EXP-2025-011', '2025-01-18', 'Internet and phone services', 15000.00, 4, 5, 'CARD', 'REF-011', 'RCP-011', 'PTCL', 'Customer Service', '0300-1234567', 'billing@ptcl.com', 'APPROVED', 2, '2025-01-19', 'Monthly internet package', 5, '2025-01-18'),
('EXP-2025-012', '2025-01-25', 'Water bill payment', 3500.00, 4, 5, 'CASH', 'REF-012', 'RCP-012', 'WASA', 'Billing Office', '0300-2345678', 'billing@wasa.com', 'PENDING', NULL, NULL, 'Office water bill', 5, '2025-01-25'),

-- Maintenance & Repairs
('EXP-2025-013', '2025-01-19', 'Air conditioning repair', 18000.00, 5, 6, 'CHEQUE', 'REF-013', 'RCP-013', 'AC Repair Services', 'Technician', '0300-3456789', 'repair@acservices.com', 'PAID', 2, '2025-01-20', 'Office AC maintenance', 6, '2025-01-19'),
('EXP-2025-014', '2025-01-26', 'Computer system maintenance', 8000.00, 5, 3, 'CARD', 'REF-014', 'RCP-014', 'IT Support Company', 'Tech Support', '0300-4567890', 'support@itcompany.com', 'APPROVED', 2, '2025-01-27', 'System maintenance contract', 3, '2025-01-26'),
('EXP-2025-015', '2025-01-29', 'Office furniture repair', 5500.00, 5, 6, 'BANK_TRANSFER', 'REF-015', 'RCP-015', 'Furniture Repair Shop', 'Craftsman', '0300-5678901', 'repair@furniture.com', 'PENDING', NULL, NULL, 'Chair and table repairs', 6, '2025-01-29'),

-- Marketing & Advertising
('EXP-2025-016', '2025-01-17', 'Social media advertising campaign', 12000.00, 6, 7, 'CARD', 'REF-016', 'RCP-016', 'Digital Marketing Agency', 'Marketing Manager', '0300-6789012', 'marketing@digitalagency.com', 'PAID', 2, '2025-01-18', 'Facebook and Google ads', 7, '2025-01-17'),
('EXP-2025-017', '2025-01-23', 'Print advertising in newspaper', 25000.00, 6, 7, 'CHEQUE', 'REF-017', 'RCP-017', 'Daily News', 'Ad Sales', '0300-7890123', 'ads@dailynews.com', 'APPROVED', 2, '2025-01-24', 'Product launch advertisement', 7, '2025-01-23'),
('EXP-2025-018', '2025-01-27', 'Promotional materials printing', 8500.00, 6, 7, 'CASH', 'REF-018', 'RCP-018', 'Print Shop', 'Print Manager', '0300-8901234', 'print@shop.com', 'PENDING', NULL, NULL, 'Brochures and flyers', 7, '2025-01-27'),

-- Professional Services
('EXP-2025-019', '2025-01-21', 'Legal consultation fees', 35000.00, 7, 8, 'BANK_TRANSFER', 'REF-019', 'RCP-019', 'Law Firm', 'Senior Partner', '0300-9012345', 'legal@lawfirm.com', 'PAID', 2, '2025-01-22', 'Contract review and advice', 8, '2025-01-21'),
('EXP-2025-020', '2025-01-24', 'Accounting services', 20000.00, 7, 8, 'CARD', 'REF-020', 'RCP-020', 'Accounting Firm', 'CPA', '0300-0123456', 'accounting@firm.com', 'APPROVED', 2, '2025-01-25', 'Monthly bookkeeping services', 8, '2025-01-24'),
('EXP-2025-021', '2025-01-28', 'Business consulting fees', 45000.00, 7, 8, 'CHEQUE', 'REF-021', 'RCP-021', 'Business Consultants', 'Senior Consultant', '0300-1234567', 'consulting@business.com', 'PENDING', NULL, NULL, 'Strategic planning consultation', 8, '2025-01-28'),

-- Insurance
('EXP-2025-022', '2025-01-15', 'Business insurance premium', 50000.00, 8, 9, 'BANK_TRANSFER', 'REF-022', 'RCP-022', 'Insurance Company', 'Agent', '0300-2345678', 'agent@insurance.com', 'PAID', 2, '2025-01-16', 'Annual business insurance', 9, '2025-01-15'),
('EXP-2025-023', '2025-01-20', 'Vehicle insurance renewal', 25000.00, 8, 4, 'CARD', 'REF-023', 'RCP-023', 'Auto Insurance Co', 'Insurance Agent', '0300-3456789', 'auto@insurance.com', 'APPROVED', 2, '2025-01-21', 'Fleet vehicle insurance', 4, '2025-01-20'),

-- Rent & Lease
('EXP-2025-024', '2025-01-01', 'Office rent payment', 150000.00, 9, 10, 'BANK_TRANSFER', 'REF-024', 'RCP-024', 'Property Management', 'Property Manager', '0300-4567890', 'rent@property.com', 'PAID', 2, '2025-01-02', 'Monthly office rent', 10, '2025-01-01'),
('EXP-2025-025', '2025-01-15', 'Equipment lease payment', 35000.00, 9, 11, 'CHEQUE', 'REF-025', 'RCP-025', 'Equipment Leasing Co', 'Lease Manager', '0300-5678901', 'lease@equipment.com', 'APPROVED', 2, '2025-01-16', 'Monthly equipment lease', 11, '2025-01-15'),

-- Training & Development
('EXP-2025-026', '2025-01-18', 'Employee training workshop', 25000.00, 10, 12, 'CARD', 'REF-026', 'RCP-026', 'Training Institute', 'Training Coordinator', '0300-6789012', 'training@institute.com', 'PAID', 2, '2025-01-19', 'Sales team training program', 12, '2025-01-18'),
('EXP-2025-027', '2025-01-25', 'Online course subscription', 12000.00, 10, 13, 'EASYPAISA', 'REF-027', 'RCP-027', 'Online Learning Platform', 'Platform Manager', '0300-7890123', 'courses@onlinelearning.com', 'APPROVED', 2, '2025-01-26', 'Annual subscription for team', 13, '2025-01-25'),
('EXP-2025-028', '2025-01-29', 'Professional certification fees', 18000.00, 10, 14, 'JAZZCASH', 'REF-028', 'RCP-028', 'Certification Body', 'Certification Manager', '0300-8901234', 'cert@professional.com', 'PENDING', NULL, NULL, 'Project management certification', 14, '2025-01-29')

-- Additional expenses for February 2025
INSERT INTO Expenses (ExpenseNumber, ExpenseDate, Description, Amount, CategoryId, UserId, PaymentMethod, ReferenceNumber, ReceiptNumber, VendorName, VendorContact, VendorPhone, VendorEmail, Status, ApprovedBy, ApprovedDate, Remarks, CreatedBy, CreatedDate)
VALUES 
('EXP-2025-029', '2025-02-01', 'Office supplies for February', 3200.00, 1, 1, 'CASH', 'REF-029', 'RCP-029', 'Stationery Plus', 'John Smith', '0300-1234567', 'john@stationeryplus.com', 'APPROVED', 2, '2025-02-02', 'Monthly office supplies', 1, '2025-02-01'),
('EXP-2025-030', '2025-02-05', 'Business trip to Islamabad', 12000.00, 2, 2, 'CARD', 'REF-030', 'RCP-030', 'Travel Agency', 'Ahmed Ali', '0300-4567890', 'ahmed@travelagency.com', 'PAID', 2, '2025-02-06', 'Government meeting in Islamabad', 2, '2025-02-05'),
('EXP-2025-031', '2025-02-10', 'Client entertainment dinner', 6500.00, 3, 1, 'CARD', 'REF-031', 'RCP-031', 'Fine Dining Restaurant', 'Chef Manager', '0300-8901234', 'chef@finedining.com', 'APPROVED', 2, '2025-02-11', 'Important client dinner', 1, '2025-02-10'),
('EXP-2025-032', '2025-02-15', 'Monthly electricity bill', 28000.00, 4, 5, 'BANK_TRANSFER', 'REF-032', 'RCP-032', 'LESCO', 'Billing Department', '0300-0123456', 'billing@lesco.com', 'PAID', 2, '2025-02-16', 'Office electricity bill', 5, '2025-02-15'),
('EXP-2025-033', '2025-02-20', 'Computer system upgrade', 45000.00, 5, 3, 'CHEQUE', 'REF-033', 'RCP-033', 'IT Support Company', 'Tech Support', '0300-4567890', 'support@itcompany.com', 'PENDING', NULL, NULL, 'System hardware upgrade', 3, '2025-02-20')

PRINT 'Expense data seeded successfully!'
PRINT 'Total Expense Categories: ' + CAST((SELECT COUNT(*) FROM ExpenseCategories) AS VARCHAR(10))
PRINT 'Total Expenses: ' + CAST((SELECT COUNT(*) FROM Expenses) AS VARCHAR(10))
PRINT 'Total Amount: ' + CAST((SELECT SUM(Amount) FROM Expenses) AS VARCHAR(20))
