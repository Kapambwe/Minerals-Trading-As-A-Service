using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface ISettlementManager
{
    Task<IEnumerable<Settlement>> GetAllSettlementsAsync();
    Task<Settlement?> GetSettlementByIdAsync(string id);
    Task<Settlement?> GetSettlementByTradeIdAsync(string tradeId);
    Task<Settlement> CreateSettlementAsync(Settlement settlement);
    Task<Settlement> UpdateSettlementAsync(Settlement settlement);
    Task<bool> DeleteSettlementAsync(string id);
    Task<Settlement> CompleteSettlementAsync(string settlementId);
    Task<Settlement> ProcessPhysicalSettlementAsync(string tradeId, string warrantNumber, string warehouseLocation);
    Task<Settlement> ProcessCashSettlementAsync(string tradeId, decimal finalPrice);
    Task<bool> ValidateSettlementAsync(Settlement settlement);
}
