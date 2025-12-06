using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Trading;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for order book management and trading operations.
/// </summary>
public interface IOrderBookService
{
    // Orders
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(string id);
    Task<IEnumerable<Order>> GetOrdersByParticipantAsync(string participantId);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Order order);
    Task<Order> CancelOrderAsync(string id);
    Task<bool> DeleteOrderAsync(string id);
    
    // Order Book
    Task<OrderBook?> GetOrderBookAsync(MetalType metalType, string? qualityGrade = null);
    Task<IEnumerable<OrderBook>> GetAllOrderBooksAsync();
    Task<OrderBook> RefreshOrderBookAsync(MetalType metalType, string? qualityGrade = null);
    
    // Order Matching
    Task<IEnumerable<Trade>> MatchOrdersAsync(Order order);
    
    // Trading Session
    Task<bool> OpenTradingSessionAsync(MetalType metalType);
    Task<bool> CloseTradingSessionAsync(MetalType metalType);
    Task<bool> HaltTradingAsync(MetalType metalType, string reason);
}
