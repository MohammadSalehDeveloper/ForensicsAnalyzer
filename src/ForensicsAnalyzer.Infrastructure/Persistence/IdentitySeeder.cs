using System.Security.Claims;
using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ForensicsAnalyzer.Infrastructure.Persistence;

public static class IdentitySeeder
{
    public const string AdministratorRole = "Administrator";
    public const string AnalystRole = "Analyst";
    public const string ViewerRole = "Viewer";

    private static readonly Dictionary<string, string[]> RolePermissions = new()
    {
        [AdministratorRole] = Permissions.GetAll().ToArray(),
        [AnalystRole] =
        [
            Permissions.Cases.Create, Permissions.Cases.Read, Permissions.Cases.Update, Permissions.Cases.Assign,
            Permissions.Sources.Read,
            Permissions.Artifacts.Create, Permissions.Artifacts.Read, Permissions.Artifacts.Update,
            Permissions.Social.Create, Permissions.Social.Read, Permissions.Social.Update
        ],
        [ViewerRole] =
        [
            Permissions.Cases.Read,
            Permissions.Sources.Read,
            Permissions.Artifacts.Read,
            Permissions.Social.Read,
            Permissions.Users.Read
        ]
    };

    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var config = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("IdentitySeeder");

        foreach (var (roleName, permissions) in RolePermissions)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                logger.LogInformation("Created role {Role}", roleName);
            }

            var role = await roleManager.FindByNameAsync(roleName);
            if (role is null) continue;

            var existingClaims = await roleManager.GetClaimsAsync(role);
            foreach (var permission in permissions)
            {
                if (existingClaims.Any(c => c.Type == "permission" && c.Value == permission))
                    continue;

                await roleManager.AddClaimAsync(role, new Claim("permission", permission));
            }
        }

        var adminEmail = config["SeedAdmin:Email"];
        var adminPassword = config["SeedAdmin:Password"];
        if (string.IsNullOrWhiteSpace(adminEmail) || string.IsNullOrWhiteSpace(adminPassword))
        {
            logger.LogWarning("SeedAdmin credentials not configured; skipping admin user seeding.");
            return;
        }

        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = config["SeedAdmin:FullName"] ?? "Administrator"
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
            {
                logger.LogError("Failed to seed admin user: {Errors}", string.Join("; ", result.Errors.Select(e => e.Description)));
                return;
            }

            logger.LogInformation("Seeded admin user {Email}", adminEmail);
        }

        if (!await userManager.IsInRoleAsync(adminUser, AdministratorRole))
            await userManager.AddToRoleAsync(adminUser, AdministratorRole);
    }
}
