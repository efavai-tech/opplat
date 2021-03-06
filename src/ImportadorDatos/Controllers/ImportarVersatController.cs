using System.Threading.Tasks;
using ImportadorDatos.Jobs;
using Microsoft.AspNetCore.Mvc;

namespace ImportadorDatos.Controllers
{
    [Route("importador/[controller]")]
    [ApiController]
    public class ImportarVersatController : ControllerBase
    {
        ImportarVersat importador;

        public ImportarVersatController(ImportarVersat importador)
        {
            this.importador = importador;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            importador.ImportarCuentasAsync();
            return Ok("Cuentas importadas.");
        }

        [HttpGet("periodos")]
        public async Task<IActionResult> GetPeriodos()
        {
            importador.ImportarPeriodosContables();
            return Ok("Periodos importadas.");
        }

        [HttpGet("asientos")]
        public async Task<IActionResult> GetAsientos()
        {
            importador.ImportarAsientos();
            return Ok("Asientos importados.");
        }
    }
}