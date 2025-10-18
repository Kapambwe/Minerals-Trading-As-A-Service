namespace Platform.Trading.Management.Models;

public enum TradeStatus
{
    Pending,
    Confirmed,
    Novated,
    MarginCollected,
    Active,
    Settled,
    Completed,
    Cancelled
}
