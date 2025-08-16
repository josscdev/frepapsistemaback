using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica;
using RombiBack.Services.ROM.ENTEL_TPF.MGM_InventarioTPF;

namespace RombiBack.Controllers.ROM.ENTEL_TPF.MGM_InventarioTPF
{
    [Route("api/[controller]")]
    [ApiController]
    public class OracleLogisticaTPFController : ControllerBase
    {
        private readonly IOracleLogisticaTPFServices _oraclelogisticaTPFServices;
        public OracleLogisticaTPFController(IOracleLogisticaTPFServices oraclelogisticaTPFServices)
        {
            _oraclelogisticaTPFServices = oraclelogisticaTPFServices;
        }

        [HttpPost("PostOracleLogisticaMasivoTPF")]
        public async Task<IActionResult> PostOracleLogisticaMasivo([FromBody] List<OracleLogistica> registros, [FromQuery] string usuario)
        {
            try
            {
                if (registros == null)
                    return BadRequest(new { message = "No se recibieron datos." });
                var result = await _oraclelogisticaTPFServices.PostOracleLogisticaMasivoTPF(registros, usuario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar inventario: {ex.Message}");
            }

        }

        [HttpPost("ObtenerConciliacionesAsyncTPF")]
        public async Task<IActionResult> ObtenerConciliacionesAsync([FromBody] FiltroInsertarConciliar parametros)
        {
            try
            {
                var resultado = await _oraclelogisticaTPFServices.ObtenerConciliacionesAsyncTPF(parametros);

                if (resultado == null || !resultado.Any())
                {
                    return NotFound("No se encontraron datos para los filtros proporcionados.");
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                // Aquí también puedes usar ILogger si lo inyectas en el constructor
                Console.WriteLine($"Error en el controlador: {ex.Message}");
                return StatusCode(500, "Ocurrió un error al procesar la solicitud.");
            }
        }
    }
}
