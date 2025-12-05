using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Trading;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for price index and benchmark price operations.
/// </summary>
public interface IPriceIndexService
{
    // Price Indices
    Task<IEnumerable<PriceIndex>> GetAllPriceIndicesAsync();
    Task<PriceIndex?> GetPriceIndexByIdAsync(string id);
    Task<IEnumerable<PriceIndex>> GetPriceIndicesBySourceAsync(string indexSource);
    Task<IEnumerable<PriceIndex>> GetPriceIndicesByMetalTypeAsync(MetalType metalType);
    Task<PriceIndex?> GetLatestPriceAsync(string indexSource, MetalType metalType, string priceType = "Official");
    Task<PriceIndex> CreatePriceIndexAsync(PriceIndex priceIndex);
    Task<PriceIndex> UpdatePriceIndexAsync(PriceIndex priceIndex);
    
    // Price History
    Task<IEnumerable<PriceHistory>> GetPriceHistoryAsync(string indexSource, MetalType metalType, DateTime fromDate, DateTime toDate);
    Task<PriceHistory> AddPriceHistoryAsync(PriceHistory history);
    
    // External Price Feeds
    Task<PriceIndex> FetchLatestLmePriceAsync(MetalType metalType);
    Task<PriceIndex> FetchLatestComexPriceAsync(MetalType metalType);
    Task RefreshAllPriceIndicesAsync();
}
