using MiningTradingClientApp.Models;
using MiningTradingClientApp.Services;

namespace MiningTradingClientApp.Views;

[QueryProperty(nameof(MineralId), "mineralId")]
[QueryProperty(nameof(Quantity), "quantity")]
[QueryProperty(nameof(Buyer), "buyer")]
public partial class OrderConfirmationPage : ContentPage
{
    private readonly IMineralService _mineralService;

    public string? MineralId { get; set; }
    public string? Quantity { get; set; }
    public string? Buyer { get; set; }

    public OrderConfirmationPage(IMineralService mineralService)
    {
        InitializeComponent();
        _mineralService = mineralService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadOrderDetails();
    }

    private async Task LoadOrderDetails()
    {
        if (string.IsNullOrEmpty(MineralId) || !Guid.TryParse(MineralId, out var mineralGuid))
        {
            return;
        }

        try
        {
            var minerals = await _mineralService.GetAvailableMineralsAsync();
            var mineral = minerals.FirstOrDefault(m => m.Id == mineralGuid);

            if (mineral != null && double.TryParse(Quantity, out var quantity))
            {
                MineralNameLabel.Text = mineral.Name;
                QuantityLabel.Text = $"{quantity:N2} kg";
                BuyerLabel.Text = Buyer ?? "Unknown";
                
                var total = mineral.Price * (decimal)quantity;
                TotalLabel.Text = total.ToString("C");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load order details: {ex.Message}", "OK");
        }
    }

    private async void OnViewMarketsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//minerals");
    }

    private async void OnViewPortfolioClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//portfolio");
    }
}
