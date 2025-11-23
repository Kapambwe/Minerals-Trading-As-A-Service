using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public class MockTradeService : ITradeService
    {
        public async Task<Trade?> GetTradeByIdAsync(string tradeId)
        {
            // Simulate API call delay
            await Task.Delay(500);

            // Return a mock trade for demonstration
            if (tradeId == "123")
            {
                return new Trade
                {
                    TradeNumber = "123",
                    MetalType = "Gold",
                    BuyerName = "Acme Corp",
                    SellerName = "Globex Inc.",
                    Quantity = 100,
                    PricePerTon = 50000m,
                    TotalValue = 5000000m,
                    DeliveryDate = DateTime.Now.AddDays(30),
                    Status = "Pending",
                    IsNovated = false,
                    Notes = "Standard gold trade."
                };
            }
            return null;
        }

        public async Task<IEnumerable<Trade>> GetAllTradesAsync()
        {
            await Task.Delay(500);
            return new List<Trade>
            {
                new Trade
                {
                    TradeNumber = "123",
                    MetalType = "Gold",
                    BuyerName = "Acme Corp",
                    SellerName = "Globex Inc.",
                    Quantity = 100,
                    PricePerTon = 50000m,
                    TotalValue = 5000000m,
                    DeliveryDate = DateTime.Now.AddDays(30),
                    Status = "Pending",
                    IsNovated = false,
                    Notes = "Standard gold trade."
                },
                new Trade
                {
                    TradeNumber = "124",
                    MetalType = "Copper",
                    BuyerName = "Wayne Enterprises",
                    SellerName = "LexCorp",
                    Quantity = 500,
                    PricePerTon = 10000m,
                    TotalValue = 5000000m,
                    DeliveryDate = DateTime.Now.AddDays(60),
                    Status = "Completed",
                    IsNovated = true,
                    NovationDate = DateTime.Now.AddDays(10),
                    ClearingHouse = "Global Clearing",
                    Notes = "Copper trade with novation."
                }
            };
        }
    }
}