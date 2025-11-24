using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public class PaymentManager : IPaymentManager
{
    private readonly TradingDbContext _context;

    public PaymentManager(TradingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetAllPaymentsAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<Payment?> GetPaymentByIdAsync(string id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task<IEnumerable<Payment>> GetPaymentsByTradeIdAsync(string tradeId)
    {
        return await _context.Payments
            .Where(p => p.TradeId == tradeId)
            .ToListAsync();
    }

    public async Task<Payment> CreatePaymentAsync(Payment payment)
    {
        // Validate payment
        if (!await ValidatePaymentAsync(payment))
        {
            throw new InvalidOperationException("Payment validation failed");
        }

        payment.Id = Guid.NewGuid().ToString();
        
        _context.Payments.Add(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

    public async Task<Payment> UpdatePaymentAsync(Payment payment)
    {
        var existing = await _context.Payments.FindAsync(payment.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Payment with ID {payment.Id} not found");
        }

        _context.Entry(existing).CurrentValues.SetValues(payment);
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeletePaymentAsync(string id)
    {
        var payment = await _context.Payments.FindAsync(id);
        if (payment == null)
        {
            return false;
        }

        _context.Payments.Remove(payment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ValidatePaymentAsync(Payment payment)
    {
        if (string.IsNullOrWhiteSpace(payment.TradeId))
        {
            throw new ArgumentException("Trade ID is required");
        }

        var trade = await _context.Trades.FindAsync(payment.TradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {payment.TradeId} not found");
        }

        if (payment.Amount <= 0)
        {
            throw new ArgumentException("Payment amount must be greater than zero");
        }

        // Check if total payments would exceed trade value
        var existingPayments = await GetTotalPaymentsForTradeAsync(payment.TradeId);
        var totalWithNewPayment = existingPayments + payment.Amount;

        if (totalWithNewPayment > trade.TotalValue * 1.1m) // Allow 10% buffer for fees
        {
            throw new InvalidOperationException(
                $"Total payments (${totalWithNewPayment:F2}) would exceed trade value (${trade.TotalValue:F2}) by more than 10%");
        }

        if (string.IsNullOrWhiteSpace(payment.Description))
        {
            throw new ArgumentException("Payment description is required");
        }

        return true;
    }

    public async Task<decimal> GetTotalPaymentsForTradeAsync(string tradeId)
    {
        var payments = await GetPaymentsByTradeIdAsync(tradeId);
        return payments.Sum(p => p.Amount);
    }

    public async Task<bool> IsTradeFullyPaidAsync(string tradeId)
    {
        var trade = await _context.Trades.FindAsync(tradeId);
        if (trade == null)
        {
            throw new KeyNotFoundException($"Trade with ID {tradeId} not found");
        }

        var totalPayments = await GetTotalPaymentsForTradeAsync(tradeId);
        
        // Consider fully paid if payments are within 1% of trade value
        return totalPayments >= trade.TotalValue * 0.99m;
    }
}
