using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Translate.API.Filters;
using Translate.Application.Commands.Usuarios.AutenticarUsuario;
using Translate.Application.Commands.Usuarios.ObterUsuario;
using Translate.Domain.Enums;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : BaseController<FrasesController>
{
    public UsuariosController()
    {

    }

    [HttpPost("autenticar")]
    [AllowAnonymous]
    public async Task<IActionResult> Autenticar([FromServices] IMediator mediator, [FromBody] AutenticarUsuarioRequest command)
    {
        if (User.Identity!.IsAuthenticated)
        {
            throw new Exception(ObterDescricaoEnum(CodigoErroEnum.UsuarioJaAutenticado));
        }

        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    [AuthorizeFilter(UsuarioRoleEnum.Comum, UsuarioRoleEnum.Administrador)]
    public async Task<IActionResult> Obter([FromServices] IMediator mediator, [FromQuery] ObterUsuarioRequest command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}