using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class MarginDisplay : ContentView
{
    public MarginDisplay()
    {
        InitializeComponent();
        BindingContext = new MarginDisplayViewModel(new Services.MockMarginService(), new Services.MockMarginRequestService()); // This will be replaced by DI later
    }

    public static readonly BindableProperty TradeIdProperty =
        BindableProperty.Create(nameof(TradeId), typeof(string), typeof(MarginDisplay), string.Empty, propertyChanged: OnTradeIdChanged);

    public string TradeId
    {
        get => (string)GetValue(TradeIdProperty);
        set => SetValue(TradeIdProperty, value);
    }

    private static async void OnTradeIdChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is MarginDisplay control && control.BindingContext is MarginDisplayViewModel viewModel)
        {
            viewModel.TradeId = (string)newValue;
            await viewModel.LoadMarginDetailsAsync();
        }
    }
}