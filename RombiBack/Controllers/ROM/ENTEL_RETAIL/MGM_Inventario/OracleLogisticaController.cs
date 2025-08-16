using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.Inventario;
using RombiBack.Entities.ROM.ENTEL_RETAIL.Models.OracleLogistica;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Inventario;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Inventario
{
    [Route("api/[controller]")]
    [ApiController]
    public class OracleLogisticaController : ControllerBase
    {
        private readonly IOracleLogisticaServices _oraclelogisticaServices;

        public OracleLogisticaController(IOracleLogisticaServices oraclelogisticaServices)
        {
            _oraclelogisticaServices = oraclelogisticaServices;
        }


        [HttpPost("PostOracleLogisticaMasivo")]
        public async Task<IActionResult> PostOracleLogisticaMasivo([FromBody] List<OracleLogistica> registros, [FromQuery] string usuario)
        {
            try
            {
                if (registros == null)
                    return BadRequest(new { message = "No se recibieron datos." });
                var result = await _oraclelogisticaServices.PostOracleLogisticaMasivo(registros, usuario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar inventario: {ex.Message}");
            }

        }

        [HttpPost("ObtenerConciliacionesAsync")]
        public async Task<IActionResult> ObtenerConciliacionesAsync([FromBody] FiltroInsertarConciliar parametros)
        {
            try
            {
                var resultado = await _oraclelogisticaServices.ObtenerConciliacionesAsync(parametros);

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
