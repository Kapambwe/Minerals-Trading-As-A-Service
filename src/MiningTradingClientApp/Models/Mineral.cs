using System;

namespace MiningTradingClientApp.Models
{
    public class Mineral
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; } // in kilograms
        public string? Seller { get; set; }
        public string? Origin { get; set; }
        public DateTime DateListed { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsVerified { get; set; }
    }
}