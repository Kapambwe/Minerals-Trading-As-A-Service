using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpPaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/payments";

    public HttpPaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Payment>> GetPaymentsForTradeAsync(string tradeId)
    {
        var payments = await _httpClient.GetFromJsonAsync<IEnumerable<Payment>>($"{BaseEndpoint}/trade/{tradeId}");
        return payments ?? Enumerable.Empty<Payment>();
    }

    public async Task<Payment> GetPaymentByIdAsync(string paymentId)
    {
        return (await _httpClient.GetFromJsonAsync<Payment>($"{BaseEndpoint}/{paymentId}"))!;
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, payment);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdatePaymentAsync(Payment payment)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{payment.Id}", payment);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePaymentAsync(string paymentId)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{paymentId}");
        response.EnsureSuccessStatusCode();
    }
}
