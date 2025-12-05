namespace Platform.Trading.Management.Models.Trading;

/// <summary>
/// Represents an order in the order book.
/// Addresses Gap TO-001: Order Book Management with real-time order matching.
/// </summary>
public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    
    // Order Type
    public string OrderType { get; set; } = "Limit"; // Limit, Market, Stop, StopLimit
    public string Side { get; set; } = string.Empty; // Buy, Sell
    public string TimeInForce { get; set; } = "GTC"; // GTC (Good Till Cancel), DAY, IOC (Immediate or Cancel), FOK (Fill or Kill)
    
    // Participant Information
    public string ParticipantId { get; set; } = string.Empty;
    public string ParticipantName { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    
    // Instrument Details
    public MetalType MetalType { get; set; }
    public string? QualityGrade { get; set; } // LME Grade A, etc.
    public string? ContractMonth { get; set; } // For futures: e.g., "Mar24"
    
    // Quantity and Price
    public decimal Quantity { get; set; } // in metric tons
    public decimal? LimitPrice { get; set; } // Price per ton for limit orders
    public decimal? StopPrice { get; set; } // Trigger price for stop orders
    public decimal FilledQuantity { get; set; }
    public decimal RemainingQuantity => Quantity - FilledQuantity;
    public decimal? AverageFilledPrice { get; set; }
    
    // Currency
    public string Currency { get; set; } = "USD";
    
    // Status
    public string Status { get; set; } = "Pending"; // Pending, Open, PartiallyFilled, Filled, Cancelled, Rejected, Expired
    public string? RejectionReason { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    // Execution Details
    public int? Priority { get; set; } // For order book priority
    public DateTime? FirstFillTime { get; set; }
    public DateTime? LastFillTime { get; set; }
    public List<string> TradeIds { get; set; } = new(); // Related trade executions
    
    // Risk Control
    public bool PassedPreTradeRiskCheck { get; set; }
    public string? RiskCheckNotes { get; set; }
    
    public string? Notes { get; set; }
}
