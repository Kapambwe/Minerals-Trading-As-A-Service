namespace MiningTradingMobileApp.Models
{
    public class Margin
    {
        public string PartyName { get; set; }
        public decimal InitialMargin { get; set; }
        public decimal VariationMargin { get; set; }
        public decimal TotalMargin { get; set; }
        public string Status { get; set; }
    }

    public class MarginRequest
    {
        public decimal RequestedAmount { get; set; }
        public DateTime RequestDate { get; set; }
        public string Status { get; set; }
    }
}