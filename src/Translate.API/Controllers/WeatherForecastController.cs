using Microsoft.AspNetCore.Mvc;
using Translate.Domain.Consts;

namespace Translate.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public WeatherForecastController()
    {

    }

    [HttpGet("Obter")]
    public string Obter()
    {
        string aea = SistemaConst.NomeSistema;
        return aea;
    }
}