using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpSettlementService : ISettlementService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/settlements";

    public HttpSettlementService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Settlement>> GetAllSettlementsAsync()
    {
        var settlements = await _httpClient.GetFromJsonAsync<IEnumerable<Settlement>>(BaseEndpoint);
        return settlements ?? Enumerable.Empty<Settlement>();
    }

    public async Task<Settlement?> GetSettlementByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Settlement>($"{BaseEndpoint}/{id}");
    }

    public async Task<Settlement?> GetSettlementByTradeIdAsync(string tradeId)
    {
        return await _httpClient.GetFromJsonAsync<Settlement>($"{BaseEndpoint}/trade/{tradeId}");
    }

    public async Task<Settlement> CreateSettlementAsync(Settlement settlement)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, settlement);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Settlement>() ?? settlement;
    }

    public async Task<Settlement> UpdateSettlementAsync(Settlement settlement)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{settlement.Id}", settlement);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Settlement>() ?? settlement;
    }

    public async Task<bool> DeleteSettlementAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Settlement> CompleteSettlementAsync(string settlementId)
    {
        var response = await _httpClient.PostAsync($"{BaseEndpoint}/{settlementId}/complete", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Settlement>() ?? new Settlement();
    }
}
