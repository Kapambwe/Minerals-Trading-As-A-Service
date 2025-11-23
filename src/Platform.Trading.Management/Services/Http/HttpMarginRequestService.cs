using System.Net.Http.Json;
using Platform.Trading.Management.Models;
using Platform.Trading.Management.Services.Interfaces;

namespace Platform.Trading.Management.Services.Http;

public class HttpMarginRequestService : IMarginRequestService
{
    private readonly HttpClient _httpClient;
    private const string BaseEndpoint = "api/marginrequests";

    public HttpMarginRequestService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<MarginRequest>> GetMarginRequestsForTradeAsync(string tradeId)
    {
        var marginRequests = await _httpClient.GetFromJsonAsync<IEnumerable<MarginRequest>>($"{BaseEndpoint}/trade/{tradeId}");
        return marginRequests ?? Enumerable.Empty<MarginRequest>();
    }

    public async Task<MarginRequest> GetMarginRequestByIdAsync(string marginRequestId)
    {
        return (await _httpClient.GetFromJsonAsync<MarginRequest>($"{BaseEndpoint}/{marginRequestId}"))!;
    }

    public async Task AddMarginRequestAsync(MarginRequest marginRequest)
    {
        var response = await _httpClient.PostAsJsonAsync(BaseEndpoint, marginRequest);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateMarginRequestAsync(MarginRequest marginRequest)
    {
        var response = await _httpClient.PutAsJsonAsync($"{BaseEndpoint}/{marginRequest.Id}", marginRequest);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteMarginRequestAsync(string marginRequestId)
    {
        var response = await _httpClient.DeleteAsync($"{BaseEndpoint}/{marginRequestId}");
        response.EnsureSuccessStatusCode();
    }
}
