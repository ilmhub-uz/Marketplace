using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Marketplace.Organizations.Blazor;
using Blazored.LocalStorage;
using OrganizationBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7075") });
builder.Services.AddScoped<OrganizationService>();
;
await builder.Build().RunAsync();
