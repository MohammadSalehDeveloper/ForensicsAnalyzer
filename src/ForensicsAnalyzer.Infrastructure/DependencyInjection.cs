using ForensicsAnalyzer.Application.Authorization;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Infrastructure.Auth;
using ForensicsAnalyzer.Infrastructure.Authorization;
using ForensicsAnalyzer.Infrastructure.Identity;
using ForensicsAnalyzer.Infrastructure.Persistence;
using ForensicsAnalyzer.Infrastructure.Profiles;
using ForensicsAnalyzer.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ForensicsAnalyzer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var jwtSettings = new JwtSettings();
        config.GetSection("JwtSettings").Bind(jwtSettings);

        var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        var googleClientId = config["GoogleAuth:ClientId"];
        var googleClientSecret = config["GoogleAuth:ClientSecret"];
        if (!string.IsNullOrWhiteSpace(googleClientId) && !string.IsNullOrWhiteSpace(googleClientSecret)
            && googleClientId != "YOUR_GOOGLE_CLIENT_ID")
        {
            authBuilder.AddGoogle(options =>
            {
                options.ClientId = googleClientId;
                options.ClientSecret = googleClientSecret;
            });
        }

        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddAuthorization(options =>
        {
            foreach (var permission in Permissions.GetAll())
            {
                options.AddPolicy(permission, policy =>
                    policy.Requirements.Add(new PermissionRequirement(permission)));
            }
        });

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddAutoMapper(typeof(CaseProfile).Assembly);

        services.AddScoped<ICaseRepository, CaseRepository>();
        services.AddScoped<ICaseAssignmentRepository, CaseAssignmentRepository>();
        services.AddScoped<ISourceRepository, SourceRepository>();
        services.AddScoped<IArtifactRepository, ArtifactRepository>();
        services.AddScoped<IArtifactItemRepository, ArtifactItemRepository>();
        services.AddScoped<ICallLogRepository, CallLogRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IFileCustomRepository, FileCustomRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IThumbnailRepository, ThumbnailRepository>();
        services.AddScoped<ILocationImageRepository, LocationImageRepository>();
        services.AddScoped<ISocialMessengerRepository, SocialMessengerRepository>();
        services.AddScoped<ISocialChatRepository, SocialChatRepository>();
        services.AddScoped<ISocialMessageRepository, SocialMessageRepository>();
        services.AddScoped<ISocialMemberRepository, SocialMemberRepository>();

        return services;
    }
}
