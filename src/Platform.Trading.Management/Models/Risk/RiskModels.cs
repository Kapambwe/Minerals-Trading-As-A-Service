namespace Platform.Trading.Management.Models.Risk;

/// <summary>
/// Represents Value at Risk (VaR) calculation for portfolio risk management.
/// Addresses Gap RM-001: Real-time VaR calculations per portfolio.
/// </summary>
public class VaRCalculation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CalculationDate { get; set; } = DateTime.UtcNow;
    public DateTime AsOfDate { get; set; }
    
    // Portfolio/Entity
    public string EntityId { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // Portfolio, ClearingMember, Exchange, Market
    
    // VaR Parameters
    public decimal ConfidenceLevel { get; set; } = 99m; // 99% confidence
    public int HoldingPeriod { get; set; } = 1; // in days
    public string CalculationMethod { get; set; } = "HistoricalSimulation"; // HistoricalSimulation, MonteCarloSimulation, ParametricVaR
    public int HistoricalDays { get; set; } = 250; // lookback period
    
    // Results
    public decimal VaRAmount { get; set; }
    public string Currency { get; set; } = "USD";
    public decimal VaRAsPercentageOfPortfolio { get; set; }
    
    // Expected Shortfall (CVaR)
    public decimal ExpectedShortfall { get; set; }
    public decimal ExpectedShortfallPercentage { get; set; }
    
    // Portfolio Details
    public decimal PortfolioValue { get; set; }
    public List<VaRContribution> Contributions { get; set; } = new();
    
    // Stress Scenarios
    public decimal StressVaR { get; set; }
    public string? StressScenarioApplied { get; set; }
    
    // Limits
    public decimal VaRLimit { get; set; }
    public bool ExceedsLimit => VaRAmount > VaRLimit;
    public decimal UtilizationPercentage => VaRLimit > 0 ? (VaRAmount / VaRLimit) * 100 : 0;
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents individual contribution to VaR from a position.
/// </summary>
public class VaRContribution
{
    public string PositionId { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public string PositionType { get; set; } = string.Empty; // Long, Short
    public decimal Quantity { get; set; }
    public decimal MarketValue { get; set; }
    
    public decimal MarginalVaR { get; set; }
    public decimal ComponentVaR { get; set; }
    public decimal PercentageContribution { get; set; }
    
    public decimal Volatility { get; set; }
    public decimal Beta { get; set; }
}

/// <summary>
/// Represents stress testing scenarios and results.
/// Addresses Gap RM-002: Regular stress tests with historical and hypothetical scenarios.
/// </summary>
public class StressTest
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TestName { get; set; } = string.Empty;
    public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
    
    // Scope
    public string TestType { get; set; } = "Historical"; // Historical, Hypothetical, Regulatory, ReverseStress
    public string Scope { get; set; } = "Exchange"; // Exchange, ClearingMember, Portfolio, Market
    public string? EntityId { get; set; }
    public string? EntityName { get; set; }
    
    // Scenario
    public string ScenarioName { get; set; } = string.Empty;
    public string ScenarioDescription { get; set; } = string.Empty;
    public List<StressScenarioParameter> Parameters { get; set; } = new();
    
    // Results
    public decimal BaselineValue { get; set; }
    public decimal StressedValue { get; set; }
    public decimal Loss { get; set; }
    public decimal LossPercentage { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Coverage Analysis
    public decimal GuaranteeFundCoverage { get; set; }
    public bool GuaranteeFundSufficient { get; set; }
    public decimal AdditionalCapitalNeeded { get; set; }
    
    // Historical Reference (for historical scenarios)
    public string? HistoricalEventName { get; set; }
    public DateTime? HistoricalEventDate { get; set; }
    
    // Risk Assessment
    public string RiskLevel { get; set; } = "Moderate"; // Low, Moderate, High, Critical
    public List<string> RecommendedActions { get; set; } = new();
    
    // Audit
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovalDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a parameter in a stress scenario.
/// </summary>
public class StressScenarioParameter
{
    public string ParameterName { get; set; } = string.Empty;
    public string ParameterType { get; set; } = string.Empty; // PriceShock, VolatilityShock, LiquidityShock, CorrelationShock
    public MetalType? MetalType { get; set; }
    
    public decimal BaseValue { get; set; }
    public decimal ShockValue { get; set; }
    public decimal ShockPercentage { get; set; }
    public string ShockDirection { get; set; } = "Down"; // Up, Down
}

/// <summary>
/// Represents predefined stress scenarios.
/// </summary>
public class StressScenarioTemplate
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ScenarioType { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    
    public List<StressScenarioParameter> Parameters { get; set; } = new();
    
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = string.Empty;
}

/// <summary>
/// Represents real-time counterparty exposure monitoring.
/// Addresses Gap RM-004: Real-time counterparty exposure monitoring.
/// </summary>
public class ExposureMonitoring
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AsOfDate { get; set; } = DateTime.UtcNow;
    
    // Entity being monitored
    public string EntityId { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // ClearingMember, Counterparty, Participant
    
    // Current Exposure
    public decimal GrossExposure { get; set; }
    public decimal NetExposure { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Exposure Breakdown
    public List<ExposureDetail> ExposureDetails { get; set; } = new();
    
    // Limits
    public decimal GrossExposureLimit { get; set; }
    public decimal NetExposureLimit { get; set; }
    public decimal GrossUtilization => GrossExposureLimit > 0 ? (GrossExposure / GrossExposureLimit) * 100 : 0;
    public decimal NetUtilization => NetExposureLimit > 0 ? (Math.Abs(NetExposure) / NetExposureLimit) * 100 : 0;
    
    // Alerts
    public string AlertLevel { get; set; } = "Normal"; // Normal, Warning, Critical, Breach
    public bool IsInBreach => GrossExposure > GrossExposureLimit || Math.Abs(NetExposure) > NetExposureLimit;
    public List<ExposureAlert> Alerts { get; set; } = new();
    
    // Collateral Coverage
    public decimal CollateralHeld { get; set; }
    public decimal CollateralRequired { get; set; }
    public decimal CollateralSurplus => CollateralHeld - CollateralRequired;
    public bool CollateralSufficient => CollateralHeld >= CollateralRequired;
    
    // Trend
    public decimal ExposureChange24h { get; set; }
    public decimal ExposureChangePercentage24h { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents detailed exposure by position or trade.
/// </summary>
public class ExposureDetail
{
    public string PositionId { get; set; } = string.Empty;
    public string TradeId { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public string Side { get; set; } = string.Empty; // Long, Short
    
    public decimal Quantity { get; set; }
    public decimal MarketPrice { get; set; }
    public decimal MarketValue { get; set; }
    public decimal Exposure { get; set; }
    
    public DateTime TradeDate { get; set; }
    public DateTime SettlementDate { get; set; }
    public int DaysToSettlement { get; set; }
}

/// <summary>
/// Represents an exposure alert.
/// </summary>
public class ExposureAlert
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AlertTime { get; set; }
    public string AlertType { get; set; } = string.Empty; // LimitWarning, LimitBreach, RapidIncrease, CollateralShortfall
    public string Severity { get; set; } = string.Empty; // Info, Warning, Critical
    public string Message { get; set; } = string.Empty;
    
    public bool Acknowledged { get; set; }
    public DateTime? AcknowledgedTime { get; set; }
    public string? AcknowledgedBy { get; set; }
    
    public string? ActionTaken { get; set; }
}

/// <summary>
/// Represents market surveillance for manipulation detection.
/// Addresses Gap TO-003: Real-time market manipulation detection and circuit breakers.
/// </summary>
public class MarketSurveillance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime DetectionTime { get; set; } = DateTime.UtcNow;
    
    // Alert Details
    public string AlertType { get; set; } = string.Empty; // PriceManipulation, Spoofing, Layering, WashTrading, FrontRunning, InsiderTrading
    public string AlertSeverity { get; set; } = string.Empty; // Low, Medium, High, Critical
    public string AlertDescription { get; set; } = string.Empty;
    
    // Market Context
    public MetalType MetalType { get; set; }
    public decimal TriggerPrice { get; set; }
    public decimal ReferencePrice { get; set; }
    public decimal PriceDeviation { get; set; }
    public decimal VolumeAnomaly { get; set; }
    
    // Suspected Parties
    public List<SuspectedParty> SuspectedParties { get; set; } = new();
    
    // Related Activity
    public List<string> RelatedOrderIds { get; set; } = new();
    public List<string> RelatedTradeIds { get; set; } = new();
    
    // Investigation
    public string Status { get; set; } = "Detected"; // Detected, UnderReview, Confirmed, FalsePositive, Escalated, Closed
    public string? AssignedTo { get; set; }
    public DateTime? InvestigationStartDate { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public string? Findings { get; set; }
    public string? ActionTaken { get; set; }
    
    // Regulatory Reporting
    public bool ReportedToRegulator { get; set; }
    public string? RegulatoryReportReference { get; set; }
    public DateTime? ReportedDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a suspected party in market manipulation.
/// </summary>
public class SuspectedParty
{
    public string PartyId { get; set; } = string.Empty;
    public string PartyName { get; set; } = string.Empty;
    public string PartyType { get; set; } = string.Empty; // Trader, Participant, Member
    public string SuspicionReason { get; set; } = string.Empty;
    public decimal SuspicionScore { get; set; } // 0-100
}

/// <summary>
/// Represents circuit breaker configuration and events.
/// </summary>
public class CircuitBreaker
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public MetalType MetalType { get; set; }
    
    // Configuration
    public decimal PriceMovementThreshold { get; set; } // Percentage
    public int TimeWindowMinutes { get; set; } = 5;
    public int HaltDurationMinutes { get; set; } = 15;
    
    // Current State
    public string Status { get; set; } = "Active"; // Active, Triggered, Resumed
    public bool IsTriggered { get; set; }
    public DateTime? TriggeredTime { get; set; }
    public DateTime? ResumeTime { get; set; }
    
    // Trigger Details
    public decimal? TriggeringPrice { get; set; }
    public decimal? ReferencePrice { get; set; }
    public decimal? PriceMovement { get; set; }
    public string? TriggerReason { get; set; }
    
    public string? Notes { get; set; }
}
