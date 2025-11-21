using MiningTradingClientApp.Models;
using MiningTradingClientApp.Services;

namespace MiningTradingClientApp.Views;

[QueryProperty(nameof(MineralId), "id")]
public partial class OrderPage : ContentPage
{
    private readonly IMineralService _mineralService;
    private Mineral? _mineral;

    public string? MineralId { get; set; }

    public OrderPage(IMineralService mineralService)
    {
        InitializeComponent();
        _mineralService = mineralService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMineralData();
    }

    private async Task LoadMineralData()
    {
        if (string.IsNullOrEmpty(MineralId) || !Guid.TryParse(MineralId, out var mineralGuid))
        {
            await DisplayAlert("Error", "Invalid mineral ID", "OK");
            await Shell.Current.GoToAsync("..");
            return;
        }

        try
        {
            var minerals = await _mineralService.GetAvailableMineralsAsync();
            _mineral = minerals.FirstOrDefault(m => m.Id == mineralGuid);

            if (_mineral == null)
            {
                await DisplayAlert("Error", "Mineral not found", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }

            MineralNameLabel.Text = _mineral.Name;
            PriceLabel.Text = _mineral.Price.ToString("C");
            SellerLabel.Text = _mineral.Seller;
            
            CalculateTotal();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load mineral data: {ex.Message}", "OK");
        }
    }

    private void OnQuantityChanged(object sender, TextChangedEventArgs e)
    {
        CalculateTotal();
    }

    private void CalculateTotal()
    {
        if (_mineral == null)
            return;

        if (double.TryParse(QuantityEntry.Text, out var quantity) && quantity > 0)
        {
            var total = _mineral.Price * (decimal)quantity;
            TotalLabel.Text = total.ToString("C");
        }
        else
        {
            TotalLabel.Text = "$0.00";
        }
    }

    private async void OnSubmitOrderClicked(object sender, EventArgs e)
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(BuyerNameEntry.Text))
        {
            await DisplayAlert("Validation Error", "Please enter your name", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(BuyerEmailEntry.Text))
        {
            await DisplayAlert("Validation Error", "Please enter your email", "OK");
            return;
        }

        if (!double.TryParse(QuantityEntry.Text, out var quantity) || quantity <= 0)
        {
            await DisplayAlert("Validation Error", "Please enter a valid quantity", "OK");
            return;
        }

        if (_mineral == null)
        {
            await DisplayAlert("Error", "Mineral data is not available", "OK");
            return;
        }

        // In a real app, you would submit the order to a backend service here
        var total = _mineral.Price * (decimal)quantity;
        var message = $"Order placed for {quantity} kg of {_mineral.Name}\n" +
                     $"Total: {total:C}\n" +
                     $"Buyer: {BuyerNameEntry.Text}";

        // Navigate to confirmation page
        await Shell.Current.GoToAsync($"orderconfirmation?mineralId={_mineral.Id}&quantity={quantity}&buyer={BuyerNameEntry.Text}");
    }
}
