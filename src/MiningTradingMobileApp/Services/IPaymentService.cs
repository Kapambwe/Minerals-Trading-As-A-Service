using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public interface IPaymentService
    {
        Task AddPaymentAsync(Payment payment);
    }
}