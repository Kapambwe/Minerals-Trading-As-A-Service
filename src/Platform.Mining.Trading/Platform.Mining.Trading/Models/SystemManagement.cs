namespace Platform.Mining.Trading.Models
{
    public class AuditLogEntry
    {
        public string LogId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Resource { get; set; } = string.Empty;
        public string? RequestPayload { get; set; }
        public string? ResponsePayload { get; set; }
        public string SessionId { get; set; } = string.Empty;
        public string IpAddress { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Success, Failed
    }

    public class SystemMetric
    {
        public string MetricId { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string Component { get; set; } = string.Empty; // MatchingEngine, Database, API, Network
        public string MetricName { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Unit { get; set; } = string.Empty;
    }

    public class FixSession
    {
        public string SessionId { get; set; } = string.Empty;
        public string CounterpartyId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty; // Connected, Disconnected, LoggedOn, LoggedOff
        public int InboundSeqNum { get; set; }
        public int OutboundSeqNum { get; set; }
        public DateTime? LastMessageTime { get; set; }
        public decimal LatencyMs { get; set; }
    }

    public class ApiEndpoint
    {
        public string EndpointId { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int RequestCount { get; set; }
        public decimal AvgLatencyMs { get; set; }
        public int ErrorCount { get; set; }
    }
}
