namespace Platform.Trading.Management.Models;

public class IncidentReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string IncidentNumber { get; set; } = string.Empty;
    public DateTime IncidentDate { get; set; } = DateTime.Now;
    public string ReportedBy { get; set; } = string.Empty;
    public string RelatedEntityId { get; set; } = string.Empty; // e.g., WarehouseId, TradeId
    public string RelatedEntityName { get; set; } = string.Empty; // e.g., WarehouseCode, TradeNumber
    public string IncidentType { get; set; } = string.Empty; // e.g., Theft, Damage, Compliance Breach, System Error
    public string Description { get; set; } = string.Empty;
    public string Severity { get; set; } = "Medium"; // e.g., Low, Medium, High, Critical
    public string Status { get; set; } = "Reported"; // e.g., Reported, Investigating, Resolved, Closed
    public string? ActionsTaken { get; set; }
    public DateTime? ResolutionDate { get; set; }
    public string? Notes { get; set; }
}
