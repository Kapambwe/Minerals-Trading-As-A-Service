using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IMarketOperationsService
    {
        Task<MarketStatus> GetMarketStatusAsync();
        Task<bool> StartSessionAsync(string marketId);
        Task<bool> StopSessionAsync(string marketId, string reason);
        Task<bool> EmergencyHaltAsync(string marketId, string reason);
        Task<List<ScheduledEvent>> GetScheduledEventsAsync();
        Task<string> ScheduleAuctionAsync(ScheduledEvent auctionEvent);
        Task<List<CircuitBreaker>> GetCircuitBreakersAsync();
        Task<bool> UpdateCircuitBreakerAsync(CircuitBreaker breaker);
        Task<List<OverrideLog>> GetOverrideLogsAsync();
    }
}
