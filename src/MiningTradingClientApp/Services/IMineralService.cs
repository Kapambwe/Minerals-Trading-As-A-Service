using MiningTradingClientApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiningTradingClientApp.Services
{
    public interface IMineralService
    {
        Task<IEnumerable<Mineral>> GetAvailableMineralsAsync();
        Task<IEnumerable<Mineral>> SearchMineralsAsync(string searchTerm);
        Task<IEnumerable<OrderTracking>> GetOrderTrackingAsync();
    }
}