using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface ISettlementService
{
    Task<IEnumerable<Settlement>> GetAllSettlementsAsync();
    Task<Settlement?> GetSettlementByIdAsync(string id);
    Task<Settlement?> GetSettlementByTradeIdAsync(string tradeId);
    Task<Settlement> CreateSettlementAsync(Settlement settlement);
    Task<Settlement> UpdateSettlementAsync(Settlement settlement);
    Task<bool> DeleteSettlementAsync(string id);
    Task<Settlement> CompleteSettlementAsync(string settlementId);
}
