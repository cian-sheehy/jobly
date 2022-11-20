using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jobly.Models;

namespace Jobly.Clients;

public class GreenhouseClient
{
    private readonly HttpClient _httpClient;
    private const string BaseUri = "https://boards-api.greenhouse.io/v1";

    public GreenhouseClient(HttpClient http)
    {
        _httpClient = http;
    }

    public async Task<GreenhouseJobs?> GetJobsFromCompany(string company)
    {
        var response = await _httpClient.GetAsync($"{BaseUri}/boards/{company}/jobs");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<GreenhouseJobs>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}