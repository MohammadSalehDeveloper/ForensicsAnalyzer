namespace ForensicsAnalyzer.WebClient.Auth;

public interface IAuthService
{
    Task<bool> LoginAsync(string username, string password);
    Task<bool> RegisterAsync(string username, string email, string password);
    Task LogoutAsync();
    Task<string?> GetTokenAsync();
}
