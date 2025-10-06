namespace Platform.Mining.Trading.Models
{
    public class ReconciliationReport
    {
        public string ReportId { get; set; } = string.Empty;
        public DateTime ReportDate { get; set; }
        public string ReportType { get; set; } = string.Empty; // TradeToClear, ClearToSettle, FeeRecon
        public int TotalRecords { get; set; }
        public int MatchedRecords { get; set; }
        public int UnmatchedRecords { get; set; }
        public string Status { get; set; } = string.Empty; // InProgress, Completed, Failed
    }

    public class ReconciliationMismatch
    {
        public string MismatchId { get; set; } = string.Empty;
        public string ReportId { get; set; } = string.Empty;
        public string RecordId { get; set; } = string.Empty;
        public string MismatchType { get; set; } = string.Empty;
        public string InternalValue { get; set; } = string.Empty;
        public string ExternalValue { get; set; } = string.Empty;
        public decimal DifferenceAmount { get; set; }
        public string Status { get; set; } = string.Empty; // Open, Investigating, Resolved
    }

    public class Invoice
    {
        public string InvoiceId { get; set; } = string.Empty;
        public string ParticipantId { get; set; } = string.Empty;
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Draft, Sent, Paid, Overdue
        public List<InvoiceLineItem> LineItems { get; set; } = new();
    }

    public class InvoiceLineItem
    {
        public string Description { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
