using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ForensicsAnalyzer.WebClient;
using ForensicsAnalyzer.WebClient.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<LocalStorageAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<LocalStorageAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<IAuthService, AuthService>();

// Register handler without pulling HttpClient into AuthStateProvider (avoids DI deadlock).
builder.Services.AddScoped<AuthorizingHttpClientHandler>();
builder.Services.AddScoped(sp =>
{
    var authHandler = sp.GetRequiredService<AuthorizingHttpClientHandler>();
    authHandler.InnerHandler = new HttpClientHandler();
    return new HttpClient(authHandler)
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    };
});

await builder.Build().RunAsync();
