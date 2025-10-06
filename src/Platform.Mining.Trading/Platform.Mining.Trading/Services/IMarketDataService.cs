using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IMarketDataService
    {
        Task<MarketDepth> GetMarketDepthAsync(string instrument);
        Task<List<Trade>> GetLastTradesAsync(string instrument, int count = 50);
        Task<decimal> GetCurrentPriceAsync(string instrument);
        Task<List<string>> GetInstrumentsAsync();
    }
}
