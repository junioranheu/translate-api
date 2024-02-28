using Microsoft.Extensions.DependencyInjection;

namespace Translate.Domain;

public static class DependencyInjecton
{
    public static IServiceCollection AddDependencyInjectionDomain(this IServiceCollection services)
    {
        return services;
    }
}