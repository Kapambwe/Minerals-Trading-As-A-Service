using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockOrderService : IOrderService
    {
        private List<Order> _orders = new();

        public MockOrderService()
        {
            InitializeSampleOrders();
        }

        private void InitializeSampleOrders()
        {
            _orders = new List<Order>
            {
                new Order
                {
                    OrderId = "ORD-2024-001",
                    Instrument = "COPPER-JAN24",
                    MineralType = "Copper",
                    Grade = "High Grade",
                    ContractMonth = "JAN24",
                    Direction = "Buy",
                    Quantity = 500,
                    FilledQuantity = 200,
                    RemainingQuantity = 300,
                    Price = 8500.00m,
                    OrderType = "Limit",
                    TimeInForce = "GTC",
                    Status = "Active",
                    Timestamp = DateTime.Now.AddHours(-2),
                    Venue = "ZMX",
                    Account = "ACC-001",
                    DeliveryOption = "Physical"
                },
                new Order
                {
                    OrderId = "ORD-2024-002",
                    Instrument = "EMERALD-FEB24",
                    MineralType = "Emerald",
                    Grade = "Premium",
                    ContractMonth = "FEB24",
                    Direction = "Sell",
                    Quantity = 100,
                    FilledQuantity = 0,
                    RemainingQuantity = 100,
                    Price = 125000.00m,
                    OrderType = "Limit",
                    TimeInForce = "Day",
                    Status = "Active",
                    Timestamp = DateTime.Now.AddMinutes(-45),
                    Venue = "ZMX",
                    Account = "ACC-001",
                    DeliveryOption = "Cash"
                },
                new Order
                {
                    OrderId = "ORD-2024-003",
                    Instrument = "COBALT-MAR24",
                    MineralType = "Cobalt",
                    Grade = "Industrial Grade",
                    ContractMonth = "MAR24",
                    Direction = "Buy",
                    Quantity = 250,
                    FilledQuantity = 250,
                    RemainingQuantity = 0,
                    Price = 34500.00m,
                    OrderType = "Market",
                    TimeInForce = "IOC",
                    Status = "Filled",
                    Timestamp = DateTime.Now.AddHours(-5),
                    Venue = "ZMX",
                    Account = "ACC-001",
                    DeliveryOption = "Physical"
                }
            };
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            await Task.Delay(100);
            return _orders;
        }

        public async Task<Order> GetOrderByIdAsync(string orderId)
        {
            await Task.Delay(50);
            return _orders.FirstOrDefault(o => o.OrderId == orderId) ?? new Order();
        }

        public async Task<string> PlaceOrderAsync(Order order)
        {
            await Task.Delay(200);
            order.OrderId = $"ORD-2024-{_orders.Count + 1:D3}";
            order.Timestamp = DateTime.Now;
            order.Status = "Active";
            order.FilledQuantity = 0;
            order.RemainingQuantity = order.Quantity;
            _orders.Add(order);
            return order.OrderId;
        }

        public async Task<bool> ModifyOrderAsync(Order order)
        {
            await Task.Delay(150);
            var existingOrder = _orders.FirstOrDefault(o => o.OrderId == order.OrderId);
            if (existingOrder != null)
            {
                existingOrder.Price = order.Price;
                existingOrder.Quantity = order.Quantity;
                existingOrder.RemainingQuantity = order.Quantity - existingOrder.FilledQuantity;
                return true;
            }
            return false;
        }

        public async Task<bool> CancelOrderAsync(string orderId)
        {
            await Task.Delay(100);
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null && order.Status == "Active")
            {
                order.Status = "Cancelled";
                return true;
            }
            return false;
        }

        public async Task<bool> ValidateOrderAsync(Order order)
        {
            await Task.Delay(50);
            // Simple validation logic
            return order.Quantity > 0 && order.Price > 0 && !string.IsNullOrEmpty(order.Instrument);
        }

        public async Task<decimal> CalculateFeesAsync(Order order)
        {
            await Task.Delay(50);
            // Simple fee calculation: 0.5% of total value
            return order.Quantity * order.Price * 0.005m;
        }
    }
}
