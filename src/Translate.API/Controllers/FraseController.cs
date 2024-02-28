using Microsoft.AspNetCore.Mvc;
using Translate.Domain.Consts;
using Translate.Domain.Entities;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class FraseController : ControllerBase
{
    public FraseController()
    {

    }

    [HttpGet("Obter")]
    public string Obter()
    {
        string nomeSistema = SistemaConst.NomeSistema;
        var frase = new Frase("XD");

        return $"{nomeSistema}{frase.Id}";
    }
}