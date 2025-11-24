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

        settlement.IsCompleted = true;
        settlement.CompletionDate = DateTime.Now;
        settlement.Status = "Completed";
        
        await _context.SaveChangesAsync();
        return settlement;
    }
}
