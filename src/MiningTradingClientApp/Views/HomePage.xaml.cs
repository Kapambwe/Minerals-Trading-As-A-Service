using MiningTradingClientApp.Models;
using MiningTradingClientApp.Services;

namespace MiningTradingClientApp.Views;

public partial class HomePage : ContentPage
{
    private readonly IMineralService _mineralService;

    public HomePage(IMineralService mineralService)
    {
        InitializeComponent();
        _mineralService = mineralService;
        LoadData();
    }

    private async void LoadData()
    {
        try
        {
            var minerals = await _mineralService.GetAvailableMineralsAsync();
            FeaturedMineralsCollection.ItemsSource = minerals.Take(5);

            // Mock portfolio data
            PortfolioValueLabel.Text = "$12,543.50";
            PortfolioChangeLabel.Text = "+$243.50 (1.98%)";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
        }
    }

    private async void OnViewMarketsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//minerals");
    }

    private async void OnPortfolioClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//portfolio");
    }

    private async void OnMineralTapped(object sender, EventArgs e)
    {
        if (sender is View view && view.GestureRecognizers[0] is TapGestureRecognizer tap)
        {
            if (tap.CommandParameter is Mineral mineral)
            {
                await Shell.Current.GoToAsync($"mineraldetail?id={mineral.Id}");
            }
        }
    }
}
