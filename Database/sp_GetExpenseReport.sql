-- =============================================
-- Author: Distribution Software
-- Create date: 2025
-- Description: Stored procedure to get expense report data
-- Shows expense transactions with filters for category, date range, and user
-- =============================================
CREATE PROCEDURE [dbo].[sp_GetExpenseReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CategoryId INT = NULL,
    @UserId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Main query to get expense report data
    SELECT 
        e.ExpenseId,
        e.ExpenseNumber,
        e.ExpenseDate,
        e.Description,
        e.Amount,
        e.CategoryId,
        ec.CategoryName,
        ec.CategoryCode,
        ec.Description AS CategoryDescription,
        e.UserId,
        u.FirstName + ' ' + u.LastName AS UserName,
        u.Username,
        u.Email AS UserEmail,
        u.Phone AS UserPhone,
        e.PaymentMethod,
        e.ReferenceNumber,
        e.ReceiptNumber,
        e.VendorName,
        e.VendorContact,
        e.VendorPhone,
        e.VendorEmail,
        e.Status,
        e.ApprovedBy,
        approver.FirstName + ' ' + approver.LastName AS ApprovedByName,
        e.ApprovedDate,
        e.Remarks,
        e.CreatedBy,
        creator.FirstName + ' ' + creator.LastName AS CreatedByName,
        e.CreatedDate,
        e.ModifiedBy,
        modifier.FirstName + ' ' + modifier.LastName AS ModifiedByName,
        e.ModifiedDate,
        -- Calculate derived fields
        CASE 
            WHEN e.Status = 'PENDING' THEN 'Pending Approval'
            WHEN e.Status = 'APPROVED' THEN 'Approved'
            WHEN e.Status = 'REJECTED' THEN 'Rejected'
            WHEN e.Status = 'PAID' THEN 'Paid'
            WHEN e.Status = 'CANCELLED' THEN 'Cancelled'
            ELSE e.Status
        END AS StatusDescription,
        CASE 
            WHEN e.PaymentMethod = 'CASH' THEN 'Cash'
            WHEN e.PaymentMethod = 'CARD' THEN 'Card'
            WHEN e.PaymentMethod = 'BANK_TRANSFER' THEN 'Bank Transfer'
            WHEN e.PaymentMethod = 'CHEQUE' THEN 'Cheque'
            WHEN e.PaymentMethod = 'EASYPAISA' THEN 'Easypaisa'
            WHEN e.PaymentMethod = 'JAZZCASH' THEN 'Jazzcash'
            ELSE e.PaymentMethod
        END AS PaymentMethodDescription,
        -- Calculate days since expense
        DATEDIFF(DAY, e.ExpenseDate, GETDATE()) AS DaysSinceExpense,
        -- Calculate days until approval (if pending)
        CASE 
            WHEN e.Status = 'PENDING' AND e.ApprovedDate IS NULL THEN DATEDIFF(DAY, e.ExpenseDate, GETDATE())
            WHEN e.ApprovedDate IS NOT NULL THEN DATEDIFF(DAY, e.ExpenseDate, e.ApprovedDate)
            ELSE NULL
        END AS DaysToApproval,
        -- Calculate approval status
        CASE 
            WHEN e.Status = 'PENDING' AND DATEDIFF(DAY, e.ExpenseDate, GETDATE()) > 7 THEN 'Overdue Approval'
            WHEN e.Status = 'PENDING' AND DATEDIFF(DAY, e.ExpenseDate, GETDATE()) <= 7 THEN 'Pending Approval'
            WHEN e.Status = 'APPROVED' THEN 'Approved'
            WHEN e.Status = 'REJECTED' THEN 'Rejected'
            WHEN e.Status = 'PAID' THEN 'Paid'
            ELSE 'Unknown'
        END AS ApprovalStatus,
        -- Calculate expense amount categories
        CASE 
            WHEN e.Amount >= 10000 THEN 'High Value'
            WHEN e.Amount >= 5000 THEN 'Medium Value'
            WHEN e.Amount >= 1000 THEN 'Low Value'
            ELSE 'Very Low Value'
        END AS AmountCategory,
        -- Calculate monthly expense (for trend analysis)
        YEAR(e.ExpenseDate) AS ExpenseYear,
        MONTH(e.ExpenseDate) AS ExpenseMonth,
        DATENAME(MONTH, e.ExpenseDate) AS ExpenseMonthName,
        -- Calculate expense frequency
        CASE 
            WHEN e.ExpenseDate >= DATEADD(DAY, -7, GETDATE()) THEN 'This Week'
            WHEN e.ExpenseDate >= DATEADD(DAY, -30, GETDATE()) THEN 'This Month'
            WHEN e.ExpenseDate >= DATEADD(DAY, -90, GETDATE()) THEN 'This Quarter'
            ELSE 'Older'
        END AS ExpenseFrequency
    FROM Expenses e
    LEFT JOIN ExpenseCategories ec ON e.CategoryId = ec.CategoryId
    LEFT JOIN Users u ON e.UserId = u.UserId
    LEFT JOIN Users approver ON e.ApprovedBy = approver.UserId
    LEFT JOIN Users creator ON e.CreatedBy = creator.UserId
    LEFT JOIN Users modifier ON e.ModifiedBy = modifier.UserId
    WHERE e.ExpenseDate >= @StartDate
        AND e.ExpenseDate <= @EndDate
        AND (@CategoryId IS NULL OR e.CategoryId = @CategoryId)
        AND (@UserId IS NULL OR e.UserId = @UserId)
    ORDER BY e.ExpenseDate DESC, e.ExpenseNumber DESC;
    
    -- Return summary information
    SELECT 
        COUNT(*) AS TotalExpenses,
        COUNT(DISTINCT e.UserId) AS TotalUsers,
        COUNT(DISTINCT e.CategoryId) AS TotalCategories,
        SUM(e.Amount) AS TotalAmount,
        AVG(e.Amount) AS AverageAmount,
        MIN(e.Amount) AS MinAmount,
        MAX(e.Amount) AS MaxAmount,
        -- Count by status
        SUM(CASE WHEN e.Status = 'PENDING' THEN 1 ELSE 0 END) AS PendingExpenses,
        SUM(CASE WHEN e.Status = 'APPROVED' THEN 1 ELSE 0 END) AS ApprovedExpenses,
        SUM(CASE WHEN e.Status = 'REJECTED' THEN 1 ELSE 0 END) AS RejectedExpenses,
        SUM(CASE WHEN e.Status = 'PAID' THEN 1 ELSE 0 END) AS PaidExpenses,
        SUM(CASE WHEN e.Status = 'CANCELLED' THEN 1 ELSE 0 END) AS CancelledExpenses,
        -- Count by payment method
        SUM(CASE WHEN e.PaymentMethod = 'CASH' THEN 1 ELSE 0 END) AS CashExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CARD' THEN 1 ELSE 0 END) AS CardExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'BANK_TRANSFER' THEN 1 ELSE 0 END) AS BankTransferExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CHEQUE' THEN 1 ELSE 0 END) AS ChequeExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'EASYPAISA' THEN 1 ELSE 0 END) AS EasypaisaExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'JAZZCASH' THEN 1 ELSE 0 END) AS JazzcashExpenses,
        -- Calculate totals by payment method
        SUM(CASE WHEN e.PaymentMethod = 'CASH' THEN e.Amount ELSE 0 END) AS TotalCashAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CARD' THEN e.Amount ELSE 0 END) AS TotalCardAmount,
        SUM(CASE WHEN e.PaymentMethod = 'BANK_TRANSFER' THEN e.Amount ELSE 0 END) AS TotalBankTransferAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CHEQUE' THEN e.Amount ELSE 0 END) AS TotalChequeAmount,
        SUM(CASE WHEN e.PaymentMethod = 'EASYPAISA' THEN e.Amount ELSE 0 END) AS TotalEasypaisaAmount,
        SUM(CASE WHEN e.PaymentMethod = 'JAZZCASH' THEN e.Amount ELSE 0 END) AS TotalJazzcashAmount,
        -- Calculate approval statistics
        SUM(CASE WHEN e.Status = 'PENDING' AND DATEDIFF(DAY, e.ExpenseDate, GETDATE()) > 7 THEN 1 ELSE 0 END) AS OverdueApprovals,
        SUM(CASE WHEN e.Status = 'PENDING' AND DATEDIFF(DAY, e.ExpenseDate, GETDATE()) <= 7 THEN 1 ELSE 0 END) AS PendingApprovals,
        -- Calculate amount categories
        SUM(CASE WHEN e.Amount >= 10000 THEN 1 ELSE 0 END) AS HighValueExpenses,
        SUM(CASE WHEN e.Amount >= 5000 AND e.Amount < 10000 THEN 1 ELSE 0 END) AS MediumValueExpenses,
        SUM(CASE WHEN e.Amount >= 1000 AND e.Amount < 5000 THEN 1 ELSE 0 END) AS LowValueExpenses,
        SUM(CASE WHEN e.Amount < 1000 THEN 1 ELSE 0 END) AS VeryLowValueExpenses,
        -- Calculate totals by amount category
        SUM(CASE WHEN e.Amount >= 10000 THEN e.Amount ELSE 0 END) AS TotalHighValueAmount,
        SUM(CASE WHEN e.Amount >= 5000 AND e.Amount < 10000 THEN e.Amount ELSE 0 END) AS TotalMediumValueAmount,
        SUM(CASE WHEN e.Amount >= 1000 AND e.Amount < 5000 THEN e.Amount ELSE 0 END) AS TotalLowValueAmount,
        SUM(CASE WHEN e.Amount < 1000 THEN e.Amount ELSE 0 END) AS TotalVeryLowValueAmount
    FROM Expenses e
    LEFT JOIN ExpenseCategories ec ON e.CategoryId = ec.CategoryId
    LEFT JOIN Users u ON e.UserId = u.UserId
    WHERE e.ExpenseDate >= @StartDate
        AND e.ExpenseDate <= @EndDate
        AND (@CategoryId IS NULL OR e.CategoryId = @CategoryId)
        AND (@UserId IS NULL OR e.UserId = @UserId);
END
