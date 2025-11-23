using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class PaymentForm : ContentView
{
    public PaymentForm()
    {
        InitializeComponent();
        BindingContext = new PaymentFormViewModel(new Services.MockPaymentService()); // This will be replaced by DI later
    }

    public static readonly BindableProperty TradeIdProperty =
        BindableProperty.Create(nameof(TradeId), typeof(string), typeof(PaymentForm), string.Empty, propertyChanged: OnTradeIdChanged);

    public string TradeId
    {
        get => (string)GetValue(TradeIdProperty);
        set => SetValue(TradeIdProperty, value);
    }

    private static void OnTradeIdChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PaymentForm control && control.BindingContext is PaymentFormViewModel viewModel)
        {
            viewModel.TradeId = (string)newValue;
        }
    }
}