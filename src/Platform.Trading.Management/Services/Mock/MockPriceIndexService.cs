using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Trading;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IPriceIndexService for development and testing.
/// </summary>
public class MockPriceIndexService : IPriceIndexService
{
    private readonly List<PriceIndex> _priceIndices = new();
    private readonly List<PriceHistory> _priceHistory = new();

    public MockPriceIndexService()
    {
        SeedData();
    }

    private void SeedData()
    {
        var today = DateTime.Today;
        
        _priceIndices.AddRange(new[]
        {
            // LME Copper Prices
            new PriceIndex
            {
                IndexSource = "LME",
                IndexName = "LME Copper Official Price",
                MetalType = MetalType.Copper,
                QualityGrade = "Grade A",
                Price = 8525.50m,
                Currency = "USD",
                PriceUnit = "per metric ton",
                PriceDate = today,
                PriceType = "Official",
                PreviousPrice = 8498.00m,
                Change = 27.50m,
                ChangePercentage = 0.32m,
                OpenPrice = 8510.00m,
                HighPrice = 8545.00m,
                LowPrice = 8475.00m,
                Volume = 125000,
                OpenInterest = 285000,
                DataStatus = "Active",
                DataSource = "LME Direct Feed"
            },
            new PriceIndex
            {
                IndexSource = "LME",
                IndexName = "LME Copper 3-Month",
                MetalType = MetalType.Copper,
                QualityGrade = "Grade A",
                Price = 8565.00m,
                Currency = "USD",
                PriceUnit = "per metric ton",
                PriceDate = today,
                PriceType = "3-Month",
                PreviousPrice = 8540.00m,
                Change = 25.00m,
                ChangePercentage = 0.29m,
                DataStatus = "Active"
            },
            // COMEX Copper
            new PriceIndex
            {
                IndexSource = "COMEX",
                IndexName = "COMEX Copper Futures",
                MetalType = MetalType.Copper,
                Price = 3.87m * 2204.62m, // Convert from $/lb to $/ton
                Currency = "USD",
                PriceUnit = "per metric ton",
                PriceDate = today,
                PriceType = "Settlement",
                DataStatus = "Active",
                DataSource = "CME Group"
            },
            // LME Cobalt
            new PriceIndex
            {
                IndexSource = "LME",
                IndexName = "LME Cobalt Official Price",
                MetalType = MetalType.Cobalt,
                Price = 33250.00m,
                Currency = "USD",
                PriceUnit = "per metric ton",
                PriceDate = today,
                PriceType = "Official",
                PreviousPrice = 33100.00m,
                Change = 150.00m,
                ChangePercentage = 0.45m,
                DataStatus = "Active"
            },
            // LME Zinc
            new PriceIndex
            {
                IndexSource = "LME",
                IndexName = "LME Zinc Official Price",
                MetalType = MetalType.Zinc,
                Price = 2485.00m,
                Currency = "USD",
                PriceUnit = "per metric ton",
                PriceDate = today,
                PriceType = "Official",
                DataStatus = "Active"
            },
            // LME Lead
            new PriceIndex
            {
                IndexSource = "LME",
                IndexName = "LME Lead Official Price",
                MetalType = MetalType.Lead,
                Price = 2125.00m,
                Currency = "USD",
                PriceUnit = "per metric ton",
                PriceDate = today,
                PriceType = "Official",
                DataStatus = "Active"
            },
            // ZME Reference Prices
            new PriceIndex
            {
                IndexSource = "ZME",
                IndexName = "ZME Copper Reference Price",
                MetalType = MetalType.Copper,
                QualityGrade = "Grade A",
                Price = 8520.00m,
                Currency = "USD",
                PriceUnit = "per metric ton",
                PriceDate = today,
                PriceType = "Reference",
                DataStatus = "Active",
                DataSource = "Zambia Metal Exchange"
            }
        });

        // Add historical prices
        for (int i = 30; i >= 0; i--)
        {
            var date = today.AddDays(-i);
            var basePrice = 8400 + new Random(date.GetHashCode()).Next(-200, 200);
            _priceHistory.Add(new PriceHistory
            {
                IndexSource = "LME",
                MetalType = MetalType.Copper,
                Date = date,
                OpenPrice = basePrice,
                HighPrice = basePrice + new Random(date.GetHashCode() + 1).Next(0, 100),
                LowPrice = basePrice - new Random(date.GetHashCode() + 2).Next(0, 100),
                ClosePrice = basePrice + new Random(date.GetHashCode() + 3).Next(-50, 50),
                Volume = 100000 + new Random(date.GetHashCode() + 4).Next(0, 50000),
                Currency = "USD"
            });
        }
    }

    public Task<IEnumerable<PriceIndex>> GetAllPriceIndicesAsync()
        => Task.FromResult<IEnumerable<PriceIndex>>(_priceIndices);

    public Task<PriceIndex?> GetPriceIndexByIdAsync(string id)
        => Task.FromResult(_priceIndices.FirstOrDefault(p => p.Id == id));

    public Task<IEnumerable<PriceIndex>> GetPriceIndicesBySourceAsync(string indexSource)
        => Task.FromResult<IEnumerable<PriceIndex>>(_priceIndices.Where(p => p.IndexSource == indexSource));

    public Task<IEnumerable<PriceIndex>> GetPriceIndicesByMetalTypeAsync(MetalType metalType)
        => Task.FromResult<IEnumerable<PriceIndex>>(_priceIndices.Where(p => p.MetalType == metalType));

    public Task<PriceIndex?> GetLatestPriceAsync(string indexSource, MetalType metalType, string priceType = "Official")
        => Task.FromResult(_priceIndices
            .Where(p => p.IndexSource == indexSource && p.MetalType == metalType && p.PriceType == priceType)
            .OrderByDescending(p => p.PriceDate)
            .FirstOrDefault());

    public Task<PriceIndex> CreatePriceIndexAsync(PriceIndex priceIndex)
    {
        priceIndex.Id = Guid.NewGuid().ToString();
        priceIndex.LastUpdated = DateTime.UtcNow;
        _priceIndices.Add(priceIndex);
        return Task.FromResult(priceIndex);
    }

    public Task<PriceIndex> UpdatePriceIndexAsync(PriceIndex priceIndex)
    {
        var existing = _priceIndices.FirstOrDefault(p => p.Id == priceIndex.Id);
        if (existing != null)
        {
            _priceIndices.Remove(existing);
            priceIndex.LastUpdated = DateTime.UtcNow;
            _priceIndices.Add(priceIndex);
        }
        return Task.FromResult(priceIndex);
    }

    public Task<IEnumerable<PriceHistory>> GetPriceHistoryAsync(string indexSource, MetalType metalType, DateTime fromDate, DateTime toDate)
        => Task.FromResult<IEnumerable<PriceHistory>>(_priceHistory
            .Where(h => h.IndexSource == indexSource && h.MetalType == metalType && h.Date >= fromDate && h.Date <= toDate)
            .OrderBy(h => h.Date));

    public Task<PriceHistory> AddPriceHistoryAsync(PriceHistory history)
    {
        history.Id = Guid.NewGuid().ToString();
        _priceHistory.Add(history);
        return Task.FromResult(history);
    }

    public Task<PriceIndex> FetchLatestLmePriceAsync(MetalType metalType)
    {
        // In production, this would call an external API
        var existing = _priceIndices.FirstOrDefault(p => 
            p.IndexSource == "LME" && p.MetalType == metalType && p.PriceType == "Official");
        if (existing != null)
        {
            existing.LastUpdated = DateTime.UtcNow;
        }
        return Task.FromResult(existing ?? new PriceIndex { IndexSource = "LME", MetalType = metalType });
    }

    public Task<PriceIndex> FetchLatestComexPriceAsync(MetalType metalType)
    {
        var existing = _priceIndices.FirstOrDefault(p => 
            p.IndexSource == "COMEX" && p.MetalType == metalType);
        if (existing != null)
        {
            existing.LastUpdated = DateTime.UtcNow;
        }
        return Task.FromResult(existing ?? new PriceIndex { IndexSource = "COMEX", MetalType = metalType });
    }

    public Task RefreshAllPriceIndicesAsync()
    {
        foreach (var index in _priceIndices)
        {
            index.LastUpdated = DateTime.UtcNow;
        }
        return Task.CompletedTask;
    }
}
