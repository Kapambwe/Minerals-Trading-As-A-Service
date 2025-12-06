namespace Platform.Trading.Management.Models.Tax;

/// <summary>
/// Represents a tax calculation for a trade/settlement.
/// Addresses Gap RC-009: ZRA Tax Integration - mineral royalty, export levy, withholding tax.
/// </summary>
public class TaxCalculation
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string TradeId { get; set; } = string.Empty;
    public string TradeNumber { get; set; } = string.Empty;
    public DateTime CalculationDate { get; set; } = DateTime.Now;
    
    // Trade Details
    public MetalType MetalType { get; set; }
    public decimal Quantity { get; set; } // in metric tons
    public decimal TradeValue { get; set; }
    public string Currency { get; set; } = "USD";
    public decimal? ExchangeRateToZmw { get; set; }
    public decimal? TradeValueZmw { get; set; }
    
    // Parties
    public string SellerId { get; set; } = string.Empty;
    public string SellerName { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public string BuyerName { get; set; } = string.Empty;
    
    // Tax Components
    public List<TaxComponent> TaxComponents { get; set; } = new();
    
    // Totals
    public decimal TotalTaxAmount { get; set; }
    public string TotalTaxCurrency { get; set; } = "ZMW";
    public decimal? TotalTaxAmountUsd { get; set; }
    
    // ZRA Reference
    public string? ZraAssessmentNumber { get; set; }
    public DateTime? ZraAssessmentDate { get; set; }
    public string? ZraPaymentReference { get; set; }
    
    // Status
    public string Status { get; set; } = "Calculated"; // Calculated, Submitted, Paid, Overdue
    public DateTime? DueDate { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? PaymentReference { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a single tax component within a tax calculation.
/// </summary>
public class TaxComponent
{
    public string TaxType { get; set; } = string.Empty; // MineralRoyalty, ExportLevy, WithholdingTax, Vat
    public string TaxName { get; set; } = string.Empty;
    public decimal TaxRate { get; set; } // Percentage
    public decimal TaxableAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public string Currency { get; set; } = "ZMW";
    public string? LegalReference { get; set; } // Reference to tax law/regulation
    public string? Notes { get; set; }
}

/// <summary>
/// Represents tax rates configuration for different minerals.
/// </summary>
public class TaxRateConfiguration
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public MetalType MetalType { get; set; }
    public string TaxType { get; set; } = string.Empty;
    public string TaxName { get; set; } = string.Empty;
    
    // Rate Information
    public decimal Rate { get; set; } // Percentage
    public decimal? MinimumThreshold { get; set; }
    public decimal? MaximumThreshold { get; set; }
    
    // Tiered Rates (for sliding scale taxes)
    public List<TaxTier>? Tiers { get; set; }
    
    // Validity
    public DateTime EffectiveFrom { get; set; }
    public DateTime? EffectiveTo { get; set; }
    public bool IsActive { get; set; } = true;
    
    // Legal Reference
    public string? LegalReference { get; set; }
    public string? GazetteReference { get; set; }
    
    public string? Notes { get; set; }
}

/// <summary>
/// Represents a tier in a tiered tax rate structure.
/// </summary>
public class TaxTier
{
    public decimal FromValue { get; set; }
    public decimal? ToValue { get; set; }
    public decimal Rate { get; set; }
}
