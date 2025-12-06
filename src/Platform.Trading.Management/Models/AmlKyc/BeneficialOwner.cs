namespace Platform.Trading.Management.Models.AmlKyc;

/// <summary>
/// Represents an Ultimate Beneficial Owner (UBO) of a company.
/// Addresses Gap RC-002: Beneficial Ownership Registry.
/// </summary>
public class BeneficialOwner
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string EntityId { get; set; } = string.Empty; // Buyer or Seller ID
    public string EntityType { get; set; } = string.Empty; // "Buyer" or "Seller"
    
    // Owner Details
    public string FullName { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;
    public string CountryOfResidence { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string IdentificationType { get; set; } = string.Empty; // Passport, National ID, etc.
    public string IdentificationNumber { get; set; } = string.Empty;
    public string? IdentificationCountry { get; set; }
    
    // Ownership Structure
    public decimal OwnershipPercentage { get; set; }
    public bool IsDirectOwnership { get; set; } = true;
    public string? OwnershipChainDescription { get; set; } // For indirect ownership
    
    // Control Details
    public bool HasVotingRights { get; set; }
    public decimal? VotingRightsPercentage { get; set; }
    public bool HasBoardControl { get; set; }
    public string? ControlDescription { get; set; }
    
    // PEP Status
    public bool IsPep { get; set; }
    public string? PepPosition { get; set; }
    public string? PepCountry { get; set; }
    
    // Verification
    public string VerificationStatus { get; set; } = "Pending"; // Pending, Verified, Rejected
    public DateTime? VerificationDate { get; set; }
    public string? VerifiedBy { get; set; }
    public string? VerificationNotes { get; set; }
    
    // Document References
    public List<string> DocumentIds { get; set; } = new();
    
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastUpdated { get; set; }
}
