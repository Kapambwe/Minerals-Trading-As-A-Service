namespace Platform.Trading.Management.Models.Participant;

/// <summary>
/// Represents an Artisanal and Small-scale Miner (ASM) participant.
/// Addresses Gap PM-010: Support for artisanal and small-scale miners.
/// </summary>
public class ArtisanalMiner
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string RegistrationNumber { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
    
    // Miner Information
    public string MinerType { get; set; } = "Individual"; // Individual, Cooperative, SmallEnterprise
    public string Name { get; set; } = string.Empty;
    public string? CooperativeName { get; set; }
    public int MemberCount { get; set; } = 1; // For cooperatives
    
    // Contact Details
    public string PhoneNumber { get; set; } = string.Empty;
    public string? AlternativePhone { get; set; }
    public string? Email { get; set; }
    public string Province { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Village { get; set; } = string.Empty;
    public string? GpsCoordinates { get; set; }
    
    // Identity Verification
    public string IdType { get; set; } = string.Empty; // NRC, Passport
    public string IdNumber { get; set; } = string.Empty;
    public DateTime? IdVerifiedDate { get; set; }
    public bool IdVerified { get; set; }
    
    // Mining License
    public string? ArtisanalMiningLicense { get; set; }
    public DateTime? LicenseIssueDate { get; set; }
    public DateTime? LicenseExpiryDate { get; set; }
    public bool LicenseVerified { get; set; }
    public string LicenseStatus { get; set; } = "Pending"; // Pending, Valid, Expired, Suspended
    
    // Mining Details
    public List<MetalType> MineralsProduced { get; set; } = new();
    public string MiningMethod { get; set; } = string.Empty; // Manual, SemiMechanized, Alluvial
    public decimal EstimatedMonthlyProduction { get; set; } // kg
    public string? MiningAreaDescription { get; set; }
    
    // Aggregation Center
    public string? AggregationCenterId { get; set; }
    public string? AggregationCenterName { get; set; }
    
    // Banking/Payment
    public bool HasBankAccount { get; set; }
    public string? BankName { get; set; }
    public string? AccountNumber { get; set; }
    public string? MobileMoneyProvider { get; set; } // MTN_MoMo, Airtel_Money, Zamtel_Kwacha
    public string? MobileMoneyNumber { get; set; }
    public string PreferredPaymentMethod { get; set; } = "MobileMoney"; // MobileMoney, BankTransfer, Cash
    
    // Training and Certification
    public List<AsmTrainingRecord> TrainingRecords { get; set; } = new();
    public bool ResponsibleMiningCertified { get; set; }
    public DateTime? ResponsibleMiningCertDate { get; set; }
    
    // Conflict-Free Compliance
    public bool ConflictFreeVerified { get; set; }
    public DateTime? LastVerificationDate { get; set; }
    public string? OecdDueDiligenceLevel { get; set; }
    
    // Status
    public string Status { get; set; } = "Pending"; // Pending, Approved, Suspended, Inactive
    public DateTime? ApprovalDate { get; set; }
    public string? ApprovedBy { get; set; }
    public string? SuspensionReason { get; set; }
    
    // Production History
    public List<AsmProductionRecord> ProductionHistory { get; set; } = new();
    public decimal TotalLifetimeProduction { get; set; } // kg
    public decimal TotalLifetimeValue { get; set; } // USD
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents ASM training records.
/// </summary>
public class AsmTrainingRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TrainingName { get; set; } = string.Empty;
    public string TrainingType { get; set; } = string.Empty; // ResponsibleMining, Safety, Environmental, Quality
    public string Provider { get; set; } = string.Empty;
    public DateTime TrainingDate { get; set; }
    public bool Completed { get; set; }
    public string? CertificateNumber { get; set; }
    public DateTime? ExpiryDate { get; set; }
}

/// <summary>
/// Represents ASM production records.
/// </summary>
public class AsmProductionRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime ProductionDate { get; set; }
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // kg
    public string QualityGrade { get; set; } = string.Empty;
    
    // Delivery
    public string? AggregationCenterId { get; set; }
    public string? DeliveryReceiptNumber { get; set; }
    public DateTime? DeliveryDate { get; set; }
    
    // Payment
    public decimal UnitPrice { get; set; }
    public decimal TotalValue { get; set; }
    public string PaymentCurrency { get; set; } = "ZMW";
    public string PaymentStatus { get; set; } = "Pending"; // Pending, Paid
    public DateTime? PaymentDate { get; set; }
    public string? PaymentReference { get; set; }
}

/// <summary>
/// Represents an aggregation center for ASM production.
/// </summary>
public class AggregationCenter
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CenterCode { get; set; } = string.Empty;
    public string CenterName { get; set; } = string.Empty;
    public DateTime EstablishedDate { get; set; }
    
    // Location
    public string Province { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? GpsCoordinates { get; set; }
    
    // Contact
    public string ManagerName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    
    // Capacity
    public List<MetalType> MineralsAccepted { get; set; } = new();
    public decimal StorageCapacity { get; set; } // metric tons
    public decimal CurrentStock { get; set; }
    
    // Equipment
    public bool HasWeighingEquipment { get; set; }
    public bool HasQualityTestingEquipment { get; set; }
    public DateTime? LastEquipmentCalibration { get; set; }
    
    // Registration
    public int RegisteredMiners { get; set; }
    public decimal MonthlyThroughput { get; set; } // kg
    
    // Compliance
    public bool ZemaApproved { get; set; }
    public string? ZemaCertificateNumber { get; set; }
    public bool MiningMinistryApproved { get; set; }
    public string? MiningLicenseNumber { get; set; }
    
    public string Status { get; set; } = "Active"; // Active, Suspended, Closed
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a dispute case.
/// Addresses Gap PM-007: Arbitration and dispute management system.
/// </summary>
public class DisputeCase
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CaseNumber { get; set; } = string.Empty;
    public DateTime FiledDate { get; set; } = DateTime.Now;
    
    // Parties
    public string ClaimantId { get; set; } = string.Empty;
    public string ClaimantName { get; set; } = string.Empty;
    public string ClaimantType { get; set; } = string.Empty; // Buyer, Seller, ClearingMember, Broker
    
    public string RespondentId { get; set; } = string.Empty;
    public string RespondentName { get; set; } = string.Empty;
    public string RespondentType { get; set; } = string.Empty;
    
    // Dispute Details
    public string DisputeType { get; set; } = string.Empty; // TradeDispute, SettlementDispute, QualityDispute, PaymentDispute, DeliveryDispute
    public string DisputeCategory { get; set; } = string.Empty; // Contract, Quality, Timing, Price, Quantity
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    // Related Trade
    public string? TradeId { get; set; }
    public string? TradeNumber { get; set; }
    public string? SettlementId { get; set; }
    
    // Amount in Dispute
    public decimal? DisputedAmount { get; set; }
    public string? DisputedCurrency { get; set; } = "USD";
    public decimal? DisputedQuantity { get; set; } // metric tons
    
    // Status
    public string Status { get; set; } = "Filed"; // Filed, UnderReview, Mediation, Arbitration, Resolved, Withdrawn, Appealed
    public DateTime? StatusDate { get; set; }
    
    // Resolution Process
    public string? AssignedMediatorId { get; set; }
    public string? AssignedMediatorName { get; set; }
    public List<string>? ArbitrationPanelIds { get; set; }
    public List<string>? ArbitrationPanelNames { get; set; }
    
    // Timeline
    public DateTime? MediationStartDate { get; set; }
    public DateTime? MediationEndDate { get; set; }
    public bool MediationSuccessful { get; set; }
    
    public DateTime? ArbitrationStartDate { get; set; }
    public DateTime? ArbitrationEndDate { get; set; }
    public DateTime? HearingDate { get; set; }
    
    // Resolution
    public DateTime? ResolutionDate { get; set; }
    public string? ResolutionType { get; set; } // Mediated, Arbitrated, Withdrawn, DefaultJudgment
    public string? ResolutionSummary { get; set; }
    public decimal? AwardedAmount { get; set; }
    public string? AwardedCurrency { get; set; }
    public string? WinningParty { get; set; }
    
    // Costs
    public decimal? FilingFee { get; set; }
    public decimal? ArbitrationFee { get; set; }
    public string? FeeCurrency { get; set; } = "USD";
    public string? FeePaidBy { get; set; }
    
    // Documents
    public List<DisputeDocument> Documents { get; set; } = new();
    
    // Events/Timeline
    public List<DisputeEvent> Events { get; set; } = new();
    
    // Appeal
    public bool IsAppealed { get; set; }
    public string? AppealCaseNumber { get; set; }
    public DateTime? AppealDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a document in a dispute case.
/// </summary>
public class DisputeDocument
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DocumentType { get; set; } = string.Empty; // Claim, Response, Evidence, Contract, Award
    public string DocumentName { get; set; } = string.Empty;
    public string? DocumentReference { get; set; }
    public DateTime SubmittedDate { get; set; }
    public string SubmittedBy { get; set; } = string.Empty;
    public bool IsConfidential { get; set; }
}

/// <summary>
/// Represents an event in the dispute timeline.
/// </summary>
public class DisputeEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime EventDate { get; set; }
    public string EventType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? PerformedBy { get; set; }
}

/// <summary>
/// Represents a government mineral auction.
/// Addresses Gap TO-006: Government mineral auction platform for state-owned minerals.
/// </summary>
public class MineralAuction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string AuctionNumber { get; set; } = string.Empty;
    public string AuctionType { get; set; } = "OpenBid"; // OpenBid, SealedBid, DutchAuction, EnglishAuction
    
    // Auction Details
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string AuctionAuthority { get; set; } = string.Empty; // ZCCM-IH, Ministry of Mines, ZRA (seized minerals)
    
    // Schedule
    public DateTime AnnouncementDate { get; set; }
    public DateTime BiddingStartDate { get; set; }
    public DateTime BiddingEndDate { get; set; }
    public DateTime? ExtendedEndDate { get; set; }
    public bool IsExtended { get; set; }
    
    // Lot Details
    public List<AuctionLot> Lots { get; set; } = new();
    public int TotalLots { get; set; }
    public decimal TotalQuantity { get; set; } // metric tons
    public decimal TotalReservePrice { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Participation
    public List<AuctionParticipant> RegisteredParticipants { get; set; } = new();
    public int ParticipantCount { get; set; }
    public decimal ParticipationDeposit { get; set; }
    public string DepositCurrency { get; set; } = "USD";
    
    // Status
    public string Status { get; set; } = "Announced"; // Announced, Open, Closed, Awarded, Cancelled
    public DateTime? StatusDate { get; set; }
    
    // Results
    public decimal? TotalSoldValue { get; set; }
    public decimal? TotalSoldQuantity { get; set; }
    public int? LotsAwarded { get; set; }
    public int? LotsUnsold { get; set; }
    
    // Viewing
    public DateTime? SampleViewingStart { get; set; }
    public DateTime? SampleViewingEnd { get; set; }
    public string? ViewingLocation { get; set; }
    
    public string? TermsAndConditions { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a lot in a mineral auction.
/// </summary>
public class AuctionLot
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string LotNumber { get; set; } = string.Empty;
    
    // Mineral Details
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // metric tons
    public string QualityGrade { get; set; } = string.Empty;
    public string? AssayCertificateNumber { get; set; }
    
    // Pricing
    public decimal ReservePrice { get; set; }
    public decimal IncrementAmount { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Location
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    public string? WarrantNumber { get; set; }
    
    // Origin
    public string OriginMine { get; set; } = string.Empty;
    public string? OriginProvince { get; set; }
    public bool ConflictFreeVerified { get; set; }
    
    // Bidding
    public List<AuctionBid> Bids { get; set; } = new();
    public decimal? CurrentHighestBid { get; set; }
    public string? CurrentHighestBidderId { get; set; }
    public int BidCount { get; set; }
    
    // Result
    public string Status { get; set; } = "Available"; // Available, Bidding, Awarded, Unsold, Withdrawn
    public string? WinningBidderId { get; set; }
    public string? WinningBidderName { get; set; }
    public decimal? WinningBidAmount { get; set; }
    public DateTime? AwardedDate { get; set; }
    
    // Payment and Collection
    public bool PaymentReceived { get; set; }
    public DateTime? PaymentDate { get; set; }
    public bool Collected { get; set; }
    public DateTime? CollectionDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a bid in an auction.
/// </summary>
public class AuctionBid
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime BidTime { get; set; }
    
    public string BidderId { get; set; } = string.Empty;
    public string BidderName { get; set; } = string.Empty;
    
    public decimal BidAmount { get; set; }
    public string Currency { get; set; } = "USD";
    
    public bool IsWinningBid { get; set; }
    public bool IsValid { get; set; } = true;
    public string? InvalidationReason { get; set; }
}

/// <summary>
/// Represents a participant registered for an auction.
/// </summary>
public class AuctionParticipant
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ParticipantId { get; set; } = string.Empty;
    public string ParticipantName { get; set; } = string.Empty;
    public string ParticipantType { get; set; } = string.Empty; // Trader, Processor, Exporter
    
    public DateTime RegistrationDate { get; set; }
    public bool DepositPaid { get; set; }
    public decimal? DepositAmount { get; set; }
    public string? DepositReference { get; set; }
    
    public bool QualificationVerified { get; set; }
    public string? DisqualificationReason { get; set; }
    
    public bool IsActive { get; set; } = true;
}
