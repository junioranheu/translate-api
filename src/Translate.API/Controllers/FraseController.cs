using Microsoft.AspNetCore.Mvc;
using Translate.Domain.Handlers.CriarFrase;
using Translate.Domain.Handlers.CriarFrase.Commands.Requests;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FraseController : ControllerBase
{
    public FraseController()
    {

    }

    [HttpPost]
    public IActionResult Criar([FromServices] ICriarFraseHandler handler, [FromBody] CriarFraseRequest command)
    {
        var response = handler.Handle(command);
        return Ok(response);
    }
}