namespace Platform.Trading.Management.Models;

public class MonitoringRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseCode { get; set; } = string.Empty;
    public DateTime RecordDate { get; set; } = DateTime.Now;
    public string RecordType { get; set; } = string.Empty; // e.g., Daily Stock Report, Audit Report, Compliance Check
    public string ReportedBy { get; set; } = string.Empty;

    // Stock Report Specific
    public decimal? ReportedStock { get; set; } // in metric tons
    public decimal? ActualStock { get; set; } // for reconciliation
    public decimal? StockDiscrepancy { get; set; }
    public bool StockReportSubmitted { get; set; } = false;

    // Audit Specific
    public string? AuditorName { get; set; }
    public string? AuditOutcome { get; set; } // e.g., Compliant, Minor Issues, Non-Compliant
    public bool IndependentAuditAllowed { get; set; } = false;

    // Rules Compliance Specific
    public bool RulesOnLoadingUnloadingFollowed { get; set; } = false;
    public bool RulesOnFeesAccessFollowed { get; set; } = false;
    public string? RuleViolations { get; set; }

    public string OverallStatus { get; set; } = "Pending Review"; // e.g., Compliant, Under Review, Non-Compliant
    public string? Notes { get; set; }
}
