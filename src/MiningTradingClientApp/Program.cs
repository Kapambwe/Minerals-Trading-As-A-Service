using Microsoft.Extensions.Logging;
using MiningTradingClientApp.Services;
using MiningTradingClientApp.Views;
using CommunityToolkit.Maui;

namespace MiningTradingClientApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
            });

        // Register services
        builder.Services.AddSingleton<IMineralService, MockMineralService>();
        
        // Register pages
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<MineralsPage>();
        builder.Services.AddTransient<MineralDetailPage>();
        builder.Services.AddTransient<OrderPage>();
        builder.Services.AddTransient<OrderConfirmationPage>();
        builder.Services.AddTransient<PortfolioPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
