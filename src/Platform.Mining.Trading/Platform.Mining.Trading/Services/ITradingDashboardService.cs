using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface ITradingDashboardService
    {
        Task<List<AccountBalance>> GetAccountBalancesAsync();
        Task<MarginSummary> GetMarginSummaryAsync();
        Task<List<Order>> GetOpenOrdersAsync();
        Task<List<Position>> GetOpenPositionsAsync();
        Task<List<Alert>> GetAlertsAsync();
        Task<List<Trade>> GetRecentTradesAsync();
        Task<List<NewsItem>> GetNewsAsync();
    }
}
