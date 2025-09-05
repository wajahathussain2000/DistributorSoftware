USE [DistributionDB]
GO

-- =============================================
-- Seed Data for Distribution Software
-- Tables: SalesInvoices, Stock, Products, Categories, Customers
-- =============================================

-- Clear existing data (optional - uncomment if needed)
-- DELETE FROM SalesInvoiceDetails
-- DELETE FROM SalesInvoices
-- DELETE FROM Stock
-- DELETE FROM Products
-- DELETE FROM Categories
-- DELETE FROM Customers

-- =============================================
-- 1. CATEGORIES SEED DATA
-- =============================================
INSERT INTO Categories (CategoryName, Description, IsActive, CreatedDate, CreatedBy)
VALUES 
('Electronics', 'Electronic devices and gadgets', 1, GETDATE(), 1),
('Clothing', 'Apparel and fashion items', 1, GETDATE(), 1),
('Home & Garden', 'Home improvement and garden supplies', 1, GETDATE(), 1),
('Sports & Fitness', 'Sports equipment and fitness gear', 1, GETDATE(), 1),
('Books & Media', 'Books, magazines, and digital media', 1, GETDATE(), 1),
('Automotive', 'Car parts and automotive accessories', 1, GETDATE(), 1),
('Health & Beauty', 'Health products and beauty items', 1, GETDATE(), 1),
('Food & Beverages', 'Food items and beverages', 1, GETDATE(), 1)

-- =============================================
-- 2. BRANDS SEED DATA
-- =============================================
INSERT INTO Brands (BrandName, Description, IsActive, CreatedDate, CreatedBy)
VALUES 
('Samsung', 'South Korean electronics company', 1, GETDATE(), 1),
('Apple', 'American technology company', 1, GETDATE(), 1),
('Nike', 'American sportswear company', 1, GETDATE(), 1),
('Adidas', 'German sportswear company', 1, GETDATE(), 1),
('Sony', 'Japanese electronics company', 1, GETDATE(), 1),
('LG', 'South Korean electronics company', 1, GETDATE(), 1),
('Dell', 'American computer technology company', 1, GETDATE(), 1),
('HP', 'American technology company', 1, GETDATE(), 1),
('Canon', 'Japanese imaging and optical products company', 1, GETDATE(), 1),
('Philips', 'Dutch technology company', 1, GETDATE(), 1)

-- =============================================
-- 3. UNITS SEED DATA
-- =============================================
INSERT INTO Units (UnitName, UnitCode, Description, IsActive, CreatedDate)
VALUES 
('Piece', 'PCS', 'Individual items', 1, GETDATE()),
('Kilogram', 'KG', 'Weight measurement', 1, GETDATE()),
('Liter', 'L', 'Volume measurement', 1, GETDATE()),
('Meter', 'M', 'Length measurement', 1, GETDATE()),
('Box', 'BOX', 'Packaged items', 1, GETDATE()),
('Pack', 'PK', 'Packaged items', 1, GETDATE()),
('Dozen', 'DZ', '12 items', 1, GETDATE()),
('Pair', 'PR', '2 items', 1, GETDATE())

-- =============================================
-- 4. WAREHOUSES SEED DATA
-- =============================================
INSERT INTO Warehouses (WarehouseName, Location, ContactPerson, ContactPhone, IsActive, CreatedDate, CreatedBy)
VALUES 
('Main Warehouse', '123 Industrial Ave, City Center', 'John Smith', '+1-555-0101', 1, GETDATE(), 1),
('North Branch', '456 Commerce St, North District', 'Sarah Johnson', '+1-555-0102', 1, GETDATE(), 1),
('South Storage', '789 Business Blvd, South Zone', 'Mike Wilson', '+1-555-0103', 1, GETDATE(), 1),
('East Distribution', '321 Trade Center, East Side', 'Lisa Brown', '+1-555-0104', 1, GETDATE(), 1),
('West Facility', '654 Logistics Lane, West End', 'David Davis', '+1-555-0105', 1, GETDATE(), 1)

-- =============================================
-- 5. CUSTOMERS SEED DATA
-- =============================================
INSERT INTO Customers (CustomerCode, CompanyName, ContactName, Email, Phone, Address, City, State, PostalCode, Country, IsActive, CreatedDate)
VALUES 
('CUST001', 'Tech Solutions Inc', 'Robert Anderson', 'robert@techsolutions.com', '+1-555-1001', '100 Technology Drive', 'New York', 'NY', '10001', 'USA', 1, GETDATE()),
('CUST002', 'Fashion Forward Ltd', 'Emily Chen', 'emily@fashionforward.com', '+1-555-1002', '200 Style Avenue', 'Los Angeles', 'CA', '90210', 'USA', 1, GETDATE()),
('CUST003', 'Home Depot Plus', 'Michael Rodriguez', 'michael@homedepotplus.com', '+1-555-1003', '300 Home Street', 'Chicago', 'IL', '60601', 'USA', 1, GETDATE()),
('CUST004', 'Sports World', 'Jennifer Taylor', 'jennifer@sportsworld.com', '+1-555-1004', '400 Athletic Blvd', 'Houston', 'TX', '77001', 'USA', 1, GETDATE()),
('CUST005', 'Book Haven', 'Christopher Lee', 'chris@bookhaven.com', '+1-555-1005', '500 Literature Lane', 'Phoenix', 'AZ', '85001', 'USA', 1, GETDATE()),
('CUST006', 'Auto Parts Central', 'Amanda White', 'amanda@autoparts.com', '+1-555-1006', '600 Garage Road', 'Philadelphia', 'PA', '19101', 'USA', 1, GETDATE()),
('CUST007', 'Beauty Essentials', 'Daniel Kim', 'daniel@beautyessentials.com', '+1-555-1007', '700 Glamour Street', 'San Antonio', 'TX', '78201', 'USA', 1, GETDATE()),
('CUST008', 'Gourmet Foods', 'Michelle Garcia', 'michelle@gourmetfoods.com', '+1-555-1008', '800 Culinary Court', 'San Diego', 'CA', '92101', 'USA', 1, GETDATE()),
('CUST009', 'Electronics Hub', 'Kevin Martinez', 'kevin@electronicshub.com', '+1-555-1009', '900 Digital Drive', 'Dallas', 'TX', '75201', 'USA', 1, GETDATE()),
('CUST010', 'Retail Chain Corp', 'Nicole Thompson', 'nicole@retailchain.com', '+1-555-1010', '1000 Commerce Center', 'San Jose', 'CA', '95101', 'USA', 1, GETDATE())

-- =============================================
-- 6. PRODUCTS SEED DATA
-- =============================================
INSERT INTO Products (ProductCode, ProductName, Description, Category, UnitPrice, StockQuantity, ReorderLevel, IsActive, CreatedDate, ModifiedDate, BrandId, CategoryId, UnitId, Barcode, WarehouseId, BatchNumber, ExpiryDate)
VALUES 
-- Electronics
('PROD001', 'Samsung Galaxy S21', 'Latest smartphone with advanced camera', 'Electronics', 899.99, 50, 10, 1, GETDATE(), NULL, 1, 1, 1, '1234567890123', 1, 'BATCH001', '2025-12-31'),
('PROD002', 'Apple iPhone 13', 'Premium smartphone with A15 chip', 'Electronics', 999.99, 30, 5, 1, GETDATE(), NULL, 2, 1, 1, '1234567890124', 1, 'BATCH002', '2025-12-31'),
('PROD003', 'Sony WH-1000XM4', 'Noise-cancelling wireless headphones', 'Electronics', 349.99, 25, 8, 1, GETDATE(), NULL, 5, 1, 1, '1234567890125', 1, 'BATCH003', '2025-12-31'),
('PROD004', 'Dell XPS 13', 'Ultrabook laptop with Intel i7', 'Electronics', 1299.99, 15, 5, 1, GETDATE(), NULL, 7, 1, 1, '1234567890126', 1, 'BATCH004', '2025-12-31'),
('PROD005', 'LG OLED TV 55"', '4K OLED smart television', 'Electronics', 1499.99, 12, 3, 1, GETDATE(), NULL, 6, 1, 1, '1234567890127', 1, 'BATCH005', '2025-12-31'),

-- Clothing
('PROD006', 'Nike Air Max 270', 'Comfortable running shoes', 'Clothing', 150.00, 100, 20, 1, GETDATE(), NULL, 3, 2, 8, '1234567890128', 2, 'BATCH006', '2025-12-31'),
('PROD007', 'Adidas Ultraboost 22', 'High-performance running shoes', 'Clothing', 180.00, 80, 15, 1, GETDATE(), NULL, 4, 2, 8, '1234567890129', 2, 'BATCH007', '2025-12-31'),
('PROD008', 'Nike Dri-FIT T-Shirt', 'Moisture-wicking athletic shirt', 'Clothing', 25.00, 200, 50, 1, GETDATE(), NULL, 3, 2, 1, '1234567890130', 2, 'BATCH008', '2025-12-31'),
('PROD009', 'Adidas Track Pants', 'Comfortable athletic pants', 'Clothing', 45.00, 150, 30, 1, GETDATE(), NULL, 4, 2, 1, '1234567890131', 2, 'BATCH009', '2025-12-31'),

-- Home & Garden
('PROD010', 'Philips LED Bulb Pack', 'Energy-efficient LED light bulbs (4-pack)', 'Home & Garden', 19.99, 300, 50, 1, GETDATE(), NULL, 10, 3, 5, '1234567890132', 3, 'BATCH010', '2025-12-31'),
('PROD011', 'Garden Hose 50ft', 'Heavy-duty garden watering hose', 'Home & Garden', 29.99, 75, 15, 1, GETDATE(), NULL, NULL, 3, 1, '1234567890133', 3, 'BATCH011', '2025-12-31'),
('PROD012', 'Power Drill Set', 'Cordless drill with accessories', 'Home & Garden', 89.99, 40, 10, 1, GETDATE(), NULL, NULL, 3, 1, '1234567890134', 3, 'BATCH012', '2025-12-31'),

-- Sports & Fitness
('PROD013', 'Yoga Mat Premium', 'Non-slip yoga and exercise mat', 'Sports & Fitness', 39.99, 120, 25, 1, GETDATE(), NULL, NULL, 4, 1, '1234567890135', 4, 'BATCH013', '2025-12-31'),
('PROD014', 'Dumbbell Set 20lbs', 'Adjustable weight dumbbells', 'Sports & Fitness', 79.99, 60, 12, 1, GETDATE(), NULL, NULL, 4, 1, '1234567890136', 4, 'BATCH014', '2025-12-31'),
('PROD015', 'Basketball Official', 'Official size basketball', 'Sports & Fitness', 24.99, 80, 20, 1, GETDATE(), NULL, NULL, 4, 1, '1234567890137', 4, 'BATCH015', '2025-12-31'),

-- Books & Media
('PROD016', 'Programming Book Set', 'Complete programming guide collection', 'Books & Media', 49.99, 200, 40, 1, GETDATE(), NULL, NULL, 5, 1, '1234567890138', 5, 'BATCH016', '2025-12-31'),
('PROD017', 'Business Strategy Guide', 'Modern business management book', 'Books & Media', 29.99, 150, 30, 1, GETDATE(), NULL, NULL, 5, 1, '1234567890139', 5, 'BATCH017', '2025-12-31'),

-- Automotive
('PROD018', 'Car Battery 12V', 'High-performance car battery', 'Automotive', 129.99, 45, 10, 1, GETDATE(), NULL, NULL, 6, 1, '1234567890140', 1, 'BATCH018', '2025-12-31'),
('PROD019', 'Engine Oil 5W-30', 'Synthetic engine oil 1 quart', 'Automotive', 12.99, 300, 50, 1, GETDATE(), NULL, NULL, 6, 1, '1234567890141', 1, 'BATCH019', '2025-12-31'),

-- Health & Beauty
('PROD020', 'Vitamin C Serum', 'Anti-aging vitamin C facial serum', 'Health & Beauty', 34.99, 180, 35, 1, GETDATE(), NULL, NULL, 7, 1, '1234567890142', 2, 'BATCH020', '2025-12-31'),
('PROD021', 'Protein Powder', 'Whey protein supplement 2lbs', 'Health & Beauty', 39.99, 120, 25, 1, GETDATE(), NULL, NULL, 7, 1, '1234567890143', 2, 'BATCH021', '2025-12-31'),

-- Food & Beverages
('PROD022', 'Organic Coffee Beans', 'Premium organic coffee 1lb', 'Food & Beverages', 15.99, 250, 50, 1, GETDATE(), NULL, NULL, 8, 1, '1234567890144', 3, 'BATCH022', '2025-12-31'),
('PROD023', 'Green Tea Pack', 'Premium green tea 100 bags', 'Food & Beverages', 12.99, 200, 40, 1, GETDATE(), NULL, NULL, 8, 1, '1234567890145', 3, 'BATCH023', '2025-12-31')

-- =============================================
-- 7. STOCK SEED DATA
-- =============================================
INSERT INTO Stock (ProductId, WarehouseId, Quantity, BatchNumber, ExpiryDate, LastUpdated, UpdatedBy)
VALUES 
-- Main Warehouse Stock
(1, 1, 50, 'BATCH001', '2025-12-31', GETDATE(), 1),
(2, 1, 30, 'BATCH002', '2025-12-31', GETDATE(), 1),
(3, 1, 25, 'BATCH003', '2025-12-31', GETDATE(), 1),
(4, 1, 15, 'BATCH004', '2025-12-31', GETDATE(), 1),
(5, 1, 12, 'BATCH005', '2025-12-31', GETDATE(), 1),
(18, 1, 45, 'BATCH018', '2025-12-31', GETDATE(), 1),
(19, 1, 300, 'BATCH019', '2025-12-31', GETDATE(), 1),

-- North Branch Stock
(6, 2, 100, 'BATCH006', '2025-12-31', GETDATE(), 1),
(7, 2, 80, 'BATCH007', '2025-12-31', GETDATE(), 1),
(8, 2, 200, 'BATCH008', '2025-12-31', GETDATE(), 1),
(9, 2, 150, 'BATCH009', '2025-12-31', GETDATE(), 1),
(20, 2, 180, 'BATCH020', '2025-12-31', GETDATE(), 1),
(21, 2, 120, 'BATCH021', '2025-12-31', GETDATE(), 1),

-- South Storage Stock
(10, 3, 300, 'BATCH010', '2025-12-31', GETDATE(), 1),
(11, 3, 75, 'BATCH011', '2025-12-31', GETDATE(), 1),
(12, 3, 40, 'BATCH012', '2025-12-31', GETDATE(), 1),
(22, 3, 250, 'BATCH022', '2025-12-31', GETDATE(), 1),
(23, 3, 200, 'BATCH023', '2025-12-31', GETDATE(), 1),

-- East Distribution Stock
(13, 4, 120, 'BATCH013', '2025-12-31', GETDATE(), 1),
(14, 4, 60, 'BATCH014', '2025-12-31', GETDATE(), 1),
(15, 4, 80, 'BATCH015', '2025-12-31', GETDATE(), 1),

-- West Facility Stock
(16, 5, 200, 'BATCH016', '2025-12-31', GETDATE(), 1),
(17, 5, 150, 'BATCH017', '2025-12-31', GETDATE(), 1)

-- =============================================
-- 8. SALES INVOICES SEED DATA
-- =============================================
INSERT INTO SalesInvoices (InvoiceNumber, CustomerId, SalesmanId, InvoiceDate, DueDate, SubTotal, TaxAmount, DiscountAmount, TotalAmount, PaidAmount, BalanceAmount, PaymentMode, Status, Remarks, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy)
VALUES 
('INV-2024-001', 1, 1, '2024-01-15', '2024-02-15', 899.99, 89.99, 0.00, 989.98, 989.98, 0.00, 'Credit Card', 'PAID', 'Samsung Galaxy S21 sale', GETDATE(), 1, NULL, NULL),
('INV-2024-002', 2, 1, '2024-01-16', '2024-02-16', 150.00, 15.00, 10.00, 155.00, 155.00, 0.00, 'Cash', 'PAID', 'Nike shoes sale', GETDATE(), 1, NULL, NULL),
('INV-2024-003', 3, 1, '2024-01-17', '2024-02-17', 1299.99, 130.00, 50.00, 1379.99, 500.00, 879.99, 'Bank Transfer', 'PENDING', 'Dell laptop sale', GETDATE(), 1, NULL, NULL),
('INV-2024-004', 4, 1, '2024-01-18', '2024-02-18', 349.99, 35.00, 0.00, 384.99, 384.99, 0.00, 'Credit Card', 'PAID', 'Sony headphones sale', GETDATE(), 1, NULL, NULL),
('INV-2024-005', 5, 1, '2024-01-19', '2024-02-19', 49.99, 5.00, 0.00, 54.99, 54.99, 0.00, 'Cash', 'PAID', 'Programming book sale', GETDATE(), 1, NULL, NULL),
('INV-2024-006', 6, 1, '2024-01-20', '2024-02-20', 129.99, 13.00, 0.00, 142.99, 0.00, 142.99, 'Bank Transfer', 'PENDING', 'Car battery sale', GETDATE(), 1, NULL, NULL),
('INV-2024-007', 7, 1, '2024-01-21', '2024-02-21', 34.99, 3.50, 0.00, 38.49, 38.49, 0.00, 'Credit Card', 'PAID', 'Vitamin C serum sale', GETDATE(), 1, NULL, NULL),
('INV-2024-008', 8, 1, '2024-01-22', '2024-02-22', 15.99, 1.60, 0.00, 17.59, 17.59, 0.00, 'Cash', 'PAID', 'Coffee beans sale', GETDATE(), 1, NULL, NULL),
('INV-2024-009', 9, 1, '2024-01-23', '2024-02-23', 999.99, 100.00, 100.00, 999.99, 999.99, 0.00, 'Credit Card', 'PAID', 'iPhone 13 sale', GETDATE(), 1, NULL, NULL),
('INV-2024-010', 10, 1, '2024-01-24', '2024-02-24', 1499.99, 150.00, 0.00, 1649.99, 800.00, 849.99, 'Bank Transfer', 'PENDING', 'LG TV sale', GETDATE(), 1, NULL, NULL),
('INV-2024-011', 1, 1, '2024-01-25', '2024-02-25', 180.00, 18.00, 0.00, 198.00, 198.00, 0.00, 'Credit Card', 'PAID', 'Adidas shoes sale', GETDATE(), 1, NULL, NULL),
('INV-2024-012', 2, 1, '2024-01-26', '2024-02-26', 19.99, 2.00, 0.00, 21.99, 21.99, 0.00, 'Cash', 'PAID', 'LED bulbs sale', GETDATE(), 1, NULL, NULL),
('INV-2024-013', 3, 1, '2024-01-27', '2024-02-27', 39.99, 4.00, 0.00, 43.99, 43.99, 0.00, 'Credit Card', 'PAID', 'Yoga mat sale', GETDATE(), 1, NULL, NULL),
('INV-2024-014', 4, 1, '2024-01-28', '2024-02-28', 29.99, 3.00, 0.00, 32.99, 32.99, 0.00, 'Cash', 'PAID', 'Business book sale', GETDATE(), 1, NULL, NULL),
('INV-2024-015', 5, 1, '2024-01-29', '2024-02-29', 12.99, 1.30, 0.00, 14.29, 14.29, 0.00, 'Credit Card', 'PAID', 'Engine oil sale', GETDATE(), 1, NULL, NULL)

-- =============================================
-- 9. SALES INVOICE DETAILS SEED DATA
-- =============================================
INSERT INTO SalesInvoiceDetails (SalesInvoiceId, ProductId, Quantity, UnitPrice, TaxPercentage, TaxAmount, DiscountPercentage, DiscountAmount, TotalAmount, Remarks)
VALUES 
-- Invoice 1 Details
(1, 1, 1, 899.99, 10.00, 89.99, 0.00, 0.00, 989.98, 'Samsung Galaxy S21'),

-- Invoice 2 Details
(2, 6, 1, 150.00, 10.00, 15.00, 6.67, 10.00, 155.00, 'Nike Air Max 270'),

-- Invoice 3 Details
(3, 4, 1, 1299.99, 10.00, 130.00, 3.85, 50.00, 1379.99, 'Dell XPS 13'),

-- Invoice 4 Details
(4, 3, 1, 349.99, 10.00, 35.00, 0.00, 0.00, 384.99, 'Sony WH-1000XM4'),

-- Invoice 5 Details
(5, 16, 1, 49.99, 10.00, 5.00, 0.00, 0.00, 54.99, 'Programming Book Set'),

-- Invoice 6 Details
(6, 18, 1, 129.99, 10.00, 13.00, 0.00, 0.00, 142.99, 'Car Battery 12V'),

-- Invoice 7 Details
(7, 20, 1, 34.99, 10.00, 3.50, 0.00, 0.00, 38.49, 'Vitamin C Serum'),

-- Invoice 8 Details
(8, 22, 1, 15.99, 10.00, 1.60, 0.00, 0.00, 17.59, 'Organic Coffee Beans'),

-- Invoice 9 Details
(9, 2, 1, 999.99, 10.00, 100.00, 10.00, 100.00, 999.99, 'Apple iPhone 13'),

-- Invoice 10 Details
(10, 5, 1, 1499.99, 10.00, 150.00, 0.00, 0.00, 1649.99, 'LG OLED TV 55"'),

-- Invoice 11 Details
(11, 7, 1, 180.00, 10.00, 18.00, 0.00, 0.00, 198.00, 'Adidas Ultraboost 22'),

-- Invoice 12 Details
(12, 10, 1, 19.99, 10.00, 2.00, 0.00, 0.00, 21.99, 'Philips LED Bulb Pack'),

-- Invoice 13 Details
(13, 13, 1, 39.99, 10.00, 4.00, 0.00, 0.00, 43.99, 'Yoga Mat Premium'),

-- Invoice 14 Details
(14, 17, 1, 29.99, 10.00, 3.00, 0.00, 0.00, 32.99, 'Business Strategy Guide'),

-- Invoice 15 Details
(15, 19, 1, 12.99, 10.00, 1.30, 0.00, 0.00, 14.29, 'Engine Oil 5W-30')

-- =============================================
-- VERIFICATION QUERIES
-- =============================================
SELECT 'Seed data insertion completed successfully!' as Status;

SELECT 
    'Categories' as TableName, COUNT(*) as RecordCount FROM Categories
UNION ALL
SELECT 'Brands', COUNT(*) FROM Brands
UNION ALL
SELECT 'Units', COUNT(*) FROM Units
UNION ALL
SELECT 'Warehouses', COUNT(*) FROM Warehouses
UNION ALL
SELECT 'Customers', COUNT(*) FROM Customers
UNION ALL
SELECT 'Products', COUNT(*) FROM Products
UNION ALL
SELECT 'Stock', COUNT(*) FROM Stock
UNION ALL
SELECT 'Sales Invoices', COUNT(*) FROM SalesInvoices
UNION ALL
SELECT 'Sales Invoice Details', COUNT(*) FROM SalesInvoiceDetails;

GO
