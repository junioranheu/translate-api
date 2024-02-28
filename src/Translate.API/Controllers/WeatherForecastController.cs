using Microsoft.AspNetCore.Mvc;
using Translate.Domain.Consts;
using Translate.Domain.Entities;

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

        WeatherForecast aea2 = new()
        {
            Summary = aea
        };

        return aea2.Summary;
    }
}