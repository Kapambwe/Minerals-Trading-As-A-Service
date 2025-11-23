using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public class MockWarrantService : IWarrantService
    {
        public async Task<Warrant?> GetWarrantByIdAsync(string warrantId)
        {
            await Task.Delay(500);
            if (warrantId == "W123")
            {
                return new Warrant
                {
                    WarrantNumber = "W123",
                    MetalType = "Gold",
                    TradeId = "123",
                    WarehouseName = "Global Storage Inc.",
                    Quantity = 100,
                    CurrentOwner = "Acme Corp",
                    PreviousOwner = "Globex Inc.",
                    IssueDate = DateTime.Now.AddMonths(-1),
                    TransferDate = DateTime.Now.AddDays(-10),
                    QualityGrade = "24K",
                    LotNumber = "LOT-G-001",
                    Status = "Active",
                    IsActive = true,
                    Notes = "Gold warrant for trade 123."
                };
            }
            return null;
        }

        public async Task<IEnumerable<Warrant>> GetAllWarrantsAsync()
        {
            await Task.Delay(500);
            return new List<Warrant>
            {
                new Warrant
                {
                    WarrantNumber = "W123",
                    MetalType = "Gold",
                    TradeId = "123",
                    WarehouseName = "Global Storage Inc.",
                    Quantity = 100,
                    CurrentOwner = "Acme Corp",
                    PreviousOwner = "Globex Inc.",
                    IssueDate = DateTime.Now.AddMonths(-1),
                    TransferDate = DateTime.Now.AddDays(-10),
                    QualityGrade = "24K",
                    LotNumber = "LOT-G-001",
                    Status = "Active",
                    IsActive = true,
                    Notes = "Gold warrant for trade 123."
                },
                new Warrant
                {
                    WarrantNumber = "W124",
                    MetalType = "Copper",
                    TradeId = "124",
                    WarehouseName = "Copper Storage Ltd.",
                    Quantity = 500,
                    CurrentOwner = "Wayne Enterprises",
                    PreviousOwner = "LexCorp",
                    IssueDate = DateTime.Now.AddMonths(-2),
                    TransferDate = DateTime.Now.AddDays(-20),
                    QualityGrade = "Grade A",
                    LotNumber = "LOT-C-002",
                    Status = "Transferred",
                    IsActive = false,
                    Notes = "Copper warrant for trade 124."
                }
            };
        }
    }
}