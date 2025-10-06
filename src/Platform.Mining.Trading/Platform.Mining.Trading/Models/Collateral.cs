namespace Platform.Mining.Trading.Models
{
    public class Collateral
    {
        public string CollateralId { get; set; } = string.Empty;
        public string CollateralType { get; set; } = string.Empty; // Cash, Securities, Commodities
        public string Asset { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal MarketValue { get; set; }
        public decimal HaircutPercentage { get; set; }
        public decimal EligibleValue { get; set; }
        public string Status { get; set; } = string.Empty; // Posted, Available, Withdrawn
        public DateTime PostedDate { get; set; }
    }
}
