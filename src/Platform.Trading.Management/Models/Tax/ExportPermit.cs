namespace Platform.Trading.Management.Models.Tax;

/// <summary>
/// Represents an export permit for mineral exports.
/// Addresses Gap RC-010: Export Permit Tracking.
/// </summary>
public class ExportPermit
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PermitNumber { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; } = DateTime.Now;
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    // Applicant Details
    public string ApplicantId { get; set; } = string.Empty; // Seller ID
    public string ApplicantName { get; set; } = string.Empty;
    public string ApplicantMiningLicense { get; set; } = string.Empty;
    
    // Export Details
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // in metric tons
    public string QualityGrade { get; set; } = string.Empty;
    public string DestinationCountry { get; set; } = string.Empty;
    public string DestinationPort { get; set; } = string.Empty;
    public string BuyerName { get; set; } = string.Empty;
    
    // Value
    public decimal DeclaredValue { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Compliance Checks
    public bool TaxClearanceVerified { get; set; }
    public string? TaxClearanceNumber { get; set; }
    public bool EnvironmentalClearanceVerified { get; set; }
    public string? ZemaCertificateNumber { get; set; }
    public bool MiningLicenseVerified { get; set; }
    
    // Processing
    public string Status { get; set; } = "Pending"; // Pending, UnderReview, Approved, Rejected, Expired
    public string? ReviewedBy { get; set; }
    public DateTime? ReviewDate { get; set; }
    public string? ApprovalAuthority { get; set; }
    public string? RejectionReason { get; set; }
    
    // Related Documents
    public List<string> AssayCertificateIds { get; set; } = new();
    public List<string> TradeIds { get; set; } = new();
    public string? CustomsDeclarationNumber { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a regulatory filing to Bank of Zambia.
/// Addresses Gap RC-006: Bank of Zambia Reporting.
/// </summary>
public class BozTransaction
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ReportingReference { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; }
    public DateTime ReportingDate { get; set; } = DateTime.Now;
    
    // Transaction Details
    public string TransactionType { get; set; } = string.Empty; // Export, Import, Transfer
    public string TradeId { get; set; } = string.Empty;
    public string TradeNumber { get; set; } = string.Empty;
    
    // Forex Details
    public decimal TransactionAmount { get; set; }
    public string TransactionCurrency { get; set; } = "USD";
    public decimal? ExchangeRate { get; set; }
    public decimal? ZmwEquivalent { get; set; }
    
    // Parties
    public string LocalPartyId { get; set; } = string.Empty;
    public string LocalPartyName { get; set; } = string.Empty;
    public string ForeignPartyName { get; set; } = string.Empty;
    public string ForeignPartyCountry { get; set; } = string.Empty;
    public string? ForeignBankName { get; set; }
    public string? ForeignBankSwiftCode { get; set; }
    
    // Mineral Details
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; }
    
    // Reporting Status
    public string Status { get; set; } = "Pending"; // Pending, Submitted, Acknowledged, Rejected
    public DateTime? SubmissionDate { get; set; }
    public string? BozAcknowledgementNumber { get; set; }
    public DateTime? AcknowledgementDate { get; set; }
    public string? RejectionReason { get; set; }
    
    // Large Transaction Reporting
    public bool IsLargeTransaction { get; set; } // Above threshold requiring additional reporting
    public decimal? ThresholdAmount { get; set; }
    
    public string? Notes { get; set; }
}
