using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> ObterAsync([FromServices] IMediator mediator, [FromQuery] ObterFraseRequest command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CriarAsync([FromServices] IMediator mediator, [FromBody] CriarFraseRequest command)
    {
        var response = await mediator.Send(command);
        return Ok(response);
    }
}