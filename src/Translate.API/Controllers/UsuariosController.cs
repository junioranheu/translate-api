using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translate.Application.Commands.Usuarios.ObterUsuario;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsuariosController : ControllerBase
{
    public UsuariosController()
    {

    }

    [HttpGet]
    public async Task<IActionResult> ObterAsync([FromServices] IMediator mediator, [FromQuery] ObterUsuarioRequest command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}