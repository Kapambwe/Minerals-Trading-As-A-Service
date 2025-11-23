using MiningTradingMobileApp.ViewModels;

namespace MiningTradingMobileApp.Views.SellerPages;

public partial class SettlementList : ContentView
{
    public SettlementList()
    {
        InitializeComponent();
        BindingContext = new SettlementListViewModel(new Services.MockSettlementService()); // This will be replaced by DI later
    }
}