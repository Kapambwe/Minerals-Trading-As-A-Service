using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockTradingDashboardService : ITradingDashboardService
    {
        public async Task<List<AccountBalance>> GetAccountBalancesAsync()
        {
            await Task.Delay(100); // Simulate API call
            return new List<AccountBalance>
            {
                new AccountBalance
                {
                    Currency = "ZMW",
                    Balance = 2500000.00m,
                    AvailableBalance = 1850000.00m,
                    ReservedBalance = 650000.00m
                },
                new AccountBalance
                {
                    Currency = "USD",
                    Balance = 150000.00m,
                    AvailableBalance = 95000.00m,
                    ReservedBalance = 55000.00m
                }
            };
        }

        public async Task<MarginSummary> GetMarginSummaryAsync()
        {
            await Task.Delay(100);
            return new MarginSummary
            {
                InitialMargin = 450000.00m,
                MaintenanceMargin = 300000.00m,
                VariationMargin = 25000.00m,
                TotalMarginRequired = 475000.00m,
                CurrentCollateral = 650000.00m,
                MarginUtilization = 73.08m,
                ExcessMargin = 175000.00m
            };
        }

        public async Task<List<Order>> GetOpenOrdersAsync()
        {
            await Task.Delay(100);
            return new List<Order>
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
                }
            };
        }

        public async Task<List<Position>> GetOpenPositionsAsync()
        {
            await Task.Delay(100);
            return new List<Position>
            {
                new Position
                {
                    Instrument = "COPPER-JAN24",
                    MineralType = "Copper",
                    Account = "ACC-001",
                    OpenQuantity = 1500,
                    AveragePrice = 8200.00m,
                    CurrentPrice = 8500.00m,
                    MarkToMarket = 12750000.00m,
                    UnrealizedPnL = 450000.00m,
                    RealizedPnL = 125000.00m,
                    TotalPnL = 575000.00m,
                    RequiredMargin = 250000.00m,
                    CollateralPosted = 350000.00m,
                    LastUpdated = DateTime.Now.AddMinutes(-5)
                },
                new Position
                {
                    Instrument = "COBALT-MAR24",
                    MineralType = "Cobalt",
                    Account = "ACC-001",
                    OpenQuantity = 750,
                    AveragePrice = 35000.00m,
                    CurrentPrice = 34500.00m,
                    MarkToMarket = 25875000.00m,
                    UnrealizedPnL = -375000.00m,
                    RealizedPnL = 85000.00m,
                    TotalPnL = -290000.00m,
                    RequiredMargin = 200000.00m,
                    CollateralPosted = 300000.00m,
                    LastUpdated = DateTime.Now.AddMinutes(-3)
                }
            };
        }

        public async Task<List<Alert>> GetAlertsAsync()
        {
            await Task.Delay(100);
            return new List<Alert>
            {
                new Alert
                {
                    AlertId = "ALT-001",
                    Message = "Margin call: Please post additional collateral of ZMW 100,000",
                    Severity = "Critical",
                    Timestamp = DateTime.Now.AddMinutes(-15),
                    IsRead = false
                },
                new Alert
                {
                    AlertId = "ALT-002",
                    Message = "Price alert: Copper exceeded ZMW 8,500",
                    Severity = "Info",
                    Timestamp = DateTime.Now.AddMinutes(-30),
                    IsRead = false
                },
                new Alert
                {
                    AlertId = "ALT-003",
                    Message = "Order ORD-2024-001 partially filled: 200/500 tonnes",
                    Severity = "Info",
                    Timestamp = DateTime.Now.AddHours(-2),
                    IsRead = true
                }
            };
        }

        public async Task<List<Trade>> GetRecentTradesAsync()
        {
            await Task.Delay(100);
            return new List<Trade>
            {
                new Trade
                {
                    TradeId = "TRD-2024-0045",
                    OrderId = "ORD-2024-001",
                    Instrument = "COPPER-JAN24",
                    Direction = "Buy",
                    Quantity = 200,
                    Price = 8500.00m,
                    TotalValue = 1700000.00m,
                    Fees = 8500.00m,
                    NetValue = 1708500.00m,
                    TradeTime = DateTime.Now.AddHours(-2),
                    Counterparty = "Mopani Copper Mines",
                    ClearingReference = "CLR-2024-0045",
                    SettlementDate = DateTime.Now.AddDays(2),
                    Status = "Executed",
                    Venue = "ZMX"
                },
                new Trade
                {
                    TradeId = "TRD-2024-0044",
                    OrderId = "ORD-2024-003",
                    Instrument = "EMERALD-FEB24",
                    Direction = "Sell",
                    Quantity = 50,
                    Price = 124000.00m,
                    TotalValue = 6200000.00m,
                    Fees = 31000.00m,
                    NetValue = 6169000.00m,
                    TradeTime = DateTime.Now.AddHours(-5),
                    Counterparty = "Gemfields Zambia",
                    ClearingReference = "CLR-2024-0044",
                    SettlementDate = DateTime.Now.AddDays(2),
                    Status = "Settled",
                    Venue = "ZMX"
                }
            };
        }

        public async Task<List<NewsItem>> GetNewsAsync()
        {
            await Task.Delay(100);
            return new List<NewsItem>
            {
                new NewsItem
                {
                    NewsId = "NEWS-001",
                    Headline = "Copper prices surge on supply concerns",
                    Summary = "Global copper prices reached a 3-month high as major mines in Chile announced production cuts...",
                    Source = "Mining Weekly",
                    PublishedTime = DateTime.Now.AddHours(-1),
                    Category = "Market"
                },
                new NewsItem
                {
                    NewsId = "NEWS-002",
                    Headline = "Zambian government announces new mining tax reforms",
                    Summary = "The Ministry of Mines has proposed changes to royalty rates for precious metals and base metals...",
                    Source = "Lusaka Times",
                    PublishedTime = DateTime.Now.AddHours(-3),
                    Category = "Regulatory"
                },
                new NewsItem
                {
                    NewsId = "NEWS-003",
                    Headline = "Emerald demand increases in Asian markets",
                    Summary = "Strong demand from China and India drives premium emerald prices higher this quarter...",
                    Source = "Gemstone News",
                    PublishedTime = DateTime.Now.AddHours(-6),
                    Category = "Market"
                }
            };
        }
    }
}
