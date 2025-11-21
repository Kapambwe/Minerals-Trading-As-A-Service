namespace MineralsTradingMobileApp.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnBuyersClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.Buyers);
    }

    private async void OnPlaceOrderClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.PlaceOrder);
    }

    private async void OnTradesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.Trades);
    }

    private async void OnWarehousesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.Warehouses);
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.Dashboard);
    }

    private async void OnInspectionsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.Inspections);
    }

    private async void OnSettlementsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.Settlements);
    }

    private async void OnWarrantsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(Routes.Warrants);
    }
}
