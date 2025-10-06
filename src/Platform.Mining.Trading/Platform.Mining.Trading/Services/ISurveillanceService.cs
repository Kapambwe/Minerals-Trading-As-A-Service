using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface ISurveillanceService
    {
        Task<List<SurveillanceAlert>> GetAlertsAsync();
        Task<List<SurveillanceCase>> GetCasesAsync();
        Task<string> CreateCaseAsync(SurveillanceCase newCase);
        Task<bool> AssignInvestigatorAsync(string caseId, string investigator);
        Task<bool> FreezeAccountAsync(string account, string reason);
        Task<byte[]> ExportEvidenceAsync(string caseId);
        Task<List<PatternDetectionResult>> GetPatternDetectionResultsAsync();
    }
}
