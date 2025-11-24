namespace Minerals.Trading.Service.Model;

public class Trade
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TradeNumber { get; set; } = string.Empty;
    public DateTime TradeDate { get; set; } = DateTime.Now;
    public string BuyerName { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // in metric tons
    public decimal PricePerTon { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime DeliveryDate { get; set; }
    public TradeStatus Status { get; set; } = TradeStatus.Pending;
    public bool IsNovated { get; set; }
    public DateTime? NovationDate { get; set; }
    public string? ClearingHouse { get; set; } = "ZME Clear";
    public string? Notes { get; set; }
}
