using Microsoft.Extensions.Logging;
using Platform.Trading.Management.Services.Mock;
using Platform.Trading.Management.Services.Interfaces;

namespace MineralsTradingMobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // Using system fonts - add custom fonts here when available
                // fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Register services from Platform.Trading.Management
        RegisterServices(builder.Services);

        return builder.Build();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        // Register mock services for mobile app
        services.AddSingleton<IBuyerService, MockBuyerService>();
        services.AddSingleton<ISellerService, MockSellerService>();
        services.AddSingleton<ITradeService, MockTradeService>();
        services.AddSingleton<IWarehouseService, MockWarehouseService>();
        services.AddSingleton<IWarrantService, MockWarrantService>();
        services.AddSingleton<IInspectionService, MockInspectionService>();
        services.AddSingleton<ISettlementService, MockSettlementService>();
        services.AddSingleton<IMarginService, MockMarginService>();
        services.AddSingleton<IMonitoringService, MockMonitoringService>();
        services.AddSingleton<IMonthlyProcessingReportService, MockMonthlyProcessingReportService>();
        services.AddSingleton<IMineralListingService, MockMineralListingService>();
        
        // Register pages
        services.AddTransient<Pages.OrderPlacementPage>();
    }
}
