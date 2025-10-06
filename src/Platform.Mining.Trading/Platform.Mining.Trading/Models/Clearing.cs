namespace Platform.Mining.Trading.Models
{
    public class ClearingMember
    {
        public string MemberId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Active, Suspended, Default
        public decimal MarginPosted { get; set; }
        public decimal MarginRequired { get; set; }
        public decimal ExcessMargin { get; set; }
    }

    public class NettingResult
    {
        public string NettingId { get; set; } = string.Empty;
        public DateTime NettingDate { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal GrossPayments { get; set; }
        public decimal GrossReceipts { get; set; }
        public decimal NetPosition { get; set; }
        public string Status { get; set; } = string.Empty; // Pending, Completed
    }

    public class SettlementObligation
    {
        public string ObligationId { get; set; } = string.Empty;
        public string Member { get; set; } = string.Empty;
        public DateTime SettlementDate { get; set; }
        public string Currency { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Direction { get; set; } = string.Empty; // Pay, Receive
        public string Status { get; set; } = string.Empty; // Pending, Settled, Failed
    }

    public class MarginCall
    {
        public string CallId { get; set; } = string.Empty;
        public string Member { get; set; } = string.Empty;
        public DateTime IssueTime { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueTime { get; set; }
        public string Status { get; set; } = string.Empty; // Issued, Met, Overdue
    }
}
