namespace MiningTradingMobileApp.Models
{
    public class MineralListing
    {
        public string Id { get; set; }
        public string MetalType { get; set; }
        public double Quantity { get; set; }
        public decimal PricePerTon { get; set; }
        public string Status { get; set; }
    }
}