using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockWarrantService : IWarrantService
{
    private readonly List<Warrant> _warrants;

    public MockWarrantService()
    {
        _warrants = new List<Warrant>
        {
            new Warrant
            {
                Id = "WRT001",
                WarrantNumber = "WRN-2025-001",
                TradeId = "TRD001",
                TradeNumber = "ZME-2025-001",
                WarehouseId = "WH001",
                WarehouseName = "Ndola Metals Storage Ltd.",
                MetalType = MetalType.Copper,
                Quantity = 250,
                CurrentOwner = "Konkola Copper Mines",
                PreviousOwner = "First Quantum Minerals Zambia",
                IssueDate = DateTime.Now.AddDays(-28),
                TransferDate = DateTime.Now.AddDays(-28),
                QualityGrade = "Grade A",
                LotNumber = "CU-2025-SIN-001",
                IsActive = true,
                Status = "Active",
                Notes = "High-grade copper cathodes"
            },
            new Warrant
            {
                Id = "WRT002",
                WarrantNumber = "WRN-2025-002",
                TradeId = "TRD002",
                TradeNumber = "ZME-2025-002",
                WarehouseId = "WH002",
                WarehouseName = "Kitwe Metals Depot",
                MetalType = MetalType.Aluminum,
                Quantity = 500,
                CurrentOwner = "Mopani Copper Mines",
                PreviousOwner = "Kansanshi Mining",
                IssueDate = DateTime.Now.AddDays(-23),
                TransferDate = DateTime.Now.AddDays(-23),
                QualityGrade = "P1020A",
                LotNumber = "AL-2025-RTM-002",
                IsActive = true,
                Status = "Active",
                Notes = "Primary aluminum ingots"
            },
            new Warrant
            {
                Id = "WRT003",
                WarrantNumber = "WRN-2025-003",
                TradeId = "TRD003",
                TradeNumber = "ZME-2025-003",
                WarehouseId = "WH004",
                WarehouseName = "Chingola Metals Warehouse",
                MetalType = MetalType.Zinc,
                Quantity = 300,
                CurrentOwner = "ZCCM Investments Holdings",
                PreviousOwner = "Barrick Lumwana Mining",
                IssueDate = DateTime.Now.AddDays(-18),
                TransferDate = DateTime.Now.AddDays(-18),
                QualityGrade = "SHG",
                LotNumber = "ZN-2025-BRE-003",
                IsActive = true,
                Status = "Active",
                Notes = "Special High Grade zinc"
            },
            new Warrant
            {
                Id = "WRT004",
                WarrantNumber = "WRN-2025-004",
                TradeId = "TRD006",
                TradeNumber = "ZME-2025-006",
                WarehouseId = "WH007",
                WarehouseName = "Mufulira Metals Storage",
                MetalType = MetalType.Lead,
                Quantity = 200,
                CurrentOwner = "Kafue Copper Smelter",
                PreviousOwner = "Mufulira Mine",
                IssueDate = DateTime.Now.AddDays(-58),
                TransferDate = DateTime.Now.AddDays(-5),
                QualityGrade = "Grade A",
                LotNumber = "PB-2024-ANT-015",
                IsActive = false,
                Status = "Delivered",
                Notes = "Physical delivery completed"
            },
            new Warrant
            {
                Id = "WRT005",
                WarrantNumber = "WRN-2025-005",
                TradeId = "TRD007",
                TradeNumber = "ZME-2025-007",
                WarehouseId = "WH006",
                WarehouseName = "Kalulushi Metal Exchange Warehouse",
                MetalType = MetalType.Tin,
                Quantity = 75,
                CurrentOwner = "NFC Africa Mining",
                PreviousOwner = "Sentinel Mining Zambia",
                IssueDate = DateTime.Now.AddDays(-3),
                TransferDate = DateTime.Now.AddDays(-3),
                QualityGrade = "Grade A",
                LotNumber = "SN-2025-SHG-005",
                IsActive = true,
                Status = "Active",
                Notes = "99.9% pure tin ingots"
            }
        };
    }

    public Task<IEnumerable<Warrant>> GetAllWarrantsAsync()
    {
        return Task.FromResult<IEnumerable<Warrant>>(_warrants);
    }

    public Task<Warrant?> GetWarrantByIdAsync(string id)
    {
        var warrant = _warrants.FirstOrDefault(w => w.Id == id);
        return Task.FromResult(warrant);
    }

    public Task<IEnumerable<Warrant>> GetWarrantsByTradeIdAsync(string tradeId)
    {
        var warrants = _warrants.Where(w => w.TradeId == tradeId);
        return Task.FromResult<IEnumerable<Warrant>>(warrants);
    }

    public Task<Warrant> CreateWarrantAsync(Warrant warrant)
    {
        warrant.Id = $"WRT{_warrants.Count + 1:D3}";
        warrant.WarrantNumber = $"WRN-2025-{_warrants.Count + 1:D3}";
        _warrants.Add(warrant);
        return Task.FromResult(warrant);
    }

    public Task<Warrant> UpdateWarrantAsync(Warrant warrant)
    {
        var existingWarrant = _warrants.FirstOrDefault(w => w.Id == warrant.Id);
        if (existingWarrant != null)
        {
            var index = _warrants.IndexOf(existingWarrant);
            _warrants[index] = warrant;
        }
        return Task.FromResult(warrant);
    }

    public Task<bool> DeleteWarrantAsync(string id)
    {
        var warrant = _warrants.FirstOrDefault(w => w.Id == id);
        if (warrant != null)
        {
            _warrants.Remove(warrant);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<Warrant> TransferWarrantAsync(string warrantId, string newOwner)
    {
        var warrant = _warrants.FirstOrDefault(w => w.Id == warrantId);
        if (warrant != null)
        {
            warrant.PreviousOwner = warrant.CurrentOwner;
            warrant.CurrentOwner = newOwner;
            warrant.TransferDate = DateTime.Now;
        }
        return Task.FromResult(warrant!);
    }
}
