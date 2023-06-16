using System.Net.Http.Json;

namespace Marketplace.Organizations.Blazor.Managers;

public class RequestManager
{
	private readonly ILocalStorageService _storage;
	private readonly HttpClient _httpClient;

	public RequestManager(ILocalStorageService storage, HttpClient httpClient)
	{
		_storage = storage;
		_httpClient = httpClient;
	}

	public async Task<T?> Get<T>(string url) where T : class
	{
		var response = await SendAsync(url, HttpMethod.Get);
		return await response.Content.ReadFromJsonAsync<T>();
	}

	public async Task<T?> Post<T>(string url, object body) where T : class
	{
		var content = JsonContent.Create(body);
		var response = await SendAsync(url, HttpMethod.Post, content);
		return await response.Content.ReadFromJsonAsync<T>();
	}

	public async Task<HttpResponseMessage> SendAsync(string url, HttpMethod method, HttpContent? content = null)
	{
		var token = await _storage.GetItemAsStringAsync("token");

		var request = new HttpRequestMessage(method, url);
		request.Headers.Add("Authorization", $"Bearer {token}");

		if (content != null)
			request.Content = content;

		return await _httpClient.SendAsync(request);
	}
}