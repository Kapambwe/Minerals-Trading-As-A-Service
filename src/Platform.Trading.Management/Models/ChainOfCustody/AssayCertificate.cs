namespace Platform.Trading.Management.Models.ChainOfCustody;

/// <summary>
/// Represents an assay certificate for quality verification of minerals.
/// Addresses Gap WD-003: Digital assay certificate management with lab integration.
/// </summary>
public class AssayCertificate
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string CertificateNumber { get; set; } = string.Empty;
    public DateTime IssueDate { get; set; } = DateTime.Now;
    
    // Sample Information
    public string SampleId { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;
    public string LotNumber { get; set; } = string.Empty;
    public DateTime SamplingDate { get; set; }
    public string SamplingLocation { get; set; } = string.Empty;
    public string SampledBy { get; set; } = string.Empty;
    
    // Material Details
    public MetalType MetalType { get; set; }
    public decimal SampleWeight { get; set; } // in kg
    public decimal GrossWeight { get; set; } // Total batch weight in metric tons
    
    // Laboratory Information
    public string LaboratoryName { get; set; } = string.Empty;
    public string LaboratoryAddress { get; set; } = string.Empty;
    public string LaboratoryAccreditation { get; set; } = string.Empty; // e.g., ISO 17025
    public string AnalystName { get; set; } = string.Empty;
    public DateTime AnalysisDate { get; set; }
    
    // Assay Results
    public decimal PrimaryMetalContent { get; set; } // Percentage (e.g., 99.99 for Grade A copper)
    public string QualityGrade { get; set; } = string.Empty; // e.g., "LME Grade A", "Electrolytic Cathode"
    public List<AssayElement> ElementalAnalysis { get; set; } = new();
    
    // Impurities
    public List<ImpurityResult> Impurities { get; set; } = new();
    
    // Physical Properties
    public string? PhysicalForm { get; set; } // Cathode, Concentrate, Ingot, etc.
    public string? Dimensions { get; set; }
    public decimal? Moisture { get; set; } // Percentage
    
    // Verification and Status
    public string Status { get; set; } = "Issued"; // Draft, Issued, Verified, Disputed, Cancelled
    public bool MeetsLmeStandard { get; set; }
    public bool MeetsZbsStandard { get; set; } // Zambia Bureau of Standards
    public string? StandardsNotes { get; set; }
    
    // Related Documents
    public string? WarrantId { get; set; }
    public string? CustodyRecordId { get; set; }
    
    // Digital Signature
    public string? DigitalSignature { get; set; }
    public DateTime? SignatureDate { get; set; }
    public string? SignedBy { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents the elemental analysis of a single element.
/// </summary>
public class AssayElement
{
    public string Element { get; set; } = string.Empty; // e.g., "Cu", "Co", "Ag"
    public string ElementName { get; set; } = string.Empty; // e.g., "Copper", "Cobalt", "Silver"
    public decimal Content { get; set; } // Percentage or ppm
    public string Unit { get; set; } = "Percentage"; // Percentage, ppm, ppb
    public string? Method { get; set; } // Analysis method used
}

/// <summary>
/// Represents impurity levels in the assay.
/// </summary>
public class ImpurityResult
{
    public string Element { get; set; } = string.Empty;
    public string ElementName { get; set; } = string.Empty;
    public decimal Level { get; set; } // ppm or percentage
    public string Unit { get; set; } = "ppm";
    public decimal? MaxAllowedLevel { get; set; }
    public bool IsWithinSpec { get; set; }
}
