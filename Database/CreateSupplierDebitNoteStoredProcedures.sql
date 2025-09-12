-- Create stored procedures for Supplier Debit Note functionality
-- This script creates the missing stored procedures for the Supplier Debit Note system

USE DistributionDB;
GO

-- Create the main stored procedure for creating supplier debit notes
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CreateSupplierDebitNote')
    DROP PROCEDURE sp_CreateSupplierDebitNote;
GO

CREATE PROCEDURE sp_CreateSupplierDebitNote
    @DebitNoteNumber NVARCHAR(50),
    @Barcode NVARCHAR(100),
    @SupplierId INT,
    @DebitDate DATETIME,
    @Reason NVARCHAR(100),
    @Status NVARCHAR(20),
    @Remarks NVARCHAR(MAX),
    @OriginalInvoiceId INT = NULL,
    @ReferencePurchaseId INT = NULL,
    @ReferenceGRNId INT = NULL,
    @CreatedBy INT,
    @DebitNoteId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        BEGIN TRANSACTION;
        
        -- Insert the main debit note record
        INSERT INTO SupplierDebitNote (
            DebitNoteNumber,
            Barcode,
            SupplierId,
            DebitDate,
            Reason,
            Status,
            Remarks,
            OriginalInvoiceId,
            ReferencePurchaseId,
            ReferenceGRNId,
            CreatedBy,
            CreatedDate,
            UpdatedBy,
            UpdatedDate
        )
        VALUES (
            @DebitNoteNumber,
            @Barcode,
            @SupplierId,
            @DebitDate,
            @Reason,
            @Status,
            @Remarks,
            @OriginalInvoiceId,
            @ReferencePurchaseId,
            @ReferenceGRNId,
            @CreatedBy,
            GETDATE(),
            @CreatedBy,
            GETDATE()
        );
        
        -- Get the generated debit note ID
        SET @DebitNoteId = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
        
        SELECT 'SUCCESS' AS Result, @DebitNoteId AS DebitNoteId;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Create stored procedure for creating supplier debit note items
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CreateSupplierDebitNoteItem')
    DROP PROCEDURE sp_CreateSupplierDebitNoteItem;
GO

CREATE PROCEDURE sp_CreateSupplierDebitNoteItem
    @DebitNoteId INT,
    @ProductId INT,
    @Quantity DECIMAL(18,2),
    @UnitPrice DECIMAL(18,2),
    @TaxPercentage DECIMAL(5,2),
    @TaxAmount DECIMAL(18,2),
    @DiscountPercentage DECIMAL(5,2),
    @DiscountAmount DECIMAL(18,2),
    @TotalAmount DECIMAL(18,2),
    @Reason NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        INSERT INTO SupplierDebitNoteItem (
            DebitNoteId,
            ProductId,
            Quantity,
            UnitPrice,
            TaxPercentage,
            TaxAmount,
            DiscountPercentage,
            DiscountAmount,
            TotalAmount,
            Reason,
            CreatedDate
        )
        VALUES (
            @DebitNoteId,
            @ProductId,
            @Quantity,
            @UnitPrice,
            @TaxPercentage,
            @TaxAmount,
            @DiscountPercentage,
            @DiscountAmount,
            @TotalAmount,
            @Reason,
            GETDATE()
        );
        
        SELECT 'SUCCESS' AS Result;
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Create stored procedure for generating debit note numbers
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_GenerateSupplierDebitNoteNumber')
    DROP PROCEDURE sp_GenerateSupplierDebitNoteNumber;
GO

CREATE PROCEDURE sp_GenerateSupplierDebitNoteNumber
    @GeneratedNumber NVARCHAR(50) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        DECLARE @DatePrefix NVARCHAR(8) = FORMAT(GETDATE(), 'yyyyMMdd');
        DECLARE @SequenceNumber INT;
        DECLARE @MaxNumber INT;
        
        -- Get the maximum sequence number for today
        SELECT @MaxNumber = ISNULL(MAX(CAST(SUBSTRING(DebitNoteNumber, 10, 4) AS INT)), 0)
        FROM SupplierDebitNote
        WHERE DebitNoteNumber LIKE 'DN' + @DatePrefix + '%';
        
        -- Generate next sequence number
        SET @SequenceNumber = @MaxNumber + 1;
        
        -- Format the number: DN + YYYYMMDD + 4-digit sequence
        SET @GeneratedNumber = 'DN' + @DatePrefix + RIGHT('0000' + CAST(@SequenceNumber AS NVARCHAR(4)), 4);
        
        SELECT 'SUCCESS' AS Result, @GeneratedNumber AS GeneratedNumber;
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- Create stored procedure for updating debit note status
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_UpdateSupplierDebitNoteStatus')
    DROP PROCEDURE sp_UpdateSupplierDebitNoteStatus;
GO

CREATE PROCEDURE sp_UpdateSupplierDebitNoteStatus
    @DebitNoteId INT,
    @Status NVARCHAR(20),
    @UpdatedBy INT
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRY
        UPDATE SupplierDebitNote
        SET Status = @Status,
            UpdatedBy = @UpdatedBy,
            UpdatedDate = GETDATE()
        WHERE DebitNoteId = @DebitNoteId;
        
        IF @@ROWCOUNT = 0
            RAISERROR('Debit note not found', 16, 1);
        
        SELECT 'SUCCESS' AS Result;
        
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

PRINT 'Supplier Debit Note stored procedures created successfully!';