using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpBuyerService : IBuyerService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/buyers";

    public HttpBuyerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Buyer>> GetAllBuyersAsync()
    {
        var buyers = await _httpClient.GetFromJsonAsync<IEnumerable<Buyer>>(BaseEndpoint);
        return buyers ?? Enumerable.Empty<Buyer>();
    }

    public async Task<Buyer?> GetBuyerByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Buyer>($"{BaseEndpoint}/{id}");
    }

    public async Task<Buyer> CreateBuyerAsync(Buyer buyer)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, buyer);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Buyer>() ?? buyer;
    }

    public async Task<Buyer> UpdateBuyerAsync(Buyer buyer)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{buyer.Id}", buyer);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Buyer>() ?? buyer;
    }

    public async Task<bool> DeleteBuyerAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Buyer> ApproveBuyerAsync(string buyerId)
    {
        var response = await _httpClient.PostAsync($"{BaseEndpoint}/{buyerId}/approve", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Buyer>() ?? new Buyer();
    }

    public async Task<Buyer> UpdateKYCStatusAsync(string buyerId, string kycStatus)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseEndpoint}/{buyerId}/kycstatus", new { kycStatus });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Buyer>() ?? new Buyer();
    }
}
