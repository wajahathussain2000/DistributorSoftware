using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DistributionSoftware.Models;
using DistributionSoftware.DataAccess;
using DistributionSoftware.Common;

namespace DistributionSoftware.Business
{
    public class DeliveryChallanService : IDeliveryChallanService
    {
        private readonly IDeliveryChallanRepository _deliveryChallanRepository;
        private readonly IDeliveryChallanItemRepository _deliveryChallanItemRepository;
        private readonly ISalesInvoiceRepository _salesInvoiceRepository;

        public DeliveryChallanService()
        {
            _deliveryChallanRepository = new DeliveryChallanRepository();
            _deliveryChallanItemRepository = new DeliveryChallanItemRepository();
            _salesInvoiceRepository = new SalesInvoiceRepository();
        }

        public DeliveryChallan GetById(int challanId)
        {
            try
            {
                var challan = _deliveryChallanRepository.GetById(challanId);
                if (challan != null)
                {
                    challan.Items = _deliveryChallanItemRepository.GetByChallanId(challanId);
                }
                return challan;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challan: {ex.Message}", ex);
            }
        }

        public List<DeliveryChallan> GetAll()
        {
            try
            {
                return _deliveryChallanRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challans: {ex.Message}", ex);
            }
        }

        public List<DeliveryChallan> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                return _deliveryChallanRepository.GetByDateRange(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challans by date range: {ex.Message}", ex);
            }
        }

        public DeliveryChallan GetByChallanNo(string challanNo)
        {
            try
            {
                var challan = _deliveryChallanRepository.GetByChallanNo(challanNo);
                if (challan != null)
                {
                    challan.Items = _deliveryChallanItemRepository.GetByChallanId(challan.ChallanId);
                }
                return challan;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving delivery challan by number: {ex.Message}", ex);
            }
        }

        public int CreateDeliveryChallan(DeliveryChallan challan)
        {
            try
            {
                // Generate challan number if not provided
                if (string.IsNullOrEmpty(challan.ChallanNo))
                {
                    challan.ChallanNo = GenerateChallanNumber();
                }

                // Generate barcode if not provided
                if (string.IsNullOrEmpty(challan.BarcodeImage))
                {
                    challan.BarcodeImage = GenerateChallanBarcode();
                }

                // Set created by
                challan.CreatedBy = UserSession.CurrentUserId > 0 ? UserSession.CurrentUserId : 1;

                // Create the challan
                var challanId = _deliveryChallanRepository.Create(challan);
                challan.ChallanId = challanId;

                // Create challan items
                if (challan.Items != null && challan.Items.Count > 0)
                {
                    foreach (var item in challan.Items)
                    {
                        item.ChallanId = challanId;
                        item.CreatedBy = challan.CreatedBy;
                        _deliveryChallanItemRepository.Create(item);
                    }
                }

                return challanId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating delivery challan: {ex.Message}", ex);
            }
        }

        public bool UpdateDeliveryChallan(DeliveryChallan challan)
        {
            try
            {
                challan.UpdatedDate = DateTime.Now;
                challan.UpdatedBy = UserSession.CurrentUserId > 0 ? UserSession.CurrentUserId : 1;

                // Update the challan
                var result = _deliveryChallanRepository.Update(challan);

                if (result && challan.Items != null)
                {
                    // Delete existing items
                    _deliveryChallanItemRepository.DeleteByChallanId(challan.ChallanId);

                    // Add updated items
                    foreach (var item in challan.Items)
                    {
                        item.ChallanId = challan.ChallanId;
                        item.CreatedBy = challan.UpdatedBy.Value;
                        _deliveryChallanItemRepository.Create(item);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating delivery challan: {ex.Message}", ex);
            }
        }

        public bool DeleteDeliveryChallan(int challanId)
        {
            try
            {
                // Delete items first
                _deliveryChallanItemRepository.DeleteByChallanId(challanId);

                // Delete challan
                return _deliveryChallanRepository.Delete(challanId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting delivery challan: {ex.Message}", ex);
            }
        }

        public string GenerateChallanNumber()
        {
            try
            {
                return _deliveryChallanRepository.GenerateChallanNumber();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating challan number: {ex.Message}", ex);
            }
        }

        public string GenerateChallanBarcode()
        {
            try
            {
                var challanNo = GenerateChallanNumber();
                return GenerateBarcodeImage(challanNo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating challan barcode: {ex.Message}", ex);
            }
        }

        public bool UpdateStatus(int challanId, string status)
        {
            try
            {
                return _deliveryChallanRepository.UpdateStatus(challanId, status);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating challan status: {ex.Message}", ex);
            }
        }

        public DeliveryChallan CreateFromSalesInvoice(int salesInvoiceId)
        {
            try
            {
                var salesInvoice = _salesInvoiceRepository.GetSalesInvoiceById(salesInvoiceId);
                if (salesInvoice == null)
                {
                    throw new Exception("Sales invoice not found");
                }
                
                // Load invoice details if not already loaded
                if (salesInvoice.Items == null || salesInvoice.Items.Count == 0)
                {
                    salesInvoice.Items = _salesInvoiceRepository.GetSalesInvoiceDetails(salesInvoiceId);
                }

                var challan = new DeliveryChallan
                {
                    SalesInvoiceId = salesInvoiceId,
                    CustomerName = salesInvoice.CustomerName,
                    CustomerAddress = salesInvoice.CustomerAddress,
                    ChallanDate = DateTime.Now,
                    Status = "DRAFT",
                    ChallanNo = GenerateChallanNumber(),
                    BarcodeImage = GenerateChallanBarcode(),
                    CreatedBy = UserSession.CurrentUserId > 0 ? UserSession.CurrentUserId : 1
                };

                // Copy items from sales invoice
                if (salesInvoice.Items != null)
                {
                    foreach (var salesItem in salesInvoice.Items)
                    {
                        var challanItem = new DeliveryChallanItem
                        {
                            ProductId = salesItem.ProductId,
                            ProductCode = salesItem.ProductCode,
                            ProductName = salesItem.ProductName,
                            Quantity = salesItem.Quantity,
                            Unit = "PCS", // Default unit since SalesInvoiceDetail doesn't have Unit property
                            UnitPrice = salesItem.UnitPrice,
                            TotalAmount = salesItem.TotalAmount,
                            CreatedBy = challan.CreatedBy
                        };
                        challan.Items.Add(challanItem);
                    }
                }

                return challan;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating delivery challan from sales invoice: {ex.Message}", ex);
            }
        }

        private string GenerateBarcodeImage(string data)
        {
            try
            {
                // Create a simple barcode image
                var width = 300;
                var height = 100;
                var bitmap = new Bitmap(width, height);
                var graphics = Graphics.FromImage(bitmap);

                // Fill background
                graphics.FillRectangle(Brushes.White, 0, 0, width, height);

                // Draw barcode pattern (simplified Code 128 style)
                var pattern = GenerateCode128Pattern(data);
                var barWidth = 2;
                var x = 10;

                for (int i = 0; i < pattern.Length; i++)
                {
                    if (pattern[i] == '1')
                    {
                        graphics.FillRectangle(Brushes.Black, x, 10, barWidth, height - 30);
                    }
                    x += barWidth;
                }

                // Draw text
                var font = new Font("Arial", 8, FontStyle.Regular);
                var textSize = graphics.MeasureString(data, font);
                var textX = (width - textSize.Width) / 2;
                graphics.DrawString(data, font, Brushes.Black, textX, height - 20);

                // Convert to base64 string
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    var imageBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error generating barcode image: {ex.Message}", ex);
            }
        }

        private string GenerateCode128Pattern(string data)
        {
            // Simplified Code 128 pattern generation
            // In a real implementation, you would use a proper barcode library
            var pattern = "11010010000"; // Start pattern
            
            foreach (char c in data)
            {
                var value = (int)c;
                for (int i = 0; i < 7; i++)
                {
                    pattern += ((value >> (6 - i)) & 1) == 1 ? "1" : "0";
                }
            }
            
            pattern += "11000101000"; // Stop pattern
            return pattern;
        }
    }
}
