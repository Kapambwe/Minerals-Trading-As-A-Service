namespace Platform.Trading.Management.Models.Trading;

/// <summary>
/// Represents price index data from external benchmarks.
/// Addresses Gap TO-002 & TO-008: Price Discovery and Index Integration (LME, COMEX, SHFE).
/// </summary>
public class PriceIndex
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string IndexSource { get; set; } = string.Empty; // LME, COMEX, SHFE, ZME
    public string IndexName { get; set; } = string.Empty; // e.g., "LME Copper Official Price"
    public MetalType MetalType { get; set; }
    public string? QualityGrade { get; set; }
    
    // Price Data
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public string PriceUnit { get; set; } = "per metric ton";
    public DateTime PriceDate { get; set; }
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    
    // Price Types
    public string PriceType { get; set; } = string.Empty; // Official, Settlement, Spot, 3-Month, Cash
    
    // Price Movement
    public decimal? PreviousPrice { get; set; }
    public decimal? Change { get; set; }
    public decimal? ChangePercentage { get; set; }
    public decimal? OpenPrice { get; set; }
    public decimal? HighPrice { get; set; }
    public decimal? LowPrice { get; set; }
    
    // Volume Data
    public decimal? Volume { get; set; }
    public decimal? OpenInterest { get; set; }
    
    // Data Quality
    public string DataStatus { get; set; } = "Active"; // Active, Delayed, Stale, Unavailable
    public int? DelayMinutes { get; set; }
    public string? DataSource { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a price history record for trend analysis.
/// </summary>
public class PriceHistory
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string IndexSource { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public DateTime Date { get; set; }
    
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal? Volume { get; set; }
    
    public string Currency { get; set; } = "USD";
}
