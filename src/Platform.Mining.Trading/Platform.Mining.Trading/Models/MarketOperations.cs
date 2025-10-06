namespace Platform.Mining.Trading.Models
{
    public class MarketStatus
    {
        public string MarketId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Open, Closed, Halted, Auction
        public DateTime LastUpdated { get; set; }
        public string UpdatedBy { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }

    public class ScheduledEvent
    {
        public string EventId { get; set; } = string.Empty;
        public string EventType { get; set; } = string.Empty; // Auction, SessionOpen, SessionClose, CircuitBreaker
        public DateTime ScheduledTime { get; set; }
        public string Instrument { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Pending, Active, Completed, Cancelled
    }

    public class CircuitBreaker
    {
        public string BreakerId { get; set; } = string.Empty;
        public string Instrument { get; set; } = string.Empty;
        public decimal PriceThreshold { get; set; }
        public decimal PercentageThreshold { get; set; }
        public bool IsActive { get; set; }
        public DateTime? TriggeredTime { get; set; }
    }

    public class OverrideLog
    {
        public string LogId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string User { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public string Justification { get; set; } = string.Empty;
    }
}
