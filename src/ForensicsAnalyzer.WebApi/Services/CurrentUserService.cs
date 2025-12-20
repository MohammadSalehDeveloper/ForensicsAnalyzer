using ForensicsAnalyzer.Application.Interfaces;
using System.Security.Claims;

namespace ForensicsAnalyzer.WebApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ??
                            string.Empty;

    public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;
}