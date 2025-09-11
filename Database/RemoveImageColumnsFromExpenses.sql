-- Remove image columns from Expenses table since we only need barcode images
USE [DistributionDB]
GO

-- Remove the image columns
ALTER TABLE [dbo].[Expenses] DROP COLUMN [ImageData]
GO

ALTER TABLE [dbo].[Expenses] DROP COLUMN [ImagePath]
GO

-- Show the updated table structure
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Expenses' 
ORDER BY ORDINAL_POSITION
GO
