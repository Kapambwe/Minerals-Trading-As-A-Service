using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockTradeService : ITradeService
{
    private readonly List<Trade> _trades;

    public MockTradeService()
    {
        _trades = new List<Trade>
        {
            new Trade
            {
                Id = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
                TradeNumber = "ZME-2025-001",
                TradeDate = DateTime.Now.AddDays(-30),
                BuyerName = "Konkola Copper Mines",
                SellerName = "First Quantum Minerals Zambia",
                MetalType = MetalType.Copper,
                Quantity = 250,
                PricePerTon = 8500,
                TotalValue = 2125000,
                DeliveryDate = DateTime.Now.AddDays(60),
                Status = TradeStatus.Active,
                IsNovated = true,
                NovationDate = DateTime.Now.AddDays(-28),
                ClearingHouse = "ZME Clear",
                Notes = "Standard copper cathode contract - Copperbelt grade"
            },
            new Trade
            {
                Id = "b2c3d4e5-f6a7-8901-2345-67890abcdef0",
                TradeNumber = "ZME-2025-002",
                TradeDate = DateTime.Now.AddDays(-25),
                BuyerName = "Mopani Copper Mines",
                SellerName = "Kansanshi Mining",
                MetalType = MetalType.Copper,
                Quantity = 500,
                PricePerTon = 8400,
                TotalValue = 4200000,
                DeliveryDate = DateTime.Now.AddDays(45),
                Status = TradeStatus.Active,
                IsNovated = true,
                NovationDate = DateTime.Now.AddDays(-23),
                ClearingHouse = "ZME Clear",
                Notes = "High-grade copper cathodes from Solwezi"
            },
            new Trade
            {
                Id = "c3d4e5f6-a7b8-9012-3456-7890abcdef01",
                TradeNumber = "ZME-2025-003",
                TradeDate = DateTime.Now.AddDays(-20),
                BuyerName = "ZCCM Investments Holdings",
                SellerName = "Barrick Lumwana Mining",
                MetalType = MetalType.Copper,
                Quantity = 300,
                PricePerTon = 8550,
                TotalValue = 2565000,
                DeliveryDate = DateTime.Now.AddDays(30),
                Status = TradeStatus.MarginCollected,
                IsNovated = true,
                NovationDate = DateTime.Now.AddDays(-18),
                ClearingHouse = "ZME Clear",
                Notes = "Premium Copperbelt copper"
            },
            new Trade
            {
                Id = "d4e5f6a7-b8c9-0123-4567-890abcdef012",
                TradeNumber = "ZME-2025-004",
                TradeDate = DateTime.Now.AddDays(-15),
                BuyerName = "Chambishi Metals",
                SellerName = "Nkana Smelter Operations",
                MetalType = MetalType.Copper,
                Quantity = 150,
                PricePerTon = 8600,
                TotalValue = 1290000,
                DeliveryDate = DateTime.Now.AddDays(75),
                Status = TradeStatus.Confirmed,
                IsNovated = false,
                ClearingHouse = "ZME Clear",
                Notes = "Refined copper from Kitwe smelter"
            },
            new Trade
            {
                Id = "e5f6a7b8-c9d0-1234-5678-90abcdef0123",
                TradeNumber = "ZME-2025-005",
                TradeDate = DateTime.Now.AddDays(-10),
                BuyerName = "Lubambe Copper Mine",
                SellerName = "Chibuluma Mines",
                MetalType = MetalType.Copper,
                Quantity = 400,
                PricePerTon = 8700,
                TotalValue = 3480000,
                DeliveryDate = DateTime.Now.AddDays(90),
                Status = TradeStatus.Pending,
                IsNovated = false,
                ClearingHouse = "ZME Clear",
                Notes = "Spot delivery contract - Kalulushi region"
            },
            new Trade
            {
                Id = "f6a7b8c9-d0e1-2345-6789-0abcdef01234",
                TradeNumber = "ZME-2025-006",
                TradeDate = DateTime.Now.AddDays(-60),
                BuyerName = "Kafue Copper Smelter",
                SellerName = "Mufulira Mine",
                MetalType = MetalType.Copper,
                Quantity = 200,
                PricePerTon = 8450,
                TotalValue = 1690000,
                DeliveryDate = DateTime.Now.AddDays(-5),
                Status = TradeStatus.Settled,
                IsNovated = true,
                NovationDate = DateTime.Now.AddDays(-58),
                ClearingHouse = "ZME Clear",
                Notes = "Completed trade - delivered to Ndola warehouse"
            },
            new Trade
            {
                Id = "a0b1c2d3-e4f5-6789-0123-456789abcdef",
                TradeNumber = "ZME-2025-007",
                TradeDate = DateTime.Now.AddDays(-5),
                BuyerName = "NFC Africa Mining",
                SellerName = "Sentinel Mining Zambia",
                MetalType = MetalType.Copper,
                Quantity = 350,
                PricePerTon = 8750,
                TotalValue = 3062500,
                DeliveryDate = DateTime.Now.AddDays(120),
                Status = TradeStatus.Confirmed,
                IsNovated = true,
                NovationDate = DateTime.Now.AddDays(-3),
                ClearingHouse = "ZME Clear",
                Notes = "Export-grade copper cathodes"
            }
        };
    }

    public Task<IEnumerable<Trade>> GetAllTradesAsync()
    {
        return Task.FromResult<IEnumerable<Trade>>(_trades);
    }

    public Task<Trade?> GetTradeByIdAsync(string id)
    {
        var trade = _trades.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(trade);
    }

    public Task<Trade> CreateTradeAsync(Trade trade)
    {
        trade.Id = Guid.NewGuid().ToString();
        trade.TotalValue = trade.Quantity * trade.PricePerTon;
        _trades.Add(trade);
        return Task.FromResult(trade);
    }

    public Task<Trade> UpdateTradeAsync(Trade trade)
    {
        var existingTrade = _trades.FirstOrDefault(t => t.Id == trade.Id);
        if (existingTrade != null)
        {
            var index = _trades.IndexOf(existingTrade);
            trade.TotalValue = trade.Quantity * trade.PricePerTon;
            _trades[index] = trade;
        }
        return Task.FromResult(trade);
    }

    public Task<bool> DeleteTradeAsync(string id)
    {
        var trade = _trades.FirstOrDefault(t => t.Id == id);
        if (trade != null)
        {
            _trades.Remove(trade);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<Trade> NovateTradeAsync(string tradeId)
    {
        var trade = _trades.FirstOrDefault(t => t.Id == tradeId);
        if (trade != null)
        {
            trade.IsNovated = true;
            trade.NovationDate = DateTime.Now;
            trade.Status = TradeStatus.Novated;
        }
        return Task.FromResult(trade!);
    }

    public Task<Trade> ConfirmTradeAsync(string tradeId)
    {
        var trade = _trades.FirstOrDefault(t => t.Id == tradeId);
        if (trade != null)
        {
            trade.Status = TradeStatus.Confirmed;
        }
        return Task.FromResult(trade!);
    }
}

