using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpWarrantService : IWarrantService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/warrants";

    public HttpWarrantService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Warrant>> GetAllWarrantsAsync()
    {
        var warrants = await _httpClient.GetFromJsonAsync<IEnumerable<Warrant>>(BaseEndpoint);
        return warrants ?? Enumerable.Empty<Warrant>();
    }

    public async Task<Warrant?> GetWarrantByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Warrant>($"{BaseEndpoint}/{id}");
    }

    public async Task<IEnumerable<Warrant>> GetWarrantsByTradeIdAsync(string tradeId)
    {
        var warrants = await _httpClient.GetFromJsonAsync<IEnumerable<Warrant>>($"{BaseEndpoint}/trade/{tradeId}");
        return warrants ?? Enumerable.Empty<Warrant>();
    }

    public async Task<Warrant> CreateWarrantAsync(Warrant warrant)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, warrant);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Warrant>() ?? warrant;
    }

    public async Task<Warrant> UpdateWarrantAsync(Warrant warrant)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{warrant.Id}", warrant);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Warrant>() ?? warrant;
    }

    public async Task<bool> DeleteWarrantAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Warrant> TransferWarrantAsync(string warrantId, string newOwner)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseEndpoint}/{warrantId}/transfer", new { newOwner });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Warrant>() ?? new Warrant();
    }
}
