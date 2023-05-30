using System.Net.Http.Json;
using Blazored.LocalStorage;
using Chat.Blazor.Pages.Account;
using Chat.Blazor.Pages.Conversations;
using static System.Net.WebRequestMethods;

namespace Chat.Blazor.Services;

public class ConversationsService
{
	private readonly ILocalStorageService _storage;
	private readonly HttpClient _httpClient;

	public ConversationsService(ILocalStorageService storage, HttpClient httpClient)
	{
		_storage = storage;
		_httpClient = httpClient;
	}

	public async Task<T?> Get<T>(string url) where T:class
	{
		var token = await _storage.GetItemAsStringAsync("token");

		var request = new HttpRequestMessage(HttpMethod.Get, url);
		request.Headers.Add("Authorization", $"Bearer {token}");

		var response = await _httpClient.SendAsync(request);

		return await response.Content.ReadFromJsonAsync<T>();
	}
}