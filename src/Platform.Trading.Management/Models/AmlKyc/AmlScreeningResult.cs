namespace Platform.Trading.Management.Models.AmlKyc;

/// <summary>
/// Represents the result of an AML (Anti-Money Laundering) screening check.
/// Addresses Gap RC-001: Anti-Money Laundering (AML) screening.
/// </summary>
public class AmlScreeningResult
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EntityId { get; set; } = string.Empty; // Buyer or Seller ID
    public string EntityType { get; set; } = string.Empty; // "Buyer" or "Seller"
    public string EntityName { get; set; } = string.Empty;
    public DateTime ScreeningDate { get; set; } = DateTime.Now;
    
    // Sanctions Screening
    public bool SanctionsCheckPassed { get; set; }
    public string? SanctionsListsChecked { get; set; } // e.g., "OFAC, EU, UN"
    public string? SanctionsMatchDetails { get; set; }
    
    // PEP (Politically Exposed Person) Screening
    public bool PepCheckPassed { get; set; }
    public string? PepMatchDetails { get; set; }
    
    // Adverse Media Screening
    public bool AdverseMediaCheckPassed { get; set; }
    public string? AdverseMediaDetails { get; set; }
    
    // Risk Assessment
    public decimal RiskScore { get; set; } // 0-100
    public string RiskLevel { get; set; } = "Low"; // Low, Medium, High, Critical
    
    // Overall Result
    public string ScreeningStatus { get; set; } = "Pending"; // Pending, Passed, Failed, Review Required
    public string? ReviewNotes { get; set; }
    public string? ReviewedBy { get; set; }
    public DateTime? ReviewDate { get; set; }
    
    // Expiry and Next Review
    public DateTime? ExpiryDate { get; set; }
    public DateTime? NextReviewDate { get; set; }
}
