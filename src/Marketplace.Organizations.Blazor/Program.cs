using Blazored.LocalStorage;
using Marketplace.Organizations.Blazor;
using Marketplace.Organizations.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7076") });

await builder.Build().RunAsync();
