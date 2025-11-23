namespace MiningTradingMobileApp.Models
{
    public class Settlement
    {
        public string Id { get; set; }
        public DateTime SettlementDate { get; set; }
        public string TradeId { get; set; }
        public decimal SettlementAmount { get; set; }
        public string SettlementType { get; set; }
        public string Status { get; set; }
    }
}