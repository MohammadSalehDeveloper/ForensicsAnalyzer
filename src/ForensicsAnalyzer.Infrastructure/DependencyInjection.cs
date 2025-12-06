using ForensicsAnalyzer.Application.Cases;
using ForensicsAnalyzer.Infrastructure.Cases;
using Microsoft.Extensions.DependencyInjection;

namespace ForensicsAnalyzer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // later: add DbContext, Identity, etc.
        services.AddScoped<ICaseService, CaseService>();
        return services;
    }
}