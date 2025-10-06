namespace Platform.Mining.Trading.Models
{
    public class BankEndpoint
    {
        public string EndpointId { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string EndpointType { get; set; } = string.Empty; // RTGS, SWIFT, LocalTransfer
        public string ConnectionString { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime LastSuccessfulConnection { get; set; }
        public int TimeoutSeconds { get; set; }
        public int MaxRetries { get; set; }
    }

    public class SettlementBatch
    {
        public string BatchId { get; set; } = string.Empty;
        public DateTime SettlementDate { get; set; }
        public string Currency { get; set; } = string.Empty;
        public int TotalTransactions { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty; // Pending, InProgress, Completed, Failed, RolledBack
        public DateTime? ProcessedTime { get; set; }
        public string ProcessedBy { get; set; } = string.Empty;
    }

    public class PaymentTransaction
    {
        public string TransactionId { get; set; } = string.Empty;
        public string BatchId { get; set; } = string.Empty;
        public string FromAccount { get; set; } = string.Empty;
        public string ToAccount { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Pending, Sent, Confirmed, Failed
        public string? ReferenceNumber { get; set; }
        public DateTime? CompletedTime { get; set; }
    }

    public class FxConversionRule
    {
        public string RuleId { get; set; } = string.Empty;
        public string FromCurrency { get; set; } = string.Empty;
        public string ToCurrency { get; set; } = string.Empty;
        public decimal ExchangeRate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Source { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
