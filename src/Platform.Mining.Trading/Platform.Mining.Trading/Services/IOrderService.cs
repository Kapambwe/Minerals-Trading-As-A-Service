using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(string orderId);
        Task<string> PlaceOrderAsync(Order order);
        Task<bool> ModifyOrderAsync(Order order);
        Task<bool> CancelOrderAsync(string orderId);
        Task<bool> ValidateOrderAsync(Order order);
        Task<decimal> CalculateFeesAsync(Order order);
    }
}
