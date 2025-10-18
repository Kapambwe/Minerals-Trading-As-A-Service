using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpWarehouseService : IWarehouseService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/warehouses";

    public HttpWarehouseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Warehouse>> GetAllWarehousesAsync()
    {
        var warehouses = await _httpClient.GetFromJsonAsync<IEnumerable<Warehouse>>(BaseEndpoint);
        return warehouses ?? Enumerable.Empty<Warehouse>();
    }

    public async Task<Warehouse?> GetWarehouseByIdAsync(string id)
    {
        return await _httpClient.GetFromJsonAsync<Warehouse>($"{BaseEndpoint}/{id}");
    }

    public async Task<Warehouse> CreateWarehouseAsync(Warehouse warehouse)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, warehouse);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Warehouse>() ?? warehouse;
    }

    public async Task<Warehouse> UpdateWarehouseAsync(Warehouse warehouse)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{warehouse.Id}", warehouse);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Warehouse>() ?? warehouse;
    }

    public async Task<bool> DeleteWarehouseAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<IEnumerable<Warehouse>> GetApprovedWarehousesAsync()
    {
        var warehouses = await _httpClient.GetFromJsonAsync<IEnumerable<Warehouse>>($"{BaseEndpoint}/approved");
        return warehouses ?? Enumerable.Empty<Warehouse>();
    }

    public async Task<Warehouse> ApproveWarehouseAsync(string warehouseId)
    {
        var response = await _httpClient.PostAsync($"{BaseEndpoint}/{warehouseId}/approve", null);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Warehouse>() ?? new Warehouse();
    }
}
