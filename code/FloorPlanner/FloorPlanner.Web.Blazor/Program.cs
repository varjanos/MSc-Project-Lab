using FloorPlanner.Web.Blazor;
using FloorPlanner.Web.Blazor.Client;
using FloorPlanner.Web.Blazor.Constants;
using FloorPlanner.Web.Blazor.Handlers;
using FloorPlanner.Web.Blazor.Interfaces;
using FloorPlanner.Web.Blazor.Services.DisplayError;
using FloorPlanner.Web.Blazor.Services.Progress;
using FloorPlanner.Web.Blazor.Services.Translations;
using FloorPlanner.Web.Blazor.Services.UserProfile;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var backendUrl = new Uri(builder.HostEnvironment.BaseAddress);

//If we want to wait for the Initializeable services to load, like translation and user
//The Services should be registered as IInitialize and as the ServiceType too.
builder.Services.AddSingleton<ITranslationClientService>(x => new TranslationClientService(
    x.CreateScope().ServiceProvider.GetService<ITranslationClient>(),
    x.CreateScope().ServiceProvider.GetService<IUserProfileClient>()
    ));
builder.Services.AddSingleton<IInitialize>(x => x.GetService<ITranslationClientService>());
builder.Services.AddSingleton<IInitialize>(x => x.GetService<IUserProfileClientService>());

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient(HttpClientNames.ApiHttpClientName, client => client.BaseAddress = backendUrl)
    .AddHttpMessageHandler(s => new IncludeRequestCredentialsHttpMessageHandler())
    .AddHttpMessageHandler(s => new DisplayProgressHttpMessageHandler(s.GetService<IProgressClientService>()))
    .AddHttpMessageHandler(s => new ErrorHttpMessageHandler(s.GetService<IDisplayErrorClientService>()));

builder.Services.AddApiClientServices();
builder.Services.AddClientServices();
builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
