using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockMarginService : IMarginService
{
    private readonly List<Margin> _margins;

    public MockMarginService()
    {
        _margins = new List<Margin>
        {
            new Margin
            {
                Id = "1a2b3c4d-5e6f-7890-1234-567890abcdef",
                TradeId = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                TradeNumber = "ZME-2025-001",
                InitialMargin = 212500,
                VariationMargin = 15000,
                TotalMargin = 227500,
                MarginDate = DateTime.Now.AddDays(-28),
                CurrentMarketPrice = 8560,
                PriceChange = 60,
                PartyName = "Konkola Copper Mines",
                IsPayable = true,
                Status = "Active"
            },
            new Margin
            {
                Id = "2b3c4d5e-6f7a-8901-2345-67890abcdef0",
                TradeId = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                TradeNumber = "ZME-2025-001",
                InitialMargin = 212500,
                VariationMargin = -15000,
                TotalMargin = 197500,
                MarginDate = DateTime.Now.AddDays(-28),
                CurrentMarketPrice = 8560,
                PriceChange = 60,
                PartyName = "First Quantum Minerals Zambia",
                IsPayable = false,
                Status = "Active"
            },
            new Margin
            {
                Id = "3c4d5e6f-7a8b-9012-3456-7890abcdef01",
                TradeId = "b2c3d4e5-f6a7-8901-2345-67890abcdef0",
                TradeNumber = "ZME-2025-002",
                InitialMargin = 420000,
                VariationMargin = 25000,
                TotalMargin = 445000,
                MarginDate = DateTime.Now.AddDays(-23),
                CurrentMarketPrice = 8450,
                PriceChange = 50,
                PartyName = "Mopani Copper Mines",
                IsPayable = true,
                Status = "Active"
            },
            new Margin
            {
                Id = "4d5e6f7a-8b9c-0123-4567-890abcdef012",
                TradeId = "b2c3d4e5-f6a7-8901-2345-67890abcdef0",
                TradeNumber = "ZME-2025-002",
                InitialMargin = 420000,
                VariationMargin = -25000,
                TotalMargin = 395000,
                MarginDate = DateTime.Now.AddDays(-23),
                CurrentMarketPrice = 8450,
                PriceChange = 50,
                PartyName = "Kansanshi Mining",
                IsPayable = false,
                Status = "Active"
            },
            new Margin
            {
                Id = "5e6f7a8b-9c0d-1234-5678-90abcdef0123",
                TradeId = "c3d4e5f6-a7b8-9012-3456-7890abcdef01",
                TradeNumber = "ZME-2025-003",
                InitialMargin = 256500,
                VariationMargin = -10000,
                TotalMargin = 246500,
                MarginDate = DateTime.Now.AddDays(-18),
                CurrentMarketPrice = 8520,
                PriceChange = -30,
                PartyName = "ZCCM Investments Holdings",
                IsPayable = false,
                Status = "Active"
            },
            new Margin
            {
                Id = "6f7a8b9c-0d1e-2345-6789-0abcdef01234",
                TradeId = "c3d4e5f6-a7b8-9012-3456-7890abcdef01",
                TradeNumber = "ZME-2025-003",
                InitialMargin = 256500,
                VariationMargin = 10000,
                TotalMargin = 266500,
                MarginDate = DateTime.Now.AddDays(-18),
                CurrentMarketPrice = 8520,
                PriceChange = -30,
                PartyName = "Barrick Lumwana Mining",
                IsPayable = true,
                Status = "Active"
            },
            new Margin
            {
                Id = "7a8b9c0d-1e2f-3456-7890-abcdef012345",
                TradeId = "d4e5f6a7-b8c9-0123-4567-890abcdef012",
                TradeNumber = "ZME-2025-004",
                InitialMargin = 129000,
                VariationMargin = 7500,
                TotalMargin = 136500,
                MarginDate = DateTime.Now.AddDays(-15),
                CurrentMarketPrice = 8650,
                PriceChange = 50,
                PartyName = "Chambishi Metals",
                IsPayable = true,
                Status = "Pending"
            },
            new Margin
            {
                Id = "8b9c0d1e-2f3a-4567-8901-abcdef0123456",
                TradeId = "e5f6a7b8-c9d0-1234-5678-90abcdef0123",
                TradeNumber = "ZME-2025-005",
                InitialMargin = 348000,
                VariationMargin = 20000,
                TotalMargin = 368000,
                MarginDate = DateTime.Now.AddDays(-10),
                CurrentMarketPrice = 8750,
                PriceChange = 50,
                PartyName = "Lubambe Copper Mine",
                IsPayable = true,
                Status = "Pending"
            }
        };
    }

    public Task<IEnumerable<Margin>> GetAllMarginsAsync()
    {
        return Task.FromResult<IEnumerable<Margin>>(_margins);
    }

    public Task<Margin?> GetMarginByIdAsync(string id)
    {
        var margin = _margins.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(margin);
    }

    public Task<IEnumerable<Margin>> GetMarginsByTradeIdAsync(string tradeId)
    {
        var margins = _margins.Where(m => m.TradeId == tradeId);
        return Task.FromResult<IEnumerable<Margin>>(margins);
    }

    public Task<Margin> CreateMarginAsync(Margin margin)
    {
        margin.Id = Guid.NewGuid().ToString();
        margin.TotalMargin = margin.InitialMargin + margin.VariationMargin;
        _margins.Add(margin);
        return Task.FromResult(margin);
    }

    public Task<Margin> UpdateMarginAsync(Margin margin)
    {
        var existingMargin = _margins.FirstOrDefault(m => m.Id == margin.Id);
        if (existingMargin != null)
        {
            var index = _margins.IndexOf(existingMargin);
            margin.TotalMargin = margin.InitialMargin + margin.VariationMargin;
            _margins[index] = margin;
        }
        return Task.FromResult(margin);
    }

    public Task<bool> DeleteMarginAsync(string id)
    {
        var margin = _margins.FirstOrDefault(m => m.Id == id);
        if (margin != null)
        {
            _margins.Remove(margin);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<Margin> CalculateVariationMarginAsync(string tradeId, decimal currentMarketPrice)
    {
        var tradeMargins = _margins.Where(m => m.TradeId == tradeId).ToList();
        if (tradeMargins.Any())
        {
            var buyerMargin = tradeMargins.First();
            buyerMargin.CurrentMarketPrice = currentMarketPrice;
            buyerMargin.PriceChange = currentMarketPrice - (buyerMargin.TotalMargin / buyerMargin.InitialMargin * 100);
            buyerMargin.VariationMargin = buyerMargin.PriceChange * 1000; // Simplified calculation
            buyerMargin.TotalMargin = buyerMargin.InitialMargin + buyerMargin.VariationMargin;
            buyerMargin.MarginDate = DateTime.Now;

            return Task.FromResult(buyerMargin);
        }

        return Task.FromResult(new Margin());
    }
}
