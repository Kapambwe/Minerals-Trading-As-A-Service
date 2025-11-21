using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace MineralsTradingMobileApp.Pages;

public partial class OrderPlacementPage : ContentPage
{
    private readonly IMineralListingService _mineralListingService;
    private readonly ITradeService _tradeService;
    private MineralListing? _selectedMineral;
    private decimal _currentQuantity = 0;

    public OrderPlacementPage(IMineralListingService mineralListingService, ITradeService tradeService)
    {
        InitializeComponent();
        _mineralListingService = mineralListingService;
        _tradeService = tradeService;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAvailableMinerals();
    }

    private async Task LoadAvailableMinerals()
    {
        try
        {
            LoadingLabel.IsVisible = true;
            MineralsCollectionView.IsVisible = false;

            var minerals = await _mineralListingService.GetAvailableMineralListingsAsync();
            MineralsCollectionView.ItemsSource = minerals;

            LoadingLabel.IsVisible = false;
            MineralsCollectionView.IsVisible = true;

            if (!minerals.Any())
            {
                LoadingLabel.Text = "No minerals available at the moment";
                LoadingLabel.IsVisible = true;
            }
        }
        catch (Exception ex)
        {
            LoadingLabel.Text = $"Error loading minerals: {ex.Message}";
            LoadingLabel.IsVisible = true;
        }
    }

    private void OnMineralSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is MineralListing selectedMineral)
        {
            _selectedMineral = selectedMineral;
            ShowOrderDetails(selectedMineral);
        }
    }

    private void ShowOrderDetails(MineralListing mineral)
    {
        OrderDetailsFrame.IsVisible = true;
        
        SelectedMineralLabel.Text = $"{mineral.MetalType} - {mineral.SellerCompanyName}";
        SelectedPriceLabel.Text = $"Price: ${mineral.PricePerTon:N2} per MT | Quality: {mineral.QualityGrade}";
        MaxQuantityLabel.Text = $"Maximum available: {mineral.QuantityAvailable} MT";
        
        // Reset form
        QuantityEntry.Text = "1";
        _currentQuantity = 1;
        UpdateTotalValue();
        
        // Set default delivery date to 30 days from now
        DeliveryDatePicker.Date = DateTime.Now.AddDays(30);
    }

    private void OnQuantityChanged(object? sender, TextChangedEventArgs e)
    {
        if (decimal.TryParse(e.NewTextValue, out decimal quantity))
        {
            if (_selectedMineral != null && quantity > _selectedMineral.QuantityAvailable)
            {
                quantity = _selectedMineral.QuantityAvailable;
                QuantityEntry.Text = quantity.ToString();
            }
            
            if (quantity < 0)
            {
                quantity = 0;
                QuantityEntry.Text = "0";
            }
            
            _currentQuantity = quantity;
            UpdateTotalValue();
        }
    }

    private void OnIncreaseQuantity(object sender, EventArgs e)
    {
        if (_selectedMineral != null && _currentQuantity < _selectedMineral.QuantityAvailable)
        {
            _currentQuantity++;
            QuantityEntry.Text = _currentQuantity.ToString();
        }
    }

    private void OnDecreaseQuantity(object sender, EventArgs e)
    {
        if (_currentQuantity > 0)
        {
            _currentQuantity--;
            QuantityEntry.Text = _currentQuantity.ToString();
        }
    }

    private void UpdateTotalValue()
    {
        if (_selectedMineral != null)
        {
            var totalValue = _currentQuantity * _selectedMineral.PricePerTon;
            TotalValueLabel.Text = $"Total: ${totalValue:N2}";
        }
    }

    private void OnClearOrder(object sender, EventArgs e)
    {
        QuantityEntry.Text = "1";
        BuyerNameEntry.Text = string.Empty;
        NotesEditor.Text = string.Empty;
        DeliveryDatePicker.Date = DateTime.Now.AddDays(30);
        MineralsCollectionView.SelectedItem = null;
        OrderDetailsFrame.IsVisible = false;
        _selectedMineral = null;
    }

    private async void OnPlaceOrder(object sender, EventArgs e)
    {
        if (_selectedMineral == null)
        {
            await DisplayAlert("Error", "Please select a mineral first", "OK");
            return;
        }

        if (_currentQuantity <= 0)
        {
            await DisplayAlert("Error", "Please enter a valid quantity", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(BuyerNameEntry.Text))
        {
            await DisplayAlert("Error", "Please enter your company name", "OK");
            return;
        }

        if (_currentQuantity > _selectedMineral.QuantityAvailable)
        {
            await DisplayAlert("Error", $"Quantity exceeds available amount ({_selectedMineral.QuantityAvailable} MT)", "OK");
            return;
        }

        // Confirm order
        bool confirm = await DisplayAlert(
            "Confirm Order",
            $"Place order for {_currentQuantity} MT of {_selectedMineral.MetalType} at ${_selectedMineral.PricePerTon:N2}/MT?\n\nTotal: ${(_currentQuantity * _selectedMineral.PricePerTon):N2}",
            "Yes, Place Order",
            "Cancel");

        if (!confirm)
            return;

        try
        {
            // Create trade from order
            var trade = new Trade
            {
                TradeNumber = $"ORD{DateTime.Now:yyyyMMddHHmmss}",
                TradeDate = DateTime.Now,
                BuyerName = BuyerNameEntry.Text,
                SellerName = _selectedMineral.SellerCompanyName,
                MetalType = _selectedMineral.MetalType,
                Quantity = _currentQuantity,
                PricePerTon = _selectedMineral.PricePerTon,
                TotalValue = _currentQuantity * _selectedMineral.PricePerTon,
                DeliveryDate = DeliveryDatePicker.Date,
                Status = TradeStatus.Pending,
                Notes = NotesEditor.Text
            };

            var createdTrade = await _tradeService.CreateTradeAsync(trade);

            // Update mineral listing status if fully purchased
            if (_currentQuantity >= _selectedMineral.QuantityAvailable)
            {
                await _mineralListingService.UpdateListingStatusAsync(_selectedMineral.Id, "Sold");
            }

            await DisplayAlert(
                "Success",
                $"Order placed successfully!\n\nOrder Number: {createdTrade.TradeNumber}\nTotal Value: ${createdTrade.TotalValue:N2}",
                "OK");

            // Navigate to order details or trades page
            await Shell.Current.GoToAsync($"{Routes.Trades}");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to place order: {ex.Message}", "OK");
        }
    }
}
