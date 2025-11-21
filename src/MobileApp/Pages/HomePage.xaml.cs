namespace MineralsTradingMobileApp.Pages;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent();
    }

    private async void OnBuyersClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//buyers");
    }

    private async void OnTradesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//trades");
    }

    private async void OnWarehousesClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//warehouses");
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//dashboard");
    }

    private async void OnInspectionsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//inspections");
    }

    private async void OnSettlementsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//settlements");
    }

    private async void OnWarrantsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//warrants");
    }
}
