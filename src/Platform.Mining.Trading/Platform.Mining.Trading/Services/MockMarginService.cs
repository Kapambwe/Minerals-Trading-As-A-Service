using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockMarginService : IMarginService
    {
        private List<Collateral> _collaterals = new();

        public MockMarginService()
        {
            InitializeSampleCollateral();
        }

        private void InitializeSampleCollateral()
        {
            _collaterals = new List<Collateral>
            {
                new Collateral
                {
                    CollateralId = "COL-001",
                    CollateralType = "Cash",
                    Asset = "ZMW",
                    Quantity = 500000,
                    MarketValue = 500000,
                    HaircutPercentage = 0,
                    EligibleValue = 500000,
                    Status = "Posted",
                    PostedDate = DateTime.Now.AddDays(-10)
                },
                new Collateral
                {
                    CollateralId = "COL-002",
                    CollateralType = "Cash",
                    Asset = "USD",
                    Quantity = 100000,
                    MarketValue = 100000,
                    HaircutPercentage = 0,
                    EligibleValue = 100000,
                    Status = "Posted",
                    PostedDate = DateTime.Now.AddDays(-5)
                },
                new Collateral
                {
                    CollateralId = "COL-003",
                    CollateralType = "Securities",
                    Asset = "Zambian Government Bonds",
                    Quantity = 100000,
                    MarketValue = 100000,
                    HaircutPercentage = 10,
                    EligibleValue = 90000,
                    Status = "Posted",
                    PostedDate = DateTime.Now.AddDays(-15)
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

        public async Task<List<Collateral>> GetCollateralAsync()
        {
            await Task.Delay(100);
            return _collaterals;
        }

        public async Task<bool> PostCollateralAsync(Collateral collateral)
        {
            await Task.Delay(200);
            collateral.CollateralId = $"COL-{_collaterals.Count + 1:D3}";
            collateral.PostedDate = DateTime.Now;
            collateral.Status = "Posted";
            collateral.EligibleValue = collateral.MarketValue * (1 - collateral.HaircutPercentage / 100);
            _collaterals.Add(collateral);
            return true;
        }

        public async Task<bool> WithdrawCollateralAsync(string collateralId)
        {
            await Task.Delay(150);
            var collateral = _collaterals.FirstOrDefault(c => c.CollateralId == collateralId);
            
            if (collateral != null && collateral.Status == "Posted")
            {
                collateral.Status = "Withdrawn";
                return true;
            }
            
            return false;
        }

        public async Task<bool> AcceptMarginCallAsync(string marginCallId)
        {
            await Task.Delay(100);
            // Simulate accepting a margin call
            return true;
        }

        public async Task<List<string>> GetEligibleCollateralTypesAsync()
        {
            await Task.Delay(50);
            return new List<string>
            {
                "Cash - ZMW",
                "Cash - USD",
                "Cash - EUR",
                "Zambian Government Bonds",
                "Corporate Bonds (Investment Grade)",
                "Precious Metals (Physical)",
                "Warehouse Receipts"
            };
        }
    }
}
