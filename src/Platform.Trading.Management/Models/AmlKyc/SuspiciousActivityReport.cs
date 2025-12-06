namespace Platform.Trading.Management.Models.AmlKyc;

/// <summary>
/// Represents a Suspicious Activity Report (SAR) for regulatory compliance.
/// Addresses Gap RC-001: Automated suspicious activity reporting.
/// </summary>
public class SuspiciousActivityReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ReportNumber { get; set; } = string.Empty;
    public DateTime ReportDate { get; set; } = DateTime.Now;
    
    // Subject Information
    public string SubjectEntityId { get; set; } = string.Empty;
    public string SubjectEntityType { get; set; } = string.Empty; // "Buyer" or "Seller"
    public string SubjectName { get; set; } = string.Empty;
    
    // Activity Details
    public string ActivityType { get; set; } = string.Empty; // Large Transaction, Unusual Pattern, Structuring, etc.
    public DateTime ActivityStartDate { get; set; }
    public DateTime? ActivityEndDate { get; set; }
    public decimal? TransactionAmount { get; set; }
    public string? TransactionCurrency { get; set; }
    
    // Related Transactions
    public List<string> RelatedTradeIds { get; set; } = new();
    public List<string> RelatedSettlementIds { get; set; } = new();
    
    // Suspicion Details
    public string SuspicionCategory { get; set; } = string.Empty; // Money Laundering, Terrorist Financing, Fraud, etc.
    public string SuspicionIndicators { get; set; } = string.Empty;
    public string NarrativeDescription { get; set; } = string.Empty;
    
    // Reporter Information
    public string ReportedBy { get; set; } = string.Empty;
    public string ReporterRole { get; set; } = string.Empty;
    
    // Review and Filing
    public string Status { get; set; } = "Draft"; // Draft, Under Review, Filed, Closed
    public string? ReviewedBy { get; set; }
    public DateTime? ReviewDate { get; set; }
    public DateTime? FilingDate { get; set; }
    public string? RegulatoryReference { get; set; } // Reference from Financial Intelligence Unit
    
    // Follow-up Actions
    public string? ActionTaken { get; set; }
    public bool AccountFrozen { get; set; }
    public bool TradingSuspended { get; set; }
    
    public string? Notes { get; set; }
}
