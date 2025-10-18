using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockSettlementService : ISettlementService
{
    private readonly List<Settlement> _settlements;

    public MockSettlementService()
    {
        _settlements = new List<Settlement>
        {
            new Settlement
            {
                Id = "STL001",
                SettlementNumber = "STL-2025-001",
                TradeId = "TRD006",
                TradeNumber = "ZME-2025-006",
                SettlementType = SettlementType.PhysicalDelivery,
                SettlementDate = DateTime.Now.AddDays(-5),
                SettlementAmount = 420000,
                BuyerName = "Kafue Copper Smelter",
                SellerName = "Mufulira Mine",
                MetalType = MetalType.Copper,
                Quantity = 200,
                WarrantNumber = "WRN-2025-004",
                WarehouseLocation = "Mufulira, Zambia",
                FinalPrice = 2100,
                PriceDifference = 0,
                Status = "Completed",
                IsCompleted = true,
                CompletionDate = DateTime.Now.AddDays(-5),
                Notes = "Physical delivery completed successfully"
            },
            new Settlement
            {
                Id = "STL002",
                SettlementNumber = "STL-2025-002",
                TradeId = "TRD001",
                TradeNumber = "ZME-2025-001",
                SettlementType = SettlementType.PhysicalDelivery,
                SettlementDate = DateTime.Now.AddDays(60),
                SettlementAmount = 2140000,
                BuyerName = "Konkola Copper Mines",
                SellerName = "First Quantum Minerals Zambia",
                MetalType = MetalType.Copper,
                Quantity = 250,
                WarrantNumber = "WRN-2025-001",
                WarehouseLocation = "Ndola, Zambia",
                FinalPrice = 8560,
                PriceDifference = 60,
                Status = "Pending",
                IsCompleted = false,
                Notes = "Scheduled for delivery in 60 days"
            },
            new Settlement
            {
                Id = "STL003",
                SettlementNumber = "STL-2025-003",
                TradeId = "TRD002",
                TradeNumber = "ZME-2025-002",
                SettlementType = SettlementType.CashSettlement,
                SettlementDate = DateTime.Now.AddDays(45),
                SettlementAmount = 5500,
                BuyerName = "Mopani Copper Mines",
                SellerName = "Kansanshi Mining",
                MetalType = MetalType.Copper,
                Quantity = 500,
                FinalPrice = 2311,
                PriceDifference = 11,
                Status = "Pending",
                IsCompleted = false,
                Notes = "Cash settlement based on price difference"
            },
            new Settlement
            {
                Id = "STL004",
                SettlementNumber = "STL-2025-004",
                TradeId = "TRD003",
                TradeNumber = "ZME-2025-003",
                SettlementType = SettlementType.PhysicalDelivery,
                SettlementDate = DateTime.Now.AddDays(30),
                SettlementAmount = 837000,
                BuyerName = "ZCCM Investments Holdings",
                SellerName = "Barrick Lumwana Mining",
                MetalType = MetalType.Copper,
                Quantity = 300,
                WarrantNumber = "WRN-2025-003",
                WarehouseLocation = "Chingola, Zambia",
                FinalPrice = 2790,
                PriceDifference = -10,
                Status = "Pending",
                IsCompleted = false,
                Notes = "Delivery scheduled for end of month"
            },
            new Settlement
            {
                Id = "STL005",
                SettlementNumber = "STL-2024-150",
                TradeId = "TRD000",
                TradeNumber = "ZME-2024-150",
                SettlementType = SettlementType.PhysicalDelivery,
                SettlementDate = DateTime.Now.AddDays(-90),
                SettlementAmount = 1680000,
                BuyerName = "Chambishi Metals",
                SellerName = "Lubambe Copper Mine",
                MetalType = MetalType.Copper,
                Quantity = 200,
                WarrantNumber = "WRN-2024-150",
                WarehouseLocation = "Kitwe, Zambia",
                FinalPrice = 8400,
                PriceDifference = -100,
                Status = "Completed",
                IsCompleted = true,
                CompletionDate = DateTime.Now.AddDays(-90),
                Notes = "Historical settlement - completed Q4 2024"
            },
            new Settlement
            {
                Id = "STL006",
                SettlementNumber = "STL-2024-175",
                TradeId = "TRD001",
                TradeNumber = "ZME-2024-175",
                SettlementType = SettlementType.CashSettlement,
                SettlementDate = DateTime.Now.AddDays(-45),
                SettlementAmount = 25000,
                BuyerName = "Chibuluma Mines",
                SellerName = "Sentinel Mining Zambia",
                MetalType = MetalType.Copper,
                Quantity = 100,
                FinalPrice = 17750,
                PriceDifference = -250,
                Status = "Completed",
                IsCompleted = true,
                CompletionDate = DateTime.Now.AddDays(-45),
                Notes = "Cash settlement - price decreased"
            }
        };
    }

    public Task<IEnumerable<Settlement>> GetAllSettlementsAsync()
    {
        return Task.FromResult<IEnumerable<Settlement>>(_settlements);
    }

    public Task<Settlement?> GetSettlementByIdAsync(string id)
    {
        var settlement = _settlements.FirstOrDefault(s => s.Id == id);
        return Task.FromResult(settlement);
    }

    public Task<Settlement?> GetSettlementByTradeIdAsync(string tradeId)
    {
        var settlement = _settlements.FirstOrDefault(s => s.TradeId == tradeId);
        return Task.FromResult(settlement);
    }

    public Task<Settlement> CreateSettlementAsync(Settlement settlement)
    {
        settlement.Id = $"STL{_settlements.Count + 1:D3}";
        settlement.SettlementNumber = $"STL-2025-{_settlements.Count + 1:D3}";
        _settlements.Add(settlement);
        return Task.FromResult(settlement);
    }

    public Task<Settlement> UpdateSettlementAsync(Settlement settlement)
    {
        var existingSettlement = _settlements.FirstOrDefault(s => s.Id == settlement.Id);
        if (existingSettlement != null)
        {
            var index = _settlements.IndexOf(existingSettlement);
            _settlements[index] = settlement;
        }
        return Task.FromResult(settlement);
    }

    public Task<bool> DeleteSettlementAsync(string id)
    {
        var settlement = _settlements.FirstOrDefault(s => s.Id == id);
        if (settlement != null)
        {
            _settlements.Remove(settlement);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<Settlement> CompleteSettlementAsync(string settlementId)
    {
        var settlement = _settlements.FirstOrDefault(s => s.Id == settlementId);
        if (settlement != null)
        {
            settlement.IsCompleted = true;
            settlement.CompletionDate = DateTime.Now;
            settlement.Status = "Completed";
        }
        return Task.FromResult(settlement!);
    }
}
