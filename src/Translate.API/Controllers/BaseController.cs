using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Application.Handlers.Usuarios.ObterUsuarioCache;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Controllers;

public abstract class BaseController<T> : Controller
{
    protected string ObterUsuarioEmail()
    {
        if (User.Identity!.IsAuthenticated)
        {
            // Obter o e-mail do usuário pela Azure;
            // var claim = User.Claims.First(c => c.Type == "preferred_username");
            // return claim.Value ?? string.Empty;

            // Obter o e-mail do usuário pela autenticação própria;
            string email = User.FindFirst(ClaimTypes.Email)!.Value;
            return email ?? string.Empty;
        }

        return string.Empty;
    }

    protected async Task<Guid> ObterUsuarioId()
    {
        var service = HttpContext.RequestServices.GetService<ObterUsuarioCacheHandler>() ?? throw new Exception(ObterDescricaoEnum(CodigoErroEnum.ErroInterno)); ;
        string email = ObterUsuarioEmail();

        var request = new ObterUsuarioRequest()
        {
            UsuarioId = Guid.Empty,
            Email = email
        };

        ObterUsuarioResponse? usuario = await service.Handle(request, new CancellationTokenSource().Token);

        return usuario is not null ? usuario.UsuarioId : Guid.Empty;
    }
}