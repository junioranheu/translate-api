﻿using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.SwaggerUI;
using Translate.Domain.Consts;

namespace Translate.API;

public static class DependencyAppConfiguration
{
    public static WebApplication UseAppConfiguration(this WebApplication app, WebApplicationBuilder builder)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;

        AddSwagger(app);
        AddHttpsRedirection(app);
        AddCors(app, builder);
        AddCompression(app);
        AddAuth(app);

        return app;
    }

    private static void AddSwagger(WebApplication app)
    {
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
    }

    private static void AddHttpsRedirection(WebApplication app)
    {
        if (app.Environment.IsProduction())
        {
            app.UseHttpsRedirection();
        }
    }

    private static void AddCors(WebApplication app, WebApplicationBuilder builder)
    {
        app.UseCors(builder.Configuration["CORSSettings:Cors"]!);
    }

    private static void AddCompression(WebApplication app)
    {
        /// <summary>
        /// O trecho "app.UseWhen" abaixo é necessário quando a API tem uma resposta IAsyncEnumerable/Yield;
        /// O "UseResponseCompression" conflita com esse tipo de requisição, portanto é obrigatória a verificação abaixo;
        /// Caso não existam requisições desse tipo na API, é apenas necessário o trecho "app.UseResponseCompression()";
        /// </summary>
        app.UseWhen(context => !IsStreamingRequest(context), x =>
        {
            x.UseResponseCompression();
        });

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
    }

    private static void AddAuth(WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    }
}