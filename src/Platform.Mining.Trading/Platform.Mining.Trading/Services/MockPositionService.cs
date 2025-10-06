using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockPositionService : IPositionService
    {
        private List<Position> _positions = new();

        public MockPositionService()
        {
            InitializeSamplePositions();
        }

        private void InitializeSamplePositions()
        {
            _positions = new List<Position>
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
                },
                new Position
                {
                    Instrument = "EMERALD-FEB24",
                    MineralType = "Emerald",
                    Account = "ACC-001",
                    OpenQuantity = 200,
                    AveragePrice = 120000.00m,
                    CurrentPrice = 125000.00m,
                    MarkToMarket = 25000000.00m,
                    UnrealizedPnL = 1000000.00m,
                    RealizedPnL = 250000.00m,
                    TotalPnL = 1250000.00m,
                    RequiredMargin = 150000.00m,
                    CollateralPosted = 200000.00m,
                    LastUpdated = DateTime.Now.AddMinutes(-1)
                }
            };
        }

        public async Task<List<Position>> GetPositionsAsync()
        {
            await Task.Delay(100);
            return _positions;
        }

        public async Task<Position> GetPositionByInstrumentAsync(string instrument)
        {
            await Task.Delay(50);
            return _positions.FirstOrDefault(p => p.Instrument == instrument) ?? new Position();
        }

        public async Task<decimal> GetTotalPnLAsync()
        {
            await Task.Delay(50);
            return _positions.Sum(p => p.TotalPnL);
        }

        public async Task<bool> TransferPositionAsync(string instrument, string toAccount, decimal quantity)
        {
            await Task.Delay(150);
            var position = _positions.FirstOrDefault(p => p.Instrument == instrument);
            
            if (position != null && position.OpenQuantity >= quantity)
            {
                position.OpenQuantity -= quantity;
                return true;
            }
            
            return false;
        }
    }
}
