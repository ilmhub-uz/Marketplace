using Marketplace.Blazor.Entities;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace Marketplace.Blazor.Managers;

public class CreateManager
{
    private HttpClient Http = new HttpClient();
    private NavigationManager NavigationManager { get; set; }
    public CreateUserModel userDto = new CreateUserModel();

    private async Task Register()
    {
        var response = await Http.PostAsJsonAsync("/Create", userDto);

        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo("/signin");
        }
    }
}
