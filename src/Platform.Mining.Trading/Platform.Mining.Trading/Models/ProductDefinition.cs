namespace Platform.Mining.Trading.Models
{
    public class Product
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string CommodityType { get; set; } = string.Empty;
        public string GradeSpec { get; set; } = string.Empty;
        public decimal ContractSize { get; set; }
        public string PricingUnit { get; set; } = string.Empty;
        public decimal TickSize { get; set; }
        public decimal MinLotSize { get; set; }
        public decimal MaxLotSize { get; set; }
        public string DeliveryRules { get; set; } = string.Empty;
        public List<string> DeliveryMonths { get; set; } = new();
        public DateTime ExpiryDate { get; set; }
        public string Status { get; set; } = string.Empty; // Active, Deprecated, Draft
        public int Version { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
    }

    public class ProductVersion
    {
        public string VersionId { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public int VersionNumber { get; set; }
        public string Changes { get; set; } = string.Empty;
        public DateTime EffectiveDate { get; set; }
        public string ApprovedBy { get; set; } = string.Empty;
    }

    public class InstrumentImpact
    {
        public string InstrumentId { get; set; } = string.Empty;
        public string InstrumentName { get; set; } = string.Empty;
        public int OpenPositions { get; set; }
        public int OpenOrders { get; set; }
        public string ImpactLevel { get; set; } = string.Empty; // None, Low, Medium, High
        public string RequiredAction { get; set; } = string.Empty;
    }
}
