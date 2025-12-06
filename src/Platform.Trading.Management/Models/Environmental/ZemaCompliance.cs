namespace Platform.Trading.Management.Models.Environmental;

/// <summary>
/// Represents ZEMA (Zambia Environmental Management Agency) compliance record.
/// Addresses Gap RC-003: Environmental compliance tracking for mining operations.
/// </summary>
public class ZemaCompliance
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CertificateNumber { get; set; } = string.Empty;
    public DateTime ApplicationDate { get; set; } = DateTime.Now;
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    
    // Applicant Information
    public string ApplicantId { get; set; } = string.Empty;
    public string ApplicantName { get; set; } = string.Empty;
    public string MiningLicenseNumber { get; set; } = string.Empty;
    public string MineLocation { get; set; } = string.Empty;
    public string? GpsCoordinates { get; set; }
    public string Province { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    
    // Mining Details
    public MetalType PrimaryMineral { get; set; }
    public List<MetalType> SecondaryMinerals { get; set; } = new();
    public string MiningMethod { get; set; } = string.Empty; // OpenPit, Underground, Alluvial
    public decimal EstimatedAnnualProduction { get; set; } // metric tons
    public decimal MiningAreaHectares { get; set; }
    
    // Environmental Impact Assessment
    public string? EiaReportReference { get; set; }
    public DateTime? EiaApprovalDate { get; set; }
    public string? EiaApprovedBy { get; set; }
    public List<EnvironmentalCondition> Conditions { get; set; } = new();
    
    // Environmental Monitoring
    public bool WaterQualityMonitoringRequired { get; set; }
    public bool AirQualityMonitoringRequired { get; set; }
    public bool WasteManagementPlanApproved { get; set; }
    public bool RehabilitationPlanApproved { get; set; }
    public decimal? RehabilitationBondAmount { get; set; }
    public string? RehabilitationBondCurrency { get; set; } = "ZMW";
    
    // Compliance Status
    public string Status { get; set; } = "Pending"; // Pending, UnderReview, Approved, Rejected, Expired, Suspended
    public string? ReviewedBy { get; set; }
    public DateTime? ReviewDate { get; set; }
    public string? RejectionReason { get; set; }
    
    // Inspections
    public DateTime? LastInspectionDate { get; set; }
    public string? LastInspectionResult { get; set; }
    public DateTime? NextInspectionDue { get; set; }
    public List<ZemaInspection> Inspections { get; set; } = new();
    
    // Violations
    public List<EnvironmentalViolation> Violations { get; set; } = new();
    public bool HasActiveViolations => Violations.Any(v => v.Status == "Open" || v.Status == "UnderRemediation");
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a condition attached to a ZEMA environmental certificate.
/// </summary>
public class EnvironmentalCondition
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ConditionNumber { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // Water, Air, Waste, Biodiversity, Community
    public DateTime? ComplianceDeadline { get; set; }
    public bool IsCompliant { get; set; }
    public DateTime? LastVerifiedDate { get; set; }
    public string? VerifiedBy { get; set; }
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a ZEMA site inspection.
/// </summary>
public class ZemaInspection
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string InspectionReference { get; set; } = string.Empty;
    public DateTime InspectionDate { get; set; }
    public string InspectorName { get; set; } = string.Empty;
    public string InspectionType { get; set; } = string.Empty; // Routine, FollowUp, Complaint, Incident
    
    // Inspection Areas
    public bool WaterQualityChecked { get; set; }
    public bool AirQualityChecked { get; set; }
    public bool WasteManagementChecked { get; set; }
    public bool TailingsManagementChecked { get; set; }
    public bool CommunityImpactAssessed { get; set; }
    
    // Results
    public string OverallResult { get; set; } = string.Empty; // Compliant, NonCompliant, PartiallyCompliant
    public int ComplianceScore { get; set; } // 0-100
    public List<string> FindingsSummary { get; set; } = new();
    public List<string> CorrectiveActionsRequired { get; set; } = new();
    public DateTime? CorrectiveActionDeadline { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents an environmental violation.
/// </summary>
public class EnvironmentalViolation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ViolationNumber { get; set; } = string.Empty;
    public DateTime DetectedDate { get; set; }
    public string Category { get; set; } = string.Empty; // WaterPollution, AirPollution, IllegalWasteDisposal, Deforestation, Other
    public string Severity { get; set; } = string.Empty; // Minor, Moderate, Major, Critical
    public string Description { get; set; } = string.Empty;
    
    // Enforcement
    public string Status { get; set; } = "Open"; // Open, UnderRemediation, Closed, Referred
    public decimal? FineAmount { get; set; }
    public string? FineCurrency { get; set; } = "ZMW";
    public bool FinePaid { get; set; }
    public DateTime? FinePaymentDate { get; set; }
    
    // Remediation
    public string? RemediationPlan { get; set; }
    public DateTime? RemediationDeadline { get; set; }
    public DateTime? RemediationCompletedDate { get; set; }
    public string? VerifiedBy { get; set; }
    
    public string? Notes { get; set; }
}
