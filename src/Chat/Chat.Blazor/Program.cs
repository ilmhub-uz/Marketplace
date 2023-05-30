using Blazored.LocalStorage;
using Chat.Blazor;
using Chat.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddSingleton<ConversationsService>();

builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri("http://localhost:5071")
});

await builder.Build().RunAsync();
