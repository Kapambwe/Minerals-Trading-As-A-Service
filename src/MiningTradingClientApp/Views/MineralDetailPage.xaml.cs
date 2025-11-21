using MiningTradingClientApp.Models;
using MiningTradingClientApp.Services;

namespace MiningTradingClientApp.Views;

[QueryProperty(nameof(MineralId), "id")]
public partial class MineralDetailPage : ContentPage
{
    private readonly IMineralService _mineralService;
    private Mineral? _mineral;

    public string? MineralId { get; set; }

    public MineralDetailPage(IMineralService mineralService)
    {
        InitializeComponent();
        _mineralService = mineralService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMineralDetails();
    }

    private async Task LoadMineralDetails()
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

            // Update UI
            MineralName.Text = _mineral.Name;
            PriceLabel.Text = _mineral.Price.ToString("C");
            DescriptionLabel.Text = _mineral.Description;
            WeightLabel.Text = $"{_mineral.Weight:N2} kg";
            OriginLabel.Text = _mineral.Origin;
            SellerLabel.Text = _mineral.Seller;
            DateListedLabel.Text = _mineral.DateListed.ToShortDateString();
            VerifiedBadge.IsVisible = _mineral.IsVerified;
            
            if (!string.IsNullOrEmpty(_mineral.ImageUrl))
            {
                MineralImage.Source = _mineral.ImageUrl;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load mineral details: {ex.Message}", "OK");
        }
    }

    private async void OnBuyClicked(object sender, EventArgs e)
    {
        if (_mineral != null)
        {
            await Shell.Current.GoToAsync($"order?id={_mineral.Id}");
        }
    }
}
