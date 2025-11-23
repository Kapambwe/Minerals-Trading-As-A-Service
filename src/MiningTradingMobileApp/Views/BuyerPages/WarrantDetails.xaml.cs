using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class WarrantDetails : ContentView
{
    public WarrantDetails()
    {
        InitializeComponent();
        BindingContext = new WarrantDetailsViewModel(new Services.MockWarrantService()); // This will be replaced by DI later
    }

    public static readonly BindableProperty WarrantIdProperty =
        BindableProperty.Create(nameof(WarrantId), typeof(string), typeof(WarrantDetails), string.Empty, propertyChanged: OnWarrantIdChanged);

    public string WarrantId
    {
        get => (string)GetValue(WarrantIdProperty);
        set => SetValue(WarrantIdProperty, value);
    }

    private static async void OnWarrantIdChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is WarrantDetails control && control.BindingContext is WarrantDetailsViewModel viewModel)
        {
            viewModel.WarrantId = (string)newValue;
            await viewModel.LoadWarrantDetailsAsync();
        }
    }
}