using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Tests;

public class TradeManagerTests
{
    private TradingDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TradingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new TradingDbContext(options);
    }

    [Fact]
    public async Task CreateTradeAsync_ShouldCreateTrade()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = new Trade
        {
            BuyerName = "Test Buyer",
            SellerName = "Test Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        };

        // Act
        var result = await manager.CreateTradeAsync(trade);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Id);
        Assert.NotEmpty(result.TradeNumber);
        Assert.Equal("Test Buyer", result.BuyerName);
        Assert.Equal("Test Seller", result.SellerName);
        Assert.Equal(MetalType.Copper, result.MetalType);
    }

    [Fact]
    public async Task GetAllTradesAsync_ShouldReturnAllTrades()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        
        await manager.CreateTradeAsync(new Trade 
        { 
            BuyerName = "Buyer 1", 
            SellerName = "Seller 1",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });
        
        await manager.CreateTradeAsync(new Trade 
        { 
            BuyerName = "Buyer 2", 
            SellerName = "Seller 2",
            MetalType = MetalType.Gold,
            Quantity = 50,
            PricePerTon = 60000,
            TotalValue = 3000000,
            DeliveryDate = DateTime.Now.AddMonths(2)
        });

        // Act
        var result = await manager.GetAllTradesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task NovateTradeAsync_ShouldUpdateTradeStatus()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = await manager.CreateTradeAsync(new Trade
        {
            BuyerName = "Test Buyer",
            SellerName = "Test Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        // Confirm trade first (required before novation)
        await manager.ConfirmTradeAsync(trade.Id);

        // Act
        var result = await manager.NovateTradeAsync(trade.Id);

        // Assert
        Assert.True(result.IsNovated);
        Assert.NotNull(result.NovationDate);
        Assert.Equal(TradeStatus.Novated, result.Status);
    }

    [Fact]
    public async Task ConfirmTradeAsync_ShouldUpdateTradeStatus()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = await manager.CreateTradeAsync(new Trade
        {
            BuyerName = "Test Buyer",
            SellerName = "Test Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        // Act
        var result = await manager.ConfirmTradeAsync(trade.Id);

        // Assert
        Assert.Equal(TradeStatus.Confirmed, result.Status);
    }

    [Fact]
    public async Task DeleteTradeAsync_ShouldRemoveTrade()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = await manager.CreateTradeAsync(new Trade
        {
            BuyerName = "Test Buyer",
            SellerName = "Test Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        // Act
        var result = await manager.DeleteTradeAsync(trade.Id);

        // Assert
        Assert.True(result);
        var deletedTrade = await manager.GetTradeByIdAsync(trade.Id);
        Assert.Null(deletedTrade);
    }
}
