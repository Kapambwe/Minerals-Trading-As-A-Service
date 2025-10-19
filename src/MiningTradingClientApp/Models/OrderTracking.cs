using System;

namespace MiningTradingClientApp.Models
{
    public class OrderTracking
    {
        public Guid OrderId { get; set; }
        public Guid MineralId { get; set; }
        public string? MineralName { get; set; }
        public string? BuyerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; } // e.g., "Pending", "Processing", "Shipped", "Delivered"
        public decimal PriceAtOrder { get; set; }
        public double Quantity { get; set; }
        public decimal CostPrice { get; set; } // Cost at which ZME acquired the mineral
        public decimal SellingPrice => PriceAtOrder * (decimal)Quantity; // Price at which ZME sold the mineral
        public decimal CalculatedMargin => SellingPrice - CostPrice;
        public decimal AdjustedMargin { get; set; } // Editable field for backoffice users
        public string? Notes { get; set; }
    }
}