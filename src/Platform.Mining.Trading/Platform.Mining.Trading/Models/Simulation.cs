namespace Platform.Mining.Trading.Models
{
    public class SimulationScenario
    {
        public string ScenarioId { get; set; } = string.Empty;
        public string ScenarioName { get; set; } = string.Empty;
        public string ScenarioType { get; set; } = string.Empty; // StressTest, RuleTesting, MarketReplay
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> Instruments { get; set; } = new();
        public Dictionary<string, string> Parameters { get; set; } = new();
        public string Status { get; set; } = string.Empty; // Draft, Running, Completed, Failed
        public DateTime? ExecutedTime { get; set; }
        public string ExecutedBy { get; set; } = string.Empty;
    }

    public class MarketDataSnapshot
    {
        public string SnapshotId { get; set; } = string.Empty;
        public DateTime SnapshotDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public int TotalRecords { get; set; }
        public List<string> Instruments { get; set; } = new();
        public long FileSizeBytes { get; set; }
    }

    public class SimulationResult
    {
        public string ResultId { get; set; } = string.Empty;
        public string ScenarioId { get; set; } = string.Empty;
        public int TotalOrders { get; set; }
        public int MatchedOrders { get; set; }
        public decimal TotalVolume { get; set; }
        public decimal AverageLatency { get; set; }
        public int ErrorCount { get; set; }
        public decimal PeakMemoryUsageMB { get; set; }
        public decimal PeakCpuUsagePercent { get; set; }
        public Dictionary<string, decimal> Metrics { get; set; } = new();
        public DateTime CompletedTime { get; set; }
    }

    public class ParticipantSimulation
    {
        public string ParticipantId { get; set; } = string.Empty;
        public string ParticipantName { get; set; } = string.Empty;
        public string TradingStrategy { get; set; } = string.Empty; // Aggressive, Passive, Random
        public int OrdersPerMinute { get; set; }
        public decimal AvailableCapital { get; set; }
        public List<string> AllowedInstruments { get; set; } = new();
    }

    public class BacktestMetric
    {
        public string MetricName { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Unit { get; set; } = string.Empty;
        public string Threshold { get; set; } = string.Empty;
        public bool PassedThreshold { get; set; }
    }
}
