using Blazored.LocalStorage;
using Marketplace.Organizations.Blazor.Models.CategoryModels;
using System.Net.Http.Json;

namespace Marketplace.Organizations.Blazor.Services;

public class CategoryService
{
    private readonly ILocalStorageService _storage;
    private readonly HttpClient _httpClient;

    public CategoryService(ILocalStorageService storage, HttpClient httpClient)
    {
        _storage = storage;
        _httpClient = httpClient;
    }


    public async Task<List<CategoryModel>?> GetCategories()
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Get, "/Categories");
        request.Headers.Add("Authorization", $"Bearer {token}");
        var response = await _httpClient.SendAsync(request);
        return (await response.Content.ReadFromJsonAsync<List<CategoryModel>>());
    }
    public async Task<CategoryModel?> GetCategoryById(Guid categoryId)
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Get, "/Categories");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(categoryId);
        var response = await _httpClient.SendAsync(request);
        return (await response.Content.ReadFromJsonAsync<CategoryModel>());
    }

    public async Task DeleteCategory(Guid categoryId)
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Post, $"/products/categories");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(categoryId);
         await _httpClient.SendAsync(request);
    }


    public async Task CreateCategory(CreateCategoryModel category)
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Post, "/categories");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(category);
        await _httpClient.SendAsync(request);
    }

    public async Task UpdateCategory(Guid categoryId, CreateCategoryModel category)
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Post, "/categories");
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = JsonContent.Create(new {categoryId, category });
        await _httpClient.SendAsync(request);
    }
    
}