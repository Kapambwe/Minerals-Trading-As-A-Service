using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockMarketDataService : IMarketDataService
    {
        private readonly Random _random = new();

        public async Task<MarketDepth> GetMarketDepthAsync(string instrument)
        {
            await Task.Delay(100);
            
            var basePrice = instrument.Contains("COPPER") ? 8500m :
                           instrument.Contains("EMERALD") ? 125000m :
                           instrument.Contains("COBALT") ? 34500m : 10000m;

            return new MarketDepth
            {
                Instrument = instrument,
                LastTradePrice = basePrice,
                LastTradeQuantity = 50,
                LastTradeTime = DateTime.Now.AddMinutes(-2),
                Bids = new List<PriceLevel>
                {
                    new PriceLevel { Price = basePrice - 50, Quantity = 150, OrderCount = 3 },
                    new PriceLevel { Price = basePrice - 100, Quantity = 300, OrderCount = 5 },
                    new PriceLevel { Price = basePrice - 150, Quantity = 500, OrderCount = 8 },
                    new PriceLevel { Price = basePrice - 200, Quantity = 750, OrderCount = 12 },
                    new PriceLevel { Price = basePrice - 250, Quantity = 1000, OrderCount = 15 }
                },
                Asks = new List<PriceLevel>
                {
                    new PriceLevel { Price = basePrice + 50, Quantity = 200, OrderCount = 4 },
                    new PriceLevel { Price = basePrice + 100, Quantity = 350, OrderCount = 6 },
                    new PriceLevel { Price = basePrice + 150, Quantity = 600, OrderCount = 9 },
                    new PriceLevel { Price = basePrice + 200, Quantity = 800, OrderCount = 13 },
                    new PriceLevel { Price = basePrice + 250, Quantity = 1100, OrderCount = 16 }
                }
            };
        }

        public async Task<List<Trade>> GetLastTradesAsync(string instrument, int count = 50)
        {
            await Task.Delay(100);
            
            var trades = new List<Trade>();
            var basePrice = instrument.Contains("COPPER") ? 8500m :
                           instrument.Contains("EMERALD") ? 125000m :
                           instrument.Contains("COBALT") ? 34500m : 10000m;

            for (int i = 0; i < Math.Min(count, 20); i++)
            {
                var price = basePrice + (_random.Next(-100, 101));
                var qty = _random.Next(10, 200);
                trades.Add(new Trade
                {
                    TradeId = $"TRD-2024-{1000 + i:D4}",
                    OrderId = $"ORD-2024-{500 + i:D3}",
                    Instrument = instrument,
                    Direction = _random.Next(2) == 0 ? "Buy" : "Sell",
                    Quantity = qty,
                    Price = price,
                    TotalValue = price * qty,
                    Fees = price * qty * 0.005m,
                    NetValue = price * qty * 1.005m,
                    TradeTime = DateTime.Now.AddMinutes(-i * 5),
                    Counterparty = $"Counterparty-{_random.Next(1, 10)}",
                    ClearingReference = $"CLR-2024-{1000 + i:D4}",
                    SettlementDate = DateTime.Now.AddDays(2),
                    Status = "Executed",
                    Venue = "ZMX"
                });
            }

            return trades.OrderByDescending(t => t.TradeTime).ToList();
        }

        public async Task<decimal> GetCurrentPriceAsync(string instrument)
        {
            await Task.Delay(50);
            
            return instrument.Contains("COPPER") ? 8500m :
                   instrument.Contains("EMERALD") ? 125000m :
                   instrument.Contains("COBALT") ? 34500m : 10000m;
        }

        public async Task<List<string>> GetInstrumentsAsync()
        {
            await Task.Delay(50);
            return new List<string>
            {
                "COPPER-JAN24",
                "COPPER-FEB24",
                "COPPER-MAR24",
                "EMERALD-JAN24",
                "EMERALD-FEB24",
                "EMERALD-MAR24",
                "COBALT-JAN24",
                "COBALT-FEB24",
                "COBALT-MAR24",
                "GOLD-JAN24",
                "GOLD-FEB24",
                "MANGANESE-JAN24"
            };
        }
    }
}
