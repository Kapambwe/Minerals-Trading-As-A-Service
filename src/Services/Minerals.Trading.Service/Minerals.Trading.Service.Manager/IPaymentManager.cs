using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface IPaymentManager
{
    Task<IEnumerable<Payment>> GetAllPaymentsAsync();
    Task<Payment?> GetPaymentByIdAsync(string id);
    Task<IEnumerable<Payment>> GetPaymentsByTradeIdAsync(string tradeId);
    Task<Payment> CreatePaymentAsync(Payment payment);
    Task<Payment> UpdatePaymentAsync(Payment payment);
    Task<bool> DeletePaymentAsync(string id);
}
