CREATE PROCEDURE [dbo].[sp_GetExpenseSummaryReport]
    @StartDate DATETIME,
    @EndDate DATETIME,
    @CategoryId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Main Summary Data by Category
    SELECT
        ec.CategoryId,
        ec.CategoryCode,
        ec.CategoryName,
        ec.Description AS CategoryDescription,
        COUNT(e.ExpenseId) AS TotalExpenses,
        SUM(e.Amount) AS TotalAmount,
        AVG(e.Amount) AS AverageAmount,
        MIN(e.Amount) AS MinAmount,
        MAX(e.Amount) AS MaxAmount,
        COUNT(DISTINCT e.CreatedBy) AS TotalUsers, -- Using CreatedBy instead of UserId
        SUM(CASE WHEN e.Status = 'PENDING' THEN 1 ELSE 0 END) AS PendingExpenses,
        SUM(CASE WHEN e.Status = 'APPROVED' THEN 1 ELSE 0 END) AS ApprovedExpenses,
        SUM(CASE WHEN e.Status = 'REJECTED' THEN 1 ELSE 0 END) AS RejectedExpenses,
        SUM(CASE WHEN e.Status = 'PAID' THEN 1 ELSE 0 END) AS PaidExpenses,
        SUM(CASE WHEN e.Status = 'CANCELLED' THEN 1 ELSE 0 END) AS CancelledExpenses,
        SUM(CASE WHEN e.Status = 'PENDING' THEN e.Amount ELSE 0 END) AS PendingAmount,
        SUM(CASE WHEN e.Status = 'APPROVED' THEN e.Amount ELSE 0 END) AS ApprovedAmount,
        SUM(CASE WHEN e.Status = 'REJECTED' THEN e.Amount ELSE 0 END) AS RejectedAmount,
        SUM(CASE WHEN e.Status = 'PAID' THEN e.Amount ELSE 0 END) AS PaidAmount,
        SUM(CASE WHEN e.Status = 'CANCELLED' THEN e.Amount ELSE 0 END) AS CancelledAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CASH' THEN 1 ELSE 0 END) AS CashExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CARD' THEN 1 ELSE 0 END) AS CardExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'BANK_TRANSFER' THEN 1 ELSE 0 END) AS BankTransferExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CHEQUE' THEN 1 ELSE 0 END) AS ChequeExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'EASYPAISA' THEN 1 ELSE 0 END) AS EasypaisaExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'JAZZCASH' THEN 1 ELSE 0 END) AS JazzcashExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CASH' THEN e.Amount ELSE 0 END) AS TotalCashAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CARD' THEN e.Amount ELSE 0 END) AS TotalCardAmount,
        SUM(CASE WHEN e.PaymentMethod = 'BANK_TRANSFER' THEN e.Amount ELSE 0 END) AS TotalBankTransferAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CHEQUE' THEN e.Amount ELSE 0 END) AS TotalChequeAmount,
        SUM(CASE WHEN e.PaymentMethod = 'EASYPAISA' THEN e.Amount ELSE 0 END) AS TotalEasypaisaAmount,
        SUM(CASE WHEN e.PaymentMethod = 'JAZZCASH' THEN e.Amount ELSE 0 END) AS TotalJazzcashAmount,
        SUM(CASE WHEN e.Amount >= 10000 THEN 1 ELSE 0 END) AS HighValueExpenses,
        SUM(CASE WHEN e.Amount >= 5000 AND e.Amount < 10000 THEN 1 ELSE 0 END) AS MediumValueExpenses,
        SUM(CASE WHEN e.Amount >= 1000 AND e.Amount < 5000 THEN 1 ELSE 0 END) AS LowValueExpenses,
        SUM(CASE WHEN e.Amount < 1000 THEN 1 ELSE 0 END) AS VeryLowValueExpenses,
        SUM(CASE WHEN e.Amount >= 10000 THEN e.Amount ELSE 0 END) AS TotalHighValueAmount,
        SUM(CASE WHEN e.Amount >= 5000 AND e.Amount < 10000 THEN e.Amount ELSE 0 END) AS TotalMediumValueAmount,
        SUM(CASE WHEN e.Amount >= 1000 AND e.Amount < 5000 THEN e.Amount ELSE 0 END) AS TotalLowValueAmount,
        SUM(CASE WHEN e.Amount < 1000 THEN e.Amount ELSE 0 END) AS TotalVeryLowValueAmount,
        -- Calculated percentages
        CASE 
            WHEN SUM(e.Amount) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'PENDING' THEN e.Amount ELSE 0 END) * 100.0 / SUM(e.Amount) AS DECIMAL(5,2))
            ELSE 0 
        END AS PendingPercentage,
        CASE 
            WHEN SUM(e.Amount) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'APPROVED' THEN e.Amount ELSE 0 END) * 100.0 / SUM(e.Amount) AS DECIMAL(5,2))
            ELSE 0 
        END AS ApprovedPercentage,
        CASE 
            WHEN SUM(e.Amount) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'PAID' THEN e.Amount ELSE 0 END) * 100.0 / SUM(e.Amount) AS DECIMAL(5,2))
            ELSE 0 
        END AS PaidPercentage,
        CASE 
            WHEN SUM(e.Amount) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'REJECTED' THEN e.Amount ELSE 0 END) * 100.0 / SUM(e.Amount) AS DECIMAL(5,2))
            ELSE 0 
        END AS RejectedPercentage
    FROM ExpenseCategories ec
    LEFT JOIN Expenses e ON ec.CategoryId = e.CategoryId
        AND e.ExpenseDate >= @StartDate
        AND e.ExpenseDate <= @EndDate
        AND (@CategoryId IS NULL OR e.CategoryId = @CategoryId)
    WHERE ec.IsActive = 1
    GROUP BY ec.CategoryId, ec.CategoryCode, ec.CategoryName, ec.Description
    HAVING COUNT(e.ExpenseId) > 0 OR @CategoryId IS NOT NULL
    ORDER BY SUM(e.Amount) DESC, ec.CategoryName;

    -- Overall Summary Statistics
    SELECT
        COUNT(*) AS TotalExpenses,
        COUNT(DISTINCT e.CategoryId) AS TotalCategories,
        COUNT(DISTINCT e.CreatedBy) AS TotalUsers, -- Using CreatedBy instead of UserId
        SUM(e.Amount) AS TotalAmount,
        AVG(e.Amount) AS AverageAmount,
        MIN(e.Amount) AS MinAmount,
        MAX(e.Amount) AS MaxAmount,
        SUM(CASE WHEN e.Status = 'PENDING' THEN 1 ELSE 0 END) AS PendingExpenses,
        SUM(CASE WHEN e.Status = 'APPROVED' THEN 1 ELSE 0 END) AS ApprovedExpenses,
        SUM(CASE WHEN e.Status = 'REJECTED' THEN 1 ELSE 0 END) AS RejectedExpenses,
        SUM(CASE WHEN e.Status = 'PAID' THEN 1 ELSE 0 END) AS PaidExpenses,
        SUM(CASE WHEN e.Status = 'CANCELLED' THEN 1 ELSE 0 END) AS CancelledExpenses,
        SUM(CASE WHEN e.Status = 'PENDING' THEN e.Amount ELSE 0 END) AS PendingAmount,
        SUM(CASE WHEN e.Status = 'APPROVED' THEN e.Amount ELSE 0 END) AS ApprovedAmount,
        SUM(CASE WHEN e.Status = 'REJECTED' THEN e.Amount ELSE 0 END) AS RejectedAmount,
        SUM(CASE WHEN e.Status = 'PAID' THEN e.Amount ELSE 0 END) AS PaidAmount,
        SUM(CASE WHEN e.Status = 'CANCELLED' THEN e.Amount ELSE 0 END) AS CancelledAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CASH' THEN 1 ELSE 0 END) AS CashExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CARD' THEN 1 ELSE 0 END) AS CardExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'BANK_TRANSFER' THEN 1 ELSE 0 END) AS BankTransferExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CHEQUE' THEN 1 ELSE 0 END) AS ChequeExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'EASYPAISA' THEN 1 ELSE 0 END) AS EasypaisaExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'JAZZCASH' THEN 1 ELSE 0 END) AS JazzcashExpenses,
        SUM(CASE WHEN e.PaymentMethod = 'CASH' THEN e.Amount ELSE 0 END) AS TotalCashAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CARD' THEN e.Amount ELSE 0 END) AS TotalCardAmount,
        SUM(CASE WHEN e.PaymentMethod = 'BANK_TRANSFER' THEN e.Amount ELSE 0 END) AS TotalBankTransferAmount,
        SUM(CASE WHEN e.PaymentMethod = 'CHEQUE' THEN e.Amount ELSE 0 END) AS TotalChequeAmount,
        SUM(CASE WHEN e.PaymentMethod = 'EASYPAISA' THEN e.Amount ELSE 0 END) AS TotalEasypaisaAmount,
        SUM(CASE WHEN e.PaymentMethod = 'JAZZCASH' THEN e.Amount ELSE 0 END) AS TotalJazzcashAmount,
        SUM(CASE WHEN e.Amount >= 10000 THEN 1 ELSE 0 END) AS HighValueExpenses,
        SUM(CASE WHEN e.Amount >= 5000 AND e.Amount < 10000 THEN 1 ELSE 0 END) AS MediumValueExpenses,
        SUM(CASE WHEN e.Amount >= 1000 AND e.Amount < 5000 THEN 1 ELSE 0 END) AS LowValueExpenses,
        SUM(CASE WHEN e.Amount < 1000 THEN 1 ELSE 0 END) AS VeryLowValueExpenses,
        SUM(CASE WHEN e.Amount >= 10000 THEN e.Amount ELSE 0 END) AS TotalHighValueAmount,
        SUM(CASE WHEN e.Amount >= 5000 AND e.Amount < 10000 THEN e.Amount ELSE 0 END) AS TotalMediumValueAmount,
        SUM(CASE WHEN e.Amount >= 1000 AND e.Amount < 5000 THEN e.Amount ELSE 0 END) AS TotalLowValueAmount,
        SUM(CASE WHEN e.Amount < 1000 THEN e.Amount ELSE 0 END) AS TotalVeryLowValueAmount,
        -- Date range info
        @StartDate AS ReportStartDate,
        @EndDate AS ReportEndDate,
        DATEDIFF(DAY, @StartDate, @EndDate) + 1 AS ReportDays,
        -- Calculated fields
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'PENDING' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS PendingExpensePercentage,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'APPROVED' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS ApprovedExpensePercentage,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'PAID' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS PaidExpensePercentage,
        CASE 
            WHEN COUNT(*) > 0 THEN 
                CAST(SUM(CASE WHEN e.Status = 'REJECTED' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS RejectedExpensePercentage
    FROM Expenses e
    WHERE e.ExpenseDate >= @StartDate
        AND e.ExpenseDate <= @EndDate
        AND (@CategoryId IS NULL OR e.CategoryId = @CategoryId);
END;
