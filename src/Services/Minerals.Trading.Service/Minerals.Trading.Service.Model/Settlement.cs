namespace Minerals.Trading.Service.Model;

public class Settlement
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SettlementNumber { get; set; } = string.Empty;
    public string TradeId { get; set; } = string.Empty;
    public string TradeNumber { get; set; } = string.Empty;
    public SettlementType SettlementType { get; set; }
    public DateTime SettlementDate { get; set; } = DateTime.Now;
    public decimal SettlementAmount { get; set; }
    public string BuyerName { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public MetalType? MetalType { get; set; }
    public decimal? Quantity { get; set; }
    public string? WarrantNumber { get; set; }
    public string? WarehouseLocation { get; set; }
    public decimal FinalPrice { get; set; }
    public decimal PriceDifference { get; set; }
    public string Status { get; set; } = "Pending";
    public bool IsCompleted { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string? Notes { get; set; }
}
