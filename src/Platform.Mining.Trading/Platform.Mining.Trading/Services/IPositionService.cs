using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IPositionService
    {
        Task<List<Position>> GetPositionsAsync();
        Task<Position> GetPositionByInstrumentAsync(string instrument);
        Task<decimal> GetTotalPnLAsync();
        Task<bool> TransferPositionAsync(string instrument, string toAccount, decimal quantity);
    }
}
