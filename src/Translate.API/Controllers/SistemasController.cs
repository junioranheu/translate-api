using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Translate.Infrastructure.Data;
using Translate.Infrastructure.Seed;
using static junioranheu_utils_package.Fixtures.Get;

namespace Translate.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SistemasController(TranslateContext context) : BaseController<SistemasController>
    {
        private readonly TranslateContext _context = context;

        [HttpPost("seed")]
        [AllowAnonymous]
        public async Task<IActionResult> Seed(int m)
        {
            if (GerarHorarioBrasilia().Minute != m)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "O parâmetro m está incorreto");
            }

            await DbInitializer.Initialize(_context, isAplicarMigrations: true, isAplicarSeed: true);
            return Ok($"Update-Database finalizado com sucesso às {GerarHorarioBrasilia()}");
        }
    }
}