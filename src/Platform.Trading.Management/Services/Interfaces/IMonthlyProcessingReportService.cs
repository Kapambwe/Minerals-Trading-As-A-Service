using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface IMonthlyProcessingReportService
{
    Task<IEnumerable<MonthlyProcessingReport>> GetAllMonthlyProcessingReportsAsync();
    Task<MonthlyProcessingReport?> GetMonthlyProcessingReportByIdAsync(string id);
    Task<MonthlyProcessingReport> CreateMonthlyProcessingReportAsync(MonthlyProcessingReport report);
    Task<MonthlyProcessingReport> UpdateMonthlyProcessingReportAsync(MonthlyProcessingReport report);
    Task<bool> DeleteMonthlyProcessingReportAsync(string id);
}
