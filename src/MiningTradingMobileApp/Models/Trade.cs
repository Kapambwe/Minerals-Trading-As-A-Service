namespace MiningTradingMobileApp.Models
{
    public class Trade
    {
        public string TradeNumber { get; set; }
        public string MetalType { get; set; }
        public string BuyerName { get; set; }
        public string SellerName { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerTon { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Status { get; set; }
        public bool IsNovated { get; set; }
        public DateTime? NovationDate { get; set; }
        public string ClearingHouse { get; set; }
        public string Notes { get; set; }
    }
}