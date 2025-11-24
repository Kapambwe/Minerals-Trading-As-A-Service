using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface ISellerManager
{
    Task<IEnumerable<Seller>> GetAllSellersAsync();
    Task<Seller?> GetSellerByIdAsync(string id);
    Task<Seller> CreateSellerAsync(Seller seller);
    Task<Seller> UpdateSellerAsync(Seller seller);
    Task<bool> DeleteSellerAsync(string id);
    Task<Seller> ApproveSellerAsync(string sellerId);
    Task<Seller> UpdateKYCStatusAsync(string sellerId, string kycStatus);
}
