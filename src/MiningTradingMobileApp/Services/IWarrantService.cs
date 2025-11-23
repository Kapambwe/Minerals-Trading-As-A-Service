using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public interface IWarrantService
    {
        Task<Warrant?> GetWarrantByIdAsync(string warrantId);
        Task<IEnumerable<Warrant>> GetAllWarrantsAsync();
    }
}