using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class SettlementManager : ISettlementManager
{
    private readonly TradingDbContext _context;

    public SettlementManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Settlement>> GetAllSettlementsAsync()
    {
        return await _context.Settlements.ToListAsync();
    }

    public async Task<Settlement?> GetSettlementByIdAsync(string id)
    {
        return await _context.Settlements.FindAsync(id);
    }

    public async Task<Settlement?> GetSettlementByTradeIdAsync(string tradeId)
    {
        return await _context.Settlements
            .FirstOrDefaultAsync(s => s.TradeId == tradeId);
    }

    public async Task<Settlement> CreateSettlementAsync(Settlement settlement)
    {
        settlement.Id = Guid.NewGuid().ToString();
        settlement.SettlementDate = DateTime.Now;
        
        if (string.IsNullOrEmpty(settlement.SettlementNumber))
        {
            settlement.SettlementNumber = $"STL-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
        
        _context.Settlements.Add(settlement);
        await _context.SaveChangesAsync();
        return settlement;
    }

    public async Task<Settlement> UpdateSettlementAsync(Settlement settlement)
    {
        var existing = await _context.Settlements.FindAsync(settlement.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Settlement with ID {settlement.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(settlement);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteSettlementAsync(string id)
    {
        var settlement = await _context.Settlements.FindAsync(id);
        if (settlement == null)
        {
            return false;
        }

        _context.Settlements.Remove(settlement);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Settlement> CompleteSettlementAsync(string settlementId)
    {
        var settlement = await _context.Settlements.FindAsync(settlementId);
        if (settlement == null)
        {
            throw new KeyNotFoundException($"Settlement with ID {settlementId} not found");
        }

        if (settlement.IsCompleted)
        {
            throw new InvalidOperationException("Settlement is already completed");
        }

        settlement.IsCompleted = true;
        settlement.CompletionDate = DateTime.Now;
        settlement.Status = "Completed";
        
        // Update related trade status
        var trade = await _context.Trades.FirstOrDefaultAsync(t => t.Id == settlement.TradeId);
        if (trade != null)
        {
            trade.Status = TradeStatus.Settled;
        }

        await _context.SaveChangesAsync();
        return settlement;
    }

    public async Task<Settlement> ProcessPhysicalSettlementAsync(string tradeId, string warrantNumber, string warehouseLocation)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        // Validate trade is ready for settlement
        if (trade.Status != TradeStatus.Novated && trade.Status != TradeStatus.MarginCollected && trade.Status != TradeStatus.Active)
        {
            throw new InvalidOperationException($"Trade must be in valid status for settlement. Current status: {trade.Status}");
        }

        // Check if warrant exists and is valid
        var warrant = await _context.Warrants.FirstOrDefaultAsync(w => w.WarrantNumber == warrantNumber && w.IsActive);
        if (warrant == null)
        {
            throw new InvalidOperationException($"Warrant {warrantNumber} not found or is inactive");
        }

        // Validate warrant matches trade details
        if (warrant.MetalType != trade.MetalType)
        {
            throw new InvalidOperationException($"Warrant metal type ({warrant.MetalType}) does not match trade ({trade.MetalType})");
        }

        if (warrant.Quantity < trade.Quantity)
        {
            throw new InvalidOperationException($"Warrant quantity ({warrant.Quantity}) is less than trade quantity ({trade.Quantity})");
        }

        var settlement = new Settlement
        {
            Id = Guid.NewGuid().ToString(),
            SettlementNumber = $"STL-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            TradeId = tradeId,
            TradeNumber = trade.TradeNumber,
            SettlementType = SettlementType.PhysicalDelivery,
            SettlementDate = DateTime.Now,
            SettlementAmount = trade.TotalValue,
            BuyerName = trade.BuyerName,
            SellerName = trade.SellerName,
            MetalType = trade.MetalType,
            Quantity = trade.Quantity,
            WarrantNumber = warrantNumber,
            WarehouseLocation = warehouseLocation,
            FinalPrice = trade.PricePerTon,
            PriceDifference = 0,
            Status = "Processing",
            IsCompleted = false,
            Notes = $"Physical settlement via warrant {warrantNumber}"
        };

        _context.Settlements.Add(settlement);
        trade.Status = TradeStatus.Active;
        await _context.SaveChangesAsync();

        return settlement;
    }

    public async Task<Settlement> ProcessCashSettlementAsync(string tradeId, decimal finalPrice)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        // Validate trade is ready for settlement
        if (trade.Status != TradeStatus.Novated && trade.Status != TradeStatus.MarginCollected && trade.Status != TradeStatus.Active)
        {
            throw new InvalidOperationException($"Trade must be in valid status for settlement. Current status: {trade.Status}");
        }

        if (finalPrice <= 0)
        {
            throw new ArgumentException("Final price must be greater than zero");
        }

        // Calculate price difference and settlement amount
        var priceDifference = finalPrice - trade.PricePerTon;
        var settlementAmount = priceDifference * trade.Quantity;

        var settlement = new Settlement
        {
            Id = Guid.NewGuid().ToString(),
            SettlementNumber = $"STL-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}",
            TradeId = tradeId,
            TradeNumber = trade.TradeNumber,
            SettlementType = SettlementType.CashSettlement,
            SettlementDate = DateTime.Now,
            SettlementAmount = Math.Abs(settlementAmount),
            BuyerName = trade.BuyerName,
            SellerName = trade.SellerName,
            MetalType = trade.MetalType,
            Quantity = trade.Quantity,
            FinalPrice = finalPrice,
            PriceDifference = priceDifference,
            Status = "Processing",
            IsCompleted = false,
            Notes = $"Cash settlement - Price {(priceDifference > 0 ? "increased" : "decreased")} by ${Math.Abs(priceDifference):F2} per ton. " +
                   $"{(priceDifference > 0 ? trade.BuyerName : trade.SellerName)} pays ${Math.Abs(settlementAmount):F2}"
        };

        _context.Settlements.Add(settlement);
        trade.Status = TradeStatus.Active;
        await _context.SaveChangesAsync();

        return settlement;
    }

    public async Task<bool> ValidateSettlementAsync(Settlement settlement)
    {
        if (string.IsNullOrWhiteSpace(settlement.TradeId))
        {
            throw new ArgumentException("Trade ID is required");
        }

        var trade = await _context.Trades.FindAsync(settlement.TradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {settlement.TradeId} not found");
        }

        if (settlement.SettlementAmount < 0)
        {
            throw new ArgumentException("Settlement amount cannot be negative");
        }

        if (settlement.SettlementType == SettlementType.PhysicalDelivery)
        {
            if (string.IsNullOrWhiteSpace(settlement.WarrantNumber))
            {
                throw new ArgumentException("Warrant number is required for physical settlement");
            }

            if (string.IsNullOrWhiteSpace(settlement.WarehouseLocation))
            {
                throw new ArgumentException("Warehouse location is required for physical settlement");
            }
        }

        return true;
    }
}
