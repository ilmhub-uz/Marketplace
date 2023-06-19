using Blazored.LocalStorage;
using Marketplace.Organizations.Blazor.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Marketplace.Organizations.Blazor.Managers
{
    public class SavedTokenStorageManager
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;
        

        public SavedTokenStorageManager(ILocalStorageService localStorage,HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
            
        }
        public async Task SavedToken(LoginUserModel _userModel)
        {
            var response = await _http.PostAsJsonAsync("/account/login", _userModel);
            var token = await response.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsStringAsync("token", token);

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (!string.IsNullOrEmpty(result?.Token))
            {
                await _localStorage.SetItemAsStringAsync("token", result.Token);
                
            }
        }

        public class LoginResult
        {
            public string Token { get; set; }
        }

    }
}
