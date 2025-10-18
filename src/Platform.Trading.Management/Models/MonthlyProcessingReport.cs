namespace Platform.Trading.Management.Models;

public class MonthlyProcessingReport
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ReportNumber { get; set; } = string.Empty;
    public DateTime ReportMonth { get; set; } = DateTime.Now; // Represents the month/year of the report
    public int TotalTradesProcessed { get; set; }
    public decimal TotalQuantityTraded { get; set; } // in metric tons
    public decimal TotalValueTraded { get; set; }
    public int NewBuyersRegistered { get; set; }
    public int NewSellersRegistered { get; set; }
    public int InspectionsConducted { get; set; }
    public decimal ComplianceRate { get; set; } // Percentage
    public string? Summary { get; set; }
    public string Status { get; set; } = "Draft"; // e.g., Draft, Finalized, Approved
    public DateTime GeneratedDate { get; set; } = DateTime.Now;
}
