using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using ForensicsAnalyzer.Application.DTOs;
using Microsoft.AspNetCore.Components;

namespace ForensicsAnalyzer.WebClient.Auth;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly LocalStorageAuthenticationStateProvider _authStateProvider;
    private readonly NavigationManager _navigationManager;

    public AuthService(
        HttpClient httpClient,
        LocalStorageAuthenticationStateProvider authStateProvider,
        NavigationManager navigationManager)
    {
        _httpClient = httpClient;
        _authStateProvider = authStateProvider;
        _navigationManager = navigationManager;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var request = new LoginRequest(username, password);
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            if (result?.Token == null)
                return false;

            await _authStateProvider.MarkUserAsAuthenticated(result.Token);
            _navigationManager.NavigateTo("/cases");
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> RegisterAsync(string username, string email, string password)
    {
        try
        {
            var request = new RegisterRequest(username, email, password);
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);

            return response.IsSuccessStatusCode;
        }
        catch
        {
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await _authStateProvider.MarkUserAsLoggedOut();
        _navigationManager.NavigateTo("/login");
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _authStateProvider.GetTokenAsync();
    }
}
