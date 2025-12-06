namespace Platform.Trading.Management.Models.Risk;

/// <summary>
/// Represents automated credit risk scoring for counterparties.
/// Addresses Gap RM-003: Automated credit scoring for counterparties.
/// </summary>
public class CreditRiskScore
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AssessmentDate { get; set; } = DateTime.UtcNow;
    
    // Entity
    public string EntityId { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // ClearingMember, Participant, Counterparty
    
    // Overall Score
    public decimal CreditScore { get; set; } // 0-100
    public string CreditRating { get; set; } = string.Empty; // AAA, AA, A, BBB, BB, B, CCC, CC, C, D
    public string RiskCategory { get; set; } = string.Empty; // Low, Medium, High, VeryHigh
    
    // Score Components
    public List<CreditScoreComponent> Components { get; set; } = new();
    
    // Financial Metrics
    public decimal? CurrentRatio { get; set; }
    public decimal? DebtToEquityRatio { get; set; }
    public decimal? InterestCoverageRatio { get; set; }
    public decimal? QuickRatio { get; set; }
    public decimal? WorkingCapital { get; set; }
    public string? FinancialCurrency { get; set; } = "USD";
    
    // Trading Metrics
    public decimal? TradingVolume30Days { get; set; }
    public decimal? AverageSettlementTime { get; set; }
    public int? DefaultCount { get; set; }
    public int? LatePaymentCount { get; set; }
    public decimal? PaymentPerformanceScore { get; set; }
    
    // External Ratings
    public string? ExternalRating { get; set; }
    public string? ExternalRatingAgency { get; set; }
    public DateTime? ExternalRatingDate { get; set; }
    
    // Credit Limits
    public decimal? RecommendedCreditLimit { get; set; }
    public decimal? CurrentCreditLimit { get; set; }
    public decimal? CreditUtilization { get; set; }
    public string? CreditLimitCurrency { get; set; } = "USD";
    
    // Trend
    public decimal? PreviousScore { get; set; }
    public decimal? ScoreChange { get; set; }
    public string? TrendDirection { get; set; } // Improving, Stable, Deteriorating
    
    // Alerts
    public List<CreditAlert> Alerts { get; set; } = new();
    public bool HasActiveAlerts { get; set; }
    
    // Review
    public DateTime? NextReviewDate { get; set; }
    public string? ReviewedBy { get; set; }
    public string? ReviewNotes { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a component of the credit score.
/// </summary>
public class CreditScoreComponent
{
    public string ComponentName { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Financial, Trading, Payment, External, Qualitative
    public decimal Score { get; set; } // 0-100
    public decimal Weight { get; set; } // Percentage weight
    public decimal WeightedScore { get; set; }
    public string? DataSource { get; set; }
    public DateTime? DataDate { get; set; }
}

/// <summary>
/// Represents a credit alert.
/// </summary>
public class CreditAlert
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AlertDate { get; set; }
    public string AlertType { get; set; } = string.Empty; // RatingDowngrade, LimitBreach, PaymentDefault, FinancialDeteriorating
    public string Severity { get; set; } = string.Empty; // Info, Warning, Critical
    public string Description { get; set; } = string.Empty;
    public bool Resolved { get; set; }
    public DateTime? ResolvedDate { get; set; }
    public string? Resolution { get; set; }
}

/// <summary>
/// Represents liquidity risk management.
/// Addresses Gap RM-005: Intraday liquidity monitoring and stress testing.
/// </summary>
public class LiquidityRisk
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AsOfDate { get; set; } = DateTime.UtcNow;
    public string MonitoringType { get; set; } = "Intraday"; // Intraday, EndOfDay, Stress
    
    // Entity
    public string EntityId { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // Exchange, ClearingMember, Participant
    
    // Current Liquidity Position
    public decimal AvailableLiquidity { get; set; }
    public string Currency { get; set; } = "USD";
    public List<LiquiditySource> LiquiditySources { get; set; } = new();
    
    // Liquidity Requirements
    public decimal GrossSettlementObligations { get; set; }
    public decimal NetSettlementObligations { get; set; }
    public decimal MarginCallsPayable { get; set; }
    public decimal VariationMarginPayable { get; set; }
    public decimal TotalLiquidityNeed { get; set; }
    
    // Coverage
    public decimal LiquidityCoverageRatio { get; set; } // Available / Needed
    public bool MeetsMinimumCoverage { get; set; }
    public decimal MinimumCoverageRatio { get; set; } = 100; // 100% default
    public decimal LiquidityGap { get; set; } // Positive = surplus, Negative = shortfall
    
    // Intraday Monitoring
    public List<IntradayLiquidityPoint> IntradayProfile { get; set; } = new();
    public decimal? PeakLiquidityUsage { get; set; }
    public DateTime? PeakTime { get; set; }
    public decimal? LowestAvailability { get; set; }
    public DateTime? LowestAvailabilityTime { get; set; }
    
    // Stress Testing
    public List<LiquidityStressScenario> StressScenarios { get; set; } = new();
    public bool PassesStressTests { get; set; }
    
    // Alerts
    public string AlertLevel { get; set; } = "Normal"; // Normal, Warning, Critical
    public List<LiquidityAlert> Alerts { get; set; } = new();
    
    // Contingency
    public bool ContingencyPlanActivated { get; set; }
    public List<string> AvailableContingencyMeasures { get; set; } = new();
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a source of liquidity.
/// </summary>
public class LiquiditySource
{
    public string SourceType { get; set; } = string.Empty; // Cash, CreditLine, RepoFacility, CentralBankFacility
    public string SourceName { get; set; } = string.Empty;
    public decimal AvailableAmount { get; set; }
    public decimal UtilizedAmount { get; set; }
    public decimal RemainingCapacity { get; set; }
    public string Currency { get; set; } = "USD";
    public int? AccessTimeMinutes { get; set; } // Time to access liquidity
    public decimal? CostBasisPoints { get; set; } // Cost to access
}

/// <summary>
/// Represents an intraday liquidity monitoring point.
/// </summary>
public class IntradayLiquidityPoint
{
    public DateTime Timestamp { get; set; }
    public decimal AvailableLiquidity { get; set; }
    public decimal CumulativeInflows { get; set; }
    public decimal CumulativeOutflows { get; set; }
    public decimal NetPosition { get; set; }
    public decimal UsagePercentage { get; set; }
}

/// <summary>
/// Represents a liquidity stress scenario.
/// </summary>
public class LiquidityStressScenario
{
    public string ScenarioName { get; set; } = string.Empty;
    public string ScenarioType { get; set; } = string.Empty; // HistoricalExtreme, HypotheticalSevere, RegulatoryStandard
    public decimal StressedLiquidityNeed { get; set; }
    public decimal StressedAvailability { get; set; }
    public decimal CoverageUnderStress { get; set; }
    public bool PassesScenario { get; set; }
    public decimal? AdditionalLiquidityNeeded { get; set; }
}

/// <summary>
/// Represents a liquidity alert.
/// </summary>
public class LiquidityAlert
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AlertTime { get; set; }
    public string AlertType { get; set; } = string.Empty; // ThresholdBreach, RapidDepletion, StressTestFail
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool Acknowledged { get; set; }
    public string? ActionTaken { get; set; }
}

/// <summary>
/// Represents concentration risk limits.
/// Addresses Gap RM-010: Sector and counterparty concentration limits.
/// </summary>
public class ConcentrationLimit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    // Limit Scope
    public string LimitType { get; set; } = string.Empty; // Counterparty, Sector, Country, MetalType, CollateralType
    public string LimitName { get; set; } = string.Empty;
    public string? EntityId { get; set; }
    public string? EntityName { get; set; }
    
    // Limit Definition
    public decimal LimitPercentage { get; set; } // Maximum percentage of total
    public decimal? AbsoluteLimit { get; set; } // Maximum absolute value
    public string? Currency { get; set; } = "USD";
    
    // Current Exposure
    public decimal CurrentExposure { get; set; }
    public decimal CurrentPercentage { get; set; }
    
    // Status
    public decimal UtilizationPercentage { get; set; }
    public bool IsBreached { get; set; }
    public decimal? HeadroomAmount { get; set; }
    public decimal? HeadroomPercentage { get; set; }
    
    // Warning Thresholds
    public decimal WarningThreshold { get; set; } = 80; // Percentage of limit
    public decimal CriticalThreshold { get; set; } = 95;
    public string AlertLevel { get; set; } = "Normal"; // Normal, Warning, Critical, Breach
    
    // History
    public List<ConcentrationBreachEvent> BreachHistory { get; set; } = new();
    public int BreachCount30Days { get; set; }
    
    // Exemptions
    public bool HasExemption { get; set; }
    public string? ExemptionReason { get; set; }
    public DateTime? ExemptionExpiryDate { get; set; }
    public string? ApprovedBy { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a concentration breach event.
/// </summary>
public class ConcentrationBreachEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime BreachDate { get; set; }
    public decimal BreachAmount { get; set; }
    public decimal BreachPercentage { get; set; }
    public string? Cause { get; set; }
    public string? ActionTaken { get; set; }
    public DateTime? ResolvedDate { get; set; }
}

/// <summary>
/// Represents operational risk management.
/// Addresses Gap RM-006: Comprehensive operational risk management.
/// </summary>
public class OperationalRisk
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AssessmentDate { get; set; } = DateTime.UtcNow;
    
    // Risk Category
    public string RiskCategory { get; set; } = string.Empty; // Process, People, Systems, External, Legal
    public string RiskSubCategory { get; set; } = string.Empty;
    public string RiskDescription { get; set; } = string.Empty;
    
    // Risk Assessment
    public int LikelihoodScore { get; set; } // 1-5
    public int ImpactScore { get; set; } // 1-5
    public int RiskScore => LikelihoodScore * ImpactScore;
    public string RiskLevel { get; set; } = string.Empty; // Low, Medium, High, Critical
    
    // Controls
    public List<RiskControl> Controls { get; set; } = new();
    public int ResidualLikelihood { get; set; }
    public int ResidualImpact { get; set; }
    public int ResidualRiskScore => ResidualLikelihood * ResidualImpact;
    
    // Incidents
    public List<string> RelatedIncidentIds { get; set; } = new();
    public int IncidentCount12Months { get; set; }
    public decimal? IncidentLosses12Months { get; set; }
    
    // Key Risk Indicators
    public List<KeyRiskIndicator> Indicators { get; set; } = new();
    
    // Owner
    public string RiskOwnerId { get; set; } = string.Empty;
    public string RiskOwnerName { get; set; } = string.Empty;
    public DateTime? NextReviewDate { get; set; }
    
    // Status
    public string Status { get; set; } = "Active"; // Active, Mitigated, Accepted, Closed
    public string? MitigationPlan { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a risk control.
/// </summary>
public class RiskControl
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ControlName { get; set; } = string.Empty;
    public string ControlType { get; set; } = string.Empty; // Preventive, Detective, Corrective
    public string Description { get; set; } = string.Empty;
    public string Effectiveness { get; set; } = string.Empty; // Effective, PartiallyEffective, Ineffective
    public DateTime? LastTestedDate { get; set; }
    public string? TestResult { get; set; }
}

/// <summary>
/// Represents a key risk indicator.
/// </summary>
public class KeyRiskIndicator
{
    public string IndicatorName { get; set; } = string.Empty;
    public decimal CurrentValue { get; set; }
    public decimal Threshold { get; set; }
    public string ThresholdType { get; set; } = string.Empty; // Max, Min
    public bool IsBreached { get; set; }
    public string Trend { get; set; } = string.Empty; // Improving, Stable, Deteriorating
    public DateTime MeasurementDate { get; set; }
}
