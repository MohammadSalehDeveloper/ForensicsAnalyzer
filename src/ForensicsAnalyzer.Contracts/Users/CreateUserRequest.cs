namespace ForensicsAnalyzer.Contracts.Users;

public sealed class CreateUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? FullName { get; set; }
    public List<string> Roles { get; set; } = [];
}
