using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class WarrantManager : IWarrantManager
{
    private readonly TradingDbContext _context;

    public WarrantManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Warrant>> GetAllWarrantsAsync()
    {
        return await _context.Warrants.ToListAsync();
    }

    public async Task<Warrant?> GetWarrantByIdAsync(string id)
    {
        return await _context.Warrants.FindAsync(id);
    }

    public async Task<IEnumerable<Warrant>> GetWarrantsByTradeIdAsync(string tradeId)
    {
        return await _context.Warrants
            .Where(w => w.TradeId == tradeId)
            .ToListAsync();
    }

    public async Task<Warrant> CreateWarrantAsync(Warrant warrant)
    {
        warrant.Id = Guid.NewGuid().ToString();
        warrant.IssueDate = DateTime.Now;
        
        if (string.IsNullOrEmpty(warrant.WarrantNumber))
        {
            warrant.WarrantNumber = $"WRN-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
        
        _context.Warrants.Add(warrant);
        await _context.SaveChangesAsync();
        return warrant;
    }

    public async Task<Warrant> UpdateWarrantAsync(Warrant warrant)
    {
        var existing = await _context.Warrants.FindAsync(warrant.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Warrant with ID {warrant.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(warrant);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteWarrantAsync(string id)
    {
        var warrant = await _context.Warrants.FindAsync(id);
        if (warrant == null)
        {
            return false;
        }

        _context.Warrants.Remove(warrant);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Warrant> TransferWarrantAsync(string warrantId, string newOwner)
    {
        var warrant = await _context.Warrants.FindAsync(warrantId);
        if (warrant == null)
        {
            throw new KeyNotFoundException($"Warrant with ID {warrantId} not found");
        }

        warrant.PreviousOwner = warrant.CurrentOwner;
        warrant.CurrentOwner = newOwner;
        warrant.TransferDate = DateTime.Now;
        
        await _context.SaveChangesAsync();
        return warrant;
    }
}
