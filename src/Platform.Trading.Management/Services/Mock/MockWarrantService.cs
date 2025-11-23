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
                Id = "11223344-5566-7788-9900-aabbccddeeff",
                WarrantNumber = "WRN-2025-001",
                TradeId = "a1b2c3d4-e5f6-7890-1234-567890abcdef",
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
                Id = "22334455-6677-8899-0011-aabbccddeeff",
                WarrantNumber = "WRN-2025-002",
                TradeId = "b2c3d4e5-f6a7-8901-2345-67890abcdef0",
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
                Id = "33445566-7788-9900-1122-aabbccddeeff",
                WarrantNumber = "WRN-2025-003",
                TradeId = "c3d4e5f6-a7b8-9012-3456-7890abcdef01",
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
                Id = "44556677-8899-0011-2233-aabbccddeeff",
                WarrantNumber = "WRN-2025-004",
                TradeId = "f6a7b8c9-d0e1-2345-6789-0abcdef01234",
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
                Id = "55667788-9900-1122-3344-aabbccddeeff",
                WarrantNumber = "WRN-2025-005",
                TradeId = "a0b1c2d3-e4f5-6789-0123-456789abcdef",
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
        warrant.Id = Guid.NewGuid().ToString();
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

