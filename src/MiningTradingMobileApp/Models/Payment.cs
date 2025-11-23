namespace MiningTradingMobileApp.Models
{
    public class Payment
    {
        public string TradeId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}