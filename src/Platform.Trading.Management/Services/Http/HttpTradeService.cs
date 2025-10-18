using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpTradeService : ITradeService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/trades";

    public HttpTradeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Trade>> GetAllTradesAsync()
    {
        var trades = await _httpClient.GetFromJsonAsync<IEnumerable<Trade>>(BaseEndpoint);
        return trades ?? Enumerable.Empty<Trade>();
    }

    public async Task<Trade?> GetTradeByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Trade>($"{BaseEndpoint}/{id}");
    }

    public async Task<Trade> CreateTradeAsync(Trade trade)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, trade);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Trade>() ?? trade;
    }

    public async Task<Trade> UpdateTradeAsync(Trade trade)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{trade.Id}", trade);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Trade>() ?? trade;
    }

    public async Task<bool> DeleteTradeAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Trade> NovateTradeAsync(string tradeId)
    {
        var response = await _httpClient.PostAsync($"{BaseEndpoint}/{tradeId}/novate", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Trade>() ?? new Trade();
    }

    public async Task<Trade> ConfirmTradeAsync(string tradeId)
    {
        var response = await _httpClient.PostAsync($"{BaseEndpoint}/{tradeId}/confirm", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Trade>() ?? new Trade();
    }
}
