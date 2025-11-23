using MiningTradingClientApp.Services;

namespace MiningTradingClientApp.Views;

public partial class PortfolioPage : ContentPage
{
    private readonly IMineralService _mineralService;

    public PortfolioPage(IMineralService mineralService)
    {
        InitializeComponent();
        _mineralService = mineralService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadOrderHistory();
    }

    private async Task LoadOrderHistory()
    {
        try
        {
            var orders = await _mineralService.GetOrderTrackingAsync();
            OrderHistoryCollection.ItemsSource = orders;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load order history: {ex.Message}", "OK");
        }
    }

    private async void OnBrowseMarketsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//minerals");
    }
}
