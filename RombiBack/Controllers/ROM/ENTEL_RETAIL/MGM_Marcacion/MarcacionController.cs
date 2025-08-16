using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Allocation;
using RombiBack.Services.ROM.ENTEL_RETAIL.MGM_Marcacion;

namespace RombiBack.Controllers.ROM.ENTEL_RETAIL.MGM_Marcacion
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcacionController : ControllerBase
    {
        private readonly IMarcacionServices _marcacionServices;

        public MarcacionController(IMarcacionServices marcacionServices)
        {
            _marcacionServices = marcacionServices;
        }


        [HttpGet("GetUsuarioMarcacion")]
        public IActionResult GetUsuarioMarcacion(string docusuario)
        {
            try
            {
                var respuesta = _marcacionServices.GetUsuarioMarcacion(docusuario); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("PostEmpleadoAsistenciaMarcacion")]
        public IActionResult PostEmpleadoAsistenciaMarcacion(string docusuario, DateTime fechamarcacion)
        {
            try
            {
                var respuesta = _marcacionServices.PostEmpleadoAsistenciaMarcacion( docusuario,  fechamarcacion); // Asume que GetOneRol ahora recibe un int
                return Ok(respuesta);  // Retorna un 200 OK con la respuesta
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, devuelve un error 500 con el mensaje de error
                return StatusCode(500, new { message = ex.Message });
            }
        }


    }
}
