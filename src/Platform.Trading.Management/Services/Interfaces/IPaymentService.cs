using Platform.Trading.Management.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.Trading.Management.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetPaymentsForTradeAsync(string tradeId);
        Task<Payment> GetPaymentByIdAsync(string paymentId);
        Task AddPaymentAsync(Payment payment);
        Task UpdatePaymentAsync(Payment payment);
        Task DeletePaymentAsync(string paymentId);
    }
}
