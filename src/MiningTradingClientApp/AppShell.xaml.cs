using MiningTradingClientApp.Views;

namespace MiningTradingClientApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for navigation
        Routing.RegisterRoute("mineraldetail", typeof(MineralDetailPage));
        Routing.RegisterRoute("order", typeof(OrderPage));
        Routing.RegisterRoute("orderconfirmation", typeof(OrderConfirmationPage));
    }
}
