using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Models.WarehouseManagement;

/// <summary>
/// Represents an Electronic Warehouse Receipt (EWR) with legal backing.
/// Addresses Gap WD-001: Electronic Warehouse Receipt system with legal backing.
/// </summary>
public class ElectronicWarehouseReceipt
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ReceiptNumber { get; set; } = string.Empty;
    public string ReceiptType { get; set; } = "Negotiable"; // Negotiable, NonNegotiable
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiryDate { get; set; }
    
    // Warehouse Information
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    public string WarehouseAddress { get; set; } = string.Empty;
    public string WarehouseLicenseNumber { get; set; } = string.Empty;
    
    // Commodity Details
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // in metric tons
    public string QuantityUnit { get; set; } = "MetricTons";
    public string QualityGrade { get; set; } = string.Empty;
    public string? LotNumber { get; set; }
    
    // Associated Certificates
    public string? AssayCertificateNumber { get; set; }
    public string? WeightCertificateNumber { get; set; }
    public string? OriginCertificateNumber { get; set; }
    
    // Ownership
    public string CurrentOwnerId { get; set; } = string.Empty;
    public string CurrentOwnerName { get; set; } = string.Empty;
    public DateTime OwnershipDate { get; set; }
    public List<ReceiptOwnershipTransfer> OwnershipHistory { get; set; } = new();
    
    // Legal Status
    public string LegalStatus { get; set; } = "Valid"; // Valid, Cancelled, Suspended, Redeemed
    public bool IsNegotiable { get; set; } = true;
    public bool IsEncumbered { get; set; }
    public string? EncumbranceType { get; set; } // Pledge, Lien, SecurityInterest
    public string? EncumbranceHolder { get; set; }
    
    // Regulatory Compliance
    public bool RegisteredWithRegulator { get; set; }
    public string? RegulatoryReference { get; set; }
    public DateTime? RegistrationDate { get; set; }
    public string? GovernmentBackingReference { get; set; }
    
    // Digital Signature
    public string? DigitalSignature { get; set; }
    public string? SignedBy { get; set; }
    public DateTime? SignatureDate { get; set; }
    public bool SignatureVerified { get; set; }
    
    // Storage
    public string StorageLocation { get; set; } = string.Empty;
    public decimal? StorageRate { get; set; } // per metric ton per day
    public string? StorageCurrency { get; set; } = "USD";
    public decimal? AccruedStorageFees { get; set; }
    
    // Insurance
    public string? InsurancePolicyId { get; set; }
    public bool InsuranceCurrent { get; set; }
    
    // Redemption
    public bool IsRedeemed { get; set; }
    public DateTime? RedemptionDate { get; set; }
    public string? RedemptionReference { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents an ownership transfer of a warehouse receipt.
/// </summary>
public class ReceiptOwnershipTransfer
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime TransferDate { get; set; }
    
    public string FromOwnerId { get; set; } = string.Empty;
    public string FromOwnerName { get; set; } = string.Empty;
    public string ToOwnerId { get; set; } = string.Empty;
    public string ToOwnerName { get; set; } = string.Empty;
    
    public string TransferType { get; set; } = string.Empty; // Sale, Pledge, Release, Gift
    public string? TradeReference { get; set; }
    public string? TransferReference { get; set; }
    
    public bool VerifiedByWarehouse { get; set; }
    public string? VerifiedBy { get; set; }
}

/// <summary>
/// Represents warehouse insurance integration.
/// Addresses Gap WD-007: Automated insurance for stored metals.
/// </summary>
public class WarehouseInsurance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PolicyNumber { get; set; } = string.Empty;
    public string PolicyType { get; set; } = "AllRisk"; // AllRisk, NamedPerils, MarineInland
    
    // Coverage
    public string InsurerId { get; set; } = string.Empty;
    public string InsurerName { get; set; } = string.Empty;
    public string? BrokerId { get; set; }
    public string? BrokerName { get; set; }
    
    // Policy Period
    public DateTime PolicyStartDate { get; set; }
    public DateTime PolicyEndDate { get; set; }
    public bool IsActive => DateTime.UtcNow >= PolicyStartDate && DateTime.UtcNow <= PolicyEndDate;
    public int DaysToExpiry => (PolicyEndDate - DateTime.UtcNow).Days;
    
    // Warehouse Coverage
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    public List<string>? CoveredWarehouses { get; set; }
    
    // Coverage Amounts
    public decimal MaximumCoverage { get; set; }
    public decimal CurrentCoveredValue { get; set; }
    public string Currency { get; set; } = "USD";
    public decimal? PerOccurrenceLimit { get; set; }
    public decimal? AggregateLimit { get; set; }
    public decimal Deductible { get; set; }
    
    // Covered Perils
    public List<CoveredPeril> CoveredPerils { get; set; } = new();
    public List<string> Exclusions { get; set; } = new();
    
    // Premium
    public decimal AnnualPremium { get; set; }
    public decimal PremiumRate { get; set; } // Per $100 of value
    public string PaymentFrequency { get; set; } = "Annual"; // Monthly, Quarterly, Annual
    public bool PremiumPaid { get; set; }
    public DateTime? LastPremiumPaymentDate { get; set; }
    
    // Claims
    public List<InsuranceClaim> Claims { get; set; } = new();
    public int TotalClaimsCount { get; set; }
    public decimal TotalClaimsValue { get; set; }
    
    // Certificate of Insurance
    public string? CertificateNumber { get; set; }
    public DateTime? CertificateIssueDate { get; set; }
    public string? CertificateHolderName { get; set; }
    
    // Status
    public string Status { get; set; } = "Active"; // Active, Expired, Cancelled, Suspended
    public DateTime? CancellationDate { get; set; }
    public string? CancellationReason { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a covered peril in the insurance policy.
/// </summary>
public class CoveredPeril
{
    public string PerilCode { get; set; } = string.Empty;
    public string PerilName { get; set; } = string.Empty; // Fire, Theft, Flood, Earthquake, Malicious Damage
    public bool IsCovered { get; set; } = true;
    public decimal? SubLimit { get; set; }
    public decimal? SpecificDeductible { get; set; }
}

/// <summary>
/// Represents an insurance claim.
/// </summary>
public class InsuranceClaim
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ClaimNumber { get; set; } = string.Empty;
    public DateTime IncidentDate { get; set; }
    public DateTime ClaimDate { get; set; }
    
    // Incident Details
    public string IncidentType { get; set; } = string.Empty; // Fire, Theft, Damage, Loss
    public string Description { get; set; } = string.Empty;
    public string AffectedWarehouseId { get; set; } = string.Empty;
    
    // Affected Items
    public List<string> AffectedWarrantIds { get; set; } = new();
    public MetalType? MetalType { get; set; }
    public decimal? AffectedQuantity { get; set; }
    
    // Claim Amount
    public decimal ClaimedAmount { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public decimal? PaidAmount { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Processing
    public string Status { get; set; } = "Filed"; // Filed, UnderReview, Approved, Denied, Paid, Closed
    public string? AdjusterName { get; set; }
    public DateTime? SurveyDate { get; set; }
    public string? SurveyReport { get; set; }
    
    // Resolution
    public DateTime? ResolutionDate { get; set; }
    public string? ResolutionNotes { get; set; }
    public DateTime? PaymentDate { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents load-out queue for warehouse metal withdrawal.
/// Addresses Gap WD-006: Queue management for metal withdrawal.
/// </summary>
public class LoadOutQueue
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string QueueNumber { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
    
    // Warehouse
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    
    // Requestor
    public string RequestorId { get; set; } = string.Empty;
    public string RequestorName { get; set; } = string.Empty;
    public string? AuthorizedRepresentative { get; set; }
    
    // Metal Details
    public string WarrantId { get; set; } = string.Empty;
    public string WarrantNumber { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; }
    public string QualityGrade { get; set; } = string.Empty;
    
    // Queue Position
    public int QueuePosition { get; set; }
    public DateTime? EstimatedLoadOutDate { get; set; }
    public int EstimatedWaitDays { get; set; }
    
    // Scheduling
    public DateTime? ScheduledDate { get; set; }
    public string? ScheduledTimeSlot { get; set; }
    public DateTime? ActualLoadOutDate { get; set; }
    
    // Transportation
    public string TransportMode { get; set; } = string.Empty; // Truck, Rail, Container
    public string? TransportCompany { get; set; }
    public string? VehicleRegistration { get; set; }
    public string? DriverName { get; set; }
    public string? DriverId { get; set; }
    
    // Documentation
    public bool DeliveryOrderReceived { get; set; }
    public string? DeliveryOrderNumber { get; set; }
    public bool PaymentCleared { get; set; }
    public bool StorageFeesCleared { get; set; }
    
    // Status
    public string Status { get; set; } = "Queued"; // Queued, Scheduled, InProgress, Completed, Cancelled
    public string? CancellationReason { get; set; }
    
    // Completion
    public decimal? ActualLoadedQuantity { get; set; }
    public string? LoadOutReference { get; set; }
    public string? GatePassNumber { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents warehouse storage fee management.
/// Addresses Gap WD-010: Automated rent calculation and billing.
/// </summary>
public class StorageFee
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string InvoiceNumber { get; set; } = string.Empty;
    public string BillingPeriod { get; set; } = string.Empty; // e.g., "2024-01"
    
    // Warehouse
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    
    // Warrant Holder
    public string WarrantHolderId { get; set; } = string.Empty;
    public string WarrantHolderName { get; set; } = string.Empty;
    
    // Storage Details
    public List<StorageFeeDetail> Details { get; set; } = new();
    
    // Fee Calculation
    public decimal TotalQuantity { get; set; } // metric tons
    public int TotalDays { get; set; }
    public decimal RatePerTonPerDay { get; set; }
    public decimal SubTotal { get; set; }
    
    // Additional Charges
    public decimal HandlingFee { get; set; }
    public decimal InsuranceFee { get; set; }
    public decimal OtherCharges { get; set; }
    
    // Totals
    public decimal GrossAmount { get; set; }
    public decimal? DiscountAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal NetAmount { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Billing
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public string PaymentStatus { get; set; } = "Unpaid"; // Unpaid, PartiallyPaid, Paid, Overdue
    public decimal? AmountPaid { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentReference { get; set; }
    
    // Late Payment
    public bool IsOverdue { get; set; }
    public int? DaysOverdue { get; set; }
    public decimal? LateFee { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents storage fee detail per warrant.
/// </summary>
public class StorageFeeDetail
{
    public string WarrantId { get; set; } = string.Empty;
    public string WarrantNumber { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    
    public decimal Quantity { get; set; }
    public DateTime StorageStartDate { get; set; }
    public DateTime StorageEndDate { get; set; }
    public int StorageDays { get; set; }
    
    public decimal DailyRate { get; set; }
    public decimal StorageCharge { get; set; }
}
