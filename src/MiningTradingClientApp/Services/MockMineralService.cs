using MiningTradingClientApp.Models;
using System;
using System.Collections.Generic;
using System.Linq; // Added for LINQ methods
using System.Threading.Tasks;

namespace MiningTradingClientApp.Services
{
    public class MockMineralService : IMineralService
    {
        private readonly List<Mineral> _minerals;
        private readonly List<OrderTracking> _orderTrackings; // Added

        public MockMineralService()
        {
            _minerals = new List<Mineral>
            {
                new Mineral
                {
                    Id = Guid.NewGuid(),
                    Name = "Zambian Emerald",
                    Description = "A beautiful, high-quality emerald from the rich mines of Zambia. Known for its vibrant green color and excellent clarity.",
                    Price = 1500.00m, // per carat
                    Weight = 0.5, // 0.5 kg (approx 2500 carats)
                    Seller = "Zambia Gemstone Traders",
                    Origin = "Kagem Mine, Zambia",
                    DateListed = DateTime.Now.AddDays(-10),
                    ImageUrl = "https://picsum.photos/id/237/300/200",
                    IsVerified = true // Added
                },
                new Mineral
                {
                    Id = Guid.NewGuid(),
                    Name = "Copper Ore",
                    Description = "High-grade copper ore, ideal for industrial processing. Sourced from the Copperbelt Province.",
                    Price = 8.50m, // per kg
                    Weight = 1000.0, // 1000 kg (1 tonne)
                    Seller = "Copperbelt Mining Co.",
                    Origin = "Ndola, Zambia",
                    DateListed = DateTime.Now.AddDays(-5),
                    ImageUrl = "https://picsum.photos/id/238/300/200",
                    IsVerified = false // Added
                },
                new Mineral
                {
                    Id = Guid.NewGuid(),
                    Name = "Amethyst Geode",
                    Description = "Stunning amethyst geode, perfect for collectors or decorative purposes. Deep purple crystals.",
                    Price = 250.00m, // per piece
                    Weight = 15.0,
                    Seller = "Lusaka Crystal Dealers",
                    Origin = "Mapatizya, Zambia",
                    DateListed = DateTime.Now.AddDays(-20),
                    ImageUrl = "https://picsum.photos/id/239/300/200",
                    IsVerified = true // Added
                },
                new Mineral
                {
                    Id = Guid.NewGuid(),
                    Name = "Cobalt Concentrate",
                    Description = "High purity cobalt concentrate, essential for battery manufacturing and other high-tech applications.",
                    Price = 60.00m, // per kg
                    Weight = 500.0,
                    Seller = "Zambian Mineral Exports",
                    Origin = "Chingola, Zambia",
                    DateListed = DateTime.Now.AddDays(-15),
                    ImageUrl = "https://picsum.photos/id/240/300/200",
                    IsVerified = false // Added
                },
                new Mineral
                {
                    Id = Guid.NewGuid(),
                    Name = "Gold Nugget",
                    Description = "Naturally formed gold nugget, rare and highly sought after by investors and collectors.",
                    Price = 65000.00m, // per kg
                    Weight = 0.1, // 100 grams
                    Seller = "Precious Metals Zambia",
                    Origin = "Rufunsa, Zambia",
                    DateListed = DateTime.Now.AddDays(-3),
                    ImageUrl = "https://picsum.photos/id/241/300/200",
                    IsVerified = true // Added
                }
            };

            // Initialize mock order tracking data (Added)
            _orderTrackings = new List<OrderTracking>
            {
                new OrderTracking
                {
                    OrderId = Guid.NewGuid(),
                    MineralId = _minerals[0].Id,
                    MineralName = _minerals[0].Name,
                    BuyerName = "John Doe",
                    OrderDate = DateTime.Now.AddDays(-5),
                    Status = "Processing",
                    PriceAtOrder = _minerals[0].Price,
                    Quantity = 0.2,
                    CostPrice = 1000.00m,
                    AdjustedMargin = 0 // Will be calculated or set later
                },
                new OrderTracking
                {
                    OrderId = Guid.NewGuid(),
                    MineralId = _minerals[1].Id,
                    MineralName = _minerals[1].Name,
                    BuyerName = "Jane Smith",
                    OrderDate = DateTime.Now.AddDays(-10),
                    Status = "Shipped",
                    PriceAtOrder = _minerals[1].Price,
                    Quantity = 500.0,
                    CostPrice = 7.00m,
                    AdjustedMargin = 0
                },
                new OrderTracking
                {
                    OrderId = Guid.NewGuid(),
                    MineralId = _minerals[2].Id,
                    MineralName = _minerals[2].Name,
                    BuyerName = "Peter Jones",
                    OrderDate = DateTime.Now.AddDays(-2),
                    Status = "Pending",
                    PriceAtOrder = _minerals[2].Price,
                    Quantity = 1.0,
                    CostPrice = 200.00m,
                    AdjustedMargin = 0
                }
            };
        }

        public Task<IEnumerable<Mineral>> GetAvailableMineralsAsync()
        {
            return Task.FromResult<IEnumerable<Mineral>>(_minerals);
        }

        public Task<IEnumerable<Mineral>> SearchMineralsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Task.FromResult<IEnumerable<Mineral>>(_minerals);
            }

            var lowerCaseSearchTerm = searchTerm.ToLowerInvariant();
            var filteredMinerals = _minerals.Where(m =>
                (m.Name ?? "").ToLowerInvariant().Contains(lowerCaseSearchTerm) ||
                (m.Description ?? "").ToLowerInvariant().Contains(lowerCaseSearchTerm) ||
                (m.Origin ?? "").ToLowerInvariant().Contains(lowerCaseSearchTerm) ||
                (m.Seller ?? "").ToLowerInvariant().Contains(lowerCaseSearchTerm)
            ).ToList();

            return Task.FromResult<IEnumerable<Mineral>>(filteredMinerals);
        }

        // Added
        public Task<IEnumerable<OrderTracking>> GetOrderTrackingAsync()
        {
            return Task.FromResult<IEnumerable<OrderTracking>>(_orderTrackings);
        }
    }
}