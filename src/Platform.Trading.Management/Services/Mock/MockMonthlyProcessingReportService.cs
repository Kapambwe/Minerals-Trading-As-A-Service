using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Mock;

public class MockMonthlyProcessingReportService : IMonthlyProcessingReportService
{
    private readonly List<MonthlyProcessingReport> _reports;

    public MockMonthlyProcessingReportService()
    {
        _reports = new List<MonthlyProcessingReport>
        {
            new MonthlyProcessingReport
            {
                Id = "MPR001",
                ReportNumber = "MPR-2025-09",
                ReportMonth = new DateTime(2025, 9, 1),
                TotalTradesProcessed = 150,
                TotalQuantityTraded = 15000,
                TotalValueTraded = 120000000,
                NewBuyersRegistered = 5,
                NewSellersRegistered = 3,
                InspectionsConducted = 10,
                ComplianceRate = 95.5m,
                Summary = "Strong trading activity with good compliance.",
                Status = "Finalized",
                GeneratedDate = new DateTime(2025, 10, 5)
            },
            new MonthlyProcessingReport
            {
                Id = "MPR002",
                ReportNumber = "MPR-2025-08",
                ReportMonth = new DateTime(2025, 8, 1),
                TotalTradesProcessed = 120,
                TotalQuantityTraded = 10000,
                TotalValueTraded = 85000000,
                NewBuyersRegistered = 3,
                NewSellersRegistered = 2,
                InspectionsConducted = 8,
                ComplianceRate = 92.0m,
                Summary = "Moderate activity, minor compliance issues noted.",
                Status = "Finalized",
                GeneratedDate = new DateTime(2025, 9, 3)
            },
            new MonthlyProcessingReport
            {
                Id = "MPR003",
                ReportNumber = "MPR-2025-10",
                ReportMonth = new DateTime(2025, 10, 1),
                TotalTradesProcessed = 80,
                TotalQuantityTraded = 7000,
                TotalValueTraded = 60000000,
                NewBuyersRegistered = 2,
                NewSellersRegistered = 1,
                InspectionsConducted = 5,
                ComplianceRate = 98.0m,
                Summary = "Current month's report, still in progress.",
                Status = "Draft",
                GeneratedDate = DateTime.Now
            }
        };
    }

    public Task<IEnumerable<MonthlyProcessingReport>> GetAllMonthlyProcessingReportsAsync()
    {
        return Task.FromResult<IEnumerable<MonthlyProcessingReport>>(_reports);
    }

    public Task<MonthlyProcessingReport?> GetMonthlyProcessingReportByIdAsync(string id)
    {
        var report = _reports.FirstOrDefault(r => r.Id == id);
        return Task.FromResult(report);
    }

    public Task<MonthlyProcessingReport> CreateMonthlyProcessingReportAsync(MonthlyProcessingReport report)
    {
        report.Id = $"MPR{_reports.Count + 1:D3}";
        report.ReportNumber = $"MPR-{report.ReportMonth.Year}-{report.ReportMonth.Month:D2}";
        report.GeneratedDate = DateTime.Now;
        report.Status = "Draft";
        _reports.Add(report);
        return Task.FromResult(report);
    }

    public Task<MonthlyProcessingReport> UpdateMonthlyProcessingReportAsync(MonthlyProcessingReport report)
    {
        var existingReport = _reports.FirstOrDefault(r => r.Id == report.Id);
        if (existingReport != null)
        {
            var index = _reports.IndexOf(existingReport);
            _reports[index] = report;
        }
        return Task.FromResult(report);
    }

    public Task<bool> DeleteMonthlyProcessingReportAsync(string id)
    {
        var report = _reports.FirstOrDefault(r => r.Id == id);
        if (report != null)
        {
            _reports.Remove(report);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}
