namespace Platform.Mining.Trading.Models
{
    public class Order
    {
        public string OrderId { get; set; } = string.Empty;
        public string Instrument { get; set; } = string.Empty;
        public string MineralType { get; set; } = string.Empty;
        public string Grade { get; set; } = string.Empty;
        public string ContractMonth { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty; // Buy/Sell
        public decimal Quantity { get; set; }
        public decimal FilledQuantity { get; set; }
        public decimal RemainingQuantity { get; set; }
        public decimal Price { get; set; }
        public string OrderType { get; set; } = string.Empty; // Limit, Market, IOC, FOK, Iceberg, Block, Auction
        public string TimeInForce { get; set; } = string.Empty; // GTC, Day, IOC, FOK
        public string Status { get; set; } = string.Empty; // Pending, Active, Filled, Cancelled, Rejected
        public DateTime Timestamp { get; set; }
        public string Venue { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string? Counterparty { get; set; }
        public string DeliveryOption { get; set; } = string.Empty; // Cash/Physical
        public string? SpecialInstructions { get; set; }
    }
}
