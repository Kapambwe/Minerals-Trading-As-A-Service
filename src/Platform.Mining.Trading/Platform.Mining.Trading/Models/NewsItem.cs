namespace Platform.Mining.Trading.Models
{
    public class NewsItem
    {
        public string NewsId { get; set; } = string.Empty;
        public string Headline { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public DateTime PublishedTime { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
