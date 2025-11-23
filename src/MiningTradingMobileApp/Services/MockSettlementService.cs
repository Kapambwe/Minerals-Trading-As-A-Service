using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public class MockSettlementService : ISettlementService
    {
        public async Task<IEnumerable<Settlement>> GetAllSettlementsAsync()
        {
            await Task.Delay(500);
            return new List<Settlement>
            {
                new Settlement { Id = "S001", SettlementDate = DateTime.Now.AddDays(-10), TradeId = "123", SettlementAmount = 5000000m, SettlementType = "Final", Status = "Completed" },
                new Settlement { Id = "S002", SettlementDate = DateTime.Now.AddDays(-5), TradeId = "124", SettlementAmount = 2500000m, SettlementType = "Partial", Status = "Pending" }
            };
        }
    }
}