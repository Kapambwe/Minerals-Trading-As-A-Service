namespace Platform.Trading.Management.Models;

public class MineralListing
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string SellerId { get; set; } = string.Empty;
    public string SellerCompanyName { get; set; } = string.Empty;
    public MetalType MetalType { get; set; }
    public decimal QuantityAvailable { get; set; } // in metric tons
    public decimal PricePerTon { get; set; }
    public string OriginCountry { get; set; } = string.Empty;
    public string QualityGrade { get; set; } = string.Empty;
    public DateTime ListingDate { get; set; } = DateTime.Now;
    public DateTime? ExpiryDate { get; set; }
    public string Status { get; set; } = "Available"; // e.g., Available, Under Offer, Sold
    public string? Notes { get; set; }
}
