using MiningTradingClientApp.Models;
using MiningTradingClientApp.Services;
using System.Collections.ObjectModel;

namespace MiningTradingClientApp.Views;

public partial class MineralsPage : ContentPage
{
    private readonly IMineralService _mineralService;
    private ObservableCollection<Mineral> _allMinerals = new();
    private ObservableCollection<Mineral> _displayedMinerals = new();

    public MineralsPage(IMineralService mineralService)
    {
        InitializeComponent();
        _mineralService = mineralService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadMinerals();
    }

    private async Task LoadMinerals()
    {
        try
        {
            var minerals = await _mineralService.GetAvailableMineralsAsync();
            _allMinerals = new ObservableCollection<Mineral>(minerals);
            _displayedMinerals = new ObservableCollection<Mineral>(minerals);
            MineralsCollection.ItemsSource = _displayedMinerals;
            
            EmptyStateView.IsVisible = !_displayedMinerals.Any();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load minerals: {ex.Message}", "OK");
        }
    }

    private async void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        var searchText = e.NewTextValue?.ToLower() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            _displayedMinerals = new ObservableCollection<Mineral>(_allMinerals);
        }
        else
        {
            var filtered = _allMinerals.Where(m =>
                m.Name?.ToLower().Contains(searchText) == true ||
                m.Description?.ToLower().Contains(searchText) == true ||
                m.Origin?.ToLower().Contains(searchText) == true ||
                m.Seller?.ToLower().Contains(searchText) == true
            );
            _displayedMinerals = new ObservableCollection<Mineral>(filtered);
        }

        MineralsCollection.ItemsSource = _displayedMinerals;
        EmptyStateView.IsVisible = !_displayedMinerals.Any();
    }

    private async void OnMineralSelected(object sender, EventArgs e)
    {
        if (sender is View view && view.GestureRecognizers[0] is TapGestureRecognizer tap)
        {
            if (tap.CommandParameter is Mineral mineral)
            {
                await Shell.Current.GoToAsync($"mineraldetail?id={mineral.Id}");
            }
        }
    }

    private async void OnBuyNowClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is Mineral mineral)
        {
            await Shell.Current.GoToAsync($"order?id={mineral.Id}");
        }
    }
}
