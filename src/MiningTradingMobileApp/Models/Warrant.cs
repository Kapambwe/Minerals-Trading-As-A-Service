namespace MiningTradingMobileApp.Models
{
    public class Warrant
    {
        public string WarrantNumber { get; set; }
        public string MetalType { get; set; }
        public string TradeId { get; set; }
        public string WarehouseName { get; set; }
        public double Quantity { get; set; }
        public string CurrentOwner { get; set; }
        public string PreviousOwner { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? TransferDate { get; set; }
        public string QualityGrade { get; set; }
        public string LotNumber { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }
}