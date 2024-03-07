using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Translate.Application.Handlers.UsuariosRoles.ListarUsuarioRoleCache;

namespace Translate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjectionApplication(this IServiceCollection services, WebApplicationBuilder builder)
    {
        AddMediatR(services);
        AddServices(services);
        AddLogger(builder);

        return services;
    }

    private static void AddMediatR(IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<ListarUsuarioRoleCacheHandler>();
    }

    private static void AddLogger(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
    }
}