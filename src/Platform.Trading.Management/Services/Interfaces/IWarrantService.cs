using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface IWarrantService
{
    Task<IEnumerable<Warrant>> GetAllWarrantsAsync();
    Task<Warrant?> GetWarrantByIdAsync(string id);
    Task<IEnumerable<Warrant>> GetWarrantsByTradeIdAsync(string tradeId);
    Task<Warrant> CreateWarrantAsync(Warrant warrant);
    Task<Warrant> UpdateWarrantAsync(Warrant warrant);
    Task<bool> DeleteWarrantAsync(string id);
    Task<Warrant> TransferWarrantAsync(string warrantId, string newOwner);
}
