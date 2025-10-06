namespace Platform.Mining.Trading.Models
{
    public class ParticipantProfile
    {
        public string ParticipantId { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public string EntityType { get; set; } = string.Empty; // Individual, Corporation, Partnership
        public string Country { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string OnboardingStatus { get; set; } = string.Empty; // Pending, Approved, Rejected, Suspended
        public DateTime SubmittedDate { get; set; }
        public decimal RiskScore { get; set; }
        public bool SanctionsCheckPassed { get; set; }
        public bool PepCheckPassed { get; set; }
    }

    public class ComplianceDocument
    {
        public string DocumentId { get; set; } = string.Empty;
        public string ParticipantId { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty; // ID, Incorporation, TaxCert, ProofOfAddress
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedDate { get; set; }
        public string Status { get; set; } = string.Empty; // Pending, Approved, Rejected
        public string? ReviewNotes { get; set; }
    }

    public class AccountLimit
    {
        public string LimitId { get; set; } = string.Empty;
        public string ParticipantId { get; set; } = string.Empty;
        public string LimitType { get; set; } = string.Empty; // TradingLimit, WithdrawalLimit, PositionLimit
        public decimal LimitValue { get; set; }
        public string Currency { get; set; } = string.Empty;
        public DateTime EffectiveDate { get; set; }
    }
}
