using MediatR;
using Microsoft.AspNetCore.Mvc;
using Translate.Application.Commands.Logs.ListarLog;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class LogsController : BaseController<LogsController>
{
    public LogsController()
    {

    }

    [HttpGet]
    public async Task<IActionResult> Listar([FromServices] IMediator mediator, [FromQuery] ListarLogRequest command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }
}
