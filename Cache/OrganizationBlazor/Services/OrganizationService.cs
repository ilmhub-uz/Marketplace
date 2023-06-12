using Blazored.LocalStorage;

namespace OrganizationBlazor.Services;

public class OrganizationService
{
    private readonly ILocalStorageService _storage;
    private readonly HttpClient _httpClient;

    public OrganizationService(ILocalStorageService storage, HttpClient httpClient)
    {
        _storage = storage;
        _httpClient = httpClient;
    }

    public async Task<T> Get<T>(string url) where T : class
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.SendAsync(request);
        return (await response.Content.ReadFromJsonAsync<T>())!;
    }
    public async Task Post<T>(string url,T t) where T : class
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(t);
        await _httpClient.SendAsync(request);
    }
}