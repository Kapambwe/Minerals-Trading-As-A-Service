namespace Platform.Trading.Management.Models.Clearing;

/// <summary>
/// Represents multilateral netting calculation for settlement efficiency.
/// Addresses Gap CS-004: Multilateral netting to reduce settlement volumes.
/// </summary>
public class NettingCalculation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string NettingCycleId { get; set; } = string.Empty;
    public DateTime CalculationDate { get; set; } = DateTime.UtcNow;
    public DateTime SettlementDate { get; set; }
    
    // Netting Type
    public string NettingType { get; set; } = "Multilateral"; // Bilateral, Multilateral
    public string NettingScope { get; set; } = "AllProducts"; // AllProducts, ByMetalType, BySettlementDate
    
    // Participants
    public List<NettingParticipant> Participants { get; set; } = new();
    public int ParticipantCount { get; set; }
    
    // Gross Values
    public decimal GrossPayables { get; set; }
    public decimal GrossReceivables { get; set; }
    public int GrossTradeCount { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Net Values
    public decimal NetPayables { get; set; }
    public decimal NetReceivables { get; set; }
    public int NetSettlementCount { get; set; }
    
    // Efficiency Metrics
    public decimal NettingEfficiency => GrossPayables > 0 ? ((GrossPayables - NetPayables) / GrossPayables) * 100 : 0;
    public decimal TradeReduction => GrossTradeCount > 0 ? ((GrossTradeCount - NetSettlementCount) / (decimal)GrossTradeCount) * 100 : 0;
    public decimal LiquiditySavings { get; set; }
    
    // Physical Delivery Netting
    public List<PhysicalNetting> PhysicalNettingResults { get; set; } = new();
    
    // Status
    public string Status { get; set; } = "Calculated"; // Calculated, Approved, Applied, Completed
    public DateTime? AppliedDate { get; set; }
    public string? ApprovedBy { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a participant's netting position.
/// </summary>
public class NettingParticipant
{
    public string ParticipantId { get; set; } = string.Empty;
    public string ParticipantName { get; set; } = string.Empty;
    
    public decimal GrossPayable { get; set; }
    public decimal GrossReceivable { get; set; }
    public decimal NetPosition { get; set; } // Positive = receive, Negative = pay
    
    public int TradesAsBuyer { get; set; }
    public int TradesAsSeller { get; set; }
    
    public string SettlementInstruction { get; set; } = string.Empty; // Pay, Receive, None
}

/// <summary>
/// Represents physical delivery netting.
/// </summary>
public class PhysicalNetting
{
    public string ParticipantId { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public string? QualityGrade { get; set; }
    
    public decimal GrossDeliverable { get; set; } // Quantity to deliver
    public decimal GrossReceivable { get; set; } // Quantity to receive
    public decimal NetPhysicalPosition { get; set; } // Positive = receive, Negative = deliver
    
    public string? WarehouseId { get; set; }
    public string DeliveryInstruction { get; set; } = string.Empty; // Deliver, Receive, None
}

/// <summary>
/// Represents collateral management with eligibility and haircut calculations.
/// Addresses Gap CS-007: Sophisticated collateral eligibility and haircut management.
/// </summary>
public class CollateralManagement
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime AsOfDate { get; set; } = DateTime.UtcNow;
    
    // Account
    public string ParticipantId { get; set; } = string.Empty;
    public string ParticipantName { get; set; } = string.Empty;
    public string AccountType { get; set; } = "Margin"; // Margin, GuaranteeFund, Prefunded
    
    // Collateral Holdings
    public List<CollateralHolding> Holdings { get; set; } = new();
    
    // Summary Values
    public decimal TotalMarketValue { get; set; }
    public decimal TotalHaircut { get; set; }
    public decimal TotalCollateralValue { get; set; } // After haircuts
    
    // Requirements
    public decimal RequiredCollateral { get; set; }
    public decimal ExcessDeficit => TotalCollateralValue - RequiredCollateral;
    public bool IsSufficient => TotalCollateralValue >= RequiredCollateral;
    public decimal CoverageRatio => RequiredCollateral > 0 ? (TotalCollateralValue / RequiredCollateral) * 100 : 100;
    
    // Concentration Limits
    public List<ConcentrationCheck> ConcentrationChecks { get; set; } = new();
    public bool PassesConcentrationLimits { get; set; }
    
    // Margin Calls
    public decimal? PendingMarginCall { get; set; }
    public DateTime? MarginCallDate { get; set; }
    public DateTime? MarginCallDeadline { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a single collateral holding.
/// </summary>
public class CollateralHolding
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CollateralType { get; set; } = string.Empty; // Cash, GovernmentBond, CorporateBond, Metal, BankGuarantee, LetterOfCredit
    public string Description { get; set; } = string.Empty;
    
    // Asset Details
    public string? SecurityId { get; set; } // ISIN, CUSIP
    public string? Issuer { get; set; }
    public DateTime? MaturityDate { get; set; }
    public string? CreditRating { get; set; }
    
    // Valuation
    public decimal Quantity { get; set; }
    public decimal MarketPrice { get; set; }
    public decimal MarketValue { get; set; }
    public string Currency { get; set; } = "USD";
    public DateTime? LastPriceDate { get; set; }
    
    // Haircut
    public decimal HaircutPercentage { get; set; }
    public decimal HaircutAmount => MarketValue * (HaircutPercentage / 100);
    public decimal CollateralValue => MarketValue - HaircutAmount;
    public string HaircutReason { get; set; } = string.Empty; // CreditRisk, LiquidityRisk, PriceVolatility, ConcentrationRisk
    
    // Eligibility
    public bool IsEligible { get; set; }
    public string? IneligibilityReason { get; set; }
    public DateTime? EligibilityCheckDate { get; set; }
    
    // Pledged Status
    public string Status { get; set; } = "Active"; // Active, Pending, Released, Substituted
    public DateTime? PledgeDate { get; set; }
    public DateTime? ReleaseDate { get; set; }
}

/// <summary>
/// Represents a concentration check for collateral.
/// </summary>
public class ConcentrationCheck
{
    public string CheckType { get; set; } = string.Empty; // SingleIssuer, AssetClass, Currency, Maturity
    public string Description { get; set; } = string.Empty;
    public decimal CurrentConcentration { get; set; }
    public decimal LimitPercentage { get; set; }
    public bool PassesLimit => CurrentConcentration <= LimitPercentage;
    public decimal? ExcessAmount { get; set; }
}

/// <summary>
/// Represents collateral eligibility criteria.
/// </summary>
public class CollateralEligibility
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string AssetClass { get; set; } = string.Empty;
    public string AssetType { get; set; } = string.Empty;
    
    public bool IsEligible { get; set; }
    public string? MinimumCreditRating { get; set; }
    public int? MaximumMaturityDays { get; set; }
    public decimal? MaximumConcentration { get; set; }
    
    public decimal StandardHaircut { get; set; }
    public decimal? StressHaircut { get; set; }
    
    public DateTime EffectiveDate { get; set; }
    public bool IsActive { get; set; } = true;
}

/// <summary>
/// Represents position limits for concentration risk management.
/// Addresses Gap CS-008: Configurable position limits per participant.
/// </summary>
public class PositionLimit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    // Scope
    public string LimitScope { get; set; } = "Participant"; // Participant, Market, MetalType, Contract
    public string? ParticipantId { get; set; }
    public string? ParticipantName { get; set; }
    public MetalType? MetalType { get; set; }
    public string? ContractCode { get; set; }
    
    // Limit Types
    public string LimitType { get; set; } = "Absolute"; // Absolute, Percentage, OpenInterest
    
    // Gross Limits
    public decimal? MaxGrossLongPosition { get; set; }
    public decimal? MaxGrossShortPosition { get; set; }
    public decimal? MaxGrossPosition { get; set; }
    
    // Net Limits
    public decimal? MaxNetLongPosition { get; set; }
    public decimal? MaxNetShortPosition { get; set; }
    
    // Spot Month Limits (for delivery month)
    public decimal? SpotMonthLimit { get; set; }
    public decimal? SpotMonthPercentage { get; set; } // Percentage of open interest
    
    // All Months Combined
    public decimal? AllMonthsLimit { get; set; }
    
    // Notional Limits
    public decimal? MaxNotionalValue { get; set; }
    public string? NotionalCurrency { get; set; } = "USD";
    
    // Current Usage
    public decimal? CurrentGrossLong { get; set; }
    public decimal? CurrentGrossShort { get; set; }
    public decimal? CurrentNetPosition { get; set; }
    public decimal? UtilizationPercentage { get; set; }
    
    // Status
    public string Status { get; set; } = "Active"; // Active, Suspended, Expired
    public bool IsBreached { get; set; }
    public DateTime? LastBreachDate { get; set; }
    public string? BreachDetails { get; set; }
    
    // Exemptions
    public bool HasExemption { get; set; }
    public string? ExemptionReason { get; set; }
    public DateTime? ExemptionExpiryDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents automated mark-to-market calculation.
/// Addresses Gap CS-009: Automated daily mark-to-market with margin calls.
/// </summary>
public class MarkToMarket
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime ValuationDate { get; set; } = DateTime.UtcNow;
    public string ValuationCycle { get; set; } = "EndOfDay"; // Intraday, EndOfDay, RealTime
    
    // Entity
    public string EntityId { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // ClearingMember, Portfolio, Account
    
    // Portfolio Summary
    public List<MtmPosition> Positions { get; set; } = new();
    public int TotalPositions { get; set; }
    
    // Valuation
    public decimal PreviousDayValue { get; set; }
    public decimal CurrentMarketValue { get; set; }
    public decimal DailyPnL { get; set; }
    public decimal UnrealizedPnL { get; set; }
    public decimal RealizedPnL { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Margin Impact
    public decimal PreviousMarginRequirement { get; set; }
    public decimal CurrentMarginRequirement { get; set; }
    public decimal MarginChange { get; set; }
    
    // Collateral
    public decimal CurrentCollateral { get; set; }
    public decimal MarginExcessDeficit { get; set; }
    
    // Margin Call
    public bool MarginCallRequired { get; set; }
    public decimal? MarginCallAmount { get; set; }
    public DateTime? MarginCallDeadline { get; set; }
    public string MarginCallStatus { get; set; } = "None"; // None, Issued, Pending, Paid, Defaulted
    
    // Variation Margin
    public decimal VariationMarginDue { get; set; } // Positive = collect, Negative = pay
    public DateTime? VariationMarginDeadline { get; set; }
    public bool VariationMarginPaid { get; set; }
    
    // Processing
    public string ProcessingStatus { get; set; } = "Completed"; // InProgress, Completed, Failed
    public DateTime? ProcessingStartTime { get; set; }
    public DateTime? ProcessingEndTime { get; set; }
    public string? ProcessingNotes { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a single position in MTM calculation.
/// </summary>
public class MtmPosition
{
    public string PositionId { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public string? ContractCode { get; set; }
    public string Side { get; set; } = string.Empty; // Long, Short
    
    public decimal Quantity { get; set; }
    public decimal AverageCost { get; set; }
    public decimal CurrentPrice { get; set; }
    
    public decimal CostBasis { get; set; }
    public decimal CurrentValue { get; set; }
    public decimal UnrealizedPnL { get; set; }
    public decimal PnLPercentage { get; set; }
    
    public decimal MarginRequired { get; set; }
    public decimal MarginChange { get; set; }
}

/// <summary>
/// Represents interest calculation on margin balances.
/// Addresses Gap CS-010: Interest on margin balances and late payments.
/// </summary>
public class InterestCalculation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CalculationDate { get; set; } = DateTime.UtcNow;
    public string CalculationPeriod { get; set; } = string.Empty; // e.g., "2024-01"
    
    // Entity
    public string ParticipantId { get; set; } = string.Empty;
    public string ParticipantName { get; set; } = string.Empty;
    
    // Balance Information
    public string BalanceType { get; set; } = string.Empty; // MarginDeposit, ExcessMargin, LatePayment
    public decimal AverageBalance { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Interest Rate
    public decimal InterestRate { get; set; } // Annual percentage
    public string RateBasis { get; set; } = string.Empty; // BOZPolicyRate, SOFR, Fixed
    public decimal? SpreadOverBase { get; set; }
    
    // Calculation
    public int Days { get; set; }
    public int DayCountBasis { get; set; } = 365; // 360, 365, Actual
    public decimal InterestAmount { get; set; }
    
    // Payment
    public string Direction { get; set; } = string.Empty; // Credit (to participant), Debit (from participant)
    public bool Applied { get; set; }
    public DateTime? AppliedDate { get; set; }
    public string? PaymentReference { get; set; }
    
    public string? Notes { get; set; }
}
