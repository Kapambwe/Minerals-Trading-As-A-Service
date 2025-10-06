namespace Platform.Mining.Trading.Models
{
    public class MarginSummary
    {
        public decimal InitialMargin { get; set; }
        public decimal MaintenanceMargin { get; set; }
        public decimal VariationMargin { get; set; }
        public decimal TotalMarginRequired { get; set; }
        public decimal CurrentCollateral { get; set; }
        public decimal MarginUtilization { get; set; }
        public decimal ExcessMargin { get; set; }
    }
}
