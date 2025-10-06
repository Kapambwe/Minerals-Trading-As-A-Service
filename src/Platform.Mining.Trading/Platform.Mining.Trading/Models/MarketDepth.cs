namespace Platform.Mining.Trading.Models
{
    public class MarketDepth
    {
        public string Instrument { get; set; } = string.Empty;
        public List<PriceLevel> Bids { get; set; } = new();
        public List<PriceLevel> Asks { get; set; } = new();
        public decimal LastTradePrice { get; set; }
        public decimal LastTradeQuantity { get; set; }
        public DateTime LastTradeTime { get; set; }
    }

    public class PriceLevel
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public int OrderCount { get; set; }
    }
}
