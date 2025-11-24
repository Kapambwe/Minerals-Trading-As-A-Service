using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class MarginManager : IMarginManager
{
    private readonly TradingDbContext _context;

    public MarginManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Margin>> GetAllMarginsAsync()
    {
        return await _context.Margins.ToListAsync();
    }

    public async Task<Margin?> GetMarginByIdAsync(string id)
    {
        return await _context.Margins.FindAsync(id);
    }

    public async Task<IEnumerable<Margin>> GetMarginsByTradeIdAsync(string tradeId)
    {
        return await _context.Margins
            .Where(m => m.TradeId == tradeId)
            .ToListAsync();
    }

    public async Task<Margin> CreateMarginAsync(Margin margin)
    {
        margin.Id = Guid.NewGuid().ToString();
        margin.MarginDate = DateTime.Now;
        
        _context.Margins.Add(margin);
        await _context.SaveChangesAsync();
        return margin;
    }

    public async Task<Margin> UpdateMarginAsync(Margin margin)
    {
        var existing = await _context.Margins.FindAsync(margin.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Margin with ID {margin.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(margin);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteMarginAsync(string id)
    {
        var margin = await _context.Margins.FindAsync(id);
        if (margin == null)
        {
            return false;
        }

        _context.Margins.Remove(margin);
        await _context.SaveChangesAsync();
        return true;
    }
}
