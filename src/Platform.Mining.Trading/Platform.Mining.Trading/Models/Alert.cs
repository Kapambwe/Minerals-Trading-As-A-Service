namespace Platform.Mining.Trading.Models
{
    public class Alert
    {
        public string AlertId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Severity { get; set; } = string.Empty; // Info, Warning, Critical
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }
}
