using Microsoft.Extensions.DependencyInjection;

namespace ForensicsAnalyzer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // later: MediatR, validators, mapping, etc.
        return services;
    }
}
