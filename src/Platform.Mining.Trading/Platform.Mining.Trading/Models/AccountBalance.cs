namespace Platform.Mining.Trading.Models
{
    public class AccountBalance
    {
        public string Currency { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal ReservedBalance { get; set; }
    }
}
