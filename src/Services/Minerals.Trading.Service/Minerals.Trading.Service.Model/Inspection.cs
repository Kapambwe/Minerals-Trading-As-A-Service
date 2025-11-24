namespace Minerals.Trading.Service.Model;

public class Inspection
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseCode { get; set; } = string.Empty;
    public DateTime InspectionDate { get; set; } = DateTime.Now;
    public string InspectorName { get; set; } = string.Empty;
    public bool SiteInspectionPassed { get; set; } = false;
    public bool WeighingSystemVerified { get; set; } = false;
    public bool QualityControlVerified { get; set; } = false;
    public bool ReportingSystemTested { get; set; } = false;
    public bool NeutralityEnsured { get; set; } = false;
    public string OverallOutcome { get; set; } = "Pending"; // e.g., Passed, Failed, Conditional Pass
    public string? Findings { get; set; }
    public string? Recommendations { get; set; }
    public DateTime? NextInspectionDate { get; set; }
}
