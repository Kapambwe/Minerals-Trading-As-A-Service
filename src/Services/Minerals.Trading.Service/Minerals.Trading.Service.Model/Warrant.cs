namespace Minerals.Trading.Service.Model;

public class Warrant
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string WarrantNumber { get; set; } = string.Empty;
    public string TradeId { get; set; } = string.Empty;
    public string TradeNumber { get; set; } = string.Empty;
    public string WarehouseId { get; set; } = string.Empty;
    public string WarehouseName { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // in metric tons
    public string CurrentOwner { get; set; } = string.Empty;
    public string PreviousOwner { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; } = DateTime.Now;
    public DateTime? TransferDate { get; set; }
    public string QualityGrade { get; set; } = string.Empty;
    public string LotNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string Status { get; set; } = "Active";
    public string? Notes { get; set; }
}
