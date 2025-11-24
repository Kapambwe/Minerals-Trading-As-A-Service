namespace Minerals.Trading.Service.Model;

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
