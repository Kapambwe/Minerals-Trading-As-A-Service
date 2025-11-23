using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public class MockMarginService : IMarginService
    {
        public async Task<IEnumerable<Margin>> GetMarginsByTradeIdAsync(string tradeId)
        {
            await Task.Delay(500);
            if (tradeId == "123")
            {
                return new List<Margin>
                {
                    new Margin { PartyName = "Buyer", InitialMargin = 10000, VariationMargin = 500, TotalMargin = 10500, Status = "Active" },
                    new Margin { PartyName = "Seller", InitialMargin = 10000, VariationMargin = -200, TotalMargin = 9800, Status = "Active" }
                };
            }
            return new List<Margin>();
        }
    }
}