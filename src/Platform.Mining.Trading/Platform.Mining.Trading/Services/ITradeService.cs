using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface ITradeService
    {
        Task<List<Trade>> GetTradesAsync(DateTime? fromDate = null, DateTime? toDate = null);
        Task<Trade> GetTradeByIdAsync(string tradeId);
        Task<byte[]> ExportTradesAsync(List<Trade> trades, string format = "CSV");
        Task<bool> RequestConfirmationAsync(string tradeId);
        Task<bool> DisputeTradeAsync(string tradeId, string reason);
    }
}
