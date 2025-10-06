namespace Platform.Mining.Trading.Models
{
    public class Position
    {
        public string Instrument { get; set; } = string.Empty;
        public string MineralType { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public decimal OpenQuantity { get; set; }
        public decimal AveragePrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal MarkToMarket { get; set; }
        public decimal UnrealizedPnL { get; set; }
        public decimal RealizedPnL { get; set; }
        public decimal TotalPnL { get; set; }
        public decimal RequiredMargin { get; set; }
        public decimal CollateralPosted { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
