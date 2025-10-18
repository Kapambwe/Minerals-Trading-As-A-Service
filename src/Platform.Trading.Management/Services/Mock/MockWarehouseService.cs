using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockWarehouseService : IWarehouseService
{
    private readonly List<Warehouse> _warehouses;

    public MockWarehouseService()
    {
        _warehouses = new List<Warehouse>
        {
            new Warehouse
            {
                Id = "WH001",
                WarehouseCode = "ZME-NDO-001",
                OperatorName = "Ndola Metals Storage Ltd.",
                Location = "Industrial Area",
                City = "Ndola",
                Country = "Zambia",
                StorageCapacity = 50000,
                CurrentStock = 32000,
                IsLMEApproved = true,
                ApprovalDate = DateTime.Now.AddYears(-3),
                SecurityLevel = "High",
                HasWeighingSystem = true,
                HasQualityControl = true,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Forklifts, cranes, and conveyor systems.",
                ComplianceNotes = "Fully compliant with LME standards.",
                AgreesToLMERules = true,
                Status = "Active",
                LastInspectionDate = DateTime.Now.AddMonths(-6),
                Notes = "Primary Copperbelt storage facility"
            },
            new Warehouse
            {
                Id = "WH002",
                WarehouseCode = "ZME-KIT-002",
                OperatorName = "Kitwe Metals Depot",
                Location = "Copperbelt Industrial Zone",
                City = "Kitwe",
                Country = "Zambia",
                StorageCapacity = 75000,
                CurrentStock = 45000,
                IsLMEApproved = true,
                ApprovalDate = DateTime.Now.AddYears(-5),
                SecurityLevel = "High",
                HasWeighingSystem = true,
                HasQualityControl = true,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Heavy-duty forklifts, automated stacking systems.",
                ComplianceNotes = "Regularly audited, no non-conformities.",
                AgreesToLMERules = true,
                Status = "Active",
                LastInspectionDate = DateTime.Now.AddMonths(-4),
                Notes = "Largest ZME warehouse in the Copperbelt"
            },
            new Warehouse
            {
                Id = "WH003",
                WarehouseCode = "ZME-LUS-003",
                OperatorName = "Lusaka Central Storage",
                Location = "Heavy Industrial Area",
                City = "Lusaka",
                Country = "Zambia",
                StorageCapacity = 40000,
                CurrentStock = 18500,
                IsLMEApproved = true,
                ApprovalDate = DateTime.Now.AddYears(-2),
                SecurityLevel = "High",
                HasWeighingSystem = true,
                HasQualityControl = true,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Standard warehouse equipment, well-maintained.",
                ComplianceNotes = "Meets all LME operational requirements.",
                AgreesToLMERules = true,
                Status = "Active",
                LastInspectionDate = DateTime.Now.AddMonths(-3),
                Notes = "Capital city strategic warehouse"
            },
            new Warehouse
            {
                Id = "WH004",
                WarehouseCode = "ZME-CHI-004",
                OperatorName = "Chingola Metals Warehouse",
                Location = "Nchanga Mining District",
                City = "Chingola",
                Country = "Zambia",
                StorageCapacity = 30000,
                CurrentStock = 22000,
                IsLMEApproved = true,
                ApprovalDate = DateTime.Now.AddYears(-4),
                SecurityLevel = "High",
                HasWeighingSystem = true,
                HasQualityControl = true,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Specialized equipment for refined metals.",
                ComplianceNotes = "Excellent record of LME compliance.",
                AgreesToLMERules = true,
                Status = "Active",
                LastInspectionDate = DateTime.Now.AddMonths(-5),
                Notes = "Specialized in refined copper storage"
            },
            new Warehouse
            {
                Id = "WH005",
                WarehouseCode = "ZME-SOL-005",
                OperatorName = "Solwezi Commodities Warehouse",
                Location = "Northwestern Mining Zone",
                City = "Solwezi",
                Country = "Zambia",
                StorageCapacity = 35000,
                CurrentStock = 15000,
                IsLMEApproved = true,
                ApprovalDate = DateTime.Now.AddYears(-1),
                SecurityLevel = "High",
                HasWeighingSystem = true,
                HasQualityControl = true,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Modern logistics equipment.",
                ComplianceNotes = "Newest LME approved facility in the region.",
                AgreesToLMERules = true,
                Status = "Active",
                LastInspectionDate = DateTime.Now.AddMonths(-2),
                Notes = "Northwestern Province primary facility"
            },
            new Warehouse
            {
                Id = "WH006",
                WarehouseCode = "ZME-KAL-006",
                OperatorName = "Kalulushi Metal Exchange Warehouse",
                Location = "Copperbelt Mining District",
                City = "Kalulushi",
                Country = "Zambia",
                StorageCapacity = 60000,
                CurrentStock = 48000,
                IsLMEApproved = true,
                ApprovalDate = DateTime.Now.AddYears(-2),
                SecurityLevel = "High",
                HasWeighingSystem = true,
                HasQualityControl = true,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Large capacity cranes and automated systems.",
                ComplianceNotes = "Consistently meets LME standards.",
                AgreesToLMERules = true,
                Status = "Active",
                LastInspectionDate = DateTime.Now.AddMonths(-3),
                Notes = "Largest capacity in the Copperbelt region"
            },
            new Warehouse
            {
                Id = "WH007",
                WarehouseCode = "ZME-MUF-007",
                OperatorName = "Mufulira Metals Storage",
                Location = "Central Mining District",
                City = "Mufulira",
                Country = "Zambia",
                StorageCapacity = 45000,
                CurrentStock = 28000,
                IsLMEApproved = true,
                ApprovalDate = DateTime.Now.AddYears(-6),
                SecurityLevel = "High",
                HasWeighingSystem = true,
                HasQualityControl = true,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Well-established equipment, regularly serviced.",
                ComplianceNotes = "Long history of LME approval.",
                AgreesToLMERules = true,
                Status = "Active",
                LastInspectionDate = DateTime.Now.AddMonths(-7),
                Notes = "Historical ZME warehouse location"
            },
            new Warehouse
            {
                Id = "WH008",
                WarehouseCode = "ZME-LIV-008",
                OperatorName = "Livingstone Border Depot",
                Location = "Border Trade Zone",
                City = "Livingstone",
                Country = "Zambia",
                StorageCapacity = 20000,
                CurrentStock = 8500,
                IsLMEApproved = false,
                HasFinancialStabilityProof = true,
                HandlingEquipmentDetails = "Basic forklifts, manual handling.",
                ComplianceNotes = "Awaiting quality control system upgrade.",
                AgreesToLMERules = false,
                ApprovalDate = DateTime.MinValue,
                SecurityLevel = "Medium",
                HasWeighingSystem = true,
                HasQualityControl = false,
                Status = "Pending Approval",
                LastInspectionDate = DateTime.Now.AddMonths(-1),
                Notes = "Awaiting ZME approval - quality control system upgrade required"
            }
        };
    }

    public Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        return Task.FromResult<IEnumerable<Warehouse>>(_warehouses);
    }

    public Task<Warehouse?> GetWarehouseByIdAsync(string id)
    {
        var warehouse = _warehouses.FirstOrDefault(w => w.Id == id);
        return Task.FromResult(warehouse);
    }

    public Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
    {
        warehouse.Id = $"WH{_warehouses.Count + 1:D3}";
        warehouse.WarehouseCode = $"ZME-{warehouse.City.Substring(0, 3).ToUpper()}-{_warehouses.Count + 1:D3}";
        _warehouses.Add(warehouse);
        return Task.FromResult(warehouse);
    }

    public Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse)
    {
        var existingWarehouse = _warehouses.FirstOrDefault(w => w.Id == warehouse.Id);
        if (existingWarehouse != null)
        {
            var index = _warehouses.IndexOf(existingWarehouse);
            _warehouses[index] = warehouse;
        }
        return Task.FromResult(warehouse);
    }

    public Task<bool> DeleteWarehouseAsync(string id)
    {
        var warehouse = _warehouses.FirstOrDefault(w => w.Id == id);
        if (warehouse != null)
        {
            _warehouses.Remove(warehouse);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<IEnumerable<Warehouse>> GetApprovedWarehousesAsync()
    {
        var approved = _warehouses.Where(w => w.IsLMEApproved);
        return Task.FromResult<IEnumerable<Warehouse>>(approved);
    }

    public Task<Warehouse> ApproveWarehouseAsync(string warehouseId)
    {
        var warehouse = _warehouses.FirstOrDefault(w => w.Id == warehouseId);
        if (warehouse != null)
        {
            warehouse.IsLMEApproved = true;
            warehouse.ApprovalDate = DateTime.Now;
            warehouse.Status = "Active";
            warehouse.HasFinancialStabilityProof = true; // Assume approved means these are met
            warehouse.AgreesToLMERules = true; // Assume approved means these are met
            warehouse.ComplianceNotes = "Approved after full LME compliance review.";
        }
        return Task.FromResult(warehouse!); // Return the updated warehouse
    }
}