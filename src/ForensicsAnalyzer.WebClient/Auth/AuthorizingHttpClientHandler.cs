using Microsoft.AspNetCore.Components;

namespace ForensicsAnalyzer.WebClient.Auth;

public class AuthorizingHttpClientHandler : DelegatingHandler
{
    private readonly LocalStorageAuthenticationStateProvider _authStateProvider;
    private readonly NavigationManager _navigationManager;

    public AuthorizingHttpClientHandler(
        LocalStorageAuthenticationStateProvider authStateProvider,
        NavigationManager navigationManager)
    {
        _authStateProvider = authStateProvider;
        _navigationManager = navigationManager;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Attach JWT token to every request
        var token = await _authStateProvider.GetTokenAsync();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // Handle 401 - redirect to login
        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await _authStateProvider.MarkUserAsLoggedOut();
            _navigationManager.NavigateTo("/login");
        }

        return response;
    }
}
