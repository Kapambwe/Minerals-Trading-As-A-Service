using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Models.Regulatory;

/// <summary>
/// Represents mining license verification against Zambia Mining Cadastre.
/// Addresses Gap RC-004: Automated verification against Zambia Mining Cadastre.
/// </summary>
public class MiningLicenseVerification
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime VerificationDate { get; set; } = DateTime.UtcNow;
    
    // License Information
    public string LicenseNumber { get; set; } = string.Empty;
    public string LicenseType { get; set; } = string.Empty; // LargeMining, SmallScale, Exploration, Artisanal, Processing
    public string LicenseHolderName { get; set; } = string.Empty;
    public string LicenseHolderId { get; set; } = string.Empty;
    
    // License Details from Cadastre
    public DateTime? LicenseIssueDate { get; set; }
    public DateTime? LicenseExpiryDate { get; set; }
    public string LicenseStatus { get; set; } = string.Empty; // Valid, Expired, Suspended, Revoked, Pending
    public decimal? AreaSize { get; set; } // in hectares
    public string? AreaLocation { get; set; }
    public string? Province { get; set; }
    public string? District { get; set; }
    
    // Minerals Covered
    public List<MetalType> AuthorizedMinerals { get; set; } = new();
    public string? MineralRightsDetails { get; set; }
    
    // Verification Result
    public string VerificationStatus { get; set; } = "Pending"; // Pending, Verified, Failed, Expired, NotFound
    public bool IsVerified { get; set; }
    public string? VerificationNotes { get; set; }
    public string? FailureReason { get; set; }
    
    // Cadastre Reference
    public string? CadastreReference { get; set; }
    public DateTime? CadastreLastUpdate { get; set; }
    public string? CadastreDataSource { get; set; }
    
    // Compliance
    public bool EnvironmentalClearanceValid { get; set; }
    public bool TaxClearanceValid { get; set; }
    public bool SafetyCertificateValid { get; set; }
    public List<LicenseCondition> Conditions { get; set; } = new();
    
    // Automated Verification
    public bool AutomatedVerification { get; set; } = true;
    public DateTime? NextVerificationDate { get; set; }
    public int VerificationFrequencyDays { get; set; } = 30;
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a condition attached to a mining license.
/// </summary>
public class LicenseCondition
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ConditionType { get; set; } = string.Empty; // Environmental, Safety, Financial, Reporting
    public string Description { get; set; } = string.Empty;
    public bool IsMet { get; set; }
    public DateTime? ComplianceDate { get; set; }
    public string? ComplianceNotes { get; set; }
}

/// <summary>
/// Represents ZCCM-IH (Zambia Consolidated Copper Mines Investment Holdings) integration.
/// Addresses Gap RC-005: Interface with national mining investment authority.
/// </summary>
public class ZccmIntegration
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime IntegrationDate { get; set; } = DateTime.UtcNow;
    
    // ZCCM-IH Entity Reference
    public string ZccmEntityId { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty; // Mine, ProcessingPlant, JointVenture, Subsidiary
    public string EntityName { get; set; } = string.Empty;
    
    // Ownership Details
    public decimal ZccmOwnershipPercentage { get; set; }
    public string? OperatingPartner { get; set; }
    public decimal? OperatingPartnerPercentage { get; set; }
    
    // Production Data
    public List<ZccmProductionRecord> ProductionRecords { get; set; } = new();
    public decimal? CurrentMonthProduction { get; set; } // in metric tons
    public decimal? YearToDateProduction { get; set; }
    
    // Financial Data
    public decimal? RoyaltiesPayable { get; set; }
    public decimal? DividendsPayable { get; set; }
    public string Currency { get; set; } = "USD";
    
    // Compliance Status
    public string ComplianceStatus { get; set; } = "Compliant"; // Compliant, NonCompliant, UnderReview
    public DateTime? LastComplianceCheck { get; set; }
    public List<string> ComplianceIssues { get; set; } = new();
    
    // Reporting
    public bool QuarterlyReportSubmitted { get; set; }
    public DateTime? LastReportDate { get; set; }
    public string? ReportReference { get; set; }
    
    // Integration Status
    public string IntegrationStatus { get; set; } = "Active"; // Active, Pending, Suspended, Terminated
    public DateTime? LastSyncDate { get; set; }
    public string? SyncStatus { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents production record from ZCCM-IH integrated mine.
/// </summary>
public class ZccmProductionRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime RecordDate { get; set; }
    public string Period { get; set; } = string.Empty; // Monthly, Quarterly
    
    public MetalType MetalType { get; set; }
    public decimal ProductionQuantity { get; set; } // in metric tons
    public string QualityGrade { get; set; } = string.Empty;
    
    public decimal? SalesQuantity { get; set; }
    public decimal? SalesValue { get; set; }
    public string? Currency { get; set; } = "USD";
    
    public decimal? RoyaltyAmount { get; set; }
    public bool RoyaltyPaid { get; set; }
}
