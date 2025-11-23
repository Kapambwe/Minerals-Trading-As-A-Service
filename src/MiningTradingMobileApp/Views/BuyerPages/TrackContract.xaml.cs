namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class TrackContract : ContentPage
{
    public TrackContract()
    {
        InitializeComponent();
    }

    private void OnLoadContractDetailsClicked(object sender, EventArgs e)
    {
        ContractDetailsControl.TradeId = TradeIdEntry.Text;
    }
}