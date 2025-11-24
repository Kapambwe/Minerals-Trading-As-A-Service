using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class TradeManager : ITradeManager
{
    private readonly TradingDbContext _context;
    private const decimal MinTradeValue = 1000m; // Minimum trade value in USD
    private const decimal MaxQuantity = 10000m; // Maximum quantity per trade in metric tons
    private const decimal ValueComparisonTolerance = 0.01m; // Tolerance for decimal comparisons

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
        // Validate trade before creation
        if (!await ValidateTradeAsync(trade))
        {
            throw new InvalidOperationException("Trade validation failed");
        }

        trade.Id = Guid.NewGuid().ToString();
        trade.TradeDate = DateTime.Now;
        
        // Calculate total value if not set
        if (trade.TotalValue == 0)
        {
            trade.TotalValue = trade.Quantity * trade.PricePerTon;
        }

        // Validate calculated value matches
        var expectedValue = trade.Quantity * trade.PricePerTon;
        if (Math.Abs(trade.TotalValue - expectedValue) > ValueComparisonTolerance)
        {
            throw new InvalidOperationException($"Trade total value mismatch. Expected: {expectedValue}, Provided: {trade.TotalValue}");
        }

        // Generate trade number if not provided
        if (string.IsNullOrEmpty(trade.TradeNumber))
        {
            trade.TradeNumber = $"TRD-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }

        // Check buyer and seller are approved (if they exist in the system)
        var buyer = await _context.Buyers.FirstOrDefaultAsync(b => b.CompanyName == trade.BuyerName);
        if (buyer != null && !buyer.IsApproved)
        {
            throw new InvalidOperationException($"Buyer '{trade.BuyerName}' is not approved for trading");
        }

        var seller = await _context.Sellers.FirstOrDefaultAsync(s => s.CompanyName == trade.SellerName);
        if (seller != null && !seller.IsApproved)
        {
            throw new InvalidOperationException($"Seller '{trade.SellerName}' is not approved for trading");
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

        // Validate trade is confirmed before novation
        if (trade.Status != TradeStatus.Confirmed)
        {
            throw new InvalidOperationException($"Trade must be confirmed before novation. Current status: {trade.Status}");
        }

        // Check if already novated
        if (trade.IsNovated)
        {
            throw new InvalidOperationException("Trade is already novated");
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

        // Validate status transition
        if (trade.Status != TradeStatus.Pending)
        {
            throw new InvalidOperationException($"Trade must be in Pending status to confirm. Current status: {trade.Status}");
        }

        trade.Status = TradeStatus.Confirmed;
        await _context.SaveChangesAsync();
        return trade;
    }

    public async Task<Trade> CancelTradeAsync(string tradeId, string reason)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        // Only allow cancellation for trades not yet settled
        if (trade.Status == TradeStatus.Settled || trade.Status == TradeStatus.Completed)
        {
            throw new InvalidOperationException($"Cannot cancel trade in {trade.Status} status");
        }

        trade.Status = TradeStatus.Cancelled;
        trade.Notes = string.IsNullOrEmpty(trade.Notes) 
            ? $"Cancelled: {reason}" 
            : $"{trade.Notes}\nCancelled: {reason}";
        
        await _context.SaveChangesAsync();
        return trade;
    }

    public async Task<bool> ValidateTradeAsync(Trade trade)
    {
        // Validate basic fields
        if (string.IsNullOrWhiteSpace(trade.BuyerName))
        {
            throw new ArgumentException("Buyer name is required");
        }

        if (string.IsNullOrWhiteSpace(trade.SellerName))
        {
            throw new ArgumentException("Seller name is required");
        }

        // Validate buyer and seller are different
        if (trade.BuyerName.Equals(trade.SellerName, StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("Buyer and seller cannot be the same entity");
        }

        // Validate quantity
        if (trade.Quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than zero");
        }

        if (trade.Quantity > MaxQuantity)
        {
            throw new ArgumentException($"Quantity cannot exceed {MaxQuantity} metric tons per trade");
        }

        // Validate price
        if (trade.PricePerTon <= 0)
        {
            throw new ArgumentException("Price per ton must be greater than zero");
        }

        // Validate total value
        var calculatedValue = trade.Quantity * trade.PricePerTon;
        if (calculatedValue < MinTradeValue)
        {
            throw new InvalidOperationException($"Trade value must be at least ${MinTradeValue}");
        }

        // Validate delivery date is in the future
        if (trade.DeliveryDate <= DateTime.Now)
        {
            throw new ArgumentException("Delivery date must be in the future");
        }

        return true;
    }

    public async Task<IEnumerable<Trade>> GetTradesByStatusAsync(TradeStatus status)
    {
        return await _context.Trades
            .Where(t => t.Status == status)
            .ToListAsync();
    }
}
