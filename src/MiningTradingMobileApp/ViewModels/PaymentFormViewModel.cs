using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MiningTradingMobileApp.Models;
using MiningTradingMobileApp.Services;

namespace MiningTradingMobileApp.ViewModels;

public partial class PaymentFormViewModel : ObservableObject
{
    private readonly IPaymentService _paymentService;

    [ObservableProperty]
    private string tradeId = string.Empty;

    [ObservableProperty]
    private decimal amount;

    [ObservableProperty]
    private string description = string.Empty;

    public PaymentFormViewModel(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [RelayCommand]
    private async Task SubmitPayment()
    {
        if (string.IsNullOrEmpty(TradeId))
        {
            // Handle error: TradeId is not set
            return;
        }

        if (Amount <= 0)
        {
            // Handle error: Amount must be greater than 0
            return;
        }

        if (string.IsNullOrEmpty(Description))
        {
            // Handle error: Description is required
            return;
        }

        var payment = new Payment
        {
            TradeId = TradeId,
            Amount = Amount,
            Description = Description,
            PaymentDate = DateTime.Now
        };

        await _paymentService.AddPaymentAsync(payment);

        // Optionally, navigate back or show a success message
        // For now, just clear the form
        Amount = 0;
        Description = string.Empty;
    }
}