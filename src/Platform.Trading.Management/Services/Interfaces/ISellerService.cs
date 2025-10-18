using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface ISellerService
{
    Task<IEnumerable<Seller>> GetAllSellersAsync();
    Task<Seller?> GetSellerByIdAsync(string id);
    Task<Seller> CreateSellerAsync(Seller seller);
    Task<Seller> UpdateSellerAsync(Seller seller);
    Task<bool> DeleteSellerAsync(string id);
    Task<Seller> ApproveSellerAsync(string sellerId);
    Task<Seller> UpdateKYCStatusAsync(string sellerId, string kycStatus);
}
