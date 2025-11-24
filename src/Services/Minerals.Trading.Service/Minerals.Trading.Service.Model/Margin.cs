namespace Minerals.Trading.Service.Model;

public class Margin
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TradeId { get; set; } = string.Empty;
    public string TradeNumber { get; set; } = string.Empty;
    public decimal InitialMargin { get; set; }
    public decimal VariationMargin { get; set; }
    public decimal TotalMargin { get; set; }
    public DateTime MarginDate { get; set; } = DateTime.Now;
    public decimal CurrentMarketPrice { get; set; }
    public decimal PriceChange { get; set; }
    public string PartyName { get; set; } = string.Empty;
    public bool IsPayable { get; set; } // true if party needs to pay, false if receiving
    public string Status { get; set; } = "Pending";
    public string? Notes { get; set; }
}
