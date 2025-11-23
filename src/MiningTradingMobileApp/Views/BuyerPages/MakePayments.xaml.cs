namespace MiningTradingMobileApp.Views.BuyerPages;

public partial class MakePayments : ContentPage
{
    public MakePayments()
    {
        InitializeComponent();
    }

    private void OnSetTradeIdClicked(object sender, EventArgs e)
    {
        PaymentFormControl.TradeId = TradeIdEntry.Text;
    }
}