using System;

namespace DistributionSoftware.Models
{
    public class PendingDeliveryReportData
    {
        public int ChallanId { get; set; }
        public string ChallanNo { get; set; }
        public int? SalesInvoiceId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime ChallanDate { get; set; }
        public string VehicleNo { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? VehicleId { get; set; }
        public int? RouteId { get; set; }

        // Sales Invoice Information
        public string InvoiceNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public string InvoiceStatus { get; set; }

        // Customer Information
        public int? CustomerId { get; set; }
        public string CustomerCode { get; set; }
        public string ContactPerson { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerPostalCode { get; set; }
        public string CustomerCountry { get; set; }

        // Vehicle Information
        public string VehicleType { get; set; }
        public string TransporterName { get; set; }
        public bool? VehicleIsActive { get; set; }

        // Route Information
        public string RouteName { get; set; }
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public decimal? Distance { get; set; }
        public string EstimatedTime { get; set; }
        public bool? RouteIsActive { get; set; }

        // Delivery Schedule Information
        public int? ScheduleId { get; set; }
        public string ScheduleRef { get; set; }
        public DateTime? ScheduledDateTime { get; set; }
        public DateTime? DispatchDateTime { get; set; }
        public DateTime? DeliveredDateTime { get; set; }
        public DateTime? ReturnedDateTime { get; set; }
        public string ScheduleStatus { get; set; }

        // Pending Analysis
        public string StatusDescription { get; set; }
        public int? ChallanAgeDays { get; set; }
        public int? InvoiceAgeDays { get; set; }
        public int? ScheduleAgeDays { get; set; }
        public string PriorityLevel { get; set; }
        public string PaymentStatus { get; set; }
        public int? ItemsCount { get; set; }
        public decimal? TotalQuantity { get; set; }
        public string ScheduleStatusDescription { get; set; }
    }
}
