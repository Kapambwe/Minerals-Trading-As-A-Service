using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public class MockPaymentService : IPaymentService
    {
        public async Task AddPaymentAsync(Payment payment)
        {
            await Task.Delay(500);
            Console.WriteLine($"MockPaymentService: Payment for TradeId {payment.TradeId} with Amount {payment.Amount:C} and Description '{payment.Description}' added.");
        }
    }
}