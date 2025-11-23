namespace Platform.Trading.Management.Models
{
    public class MarginRequest
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string TradeId { get; set; } = string.Empty;
        public decimal RequestedAmount { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
