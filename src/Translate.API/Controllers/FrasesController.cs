using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translate.API.Filters;
using Translate.Application.Commands.Frases.CriarFrase;
using Translate.Application.Commands.Frases.ListarFrase;
using Translate.Application.Commands.Frases.ObterFrase;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FrasesController : BaseController<FrasesController>
{
    public FrasesController()
    {

    }

    [HttpGet]
    [AuthorizeFilter]
    public async Task<IActionResult> Obter([FromServices] IMediator mediator, [FromQuery] ObterFraseRequest command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("listar")]
    [AuthorizeFilter]
    public async Task<IActionResult> Listar([FromServices] IMediator mediator, [FromQuery] ListarFraseRequest command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [AuthorizeFilter]
    public async Task<IActionResult> Criar([FromServices] IMediator mediator, [FromBody] CriarFraseRequest command)
    {
        command.UsuarioId = await ObterUsuarioId();
        var result = await mediator.Send(command);
        return Ok(result);
    }
}