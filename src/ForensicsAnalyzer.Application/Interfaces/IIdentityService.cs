using ForensicsAnalyzer.Contracts.Users;

namespace ForensicsAnalyzer.Application.Interfaces;

public interface IIdentityService
{
    Task<List<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default);
    Task<UserDto?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default);
    Task UpdateUserAsync(string id, string? fullName, string? email, CancellationToken cancellationToken = default);
    Task DeleteUserAsync(string id, CancellationToken cancellationToken = default);
    Task<ImportUserResult> CreateUserAsync(string email, string password, string? fullName, CancellationToken cancellationToken = default);
    Task<ImportUserResult> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default);
}

public sealed class ImportUserResult
{
    public bool Succeeded { get; init; }
    public string? UserId { get; init; }
    public IEnumerable<string> Errors { get; init; } = [];
}
