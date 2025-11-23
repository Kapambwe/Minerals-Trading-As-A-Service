using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public interface IMineralListingService
    {
        Task<IEnumerable<MineralListing>> GetAllMineralListingsAsync();
    }
}