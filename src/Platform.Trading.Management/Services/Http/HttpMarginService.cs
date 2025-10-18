using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpMarginService : IMarginService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/margins";

    public HttpMarginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Margin>> GetAllMarginsAsync()
    {
        var margins = await _httpClient.GetFromJsonAsync<IEnumerable<Margin>>(BaseEndpoint);
        return margins ?? Enumerable.Empty<Margin>();
    }

    public async Task<Margin?> GetMarginByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Margin>($"{BaseEndpoint}/{id}");
    }

    public async Task<IEnumerable<Margin>> GetMarginsByTradeIdAsync(string tradeId)
    {
        var margins = await _httpClient.GetFromJsonAsync<IEnumerable<Margin>>($"{BaseEndpoint}/trade/{tradeId}");
        return margins ?? Enumerable.Empty<Margin>();
    }

    public async Task<Margin> CreateMarginAsync(Margin margin)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, margin);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Margin>() ?? margin;
    }

    public async Task<Margin> UpdateMarginAsync(Margin margin)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{margin.Id}", margin);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Margin>() ?? margin;
    }

    public async Task<bool> DeleteMarginAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Margin> CalculateVariationMarginAsync(string tradeId, decimal currentMarketPrice)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseEndpoint}/calculate-variation", new { tradeId, currentMarketPrice });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Margin>() ?? new Margin();
    }
}
