-- Create Sales Return Tables
USE [DistributionDB]
GO

-- Create SalesReturns table
CREATE TABLE [dbo].[SalesReturns](
    [ReturnId] [int] IDENTITY(1,1) NOT NULL,
    [ReturnNumber] [nvarchar](50) NOT NULL,
    [ReturnBarcode] [nvarchar](100) NOT NULL,
    [CustomerId] [int] NOT NULL,
    [ReferenceSalesInvoiceId] [int] NULL,
    [ReturnDate] [date] NOT NULL,
    [SubTotal] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [Reason] [nvarchar](max) NULL,
    [Status] [nvarchar](20) NOT NULL DEFAULT ('PENDING'),
    [Remarks] [nvarchar](500) NULL,
    [CreatedDate] [datetime] NOT NULL DEFAULT (getdate()),
    [CreatedBy] [int] NULL,
    [ModifiedDate] [datetime] NULL,
    [ModifiedBy] [int] NULL,
    CONSTRAINT [PK_SalesReturns] PRIMARY KEY CLUSTERED 
    (
        [ReturnId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [UQ_SalesReturns_ReturnNumber] UNIQUE NONCLUSTERED 
    (
        [ReturnNumber] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
    CONSTRAINT [UQ_SalesReturns_ReturnBarcode] UNIQUE NONCLUSTERED 
    (
        [ReturnBarcode] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

-- Create SalesReturnItems table
CREATE TABLE [dbo].[SalesReturnItems](
    [ReturnItemId] [int] IDENTITY(1,1) NOT NULL,
    [ReturnId] [int] NOT NULL,
    [ProductId] [int] NOT NULL,
    [Quantity] [decimal](18, 2) NOT NULL,
    [UnitPrice] [decimal](18, 2) NOT NULL,
    [TaxPercentage] [decimal](5, 2) NULL,
    [TaxAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [DiscountPercentage] [decimal](5, 2) NULL,
    [DiscountAmount] [decimal](18, 2) NOT NULL DEFAULT ((0)),
    [TotalAmount] [decimal](18, 2) NOT NULL,
    [Remarks] [nvarchar](255) NULL,
    CONSTRAINT [PK_SalesReturnItems] PRIMARY KEY CLUSTERED 
    (
        [ReturnItemId] ASC
    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

-- Add foreign key constraints
ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [FK_SalesReturns_Customers] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([CustomerId])
GO

ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_Customers]
GO

ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [FK_SalesReturns_SalesInvoices] FOREIGN KEY([ReferenceSalesInvoiceId])
REFERENCES [dbo].[SalesInvoices] ([SalesInvoiceId])
GO

ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_SalesInvoices]
GO

ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [FK_SalesReturns_Users_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_Users_Created]
GO

ALTER TABLE [dbo].[SalesReturns] WITH CHECK ADD CONSTRAINT [FK_SalesReturns_Users_Modified] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[SalesReturns] CHECK CONSTRAINT [FK_SalesReturns_Users_Modified]
GO

ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [FK_SalesReturnItems_SalesReturns] FOREIGN KEY([ReturnId])
REFERENCES [dbo].[SalesReturns] ([ReturnId])
GO

ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [FK_SalesReturnItems_SalesReturns]
GO

ALTER TABLE [dbo].[SalesReturnItems] WITH CHECK ADD CONSTRAINT [FK_SalesReturnItems_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO

ALTER TABLE [dbo].[SalesReturnItems] CHECK CONSTRAINT [FK_SalesReturnItems_Products]
GO

-- Create indexes for better performance
CREATE NONCLUSTERED INDEX [IX_SalesReturns_CustomerId] ON [dbo].[SalesReturns]
(
    [CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_SalesReturns_ReturnDate] ON [dbo].[SalesReturns]
(
    [ReturnDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [IX_SalesReturnItems_ReturnId] ON [dbo].[SalesReturnItems]
(
    [ReturnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

-- Insert sample data for testing
INSERT INTO [dbo].[SalesReturns] ([ReturnNumber], [ReturnBarcode], [CustomerId], [ReturnDate], [SubTotal], [TaxAmount], [DiscountAmount], [TotalAmount], [Reason], [Status], [Remarks], [CreatedBy])
VALUES 
('SR-2025-000001', 'SR2025000001', 1, '2025-01-07', 100.00, 10.00, 0.00, 110.00, 'Defective product', 'PENDING', 'Customer complaint', 1),
('SR-2025-000002', 'SR2025000002', 2, '2025-01-07', 250.00, 25.00, 5.00, 270.00, 'Wrong size', 'APPROVED', 'Size mismatch', 1)

GO

-- Insert sample return items
INSERT INTO [dbo].[SalesReturnItems] ([ReturnId], [ProductId], [Quantity], [UnitPrice], [TaxPercentage], [TaxAmount], [DiscountPercentage], [DiscountAmount], [TotalAmount], [Remarks])
VALUES 
(1, 1, 2, 50.00, 10.00, 10.00, 0.00, 0.00, 110.00, 'Defective unit'),
(2, 2, 1, 250.00, 10.00, 25.00, 2.00, 5.00, 270.00, 'Wrong size ordered')

GO

PRINT 'Sales Return tables created successfully!'

