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
                Id = "TRD001",
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
                Id = "TRD002",
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
                Id = "TRD003",
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
                Id = "TRD004",
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
                Id = "TRD005",
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
                Id = "TRD006",
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
                Id = "TRD007",
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
        trade.Id = $"TRD{_trades.Count + 1:D3}";
        trade.TradeNumber = $"ZME-2025-{_trades.Count + 1:D3}";
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
