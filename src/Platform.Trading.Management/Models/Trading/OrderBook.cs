namespace Platform.Trading.Management.Models.Trading;

/// <summary>
/// Represents the order book for a specific instrument.
/// Addresses Gap TO-001: Order Book Management with bid/ask spread tracking.
/// </summary>
public class OrderBook
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public MetalType MetalType { get; set; }
    public string? QualityGrade { get; set; }
    public string? ContractMonth { get; set; }
    public DateTime LastUpdateTime { get; set; } = DateTime.UtcNow;
    
    // Order Book Levels
    public List<OrderBookLevel> Bids { get; set; } = new(); // Buy orders, sorted by price descending
    public List<OrderBookLevel> Asks { get; set; } = new(); // Sell orders, sorted by price ascending
    
    // Summary Statistics
    public decimal? BestBidPrice { get; set; }
    public decimal? BestBidQuantity { get; set; }
    public decimal? BestAskPrice { get; set; }
    public decimal? BestAskQuantity { get; set; }
    public decimal? Spread => BestAskPrice.HasValue && BestBidPrice.HasValue 
        ? BestAskPrice.Value - BestBidPrice.Value 
        : null;
    public decimal? SpreadPercentage => BestBidPrice.HasValue && BestBidPrice.Value > 0 && Spread.HasValue
        ? (Spread.Value / BestBidPrice.Value) * 100
        : null;
    
    // Market Depth
    public decimal TotalBidVolume { get; set; }
    public decimal TotalAskVolume { get; set; }
    public int TotalBidOrders { get; set; }
    public int TotalAskOrders { get; set; }
    
    // Last Trade
    public decimal? LastTradePrice { get; set; }
    public decimal? LastTradeQuantity { get; set; }
    public DateTime? LastTradeTime { get; set; }
    
    // Price Statistics
    public decimal? OpenPrice { get; set; }
    public decimal? HighPrice { get; set; }
    public decimal? LowPrice { get; set; }
    public decimal? PreviousClosePrice { get; set; }
    public decimal? PriceChange => LastTradePrice.HasValue && PreviousClosePrice.HasValue
        ? LastTradePrice.Value - PreviousClosePrice.Value
        : null;
    public decimal? PriceChangePercentage => PreviousClosePrice.HasValue && PreviousClosePrice.Value > 0 && PriceChange.HasValue
        ? (PriceChange.Value / PreviousClosePrice.Value) * 100
        : null;
    
    // Trading Session
    public string TradingStatus { get; set; } = "Open"; // Open, Closed, Halted, PreOpen, PreClose
}

/// <summary>
/// Represents a single price level in the order book.
/// </summary>
public class OrderBookLevel
{
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    public int OrderCount { get; set; }
    public string Side { get; set; } = string.Empty; // Bid, Ask
}
