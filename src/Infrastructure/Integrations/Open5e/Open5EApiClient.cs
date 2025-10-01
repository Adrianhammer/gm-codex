using System.Net.Http.Json;

namespace gm_codex.Infrastructure.Integrations.Open5e;

public class Open5EApiClient
{
    private readonly HttpClient _httpClient;

    public Open5EApiClient(HttpClient httpClient) =>  _httpClient = httpClient;

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        return await _httpClient.GetFromJsonAsync<T>(endpoint);
    }
}