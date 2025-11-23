using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public class MockMineralListingService : IMineralListingService
    {
        public async Task<IEnumerable<MineralListing>> GetAllMineralListingsAsync()
        {
            await Task.Delay(500);
            return new List<MineralListing>
            {
                new MineralListing { Id = "ML001", MetalType = "Silver", Quantity = 500, PricePerTon = 800, Status = "Available" },
                new MineralListing { Id = "ML002", MetalType = "Platinum", Quantity = 50, PricePerTon = 25000, Status = "Available" },
                new MineralListing { Id = "ML003", MetalType = "Palladium", Quantity = 20, PricePerTon = 30000, Status = "Sold" }
            };
        }
    }
}