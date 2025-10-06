namespace Platform.Mining.Trading.Models
{
    public class SurveillanceAlert
    {
        public string AlertId { get; set; } = string.Empty;
        public DateTime DetectedTime { get; set; }
        public string AlertType { get; set; } = string.Empty; // Spoofing, Layering, WashTrade, Insider
        public string Severity { get; set; } = string.Empty; // Low, Medium, High, Critical
        public string Account { get; set; } = string.Empty;
        public string Instrument { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // New, UnderReview, Resolved, Escalated
        public string? AssignedTo { get; set; }
    }

    public class SurveillanceCase
    {
        public string CaseId { get; set; } = string.Empty;
        public DateTime OpenedDate { get; set; }
        public string CaseType { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Open, Investigating, Closed, Escalated
        public string Investigator { get; set; } = string.Empty;
        public List<string> RelatedAlerts { get; set; } = new();
        public string Notes { get; set; } = string.Empty;
        public DateTime? ClosedDate { get; set; }
    }

    public class PatternDetectionResult
    {
        public string ResultId { get; set; } = string.Empty;
        public DateTime DetectedTime { get; set; }
        public string PatternType { get; set; } = string.Empty;
        public string Account { get; set; } = string.Empty;
        public decimal ConfidenceScore { get; set; }
        public string Evidence { get; set; } = string.Empty;
    }
}
