using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translate.API.Filters;
using Translate.Application.Commands.Frases.CriarFrase;
using Translate.Application.Commands.Frases.ObterFrase;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FrasesController : ControllerBase
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

    [HttpPost]
    [AuthorizeFilter]
    public async Task<IActionResult> Criar([FromServices] IMediator mediator, [FromBody] CriarFraseRequest command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}