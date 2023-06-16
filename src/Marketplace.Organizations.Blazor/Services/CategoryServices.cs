using System.Net.Http.Headers;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using Marketplace.Organizations.Blazor.Models;
using Microsoft.AspNetCore.Components;

namespace Marketplace.Organizations.Blazor.Services;

public class CategoryServices
{
    private readonly ILocalStorageService _storage;
    private readonly HttpClient _httpClient;
    private readonly NavigationManager NavigationManager;

    public CategoryServices(ILocalStorageService storage, HttpClient httpClient, NavigationManager navigationManager)
    {
        _storage = storage;
        _httpClient = httpClient;
        NavigationManager = navigationManager;
    }

    public async Task<List<OrganizationModel>?> Organizations()
        {
            var token = await _storage.GetItemAsStringAsync("token");
            var request = new HttpRequestMessage(HttpMethod.Get, "/organizations/GetOrganizations");
            request.Headers.Add("Authorization",$"Bearer {token}");
            var response = await _httpClient.SendAsync(request);
            return (await response.Content.ReadFromJsonAsync<List<OrganizationModel>>());
        }
    public async void CreateOrganization(CreateOrganizationModel model)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.Name),"Name");
            content.Add(new StringContent(model.Contact),"Contact");
            content.Add(new StringContent(model.Description),"Description");
            if (model.Addresses != null)
            {
                for (int i = 0; i < model.Addresses.Count; i++)
                {
                    content.Add(new StringContent(model.Addresses[i].Address), $"Addresses[{i}].Address");
                }
            }
            if (model.Logo != null)
            {
                using var memoryStream = new MemoryStream();
                await model.Logo.OpenReadStream().CopyToAsync(memoryStream);
                content.Add(new ByteArrayContent(memoryStream.ToArray()),"Logo",model.Logo.Name);
            }
            
            var token = await _storage.GetItemAsStringAsync("token");
            var request = new HttpRequestMessage(HttpMethod.Post, "/organizations/CreateOrganization");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",$"{token}");
        request.Content = content; 
        await _httpClient.SendAsync(request);
        NavigationManager.NavigateTo("/GetOrganizations");
    }

    public async Task<OrganizationModel?> GetById(string Id)
    {
        var token = await _storage.GetItemAsStringAsync("token");
        var request = new HttpRequestMessage(HttpMethod.Get, $"/organizations/GetById/{Id}");
        request.Headers.Add("Authorization",$"Bearer {token}");
        var response = await _httpClient.SendAsync(request);
        return (await response.Content.ReadFromJsonAsync<OrganizationModel>());
        
    }
}