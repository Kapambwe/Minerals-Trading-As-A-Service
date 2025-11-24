using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface IMineralListingManager
{
    Task<IEnumerable<MineralListing>> GetAllMineralListingsAsync();
    Task<MineralListing?> GetMineralListingByIdAsync(string id);
    Task<MineralListing> CreateMineralListingAsync(MineralListing listing);
    Task<MineralListing> UpdateMineralListingAsync(MineralListing listing);
    Task<bool> DeleteMineralListingAsync(string id);
    Task<IEnumerable<MineralListing>> GetAvailableMineralListingsAsync();
    Task<MineralListing> UpdateListingStatusAsync(string listingId, string status);
}
