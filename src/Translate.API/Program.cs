using Translate.API;
using Translate.Application;
using Translate.Domain;
using Translate.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDependencyInjectionAPI();
    builder.Services.AddDependencyInjectionApplication(builder);
    builder.Services.AddDependencyInjectionDomain();
    builder.Services.AddDependencyInjectionInfrastructure(builder);
}

WebApplication app = builder.Build();
{
    await app.UseAppConfigurationAsync(builder);
    app.Run();
}