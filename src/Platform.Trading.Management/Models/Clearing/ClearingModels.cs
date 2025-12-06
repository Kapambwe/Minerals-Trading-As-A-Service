namespace Platform.Trading.Management.Models.Clearing;

/// <summary>
/// Represents Central Counterparty (CCP) infrastructure for trade clearing.
/// Addresses Gap CS-001: CCP functionality with risk mutualization, guarantee fund, and default management.
/// </summary>
public class ClearingMember
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string MemberNumber { get; set; } = string.Empty;
    public string MemberName { get; set; } = string.Empty;
    public string MemberType { get; set; } = "General"; // General, Direct, Client
    public DateTime MemberSince { get; set; }
    
    // Status
    public string Status { get; set; } = "Active"; // Active, Suspended, Default, Terminated
    public DateTime? SuspensionDate { get; set; }
    public string? SuspensionReason { get; set; }
    
    // Financial Requirements
    public decimal MinimumCapitalRequirement { get; set; }
    public decimal CurrentCapital { get; set; }
    public bool MeetsCapitalRequirement => CurrentCapital >= MinimumCapitalRequirement;
    
    // Guarantee Fund Contribution
    public decimal GuaranteeFundContribution { get; set; }
    public decimal GuaranteeFundCurrency { get; set; }
    public DateTime? LastContributionDate { get; set; }
    
    // Position Limits
    public decimal MaxGrossPosition { get; set; } // in USD
    public decimal MaxNetPosition { get; set; }
    public decimal CurrentGrossPosition { get; set; }
    public decimal CurrentNetPosition { get; set; }
    public bool ExceedsPositionLimits => CurrentGrossPosition > MaxGrossPosition || Math.Abs(CurrentNetPosition) > MaxNetPosition;
    
    // Credit Rating
    public string? CreditRating { get; set; }
    public string? CreditRatingAgency { get; set; }
    public DateTime? CreditRatingDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents the CCP Guarantee Fund used for loss mutualization.
/// Addresses Gap CS-001: Guarantee fund for risk mutualization.
/// </summary>
public class GuaranteeFund
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FundName { get; set; } = "ZME Clearing Guarantee Fund";
    public DateTime AsOfDate { get; set; } = DateTime.UtcNow;
    
    // Fund Components
    public decimal TotalFundSize { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Contribution breakdown
    public decimal CcpSkinInTheGame { get; set; } // CCP's own capital contribution
    public decimal MemberContributions { get; set; }
    public decimal RetainedEarnings { get; set; }
    
    // Utilization
    public decimal CurrentUtilization { get; set; }
    public decimal AvailableFunds => TotalFundSize - CurrentUtilization;
    public decimal UtilizationPercentage => TotalFundSize > 0 ? (CurrentUtilization / TotalFundSize) * 100 : 0;
    
    // Risk Parameters
    public decimal StressTestCoverage { get; set; } // Percentage of worst-case scenario covered
    public DateTime? LastStressTestDate { get; set; }
    public string? StressTestResult { get; set; }
    
    // Member Contributions
    public List<GuaranteeFundContribution> MemberContributionDetails { get; set; } = new();
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a member's contribution to the guarantee fund.
/// </summary>
public class GuaranteeFundContribution
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string MemberId { get; set; } = string.Empty;
    public string MemberName { get; set; } = string.Empty;
    
    public decimal RequiredContribution { get; set; }
    public decimal ActualContribution { get; set; }
    public string Currency { get; set; } = "USD";
    
    public bool IsPaidInFull => ActualContribution >= RequiredContribution;
    public decimal Shortfall => RequiredContribution - ActualContribution;
    
    public DateTime ContributionDate { get; set; }
    public DateTime? NextReviewDate { get; set; }
    
    // Calculation basis
    public string CalculationMethod { get; set; } = "RiskBased"; // RiskBased, Volume, Fixed
    public decimal AverageRiskExposure { get; set; }
    public decimal TradingVolume { get; set; }
}

/// <summary>
/// Represents default management procedures and waterfall.
/// Addresses Gap CS-006: Default management procedures.
/// </summary>
public class DefaultManagement
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CaseNumber { get; set; } = string.Empty;
    public DateTime DefaultDate { get; set; }
    
    // Defaulting Member
    public string DefaultingMemberId { get; set; } = string.Empty;
    public string DefaultingMemberName { get; set; } = string.Empty;
    public string DefaultType { get; set; } = string.Empty; // PaymentDefault, DeliveryDefault, MarginDefault, Insolvency
    
    // Default Details
    public decimal DefaultAmount { get; set; }
    public string Currency { get; set; } = "USD";
    public string Description { get; set; } = string.Empty;
    
    // Status
    public string Status { get; set; } = "Declared"; // Declared, InManagement, PortfolioAuction, LossAllocation, Closed
    public DateTime? ResolutionDate { get; set; }
    
    // Loss Waterfall Application
    public List<WaterfallLayer> WaterfallApplication { get; set; } = new();
    public decimal TotalLoss { get; set; }
    public decimal RecoveredAmount { get; set; }
    public decimal RemainingLoss => TotalLoss - RecoveredAmount;
    
    // Portfolio Management
    public int AffectedTradesCount { get; set; }
    public decimal AffectedTradesValue { get; set; }
    public bool PortfolioHedged { get; set; }
    public bool PortfolioAuctioned { get; set; }
    public DateTime? AuctionDate { get; set; }
    public string? AuctionWinnerId { get; set; }
    
    // Timeline
    public List<DefaultManagementEvent> Events { get; set; } = new();
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a layer in the default loss waterfall.
/// </summary>
public class WaterfallLayer
{
    public int Order { get; set; }
    public string LayerName { get; set; } = string.Empty;
    public string LayerType { get; set; } = string.Empty; // DefaulterMargin, DefaulterGuaranteeFund, CcpSkinInTheGame, NonDefaulterGuaranteeFund, CcpCapital, AssessmentRights
    
    public decimal AvailableAmount { get; set; }
    public decimal UtilizedAmount { get; set; }
    public decimal RemainingAmount => AvailableAmount - UtilizedAmount;
    
    public decimal PercentageOfLossCovered { get; set; }
    public DateTime? ApplicationDate { get; set; }
}

/// <summary>
/// Represents an event in the default management process.
/// </summary>
public class DefaultManagementEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime EventDate { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? ActionTaken { get; set; }
    public string? PerformedBy { get; set; }
}

/// <summary>
/// Represents RTGS (Real-Time Gross Settlement) integration for instant settlement.
/// Addresses Gap CS-002: Integration with Zambia's RTGS for instant settlement.
/// </summary>
public class RtgsTransaction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TransactionReference { get; set; } = string.Empty;
    public DateTime InitiatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? SettledDate { get; set; }
    
    // Transaction Type
    public string TransactionType { get; set; } = string.Empty; // MarginPayment, SettlementPayment, GuaranteeFundContribution, FeePayment
    
    // Parties
    public string SenderBankCode { get; set; } = string.Empty;
    public string SenderBankName { get; set; } = string.Empty;
    public string SenderAccountNumber { get; set; } = string.Empty;
    public string SenderAccountName { get; set; } = string.Empty;
    
    public string ReceiverBankCode { get; set; } = string.Empty;
    public string ReceiverBankName { get; set; } = string.Empty;
    public string ReceiverAccountNumber { get; set; } = string.Empty;
    public string ReceiverAccountName { get; set; } = string.Empty;
    
    // Amount
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZMW";
    
    // Related Trade/Settlement
    public string? TradeId { get; set; }
    public string? SettlementId { get; set; }
    
    // Status
    public string Status { get; set; } = "Pending"; // Pending, Submitted, Processing, Settled, Failed, Rejected
    public string? RtgsConfirmationNumber { get; set; }
    public string? FailureReason { get; set; }
    
    // BOZ Reference
    public string? BozReference { get; set; }
    public DateTime? BozConfirmationTime { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents Delivery vs Payment (DvP) settlement.
/// Addresses Gap CS-003: Atomic DvP for physical settlements.
/// </summary>
public class DvpSettlement
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SettlementReference { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    
    // Trade Information
    public string TradeId { get; set; } = string.Empty;
    public string TradeNumber { get; set; } = string.Empty;
    
    // Parties
    public string SellerId { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public string BuyerName { get; set; } = string.Empty;
    
    // Delivery Leg
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // metric tons
    public string QualityGrade { get; set; } = string.Empty;
    public string WarrantId { get; set; } = string.Empty;
    public string WarrantNumber { get; set; } = string.Empty;
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    
    public string DeliveryStatus { get; set; } = "Pending"; // Pending, Blocked, Released, Transferred, Failed
    public DateTime? DeliveryBlockedTime { get; set; }
    public DateTime? DeliveryReleasedTime { get; set; }
    
    // Payment Leg
    public decimal PaymentAmount { get; set; }
    public string PaymentCurrency { get; set; } = "USD";
    public string PaymentMethod { get; set; } = "RTGS"; // RTGS, Wire, Escrow
    public string? RtgsTransactionId { get; set; }
    
    public string PaymentStatus { get; set; } = "Pending"; // Pending, Escrowed, Confirmed, Released, Failed
    public DateTime? PaymentEscrowedTime { get; set; }
    public DateTime? PaymentReleasedTime { get; set; }
    
    // Atomic Settlement
    public bool IsAtomic { get; set; } = true;
    public string OverallStatus { get; set; } = "Pending"; // Pending, InProgress, Completed, Failed, Rolled Back
    public bool DeliveryAndPaymentMatched => DeliveryStatus == "Transferred" && PaymentStatus == "Released";
    
    // Failure Handling
    public string? FailureReason { get; set; }
    public bool RolledBack { get; set; }
    public DateTime? RollbackTime { get; set; }
    public string? RollbackReason { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a settlement cycle for T+2 settlement enforcement.
/// Addresses Gap CS-005: T+2 settlement cycle.
/// </summary>
public class SettlementCycle
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime TradeDate { get; set; }
    public int SettlementDays { get; set; } = 2; // T+2
    public DateTime ScheduledSettlementDate { get; set; }
    public DateTime? ActualSettlementDate { get; set; }
    
    public string Status { get; set; } = "Scheduled"; // Scheduled, InProgress, Completed, Failed, Extended
    public bool IsOnTime => ActualSettlementDate.HasValue && ActualSettlementDate.Value <= ScheduledSettlementDate;
    
    public int? DaysLate => ActualSettlementDate.HasValue 
        ? Math.Max(0, (ActualSettlementDate.Value - ScheduledSettlementDate).Days) 
        : null;
    
    // Related items
    public List<string> TradeIds { get; set; } = new();
    public List<string> SettlementIds { get; set; } = new();
    
    public string? Notes { get; set; }
}
