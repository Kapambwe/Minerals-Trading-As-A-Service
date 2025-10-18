using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockInspectionService : IInspectionService
{
    private readonly List<Inspection> _inspections;

    public MockInspectionService()
    {
        _inspections = new List<Inspection>
        {
            new Inspection
            {
                Id = "INS001",
                WarehouseId = "WH001",
                WarehouseCode = "ZME-NDO-001",
                InspectionDate = DateTime.Now.AddMonths(-1),
                InspectorName = "John Doe",
                SiteInspectionPassed = true,
                WeighingSystemVerified = true,
                QualityControlVerified = true,
                ReportingSystemTested = true,
                NeutralityEnsured = true,
                OverallOutcome = "Passed",
                Findings = "All systems operational and compliant.",
                Recommendations = "None.",
                NextInspectionDate = DateTime.Now.AddMonths(5)
            },
            new Inspection
            {
                Id = "INS002",
                WarehouseId = "WH008",
                WarehouseCode = "ZME-LIV-008",
                InspectionDate = DateTime.Now.AddDays(-15),
                InspectorName = "Jane Smith",
                SiteInspectionPassed = true,
                WeighingSystemVerified = true,
                QualityControlVerified = false, // Failed
                ReportingSystemTested = true,
                NeutralityEnsured = true,
                OverallOutcome = "Conditional Pass",
                Findings = "Quality control system requires upgrade. Minor discrepancies in labeling observed.",
                Recommendations = "Upgrade QC system within 3 months. Implement regular staff training on labeling standards.",
                NextInspectionDate = DateTime.Now.AddMonths(3)
            },
            new Inspection
            {
                Id = "INS003",
                WarehouseId = "WH002",
                WarehouseCode = "ZME-KIT-002",
                InspectionDate = DateTime.Now.AddMonths(-2),
                InspectorName = "Alice Johnson",
                SiteInspectionPassed = true,
                WeighingSystemVerified = true,
                QualityControlVerified = true,
                ReportingSystemTested = true,
                NeutralityEnsured = true,
                OverallOutcome = "Passed",
                Findings = "Exemplary compliance.",
                Recommendations = "None.",
                NextInspectionDate = DateTime.Now.AddMonths(4)
            }
        };
    }

    public Task<IEnumerable<Inspection>> GetAllInspectionsAsync()
    {
        return Task.FromResult<IEnumerable<Inspection>>(_inspections);
    }

    public Task<Inspection?> GetInspectionByIdAsync(string id)
    {
        var inspection = _inspections.FirstOrDefault(i => i.Id == id);
        return Task.FromResult(inspection);
    }

    public Task<Inspection> CreateInspectionAsync(Inspection inspection)
    {
        inspection.Id = $"INS{_inspections.Count + 1:D3}";
        inspection.InspectionDate = DateTime.Now;
        _inspections.Add(inspection);
        return Task.FromResult(inspection);
    }

    public Task<Inspection> UpdateInspectionAsync(Inspection inspection)
    {
        var existingInspection = _inspections.FirstOrDefault(i => i.Id == inspection.Id);
        if (existingInspection != null)
        {
            var index = _inspections.IndexOf(existingInspection);
            _inspections[index] = inspection;
        }
        return Task.FromResult(inspection);
    }

    public Task<bool> DeleteInspectionAsync(string id)
    {
        var inspection = _inspections.FirstOrDefault(i => i.Id == id);
        if (inspection != null)
        {
            _inspections.Remove(inspection);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<IEnumerable<Inspection>> GetInspectionsByWarehouseIdAsync(string warehouseId)
    {
        var warehouseInspections = _inspections.Where(i => i.WarehouseId == warehouseId);
        return Task.FromResult<IEnumerable<Inspection>>(warehouseInspections);
    }
}
