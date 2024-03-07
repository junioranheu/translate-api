using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Application.Handlers.Usuarios.ObterUsuarioCache;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Filters.Base;

public sealed class BaseFilter
{
    public BaseFilter() { }

    #region usuario_id
    internal async Task<Guid> BaseObterUsuarioId(ActionExecutedContext context)
    {
        return await BaseObterUsuarioId(context.HttpContext.RequestServices.GetService<ObterUsuarioCacheHandler>(), BaseObterUsuarioEmail(context));
    }

    internal async Task<Guid> BaseObterUsuarioId(AuthorizationFilterContext context)
    {
        return await BaseObterUsuarioId(context.HttpContext.RequestServices.GetService<ObterUsuarioCacheHandler>(), BaseObterUsuarioEmail(context));
    }

    internal async Task<Guid> BaseObterUsuarioId(ExceptionContext context)
    {
        return await BaseObterUsuarioId(context.HttpContext.RequestServices.GetService<ObterUsuarioCacheHandler>(), BaseObterUsuarioEmail(context));
    }

    private static async Task<Guid> BaseObterUsuarioId(ObterUsuarioCacheHandler? service, string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return Guid.Empty;
        }

        if (service is null)
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.ErroInterno));
        }
 
        var request = new ObterUsuarioRequest() { 
            UsuarioId = Guid.Empty,
            Email = email
        };

        var usuario = await service.Handle(request, new CancellationTokenSource().Token);
        Guid usuarioId = usuario is not null ? usuario.UsuarioId : Guid.Empty;

        return usuarioId;
    }
    #endregion

    #region usuario_email
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Marcar membros como estáticos", Justification = "<Pendente>")]
    internal string BaseObterUsuarioEmail(dynamic context)
    {
        if (context is ActionExecutedContext actionExecutedContext)
        {
            return ObterUsuarioEmail(actionExecutedContext);
        }
        else if (context is AuthorizationFilterContext authorizationFilterContext)
        {
            return ObterUsuarioEmail(authorizationFilterContext);
        }
        else if (context is ExceptionContext exceptionContext)
        {
            return ObterUsuarioEmail(exceptionContext);
        }

        return string.Empty;

        static string ObterUsuarioEmail(dynamic context)
        {
            if (context.HttpContext.User.Identity!.IsAuthenticated)
            {
                string email = context.HttpContext.User.FindFirst(ClaimTypes.Email)!.Value;
                return email ?? string.Empty;
            }

            return string.Empty;
        }
    }
    #endregion
}