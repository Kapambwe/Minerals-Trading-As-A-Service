namespace Platform.Mining.Trading.Models
{
    public class MatchingEngineConfig
    {
        public string ConfigId { get; set; } = string.Empty;
        public string MatchingRule { get; set; } = string.Empty;
        public decimal TickSize { get; set; }
        public string Status { get; set; } = string.Empty; // Running, Stopped, Maintenance
        public DateTime LastRestart { get; set; }
        public int CurrentQueueDepth { get; set; }
        public int ProcessedOrdersPerSecond { get; set; }
    }

    public class ContractSpec
    {
        public string ContractId { get; set; } = string.Empty;
        public string Instrument { get; set; } = string.Empty;
        public decimal TickSize { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal MaxQuantity { get; set; }
        public decimal ContractSize { get; set; }
        public string PricingUnit { get; set; } = string.Empty;
    }

    public class MatchQueue
    {
        public string QueueId { get; set; } = string.Empty;
        public string Instrument { get; set; } = string.Empty;
        public int BuyOrderCount { get; set; }
        public int SellOrderCount { get; set; }
        public int UnmatchedOrderCount { get; set; }
        public DateTime LastProcessedTime { get; set; }
        public decimal AverageMatchLatency { get; set; }
    }

    public class UnmatchedOrder
    {
        public string OrderId { get; set; } = string.Empty;
        public string Instrument { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime SubmittedTime { get; set; }
    }

    public class EngineParameter
    {
        public string ParameterId { get; set; } = string.Empty;
        public string ParameterName { get; set; } = string.Empty;
        public string CurrentValue { get; set; } = string.Empty;
        public string ProposedValue { get; set; } = string.Empty;
        public string ApprovalStatus { get; set; } = string.Empty; // Pending, Approved, Rejected
        public string ProposedBy { get; set; } = string.Empty;
        public DateTime? ApprovedDate { get; set; }
    }
}
