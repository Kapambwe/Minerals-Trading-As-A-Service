using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class ContractDetails : ContentView
{
    public ContractDetails()
    {
        InitializeComponent();
        BindingContext = new ContractDetailsViewModel(new Services.MockTradeService()); // This will be replaced by DI later
    }

    public static readonly BindableProperty TradeIdProperty =
        BindableProperty.Create(nameof(TradeId), typeof(string), typeof(ContractDetails), string.Empty, propertyChanged: OnTradeIdChanged);

    public string TradeId
    {
        get => (string)GetValue(TradeIdProperty);
        set => SetValue(TradeIdProperty, value);
    }

    private static async void OnTradeIdChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ContractDetails control && control.BindingContext is ContractDetailsViewModel viewModel)
        {
            viewModel.TradeId = (string)newValue;
            await viewModel.LoadTradeDetailsAsync();
        }
    }
}