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
}
