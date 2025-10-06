namespace Platform.Mining.Trading.Models
{
    public class Trade
    {
        public string TradeId { get; set; } = string.Empty;
        public string OrderId { get; set; } = string.Empty;
        public string Instrument { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty; // Buy/Sell
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue { get; set; }
        public decimal Fees { get; set; }
        public decimal NetValue { get; set; }
        public DateTime TradeTime { get; set; }
        public string Counterparty { get; set; } = string.Empty;
        public string ClearingReference { get; set; } = string.Empty;
        public DateTime SettlementDate { get; set; }
        public string Status { get; set; } = string.Empty; // Executed, Settled, Pending Settlement
        public string Venue { get; set; } = string.Empty;
    }
}
