using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public interface ISettlementService
    {
        Task<IEnumerable<Settlement>> GetAllSettlementsAsync();
    }
}