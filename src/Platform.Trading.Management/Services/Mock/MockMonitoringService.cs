using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockMonitoringService : IMonitoringService
{
    private readonly List<MonitoringRecord> _records;

    public MockMonitoringService()
    {
        _records = new List<MonitoringRecord>
        {
            new MonitoringRecord
            {
                Id = "MON001",
                WarehouseId = "WH001",
                WarehouseCode = "ZME-NDO-001",
                RecordDate = DateTime.Now.AddDays(-1),
                RecordType = "Daily Stock Report",
                ReportedBy = "Ndola Metals Storage Ltd.",
                ReportedStock = 32000,
                ActualStock = 32000,
                StockDiscrepancy = 0,
                StockReportSubmitted = true,
                OverallStatus = "Compliant",
                Notes = "Daily stock report submitted on time."
            },
            new MonitoringRecord
            {
                Id = "MON002",
                WarehouseId = "WH001",
                WarehouseCode = "ZME-NDO-001",
                RecordDate = DateTime.Now.AddMonths(-3),
                RecordType = "Audit Report",
                ReportedBy = "Independent Auditor Co.",
                AuditorName = "Audit Firm A",
                AuditOutcome = "Compliant",
                IndependentAuditAllowed = true,
                OverallStatus = "Compliant",
                Notes = "Annual audit completed with no major findings."
            },
            new MonitoringRecord
            {
                Id = "MON003",
                WarehouseId = "WH008",
                WarehouseCode = "ZME-LIV-008",
                RecordDate = DateTime.Now.AddDays(-2),
                RecordType = "Daily Stock Report",
                ReportedBy = "Livingstone Border Depot",
                ReportedStock = 8500,
                ActualStock = 8500,
                StockDiscrepancy = 0,
                StockReportSubmitted = true,
                OverallStatus = "Compliant",
                Notes = "Daily stock report submitted."
            },
            new MonitoringRecord
            {
                Id = "MON004",
                WarehouseId = "WH008",
                WarehouseCode = "ZME-LIV-008",
                RecordDate = DateTime.Now.AddDays(-10),
                RecordType = "Compliance Check",
                ReportedBy = "ZME Compliance Officer",
                RulesOnLoadingUnloadingFollowed = true,
                RulesOnFeesAccessFollowed = false, // Issue found
                RuleViolations = "Minor violation in access log procedures.",
                OverallStatus = "Under Review",
                Notes = "Follow-up required for access log procedures."
            }
        };
    }

    public Task<IEnumerable<MonitoringRecord>> GetAllMonitoringRecordsAsync()
    {
        return Task.FromResult<IEnumerable<MonitoringRecord>>(_records);
    }

    public Task<MonitoringRecord?> GetMonitoringRecordByIdAsync(string id)
    {
        var record = _records.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(record);
    }

    public Task<MonitoringRecord> CreateMonitoringRecordAsync(MonitoringRecord record)
    {
        record.Id = $"MON{_records.Count + 1:D3}";
        record.RecordDate = DateTime.Now;
        _records.Add(record);
        return Task.FromResult(record);
    }

    public Task<MonitoringRecord> UpdateMonitoringRecordAsync(MonitoringRecord record)
    {
        var existingRecord = _records.FirstOrDefault(r => r.Id == record.Id);
        if (existingRecord != null)
        {
            var index = _records.IndexOf(existingRecord);
            _records[index] = record;
        }
        return Task.FromResult(record);
    }

    public Task<bool> DeleteMonitoringRecordAsync(string id)
    {
        var record = _records.FirstOrDefault(r => r.Id == id);
        if (record != null)
        {
            _records.Remove(record);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<IEnumerable<MonitoringRecord>> GetMonitoringRecordsByWarehouseIdAsync(string warehouseId)
    {
        var warehouseRecords = _records.Where(r => r.WarehouseId == warehouseId);
        return Task.FromResult<IEnumerable<MonitoringRecord>>(warehouseRecords);
    }
}
