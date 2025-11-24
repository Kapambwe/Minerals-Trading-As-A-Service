using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface IBuyerManager
{
    Task<IEnumerable<Buyer>> GetAllBuyersAsync();
    Task<Buyer?> GetBuyerByIdAsync(string id);
    Task<Buyer> CreateBuyerAsync(Buyer buyer);
    Task<Buyer> UpdateBuyerAsync(Buyer buyer);
    Task<bool> DeleteBuyerAsync(string id);
    Task<Buyer> ApproveBuyerAsync(string buyerId);
    Task<Buyer> UpdateKYCStatusAsync(string buyerId, string kycStatus);
}
