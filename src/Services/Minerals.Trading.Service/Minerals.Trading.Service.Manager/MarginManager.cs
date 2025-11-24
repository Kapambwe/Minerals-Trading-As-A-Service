using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class MarginManager : IMarginManager
{
    private readonly TradingDbContext _context;
    private const decimal DefaultMarginPercentage = 0.10m; // 10% of trade value

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

    public async Task<Margin> CalculateInitialMarginAsync(string tradeId, decimal marginPercentage = DefaultMarginPercentage)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        // Validate margin percentage
        if (marginPercentage <= 0 || marginPercentage > 0.5m)
        {
            throw new ArgumentException("Margin percentage must be between 0 and 50%");
        }

        // Calculate initial margin
        var initialMargin = trade.TotalValue * marginPercentage;

        var margin = new Margin
        {
            Id = Guid.NewGuid().ToString(),
            TradeId = tradeId,
            TradeNumber = trade.TradeNumber,
            InitialMargin = initialMargin,
            VariationMargin = 0,
            TotalMargin = initialMargin,
            MarginDate = DateTime.Now,
            CurrentMarketPrice = trade.PricePerTon,
            PriceChange = 0,
            PartyName = trade.BuyerName, // Initial margin typically from buyer
            IsPayable = true,
            Status = "Required",
            Notes = $"Initial margin calculated at {marginPercentage:P} of trade value"
        };

        _context.Margins.Add(margin);
        await _context.SaveChangesAsync();

        // Update trade status
        trade.Status = TradeStatus.MarginCollected;
        await _context.SaveChangesAsync();

        return margin;
    }

    public async Task<Margin> CalculateVariationMarginAsync(string tradeId, decimal currentMarketPrice)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        if (currentMarketPrice <= 0)
        {
            throw new ArgumentException("Current market price must be greater than zero");
        }

        // Calculate price change and variation margin
        var priceChange = currentMarketPrice - trade.PricePerTon;
        var variationMargin = priceChange * trade.Quantity;

        // Determine which party pays/receives
        bool isBuyerPayable = priceChange > 0; // If price increased, buyer pays
        string partyName = isBuyerPayable ? trade.BuyerName : trade.SellerName;

        var margin = new Margin
        {
            Id = Guid.NewGuid().ToString(),
            TradeId = tradeId,
            TradeNumber = trade.TradeNumber,
            InitialMargin = 0,
            VariationMargin = Math.Abs(variationMargin),
            TotalMargin = Math.Abs(variationMargin),
            MarginDate = DateTime.Now,
            CurrentMarketPrice = currentMarketPrice,
            PriceChange = priceChange,
            PartyName = partyName,
            IsPayable = true,
            Status = "Required",
            Notes = $"Variation margin due to {(priceChange > 0 ? "price increase" : "price decrease")} of ${Math.Abs(priceChange):F2} per ton"
        };

        _context.Margins.Add(margin);
        await _context.SaveChangesAsync();

        return margin;
    }

    public async Task<decimal> GetTotalMarginRequirementAsync(string tradeId)
    {
        var margins = await GetMarginsByTradeIdAsync(tradeId);
        return margins.Sum(m => m.TotalMargin);
    }
}
