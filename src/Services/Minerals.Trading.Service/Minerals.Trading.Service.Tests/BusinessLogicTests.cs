using Microsoft.EntityFrameworkCore;
using Minerals.Trading.Service.Data;
using Minerals.Trading.Service.Manager;
using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Tests;

public class BusinessLogicTests
{
    private TradingDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TradingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new TradingDbContext(options);
    }

    #region Trade Validation Tests

    [Fact]
    public async Task CreateTrade_WithInvalidBuyerSellerSame_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = new Trade
        {
            BuyerName = "Same Company",
            SellerName = "Same Company",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => manager.CreateTradeAsync(trade));
        Assert.Contains("cannot be the same entity", exception.Message);
    }

    [Fact]
    public async Task CreateTrade_WithZeroQuantity_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 0,
            PricePerTon = 5000,
            TotalValue = 0,
            DeliveryDate = DateTime.Now.AddMonths(1)
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => manager.CreateTradeAsync(trade));
    }

    [Fact]
    public async Task CreateTrade_WithPastDeliveryDate_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddDays(-1)
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => manager.CreateTradeAsync(trade));
        Assert.Contains("Delivery date must be in the future", exception.Message);
    }

    [Fact]
    public async Task CreateTrade_WithValueMismatch_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 400000, // Incorrect value
            DeliveryDate = DateTime.Now.AddMonths(1)
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => manager.CreateTradeAsync(trade));
        Assert.Contains("total value mismatch", exception.Message);
    }

    [Fact]
    public async Task CancelTrade_ShouldUpdateStatusAndNotes()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = await manager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        // Act
        var result = await manager.CancelTradeAsync(trade.Id, "Market conditions changed");

        // Assert
        Assert.Equal(TradeStatus.Cancelled, result.Status);
        Assert.Contains("Market conditions changed", result.Notes);
    }

    [Fact]
    public async Task NovateTradeAsync_WithPendingStatus_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new TradeManager(context);
        var trade = await manager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        // Act & Assert - Trade must be confirmed before novation
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => manager.NovateTradeAsync(trade.Id));
        Assert.Contains("must be confirmed before novation", exception.Message);
    }

    #endregion

    #region Margin Calculation Tests

    [Fact]
    public async Task CalculateInitialMargin_ShouldCreateMarginRecord()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var marginManager = new MarginManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        await tradeManager.ConfirmTradeAsync(trade.Id);
        await tradeManager.NovateTradeAsync(trade.Id);

        // Act
        var margin = await marginManager.CalculateInitialMarginAsync(trade.Id);

        // Assert
        Assert.NotNull(margin);
        Assert.Equal(50000m, margin.InitialMargin); // 10% of 500000
        Assert.Equal(trade.Id, margin.TradeId);
        Assert.Equal("Required", margin.Status);
    }

    [Fact]
    public async Task CalculateVariationMargin_WithPriceIncrease_ShouldRequireBuyerPayment()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var marginManager = new MarginManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        // Act - Price increased to 5500
        var margin = await marginManager.CalculateVariationMarginAsync(trade.Id, 5500m);

        // Assert
        Assert.NotNull(margin);
        Assert.Equal(50000m, margin.VariationMargin); // (5500-5000) * 100
        Assert.Equal("Buyer", margin.PartyName);
        Assert.True(margin.IsPayable);
    }

    [Fact]
    public async Task CalculateVariationMargin_WithPriceDecrease_ShouldRequireSellerPayment()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var marginManager = new MarginManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        // Act - Price decreased to 4500
        var margin = await marginManager.CalculateVariationMarginAsync(trade.Id, 4500m);

        // Assert
        Assert.NotNull(margin);
        Assert.Equal(50000m, margin.VariationMargin); // Absolute value
        Assert.Equal("Seller", margin.PartyName);
        Assert.True(margin.IsPayable);
    }

    [Fact]
    public async Task GetTotalMarginRequirement_ShouldSumAllMargins()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var marginManager = new MarginManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        await tradeManager.ConfirmTradeAsync(trade.Id);
        await tradeManager.NovateTradeAsync(trade.Id);

        await marginManager.CalculateInitialMarginAsync(trade.Id);
        await marginManager.CalculateVariationMarginAsync(trade.Id, 5200m);

        // Act
        var totalMargin = await marginManager.GetTotalMarginRequirementAsync(trade.Id);

        // Assert
        Assert.Equal(70000m, totalMargin); // 50000 (initial) + 20000 (variation)
    }

    #endregion

    #region Settlement Tests

    [Fact]
    public async Task ProcessPhysicalSettlement_WithValidWarrant_ShouldCreateSettlement()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var settlementManager = new SettlementManager(context);
        var warehouseManager = new WarehouseManager(context);
        var warrantManager = new WarrantManager(context);

        // Create warehouse
        var warehouse = await warehouseManager.CreateWarehouseAsync(new Warehouse
        {
            WarehouseCode = "WH001",
            OperatorName = "Test Warehouse",
            Location = "Ndola",
            City = "Ndola",
            Country = "Zambia",
            StorageCapacity = 1000,
            IsLMEApproved = true,
            ApprovalDate = DateTime.Now
        });

        // Create trade
        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        await tradeManager.ConfirmTradeAsync(trade.Id);
        await tradeManager.NovateTradeAsync(trade.Id);

        // Create warrant
        var warrant = await warrantManager.CreateWarrantAsync(new Warrant
        {
            TradeId = trade.Id,
            TradeNumber = trade.TradeNumber,
            WarehouseId = warehouse.Id,
            MetalType = MetalType.Copper,
            Quantity = 100,
            CurrentOwner = "Seller",
            QualityGrade = "Grade A",
            LotNumber = "LOT001"
        });

        // Act
        var settlement = await settlementManager.ProcessPhysicalSettlementAsync(
            trade.Id, warrant.WarrantNumber, warehouse.Location);

        // Assert
        Assert.NotNull(settlement);
        Assert.Equal(SettlementType.PhysicalDelivery, settlement.SettlementType);
        Assert.Equal(warrant.WarrantNumber, settlement.WarrantNumber);
        Assert.Equal("Processing", settlement.Status);
    }

    [Fact]
    public async Task ProcessCashSettlement_WithPriceIncrease_ShouldCalculateCorrectly()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var settlementManager = new SettlementManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        await tradeManager.ConfirmTradeAsync(trade.Id);
        await tradeManager.NovateTradeAsync(trade.Id);

        // Act - Final price is 5500
        var settlement = await settlementManager.ProcessCashSettlementAsync(trade.Id, 5500m);

        // Assert
        Assert.NotNull(settlement);
        Assert.Equal(SettlementType.CashSettlement, settlement.SettlementType);
        Assert.Equal(50000m, settlement.SettlementAmount); // (5500-5000) * 100
        Assert.Equal(500m, settlement.PriceDifference);
        Assert.Contains("Buyer pays", settlement.Notes);
    }

    [Fact]
    public async Task CompleteSettlement_ShouldUpdateTradeStatus()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var settlementManager = new SettlementManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        await tradeManager.ConfirmTradeAsync(trade.Id);
        await tradeManager.NovateTradeAsync(trade.Id);

        var settlement = await settlementManager.ProcessCashSettlementAsync(trade.Id, 5000m);

        // Act
        await settlementManager.CompleteSettlementAsync(settlement.Id);

        // Assert
        var updatedTrade = await tradeManager.GetTradeByIdAsync(trade.Id);
        Assert.NotNull(updatedTrade);
        Assert.Equal(TradeStatus.Settled, updatedTrade.Status);
    }

    #endregion

    #region Mineral Listing Validation Tests

    [Fact]
    public async Task CreateListing_WithInvalidPrice_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new MineralListingManager(context);
        var listing = new MineralListing
        {
            SellerId = "S001",
            SellerCompanyName = "Test Seller",
            MetalType = MetalType.Copper,
            QuantityAvailable = 100,
            PricePerTon = 100m, // Way too low for copper
            OriginCountry = "Zambia",
            QualityGrade = "Grade A"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => manager.CreateMineralListingAsync(listing));
        Assert.Contains("outside acceptable market range", exception.Message);
    }

    [Fact]
    public async Task CreateListing_WithValidPrice_ShouldSucceed()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new MineralListingManager(context);
        var listing = new MineralListing
        {
            SellerId = "S001",
            SellerCompanyName = "Test Seller",
            MetalType = MetalType.Copper,
            QuantityAvailable = 100,
            PricePerTon = 9000m, // Within valid range
            OriginCountry = "Zambia",
            QualityGrade = "Grade A"
        };

        // Act
        var result = await manager.CreateMineralListingAsync(listing);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Available", result.Status);
        Assert.NotNull(result.ExpiryDate); // Should set default expiry
    }

    [Fact]
    public async Task UpdateListingStatus_WithInvalidStatus_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var manager = new MineralListingManager(context);
        var listing = await manager.CreateMineralListingAsync(new MineralListing
        {
            SellerId = "S001",
            SellerCompanyName = "Test Seller",
            MetalType = MetalType.Copper,
            QuantityAvailable = 100,
            PricePerTon = 9000m,
            OriginCountry = "Zambia",
            QualityGrade = "Grade A"
        });

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(
            () => manager.UpdateListingStatusAsync(listing.Id, "InvalidStatus"));
    }

    #endregion

    #region Payment Validation Tests

    [Fact]
    public async Task CreatePayment_WithExcessiveAmount_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var paymentManager = new PaymentManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        var payment = new Payment
        {
            TradeId = trade.Id,
            Amount = 600000m, // Exceeds trade value by more than 10%
            PaymentDate = DateTime.Now,
            Description = "Initial payment"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => paymentManager.CreatePaymentAsync(payment));
        Assert.Contains("would exceed trade value", exception.Message);
    }

    [Fact]
    public async Task IsTradeFullyPaid_WithSufficientPayments_ShouldReturnTrue()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var tradeManager = new TradeManager(context);
        var paymentManager = new PaymentManager(context);

        var trade = await tradeManager.CreateTradeAsync(new Trade
        {
            BuyerName = "Buyer",
            SellerName = "Seller",
            MetalType = MetalType.Copper,
            Quantity = 100,
            PricePerTon = 5000,
            TotalValue = 500000,
            DeliveryDate = DateTime.Now.AddMonths(1)
        });

        await paymentManager.CreatePaymentAsync(new Payment
        {
            TradeId = trade.Id,
            Amount = 500000m,
            PaymentDate = DateTime.Now,
            Description = "Full payment"
        });

        // Act
        var isFullyPaid = await paymentManager.IsTradeFullyPaidAsync(trade.Id);

        // Assert
        Assert.True(isFullyPaid);
    }

    #endregion

    #region Warrant Validation Tests

    [Fact]
    public async Task CreateWarrant_WithNonApprovedWarehouse_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var warehouseManager = new WarehouseManager(context);
        var warrantManager = new WarrantManager(context);

        var warehouse = await warehouseManager.CreateWarehouseAsync(new Warehouse
        {
            WarehouseCode = "WH001",
            OperatorName = "Test Warehouse",
            Location = "Ndola",
            City = "Ndola",
            Country = "Zambia",
            StorageCapacity = 1000,
            IsLMEApproved = false // Not approved
        });

        var warrant = new Warrant
        {
            TradeId = "T001",
            TradeNumber = "TRD001",
            WarehouseId = warehouse.Id,
            MetalType = MetalType.Copper,
            Quantity = 100,
            CurrentOwner = "Owner",
            QualityGrade = "Grade A",
            LotNumber = "LOT001"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => warrantManager.CreateWarrantAsync(warrant));
        Assert.Contains("not LME approved", exception.Message);
    }

    [Fact]
    public async Task TransferWarrant_ToSameOwner_ShouldThrowException()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var warehouseManager = new WarehouseManager(context);
        var warrantManager = new WarrantManager(context);

        var warehouse = await warehouseManager.CreateWarehouseAsync(new Warehouse
        {
            WarehouseCode = "WH001",
            OperatorName = "Test Warehouse",
            Location = "Ndola",
            City = "Ndola",
            Country = "Zambia",
            StorageCapacity = 1000,
            IsLMEApproved = true,
            ApprovalDate = DateTime.Now
        });

        var warrant = await warrantManager.CreateWarrantAsync(new Warrant
        {
            TradeId = "T001",
            TradeNumber = "TRD001",
            WarehouseId = warehouse.Id,
            MetalType = MetalType.Copper,
            Quantity = 100,
            CurrentOwner = "Owner1",
            QualityGrade = "Grade A",
            LotNumber = "LOT001"
        });

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => warrantManager.TransferWarrantAsync(warrant.Id, "Owner1"));
        Assert.Contains("cannot be the same as current owner", exception.Message);
    }

    [Fact]
    public async Task VerifyWarehouseCapacity_WithExcessQuantity_ShouldReturnFalse()
    {
        // Arrange
        var context = CreateInMemoryContext();
        var warehouseManager = new WarehouseManager(context);
        var warrantManager = new WarrantManager(context);

        var warehouse = await warehouseManager.CreateWarehouseAsync(new Warehouse
        {
            WarehouseCode = "WH001",
            OperatorName = "Test Warehouse",
            Location = "Ndola",
            City = "Ndola",
            Country = "Zambia",
            StorageCapacity = 100,
            IsLMEApproved = true,
            ApprovalDate = DateTime.Now
        });

        // Act
        var hasCapacity = await warrantManager.VerifyWarehouseCapacityAsync(warehouse.Id, 150m);

        // Assert
        Assert.False(hasCapacity);
    }

    #endregion
}
