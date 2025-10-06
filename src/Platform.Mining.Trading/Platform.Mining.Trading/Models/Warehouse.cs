namespace Platform.Mining.Trading.Models
{
    public class WarehouseLocation
    {
        public string LocationId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Active, Inactive
        public decimal TotalCapacity { get; set; }
        public decimal UsedCapacity { get; set; }
    }

    public class WarehouseReceipt
    {
        public string ReceiptId { get; set; } = string.Empty;
        public string WarehouseLocation { get; set; } = string.Empty;
        public string MineralType { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public string Owner { get; set; } = string.Empty;
        public DateTime ReceivedDate { get; set; }
        public string Status { get; set; } = string.Empty; // Active, Released, Transferred
        public string? QcCertificateId { get; set; }
    }

    public class QualityCertificate
    {
        public string CertificateId { get; set; } = string.Empty;
        public string ReceiptId { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public string IssuedBy { get; set; } = string.Empty;
        public string MineralType { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public Dictionary<string, string> TestResults { get; set; } = new();
        public bool Approved { get; set; }
        public string? DocumentUrl { get; set; }
    }

    public class InventorySummary
    {
        public string MineralType { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public decimal TotalQuantity { get; set; }
        public int ReceiptCount { get; set; }
        public decimal AverageAge { get; set; } // in days
    }
}
