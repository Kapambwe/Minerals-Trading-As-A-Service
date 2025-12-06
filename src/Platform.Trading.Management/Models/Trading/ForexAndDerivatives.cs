namespace Platform.Trading.Management.Models.Trading;

/// <summary>
/// Represents a forex rate for multi-currency trading.
/// Addresses Gap TO-004: Multi-Currency Support with real-time FX integration.
/// </summary>
public class ForexRate
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime RateDate { get; set; } = DateTime.UtcNow;
    
    // Currency Pair
    public string BaseCurrency { get; set; } = "USD";
    public string QuoteCurrency { get; set; } = "ZMW";
    public string CurrencyPair => $"{BaseCurrency}/{QuoteCurrency}";
    
    // Rate Data
    public decimal Rate { get; set; }
    public decimal? BidRate { get; set; }
    public decimal? AskRate { get; set; }
    public decimal? Spread => AskRate.HasValue && BidRate.HasValue ? AskRate.Value - BidRate.Value : null;
    
    // Historical Data
    public decimal? PreviousClose { get; set; }
    public decimal? Change { get; set; }
    public decimal? ChangePercentage { get; set; }
    public decimal? DayHigh { get; set; }
    public decimal? DayLow { get; set; }
    
    // Source
    public string RateSource { get; set; } = "BOZ"; // BOZ (Bank of Zambia), Reuters, Bloomberg, Interbank
    public string RateType { get; set; } = "Mid"; // Mid, Bid, Ask, Official
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    public int? DelayMinutes { get; set; }
    
    // Validity
    public DateTime ValidFrom { get; set; }
    public DateTime ValidUntil { get; set; }
    public bool IsActive { get; set; } = true;
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a forex conversion for trade settlement.
/// </summary>
public class ForexConversion
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime ConversionDate { get; set; } = DateTime.UtcNow;
    
    // Trade Reference
    public string? TradeId { get; set; }
    public string? SettlementId { get; set; }
    
    // Conversion Details
    public string FromCurrency { get; set; } = string.Empty;
    public decimal FromAmount { get; set; }
    public string ToCurrency { get; set; } = string.Empty;
    public decimal ToAmount { get; set; }
    
    // Rate Applied
    public decimal ExchangeRate { get; set; }
    public string RateType { get; set; } = string.Empty; // Spot, Forward, Negotiated
    public string? RateSource { get; set; }
    public DateTime? RateDate { get; set; }
    
    // BOZ Compliance
    public bool BozReportingRequired { get; set; }
    public bool BozReported { get; set; }
    public string? BozReference { get; set; }
    
    // Status
    public string Status { get; set; } = "Pending"; // Pending, Completed, Failed
    public string? FailureReason { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a block trade for large volume negotiated trades.
/// Addresses Gap TO-007: Block Trading with price protection.
/// </summary>
public class BlockTrade
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string BlockTradeNumber { get; set; } = string.Empty;
    public DateTime TradeDate { get; set; } = DateTime.UtcNow;
    
    // Trade Details
    public MetalType MetalType { get; set; }
    public string? QualityGrade { get; set; }
    public decimal Quantity { get; set; } // in metric tons
    public decimal MinimumBlockSize { get; set; } = 100; // Minimum quantity for block trade
    
    // Pricing
    public decimal NegotiatedPrice { get; set; }
    public string Currency { get; set; } = "USD";
    public decimal? ReferencePrice { get; set; } // Market price at time of trade
    public decimal? PriceDeviation { get; set; } // Deviation from reference price
    public decimal? MaxAllowedDeviation { get; set; } = 5m; // Percentage
    
    // Parties
    public string BuyerId { get; set; } = string.Empty;
    public string BuyerName { get; set; } = string.Empty;
    public string SellerId { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    
    // Negotiation
    public string NegotiationType { get; set; } = "OffExchange"; // OffExchange, DarkPool, Negotiated
    public DateTime? NegotiationStartTime { get; set; }
    public DateTime? NegotiationEndTime { get; set; }
    public string? BrokerId { get; set; }
    public string? BrokerName { get; set; }
    
    // Price Protection
    public bool PriceProtectionApplied { get; set; }
    public decimal? PriceProtectionWindow { get; set; } // Minutes
    public decimal? VwapPrice { get; set; } // Volume-weighted average price
    
    // Approval
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Reported
    public bool RequiresExchangeApproval { get; set; } = true;
    public DateTime? ApprovalDate { get; set; }
    public string? ApprovedBy { get; set; }
    public string? RejectionReason { get; set; }
    
    // Reporting
    public bool ReportedToMarket { get; set; }
    public DateTime? MarketReportTime { get; set; }
    public int? ReportingDelayMinutes { get; set; } // Delayed publication
    
    // Settlement
    public string? SettlementId { get; set; }
    public DateTime? SettlementDate { get; set; }
    public string SettlementStatus { get; set; } = "Pending";
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a derivative contract (futures or options).
/// Addresses Gap TO-005: Futures & Options Contracts support.
/// </summary>
public class DerivativeContract
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ContractCode { get; set; } = string.Empty;
    public string ContractName { get; set; } = string.Empty;
    
    // Contract Type
    public string ContractType { get; set; } = "Futures"; // Futures, CallOption, PutOption, Swap
    public string UnderlyingType { get; set; } = "PhysicalMetal"; // PhysicalMetal, PriceIndex
    
    // Underlying Asset
    public MetalType MetalType { get; set; }
    public string? QualityGrade { get; set; }
    public string? PriceIndexReference { get; set; }
    
    // Contract Specifications
    public decimal ContractSize { get; set; } = 25; // metric tons per contract
    public decimal TickSize { get; set; } = 0.50m; // minimum price movement
    public decimal TickValue { get; set; } // monetary value per tick
    public string Currency { get; set; } = "USD";
    
    // Expiry
    public string ContractMonth { get; set; } = string.Empty; // e.g., "Mar24", "Jun24"
    public DateTime ExpiryDate { get; set; }
    public DateTime FirstNoticeDate { get; set; }
    public DateTime LastTradingDate { get; set; }
    public int DaysToExpiry => (ExpiryDate - DateTime.UtcNow).Days;
    
    // Settlement
    public string SettlementType { get; set; } = "Physical"; // Physical, Cash
    public string? DeliveryLocation { get; set; }
    public List<string>? ApprovedWarehouses { get; set; }
    
    // Pricing
    public decimal? SettlementPrice { get; set; }
    public decimal? OpenPrice { get; set; }
    public decimal? HighPrice { get; set; }
    public decimal? LowPrice { get; set; }
    public decimal? LastPrice { get; set; }
    public decimal? PreviousClose { get; set; }
    public decimal? Change { get; set; }
    
    // Volume and Open Interest
    public long? Volume { get; set; }
    public long? OpenInterest { get; set; }
    public decimal? NotionalValue { get; set; }
    
    // Margin Requirements
    public decimal InitialMargin { get; set; }
    public decimal MaintenanceMargin { get; set; }
    public decimal? MarginRate { get; set; } // Percentage of contract value
    
    // Position Limits
    public long? SpotMonthLimit { get; set; }
    public long? SingleMonthLimit { get; set; }
    public long? AllMonthsLimit { get; set; }
    
    // Trading Status
    public string TradingStatus { get; set; } = "Active"; // Active, Halted, Expired, Settled
    public DateTime? LastTradeTime { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents an option contract.
/// </summary>
public class OptionContract : DerivativeContract
{
    public string OptionType { get; set; } = "Call"; // Call, Put
    public string ExerciseStyle { get; set; } = "European"; // European, American
    public decimal StrikePrice { get; set; }
    
    // Greeks
    public decimal? Delta { get; set; }
    public decimal? Gamma { get; set; }
    public decimal? Theta { get; set; }
    public decimal? Vega { get; set; }
    public decimal? Rho { get; set; }
    
    // Premium
    public decimal? Premium { get; set; }
    public decimal? IntrinsicValue { get; set; }
    public decimal? TimeValue { get; set; }
    public decimal? ImpliedVolatility { get; set; }
}
