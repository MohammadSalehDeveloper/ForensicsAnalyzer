using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Users;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Identity;

public sealed class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync(cancellationToken);
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return null;

        var dto = _mapper.Map<UserDto>(user);
        dto.Roles = (await _userManager.GetRolesAsync(user)).ToList();
        return dto;
    }

    public async Task UpdateUserAsync(string id, string? fullName, string? email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("User not found.");

        if (!string.IsNullOrWhiteSpace(email) && email != user.Email)
        {
            var setEmailResult = await _userManager.SetEmailAsync(user, email);
            if (!setEmailResult.Succeeded)
                throw new InvalidOperationException("Unable to update user email.");

            user.UserName = email;
        }

        if (!string.IsNullOrWhiteSpace(fullName))
            user.FullName = fullName;

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new InvalidOperationException("Unable to update user.");
    }

    public async Task DeleteUserAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("User not found.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
            throw new InvalidOperationException("Unable to delete user.");
    }

    public async Task<ImportUserResult> CreateUserAsync(
        string email,
        string password,
        string? fullName,
        IEnumerable<string>? roles = null,
        CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            FullName = fullName
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return new ImportUserResult
            {
                Succeeded = false,
                UserId = user.Id,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        if (roles is not null)
        {
            foreach (var role in roles.Where(r => !string.IsNullOrWhiteSpace(r)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    return new ImportUserResult
                    {
                        Succeeded = false,
                        UserId = user.Id,
                        Errors = [$"Role '{role}' does not exist."]
                    };
                }

                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    return new ImportUserResult
                    {
                        Succeeded = false,
                        UserId = user.Id,
                        Errors = roleResult.Errors.Select(e => e.Description)
                    };
                }
            }
        }

        return new ImportUserResult
        {
            Succeeded = true,
            UserId = user.Id
        };
    }

    public async Task<ImportUserResult> FindUserByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return new ImportUserResult
        {
            Succeeded = user is not null,
            UserId = user?.Id
        };
    }

    public async Task<List<string>> GetUserRolesAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new KeyNotFoundException("User not found.");

        return (await _userManager.GetRolesAsync(user)).ToList();
    }

    public async Task AssignRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new KeyNotFoundException("User not found.");

        if (!await _roleManager.RoleExistsAsync(roleName))
            throw new InvalidOperationException($"Role '{roleName}' does not exist.");

        if (await _userManager.IsInRoleAsync(user, roleName))
            return;

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
    }

    public async Task RemoveRoleAsync(string userId, string roleName, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new KeyNotFoundException("User not found.");

        if (!await _userManager.IsInRoleAsync(user, roleName))
            return;

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded)
            throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
    }

    public async Task<List<string>> GetAllRolesAsync(CancellationToken cancellationToken = default)
        => await _roleManager.Roles
            .AsNoTracking()
            .Select(r => r.Name!)
            .OrderBy(n => n)
            .ToListAsync(cancellationToken);
}
