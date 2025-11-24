using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class TradeManager : ITradeManager
{
    private readonly TradingDbContext _context;

    public TradeManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Trade>> GetAllTradesAsync()
    {
        return await _context.Trades.ToListAsync();
    }

    public async Task<Trade?> GetTradeByIdAsync(string id)
    {
        return await _context.Trades.FindAsync(id);
    }

    public async Task<Trade> CreateTradeAsync(Trade trade)
    {
        trade.Id = Guid.NewGuid().ToString();
        trade.TradeDate = DateTime.Now;
        
        // Generate trade number if not provided
        if (string.IsNullOrEmpty(trade.TradeNumber))
        {
            trade.TradeNumber = $"TRD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }

        _context.Trades.Add(trade);
        await _context.SaveChangesAsync();
        return trade;
    }

    public async Task<Trade> UpdateTradeAsync(Trade trade)
    {
        var existing = await _context.Trades.FindAsync(trade.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Trade with ID {trade.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(trade);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteTradeAsync(string id)
    {
        var trade = await _context.Trades.FindAsync(id);
        if (trade == null)
        {
            return false;
        }

        _context.Trades.Remove(trade);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Trade> NovateTradeAsync(string tradeId)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        trade.IsNovated = true;
        trade.NovationDate = DateTime.Now;
        trade.Status = TradeStatus.Novated;
        
        await _context.SaveChangesAsync();
        return trade;
    }

    public async Task<Trade> ConfirmTradeAsync(string tradeId)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        trade.Status = TradeStatus.Confirmed;
        await _context.SaveChangesAsync();
        return trade;
    }
}
