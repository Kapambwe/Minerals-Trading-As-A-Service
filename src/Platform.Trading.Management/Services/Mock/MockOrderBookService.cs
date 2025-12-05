using Platform.Trading.Management.Models;
using Platform.Trading.Management.Models.Trading;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IOrderBookService for development and testing.
/// </summary>
public class MockOrderBookService : IOrderBookService
{
    private readonly List<Order> _orders = new();
    private readonly Dictionary<string, OrderBook> _orderBooks = new();

    public MockOrderBookService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Create sample orders
        _orders.AddRange(new[]
        {
            new Order
            {
                OrderNumber = "ORD-2024-001",
                OrderType = "Limit",
                Side = "Buy",
                TimeInForce = "GTC",
                ParticipantId = "buyer-001",
                ParticipantName = "Copper Trading Ltd",
                AccountId = "ACC-001",
                MetalType = MetalType.Copper,
                QualityGrade = "Grade A",
                Quantity = 25,
                LimitPrice = 8500,
                Currency = "USD",
                Status = "Open",
                PassedPreTradeRiskCheck = true
            },
            new Order
            {
                OrderNumber = "ORD-2024-002",
                OrderType = "Limit",
                Side = "Sell",
                TimeInForce = "GTC",
                ParticipantId = "seller-001",
                ParticipantName = "Zambia Mining Corp",
                AccountId = "ACC-002",
                MetalType = MetalType.Copper,
                QualityGrade = "Grade A",
                Quantity = 50,
                LimitPrice = 8550,
                Currency = "USD",
                Status = "Open",
                PassedPreTradeRiskCheck = true
            },
            new Order
            {
                OrderNumber = "ORD-2024-003",
                OrderType = "Limit",
                Side = "Buy",
                TimeInForce = "DAY",
                ParticipantId = "buyer-002",
                ParticipantName = "Global Metals Inc",
                AccountId = "ACC-003",
                MetalType = MetalType.Cobalt,
                Quantity = 10,
                LimitPrice = 33000,
                Currency = "USD",
                Status = "Open",
                PassedPreTradeRiskCheck = true
            }
        });

        // Initialize order books
        RefreshOrderBookInternal(MetalType.Copper, "Grade A");
        RefreshOrderBookInternal(MetalType.Cobalt, null);
    }

    private OrderBook RefreshOrderBookInternal(MetalType metalType, string? qualityGrade)
    {
        var key = $"{metalType}:{qualityGrade ?? "All"}";
        
        var relevantOrders = _orders.Where(o => 
            o.MetalType == metalType && 
            o.Status == "Open" &&
            (qualityGrade == null || o.QualityGrade == qualityGrade));

        var bids = relevantOrders
            .Where(o => o.Side == "Buy")
            .GroupBy(o => o.LimitPrice)
            .Select(g => new OrderBookLevel
            {
                Price = g.Key ?? 0,
                Quantity = g.Sum(o => o.RemainingQuantity),
                OrderCount = g.Count(),
                Side = "Bid"
            })
            .OrderByDescending(l => l.Price)
            .ToList();

        var asks = relevantOrders
            .Where(o => o.Side == "Sell")
            .GroupBy(o => o.LimitPrice)
            .Select(g => new OrderBookLevel
            {
                Price = g.Key ?? 0,
                Quantity = g.Sum(o => o.RemainingQuantity),
                OrderCount = g.Count(),
                Side = "Ask"
            })
            .OrderBy(l => l.Price)
            .ToList();

        var orderBook = new OrderBook
        {
            MetalType = metalType,
            QualityGrade = qualityGrade,
            LastUpdateTime = DateTime.UtcNow,
            Bids = bids,
            Asks = asks,
            BestBidPrice = bids.FirstOrDefault()?.Price,
            BestBidQuantity = bids.FirstOrDefault()?.Quantity,
            BestAskPrice = asks.FirstOrDefault()?.Price,
            BestAskQuantity = asks.FirstOrDefault()?.Quantity,
            TotalBidVolume = bids.Sum(b => b.Quantity),
            TotalAskVolume = asks.Sum(a => a.Quantity),
            TotalBidOrders = bids.Sum(b => b.OrderCount),
            TotalAskOrders = asks.Sum(a => a.OrderCount),
            TradingStatus = "Open"
        };

        _orderBooks[key] = orderBook;
        return orderBook;
    }

    public Task<IEnumerable<Order>> GetAllOrdersAsync()
        => Task.FromResult<IEnumerable<Order>>(_orders);

    public Task<Order?> GetOrderByIdAsync(string id)
        => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

    public Task<IEnumerable<Order>> GetOrdersByParticipantAsync(string participantId)
        => Task.FromResult<IEnumerable<Order>>(_orders.Where(o => o.ParticipantId == participantId));

    public Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        => Task.FromResult<IEnumerable<Order>>(_orders.Where(o => o.Status == status));

    public Task<Order> CreateOrderAsync(Order order)
    {
        order.Id = Guid.NewGuid().ToString();
        order.OrderNumber = $"ORD-{DateTime.Now:yyyyMMdd}-{_orders.Count + 1:D4}";
        order.OrderDate = DateTime.UtcNow;
        order.Status = "Open";
        order.PassedPreTradeRiskCheck = true; // Simulated risk check
        _orders.Add(order);
        
        RefreshOrderBookInternal(order.MetalType, order.QualityGrade);
        return Task.FromResult(order);
    }

    public Task<Order> UpdateOrderAsync(Order order)
    {
        var existing = _orders.FirstOrDefault(o => o.Id == order.Id);
        if (existing != null)
        {
            _orders.Remove(existing);
            order.ModifiedDate = DateTime.UtcNow;
            _orders.Add(order);
            RefreshOrderBookInternal(order.MetalType, order.QualityGrade);
        }
        return Task.FromResult(order);
    }

    public Task<Order> CancelOrderAsync(string id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.Status = "Cancelled";
            order.ModifiedDate = DateTime.UtcNow;
            RefreshOrderBookInternal(order.MetalType, order.QualityGrade);
        }
        return Task.FromResult(order!);
    }

    public Task<bool> DeleteOrderAsync(string id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            _orders.Remove(order);
            RefreshOrderBookInternal(order.MetalType, order.QualityGrade);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<OrderBook?> GetOrderBookAsync(MetalType metalType, string? qualityGrade = null)
    {
        var key = $"{metalType}:{qualityGrade ?? "All"}";
        return Task.FromResult(_orderBooks.TryGetValue(key, out var book) ? book : null);
    }

    public Task<IEnumerable<OrderBook>> GetAllOrderBooksAsync()
        => Task.FromResult<IEnumerable<OrderBook>>(_orderBooks.Values);

    public Task<OrderBook> RefreshOrderBookAsync(MetalType metalType, string? qualityGrade = null)
        => Task.FromResult(RefreshOrderBookInternal(metalType, qualityGrade));

    public Task<IEnumerable<Trade>> MatchOrdersAsync(Order order)
    {
        // Simplified matching logic - in production this would be more sophisticated
        var matchedTrades = new List<Trade>();
        var oppositeOrders = _orders.Where(o => 
            o.MetalType == order.MetalType &&
            o.Side != order.Side &&
            o.Status == "Open" &&
            ((order.Side == "Buy" && o.LimitPrice <= order.LimitPrice) ||
             (order.Side == "Sell" && o.LimitPrice >= order.LimitPrice)))
            .OrderBy(o => order.Side == "Buy" ? o.LimitPrice : -o.LimitPrice);

        // This is a simplified mock - real matching would be more complex
        return Task.FromResult<IEnumerable<Trade>>(matchedTrades);
    }

    public Task<bool> OpenTradingSessionAsync(MetalType metalType)
    {
        foreach (var book in _orderBooks.Values.Where(b => b.MetalType == metalType))
        {
            book.TradingStatus = "Open";
        }
        return Task.FromResult(true);
    }

    public Task<bool> CloseTradingSessionAsync(MetalType metalType)
    {
        foreach (var book in _orderBooks.Values.Where(b => b.MetalType == metalType))
        {
            book.TradingStatus = "Closed";
        }
        return Task.FromResult(true);
    }

    public Task<bool> HaltTradingAsync(MetalType metalType, string reason)
    {
        foreach (var book in _orderBooks.Values.Where(b => b.MetalType == metalType))
        {
            book.TradingStatus = "Halted";
        }
        return Task.FromResult(true);
    }
}
