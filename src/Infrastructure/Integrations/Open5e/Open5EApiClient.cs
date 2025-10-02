using System.Net.Http.Json;
using System.Text.Json;

namespace gm_codex.Infrastructure.Integrations.Open5e;

public class Open5EApiClient
{
    private readonly HttpClient _httpClient;

    public Open5EApiClient(HttpClient httpClient) =>  _httpClient = httpClient;

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
        {
            var raw = await response.Content.ReadAsStringAsync();
            Console.WriteLine(raw.Substring(0, Math.Min(raw.Length, 200)));
            Console.WriteLine($"Open5e error: {response.StatusCode} - {raw}");
            return default;
        }

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return await response.Content.ReadFromJsonAsync<T>(options);

    }
}