using Microsoft.AspNetCore.Mvc;

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
        return "Teste";
    }
}
