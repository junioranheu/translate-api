using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.SwaggerUI;
using Translate.API;
using Translate.Application;
using Translate.Domain;
using Translate.Domain.Consts;
using Translate.Infrastructure;

#region builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddDependencyInjectionAPI();
     builder.Services.AddDependencyInjectionApplication(builder);
    builder.Services.AddDependencyInjectionDomain();
    builder.Services.AddDependencyInjectionInfrastructure(builder);
}
#endregion

#region app
WebApplication app = builder.Build();
{
    using IServiceScope scope = app.Services.CreateScope();
    IServiceProvider services = scope.ServiceProvider;

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{SistemaConst.NomeSistema}.API");
            // c.RoutePrefix = ""; // ***
            c.DocExpansion(DocExpansion.None);
        });

        app.UseDeveloperExceptionPage();
    }

    if (app.Environment.IsProduction())
    {
        app.UseHttpsRedirection();
    }

    app.UseCors(builder.Configuration["CORSSettings:Cors"]!);

    /// <summary>
    /// O trecho "app.UseWhen" abaixo é necessário quando a API tem uma resposta IAsyncEnumerable/Yield;
    /// O "UseResponseCompression" conflita com esse tipo de requisição, portanto é obrigatória a verificação abaixo;
    /// Caso não existam requisições desse tipo na API, é apenas necessário o trecho "app.UseResponseCompression()";
    /// </summary>
    app.UseWhen(context => !IsStreamingRequest(context), x =>
    {
        x.UseResponseCompression();
    });

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
#endregion

#region metodos_auxiliares
static bool IsStreamingRequest(HttpContext context)
{
    Endpoint? endpoint = context.GetEndpoint();

    if (endpoint is RouteEndpoint routeEndpoint)
    {
        ControllerActionDescriptor? acao = routeEndpoint.Metadata.GetMetadata<ControllerActionDescriptor>();

        if (acao is not null)
        {
            Type? tipo = acao.MethodInfo.ReturnType;

            if (tipo.IsGenericType && tipo.GetGenericTypeDefinition() == typeof(IAsyncEnumerable<>))
            {
                return true;
            }

            return false;
        }
    }

    return false;
}
#endregion