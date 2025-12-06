using Platform.Trading.Management.Models.Environmental;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for ZEMA environmental compliance operations.
/// Addresses Gap RC-003: Environmental compliance tracking for mining operations.
/// </summary>
public interface IEnvironmentalService
{
    // ZEMA Compliance
    Task<IEnumerable<ZemaCompliance>> GetAllZemaCompliancesAsync();
    Task<ZemaCompliance?> GetZemaComplianceByIdAsync(string id);
    Task<ZemaCompliance?> GetZemaComplianceByApplicantAsync(string applicantId);
    Task<IEnumerable<ZemaCompliance>> GetZemaCompliancesByStatusAsync(string status);
    Task<ZemaCompliance> CreateZemaComplianceAsync(ZemaCompliance compliance);
    Task<ZemaCompliance> UpdateZemaComplianceAsync(ZemaCompliance compliance);
    Task<ZemaCompliance> ApproveZemaComplianceAsync(string id, string approvedBy);
    Task<ZemaCompliance> RejectZemaComplianceAsync(string id, string rejectedBy, string reason);
    Task<ZemaCompliance> SuspendZemaComplianceAsync(string id, string reason);
    
    // ZEMA Inspections
    Task<ZemaInspection> RecordInspectionAsync(string complianceId, ZemaInspection inspection);
    Task<IEnumerable<ZemaInspection>> GetInspectionsByComplianceAsync(string complianceId);
    Task<IEnumerable<ZemaCompliance>> GetCompliancesDueForInspectionAsync(DateTime asOfDate);
    
    // Environmental Violations
    Task<EnvironmentalViolation> RecordViolationAsync(string complianceId, EnvironmentalViolation violation);
    Task<IEnumerable<EnvironmentalViolation>> GetActiveViolationsAsync();
    Task<EnvironmentalViolation> UpdateViolationStatusAsync(string violationId, string status, string? notes = null);
    Task<EnvironmentalViolation> RecordViolationRemediationAsync(string violationId, DateTime completedDate, string verifiedBy);
    
    // Verification
    Task<bool> VerifyEnvironmentalComplianceAsync(string applicantId);
    Task<bool> ValidateExportEnvironmentalRequirementsAsync(string exportPermitId);
}
