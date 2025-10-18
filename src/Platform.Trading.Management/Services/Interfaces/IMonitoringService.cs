using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface IMonitoringService
{
    Task<IEnumerable<MonitoringRecord>> GetAllMonitoringRecordsAsync();
    Task<MonitoringRecord?> GetMonitoringRecordByIdAsync(string id);
    Task<MonitoringRecord> CreateMonitoringRecordAsync(MonitoringRecord record);
    Task<MonitoringRecord> UpdateMonitoringRecordAsync(MonitoringRecord record);
    Task<bool> DeleteMonitoringRecordAsync(string id);
    Task<IEnumerable<MonitoringRecord>> GetMonitoringRecordsByWarehouseIdAsync(string warehouseId);
}
