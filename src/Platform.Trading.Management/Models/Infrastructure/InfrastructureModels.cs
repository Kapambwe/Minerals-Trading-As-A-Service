using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Models.Infrastructure;

/// <summary>
/// Represents a real-time data feed configuration.
/// Addresses Gap TI-004: WebSocket/streaming data for live prices.
/// </summary>
public class DataFeed
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FeedName { get; set; } = string.Empty;
    public string FeedType { get; set; } = "Price"; // Price, OrderBook, Trade, News, MarketData
    
    // Connection
    public string Protocol { get; set; } = "WebSocket"; // WebSocket, REST, FIX, MQTT
    public string EndpointUrl { get; set; } = string.Empty;
    public int Port { get; set; }
    public bool UseSSL { get; set; } = true;
    
    // Authentication
    public string AuthType { get; set; } = "ApiKey"; // ApiKey, OAuth, Certificate, None
    public string? ApiKeyHeader { get; set; }
    public bool RequiresAuthentication { get; set; } = true;
    
    // Subscription
    public List<FeedSubscription> Subscriptions { get; set; } = new();
    public int MaxSubscriptions { get; set; } = 100;
    
    // Status
    public string ConnectionStatus { get; set; } = "Disconnected"; // Connected, Disconnected, Reconnecting, Error
    public DateTime? LastConnectedTime { get; set; }
    public DateTime? LastDisconnectedTime { get; set; }
    public DateTime? LastMessageTime { get; set; }
    
    // Performance
    public long MessagesReceived { get; set; }
    public long MessagesPerSecond { get; set; }
    public int LatencyMs { get; set; }
    public int? AverageLatencyMs { get; set; }
    
    // Reliability
    public int ReconnectAttempts { get; set; }
    public int MaxReconnectAttempts { get; set; } = 10;
    public int ReconnectDelayMs { get; set; } = 5000;
    public decimal UptimePercentage { get; set; }
    
    // Rate Limiting
    public int? RateLimitPerSecond { get; set; }
    public int? CurrentRatePerSecond { get; set; }
    public bool IsThrottled { get; set; }
    
    // Errors
    public List<DataFeedError> RecentErrors { get; set; } = new();
    public int ErrorCount24h { get; set; }
    
    public bool IsActive { get; set; } = true;
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a subscription to a data feed channel.
/// </summary>
public class FeedSubscription
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Channel { get; set; } = string.Empty;
    public string SubscriptionType { get; set; } = string.Empty; // Ticker, Depth, Trade, OHLC
    
    public MetalType? MetalType { get; set; }
    public string? Symbol { get; set; }
    public string? Exchange { get; set; }
    
    public bool IsActive { get; set; } = true;
    public DateTime SubscribedAt { get; set; }
    public long MessagesReceived { get; set; }
    public DateTime? LastMessageTime { get; set; }
}

/// <summary>
/// Represents a data feed error.
/// </summary>
public class DataFeedError
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime ErrorTime { get; set; }
    public string ErrorType { get; set; } = string.Empty; // Connection, Authentication, RateLimit, Timeout, Parse
    public string ErrorCode { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
    public bool WasRecovered { get; set; }
    public DateTime? RecoveryTime { get; set; }
}

/// <summary>
/// Represents a real-time price tick.
/// </summary>
public class PriceTick
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Timestamp { get; set; }
    public long SequenceNumber { get; set; }
    
    public string Source { get; set; } = string.Empty; // LME, COMEX, ZME
    public MetalType MetalType { get; set; }
    public string? Symbol { get; set; }
    
    public decimal? BidPrice { get; set; }
    public decimal? AskPrice { get; set; }
    public decimal? LastPrice { get; set; }
    public decimal? BidSize { get; set; }
    public decimal? AskSize { get; set; }
    public decimal? LastSize { get; set; }
    
    public string Currency { get; set; } = "USD";
    
    public decimal? Change { get; set; }
    public decimal? ChangePercent { get; set; }
    
    public long? Volume { get; set; }
    public decimal? VWAP { get; set; }
}

/// <summary>
/// Represents participant membership tiers.
/// Addresses Gap PM-001: Tiered membership with different rights/fees.
/// </summary>
public class MembershipTier
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TierCode { get; set; } = string.Empty;
    public string TierName { get; set; } = string.Empty;
    public int TierLevel { get; set; } // 1=highest, 5=lowest
    public string Description { get; set; } = string.Empty;
    
    // Eligibility Requirements
    public decimal? MinimumCapital { get; set; }
    public string? MinimumCreditRating { get; set; }
    public decimal? MinimumTradingVolume { get; set; } // Monthly volume
    public int? MinimumYearsInBusiness { get; set; }
    public List<string> RequiredCertifications { get; set; } = new();
    
    // Rights & Privileges
    public bool CanTradeSpot { get; set; } = true;
    public bool CanTradeFutures { get; set; }
    public bool CanTradeOptions { get; set; }
    public bool CanClearOwnTrades { get; set; }
    public bool CanClearForOthers { get; set; }
    public bool HasDirectMarketAccess { get; set; }
    public bool CanParticipateInAuctions { get; set; } = true;
    public bool CanSponsorOtherMembers { get; set; }
    
    // Position Limits Multiplier
    public decimal PositionLimitMultiplier { get; set; } = 1.0m;
    
    // Fee Structure
    public MembershipFeeStructure FeeStructure { get; set; } = new();
    
    // Access Rights
    public List<string> AllowedMarkets { get; set; } = new();
    public List<MetalType> AllowedMetals { get; set; } = new();
    public int? MaxConcurrentOrders { get; set; }
    public int? ApiRateLimitPerMinute { get; set; }
    
    // Status
    public bool IsActive { get; set; } = true;
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents fee structure for a membership tier.
/// </summary>
public class MembershipFeeStructure
{
    // Annual Fees
    public decimal AnnualMembershipFee { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Trading Fees
    public decimal TradingFeePerTon { get; set; }
    public decimal? TradingFeePercentage { get; set; }
    public decimal? MinimumTradingFee { get; set; }
    public decimal? MaximumTradingFee { get; set; }
    
    // Clearing Fees
    public decimal ClearingFeePerTrade { get; set; }
    public decimal? ClearingFeePercentage { get; set; }
    
    // Settlement Fees
    public decimal SettlementFeePhysical { get; set; }
    public decimal SettlementFeeCash { get; set; }
    
    // Other Fees
    public decimal? RegistrationFee { get; set; }
    public decimal? TechnologyFee { get; set; }
    public decimal? MarketDataFee { get; set; }
    public decimal? ApiAccessFee { get; set; }
    
    // Discounts
    public decimal? VolumeDiscountThreshold { get; set; }
    public decimal? VolumeDiscountPercentage { get; set; }
    public decimal? EarlyPaymentDiscount { get; set; }
}

/// <summary>
/// Represents participant onboarding workflow.
/// Addresses Gap PM-002: Digital onboarding with document management.
/// </summary>
public class OnboardingWorkflow
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ApplicationNumber { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; } = DateTime.UtcNow;
    
    // Applicant Information
    public string ApplicantId { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public string ApplicantType { get; set; } = string.Empty; // Individual, Corporation, Partnership, Cooperative
    public string ApplyingForTier { get; set; } = string.Empty;
    
    // Workflow Status
    public string CurrentStage { get; set; } = "Application"; // Application, DocumentReview, DueDiligence, Approval, Activation
    public string OverallStatus { get; set; } = "InProgress"; // InProgress, OnHold, Approved, Rejected, Withdrawn
    public DateTime? StatusDate { get; set; }
    
    // Stages
    public List<OnboardingStage> Stages { get; set; } = new();
    public int CompletedStages { get; set; }
    public int TotalStages { get; set; }
    public decimal ProgressPercentage => TotalStages > 0 ? ((decimal)CompletedStages / TotalStages) * 100 : 0;
    
    // Documents
    public List<OnboardingDocument> RequiredDocuments { get; set; } = new();
    public int DocumentsSubmitted { get; set; }
    public int DocumentsVerified { get; set; }
    public int DocumentsPending { get; set; }
    
    // Due Diligence
    public bool AmlCheckCompleted { get; set; }
    public string? AmlCheckResult { get; set; }
    public bool KycCheckCompleted { get; set; }
    public string? KycCheckResult { get; set; }
    public bool CreditCheckCompleted { get; set; }
    public string? CreditCheckResult { get; set; }
    public bool ReferenceCheckCompleted { get; set; }
    
    // Fees
    public decimal ApplicationFee { get; set; }
    public bool ApplicationFeePaid { get; set; }
    public DateTime? FeePaymentDate { get; set; }
    
    // Assignment
    public string? AssignedReviewerId { get; set; }
    public string? AssignedReviewerName { get; set; }
    
    // Timeline
    public DateTime? ExpectedCompletionDate { get; set; }
    public DateTime? ActualCompletionDate { get; set; }
    public int? ProcessingDays { get; set; }
    
    // Approval
    public string? ApprovedBy { get; set; }
    public DateTime? ApprovalDate { get; set; }
    public string? ApprovalNotes { get; set; }
    public string? RejectionReason { get; set; }
    
    // Activation
    public DateTime? ActivationDate { get; set; }
    public string? MembershipNumber { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a stage in the onboarding workflow.
/// </summary>
public class OnboardingStage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string StageName { get; set; } = string.Empty;
    public int StageOrder { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, InProgress, Completed, Failed, Skipped
    
    public DateTime? StartDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public string? CompletedBy { get; set; }
    
    public List<string> RequiredActions { get; set; } = new();
    public List<string> CompletedActions { get; set; } = new();
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a document in the onboarding process.
/// </summary>
public class OnboardingDocument
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DocumentType { get; set; } = string.Empty; // CertificateOfIncorporation, TaxClearance, BankReference, FinancialStatements, etc.
    public string DocumentName { get; set; } = string.Empty;
    public bool IsRequired { get; set; } = true;
    
    public string Status { get; set; } = "Pending"; // Pending, Submitted, UnderReview, Verified, Rejected
    public DateTime? SubmittedDate { get; set; }
    public DateTime? VerifiedDate { get; set; }
    public string? VerifiedBy { get; set; }
    
    public string? DocumentReference { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? RejectionReason { get; set; }
    
    public string? Notes { get; set; }
}
