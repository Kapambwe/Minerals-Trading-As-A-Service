using Platform.Mining.Trading.Models;
using System.Text;

namespace Platform.Mining.Trading.Services
{
    public class MockSurveillanceService : ISurveillanceService
    {
        private List<SurveillanceAlert> _alerts = new();
        private List<SurveillanceCase> _cases = new();
        private List<PatternDetectionResult> _patternResults = new();

        public MockSurveillanceService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _alerts = new List<SurveillanceAlert>
            {
                new SurveillanceAlert
                {
                    AlertId = "ALT-SUR-001",
                    DetectedTime = DateTime.Now.AddHours(-1),
                    AlertType = "Spoofing",
                    Severity = "High",
                    Account = "ACC-TRADE-045",
                    Instrument = "COPPER-JAN24",
                    Description = "Repeated order placement and cancellation pattern detected",
                    Status = "New"
                },
                new SurveillanceAlert
                {
                    AlertId = "ALT-SUR-002",
                    DetectedTime = DateTime.Now.AddHours(-3),
                    AlertType = "Layering",
                    Severity = "Medium",
                    Account = "ACC-TRADE-067",
                    Instrument = "EMERALD-FEB24",
                    Description = "Multiple orders placed at various price levels",
                    Status = "UnderReview",
                    AssignedTo = "compliance@zmx.com"
                },
                new SurveillanceAlert
                {
                    AlertId = "ALT-SUR-003",
                    DetectedTime = DateTime.Now.AddDays(-1),
                    AlertType = "WashTrade",
                    Severity = "Critical",
                    Account = "ACC-TRADE-023",
                    Instrument = "COBALT-MAR24",
                    Description = "Potential wash trade detected between related accounts",
                    Status = "Escalated",
                    AssignedTo = "senior.investigator@zmx.com"
                }
            };

            _cases = new List<SurveillanceCase>
            {
                new SurveillanceCase
                {
                    CaseId = "CASE-001",
                    OpenedDate = DateTime.Now.AddDays(-5),
                    CaseType = "Market Manipulation",
                    Account = "ACC-TRADE-045",
                    Status = "Investigating",
                    Investigator = "compliance@zmx.com",
                    RelatedAlerts = new List<string> { "ALT-SUR-001" },
                    Notes = "Initial review shows suspicious trading pattern. Requesting additional trade data."
                }
            };

            _patternResults = new List<PatternDetectionResult>
            {
                new PatternDetectionResult
                {
                    ResultId = "PTN-001",
                    DetectedTime = DateTime.Now.AddHours(-2),
                    PatternType = "Quote Stuffing",
                    Account = "ACC-TRADE-089",
                    ConfidenceScore = 0.85m,
                    Evidence = "High frequency order cancellation rate detected"
                }
            };
        }

        public async Task<List<SurveillanceAlert>> GetAlertsAsync()
        {
            await Task.Delay(100);
            return _alerts.OrderByDescending(a => a.DetectedTime).ToList();
        }

        public async Task<List<SurveillanceCase>> GetCasesAsync()
        {
            await Task.Delay(100);
            return _cases.OrderByDescending(c => c.OpenedDate).ToList();
        }

        public async Task<string> CreateCaseAsync(SurveillanceCase newCase)
        {
            await Task.Delay(150);
            newCase.CaseId = $"CASE-{_cases.Count + 1:D3}";
            newCase.OpenedDate = DateTime.Now;
            newCase.Status = "Open";
            _cases.Add(newCase);
            return newCase.CaseId;
        }

        public async Task<bool> AssignInvestigatorAsync(string caseId, string investigator)
        {
            await Task.Delay(100);
            var caseItem = _cases.FirstOrDefault(c => c.CaseId == caseId);
            if (caseItem != null)
            {
                caseItem.Investigator = investigator;
                caseItem.Status = "Investigating";
                return true;
            }
            return false;
        }

        public async Task<bool> FreezeAccountAsync(string account, string reason)
        {
            await Task.Delay(150);
            // Simulate freezing account
            return true;
        }

        public async Task<byte[]> ExportEvidenceAsync(string caseId)
        {
            await Task.Delay(200);
            var caseItem = _cases.FirstOrDefault(c => c.CaseId == caseId);
            if (caseItem != null)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Case Evidence Export - {caseId}");
                sb.AppendLine($"Case Type: {caseItem.CaseType}");
                sb.AppendLine($"Account: {caseItem.Account}");
                sb.AppendLine($"Status: {caseItem.Status}");
                sb.AppendLine($"Investigator: {caseItem.Investigator}");
                sb.AppendLine($"Notes: {caseItem.Notes}");
                return Encoding.UTF8.GetBytes(sb.ToString());
            }
            return Array.Empty<byte>();
        }

        public async Task<List<PatternDetectionResult>> GetPatternDetectionResultsAsync()
        {
            await Task.Delay(100);
            return _patternResults.OrderByDescending(p => p.DetectedTime).ToList();
        }
    }
}
