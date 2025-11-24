namespace Minerals.Trading.Service.Model;

public class Warehouse
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string WarehouseCode { get; set; } = string.Empty;
    public string OperatorName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public decimal StorageCapacity { get; set; } // in metric tons
    public decimal CurrentStock { get; set; } // in metric tons
    public decimal AvailableCapacity => StorageCapacity - CurrentStock;
    public bool IsLMEApproved { get; set; }
    public DateTime ApprovalDate { get; set; }
    public string SecurityLevel { get; set; } = string.Empty;
    public bool HasWeighingSystem { get; set; }
    public bool HasQualityControl { get; set; }
    public bool HasFinancialStabilityProof { get; set; } = false;
    public string HandlingEquipmentDetails { get; set; } = string.Empty;
    public string ComplianceNotes { get; set; } = string.Empty;
    public bool AgreesToLMERules { get; set; } = false;
    public string Status { get; set; } = "Active";
    public DateTime LastInspectionDate { get; set; }
    public string? Notes { get; set; }
}
