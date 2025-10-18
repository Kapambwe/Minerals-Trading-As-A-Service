using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpSellerService : ISellerService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/sellers";

    public HttpSellerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Seller>> GetAllSellersAsync()
    {
        var sellers = await _httpClient.GetFromJsonAsync<IEnumerable<Seller>>(BaseEndpoint);
        return sellers ?? Enumerable.Empty<Seller>();
    }

    public async Task<Seller?> GetSellerByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Seller>($"{BaseEndpoint}/{id}");
    }

    public async Task<Seller> CreateSellerAsync(Seller seller)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, seller);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Seller>() ?? seller;
    }

    public async Task<Seller> UpdateSellerAsync(Seller seller)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{seller.Id}", seller);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Seller>() ?? seller;
    }

    public async Task<bool> DeleteSellerAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<Seller> ApproveSellerAsync(string sellerId)
    {
        var response = await _httpClient.PostAsync($"{BaseEndpoint}/{sellerId}/approve", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Seller>() ?? new Seller();
    }

    public async Task<Seller> UpdateKYCStatusAsync(string sellerId, string kycStatus)
    {
        var response = await _httpClient.PostAsJsonAsync($"{BaseEndpoint}/{sellerId}/kycstatus", new { kycStatus });
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Seller>() ?? new Seller();
    }
}
