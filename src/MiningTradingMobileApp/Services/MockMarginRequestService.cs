using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public class MockMarginRequestService : IMarginRequestService
    {
        public async Task<IEnumerable<MarginRequest>> GetMarginRequestsForTradeAsync(string tradeId)
        {
            await Task.Delay(500);
            if (tradeId == "123")
            {
                return new List<MarginRequest>
                {
                    new MarginRequest { RequestedAmount = 1000, RequestDate = DateTime.Now.AddDays(-5), Status = "Pending" },
                    new MarginRequest { RequestedAmount = 500, RequestDate = DateTime.Now.AddDays(-10), Status = "Approved" }
                };
            }
            return new List<MarginRequest>();
        }
    }
}