using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Environmental;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IEnvironmentalService for development and testing.
/// </summary>
public class MockEnvironmentalService : IEnvironmentalService
{
    private readonly List<ZemaCompliance> _compliances = new();
    
    public MockEnvironmentalService()
    {
        SeedData();
    }
    
    private void SeedData()
    {
        _compliances.Add(new ZemaCompliance
        {
            CertificateNumber = "ZEMA-EC-2024-001",
            ApplicationDate = DateTime.Now.AddMonths(-6),
            IssueDate = DateTime.Now.AddMonths(-5),
            ExpiryDate = DateTime.Now.AddYears(2),
            ApplicantId = "seller-001",
            ApplicantName = "Zambia Mining Corp",
            MiningLicenseNumber = "ML-ZM-2024-0123",
            MineLocation = "Copperbelt Province, Kitwe",
            GpsCoordinates = "-12.8024, 28.2132",
            Province = "Copperbelt",
            District = "Kitwe",
            PrimaryMineral = MetalType.Copper,
            SecondaryMinerals = new List<MetalType> { MetalType.Cobalt },
            MiningMethod = "OpenPit",
            EstimatedAnnualProduction = 50000,
            MiningAreaHectares = 500,
            EiaReportReference = "EIA-2024-0123",
            EiaApprovalDate = DateTime.Now.AddMonths(-6),
            EiaApprovedBy = "ZEMA Environmental Officer",
            WaterQualityMonitoringRequired = true,
            AirQualityMonitoringRequired = true,
            WasteManagementPlanApproved = true,
            RehabilitationPlanApproved = true,
            RehabilitationBondAmount = 5000000,
            RehabilitationBondCurrency = "ZMW",
            Status = "Approved",
            ReviewedBy = "ZEMA Senior Inspector",
            ReviewDate = DateTime.Now.AddMonths(-5),
            LastInspectionDate = DateTime.Now.AddMonths(-1),
            LastInspectionResult = "Compliant",
            NextInspectionDue = DateTime.Now.AddMonths(5),
            Conditions = new List<EnvironmentalCondition>
            {
                new EnvironmentalCondition
                {
                    ConditionNumber = "C-001",
                    Description = "Quarterly water quality monitoring required",
                    Category = "Water",
                    IsCompliant = true,
                    LastVerifiedDate = DateTime.Now.AddMonths(-1)
                },
                new EnvironmentalCondition
                {
                    ConditionNumber = "C-002",
                    Description = "Dust suppression measures must be maintained",
                    Category = "Air",
                    IsCompliant = true,
                    LastVerifiedDate = DateTime.Now.AddMonths(-1)
                }
            },
            Inspections = new List<ZemaInspection>
            {
                new ZemaInspection
                {
                    InspectionReference = "INS-2024-001",
                    InspectionDate = DateTime.Now.AddMonths(-1),
                    InspectorName = "John Mwansa",
                    InspectionType = "Routine",
                    WaterQualityChecked = true,
                    AirQualityChecked = true,
                    WasteManagementChecked = true,
                    OverallResult = "Compliant",
                    ComplianceScore = 92
                }
            }
        });
        
        _compliances.Add(new ZemaCompliance
        {
            CertificateNumber = "ZEMA-EC-2024-002",
            ApplicationDate = DateTime.Now.AddMonths(-2),
            ApplicantId = "seller-002",
            ApplicantName = "Lumwana Copper Mines",
            MiningLicenseNumber = "ML-ZM-2024-0456",
            MineLocation = "North-Western Province, Solwezi",
            Province = "North-Western",
            District = "Solwezi",
            PrimaryMineral = MetalType.Copper,
            MiningMethod = "OpenPit",
            EstimatedAnnualProduction = 80000,
            MiningAreaHectares = 800,
            Status = "UnderReview",
            WaterQualityMonitoringRequired = true,
            AirQualityMonitoringRequired = true
        });
    }

    public Task<IEnumerable<ZemaCompliance>> GetAllZemaCompliancesAsync()
        => Task.FromResult<IEnumerable<ZemaCompliance>>(_compliances);

    public Task<ZemaCompliance?> GetZemaComplianceByIdAsync(string id)
        => Task.FromResult(_compliances.FirstOrDefault(c => c.Id == id));

    public Task<ZemaCompliance?> GetZemaComplianceByApplicantAsync(string applicantId)
        => Task.FromResult(_compliances.FirstOrDefault(c => c.ApplicantId == applicantId));

    public Task<IEnumerable<ZemaCompliance>> GetZemaCompliancesByStatusAsync(string status)
        => Task.FromResult<IEnumerable<ZemaCompliance>>(_compliances.Where(c => c.Status == status));

    public Task<ZemaCompliance> CreateZemaComplianceAsync(ZemaCompliance compliance)
    {
        compliance.Id = Guid.NewGuid().ToString();
        compliance.CertificateNumber = $"ZEMA-EC-{DateTime.Now:yyyy}-{_compliances.Count + 1:D3}";
        compliance.ApplicationDate = DateTime.Now;
        compliance.Status = "Pending";
        _compliances.Add(compliance);
        return Task.FromResult(compliance);
    }

    public Task<ZemaCompliance> UpdateZemaComplianceAsync(ZemaCompliance compliance)
    {
        var existing = _compliances.FirstOrDefault(c => c.Id == compliance.Id);
        if (existing != null)
        {
            _compliances.Remove(existing);
            _compliances.Add(compliance);
        }
        return Task.FromResult(compliance);
    }

    public Task<ZemaCompliance> ApproveZemaComplianceAsync(string id, string approvedBy)
    {
        var compliance = _compliances.FirstOrDefault(c => c.Id == id);
        if (compliance != null)
        {
            compliance.Status = "Approved";
            compliance.ReviewedBy = approvedBy;
            compliance.ReviewDate = DateTime.Now;
            compliance.IssueDate = DateTime.Now;
            compliance.ExpiryDate = DateTime.Now.AddYears(2);
        }
        return Task.FromResult(compliance!);
    }

    public Task<ZemaCompliance> RejectZemaComplianceAsync(string id, string rejectedBy, string reason)
    {
        var compliance = _compliances.FirstOrDefault(c => c.Id == id);
        if (compliance != null)
        {
            compliance.Status = "Rejected";
            compliance.ReviewedBy = rejectedBy;
            compliance.ReviewDate = DateTime.Now;
            compliance.RejectionReason = reason;
        }
        return Task.FromResult(compliance!);
    }

    public Task<ZemaCompliance> SuspendZemaComplianceAsync(string id, string reason)
    {
        var compliance = _compliances.FirstOrDefault(c => c.Id == id);
        if (compliance != null)
        {
            compliance.Status = "Suspended";
            compliance.RejectionReason = reason;
        }
        return Task.FromResult(compliance!);
    }

    public Task<ZemaInspection> RecordInspectionAsync(string complianceId, ZemaInspection inspection)
    {
        var compliance = _compliances.FirstOrDefault(c => c.Id == complianceId);
        if (compliance != null)
        {
            inspection.Id = Guid.NewGuid().ToString();
            inspection.InspectionReference = $"INS-{DateTime.Now:yyyy}-{compliance.Inspections.Count + 1:D3}";
            compliance.Inspections.Add(inspection);
            compliance.LastInspectionDate = inspection.InspectionDate;
            compliance.LastInspectionResult = inspection.OverallResult;
            compliance.NextInspectionDue = inspection.InspectionDate.AddMonths(6);
        }
        return Task.FromResult(inspection);
    }

    public Task<IEnumerable<ZemaInspection>> GetInspectionsByComplianceAsync(string complianceId)
    {
        var compliance = _compliances.FirstOrDefault(c => c.Id == complianceId);
        return Task.FromResult<IEnumerable<ZemaInspection>>(compliance?.Inspections ?? new List<ZemaInspection>());
    }

    public Task<IEnumerable<ZemaCompliance>> GetCompliancesDueForInspectionAsync(DateTime asOfDate)
        => Task.FromResult<IEnumerable<ZemaCompliance>>(
            _compliances.Where(c => c.NextInspectionDue.HasValue && c.NextInspectionDue.Value <= asOfDate));

    public Task<EnvironmentalViolation> RecordViolationAsync(string complianceId, EnvironmentalViolation violation)
    {
        var compliance = _compliances.FirstOrDefault(c => c.Id == complianceId);
        if (compliance != null)
        {
            violation.Id = Guid.NewGuid().ToString();
            violation.ViolationNumber = $"VIO-{DateTime.Now:yyyy}-{compliance.Violations.Count + 1:D3}";
            compliance.Violations.Add(violation);
        }
        return Task.FromResult(violation);
    }

    public Task<IEnumerable<EnvironmentalViolation>> GetActiveViolationsAsync()
    {
        var violations = _compliances
            .SelectMany(c => c.Violations)
            .Where(v => v.Status == "Open" || v.Status == "UnderRemediation");
        return Task.FromResult(violations);
    }

    public Task<EnvironmentalViolation> UpdateViolationStatusAsync(string violationId, string status, string? notes = null)
    {
        var violation = _compliances
            .SelectMany(c => c.Violations)
            .FirstOrDefault(v => v.Id == violationId);
        if (violation != null)
        {
            violation.Status = status;
            if (notes != null) violation.Notes = notes;
        }
        return Task.FromResult(violation!);
    }

    public Task<EnvironmentalViolation> RecordViolationRemediationAsync(string violationId, DateTime completedDate, string verifiedBy)
    {
        var violation = _compliances
            .SelectMany(c => c.Violations)
            .FirstOrDefault(v => v.Id == violationId);
        if (violation != null)
        {
            violation.Status = "Closed";
            violation.RemediationCompletedDate = completedDate;
            violation.VerifiedBy = verifiedBy;
        }
        return Task.FromResult(violation!);
    }

    public Task<bool> VerifyEnvironmentalComplianceAsync(string applicantId)
    {
        var compliance = _compliances.FirstOrDefault(c => 
            c.ApplicantId == applicantId && 
            c.Status == "Approved" && 
            c.ExpiryDate > DateTime.Now &&
            !c.HasActiveViolations);
        return Task.FromResult(compliance != null);
    }

    public Task<bool> ValidateExportEnvironmentalRequirementsAsync(string exportPermitId)
    {
        // In production, would validate against export permit and linked compliance
        return Task.FromResult(true);
    }
}
